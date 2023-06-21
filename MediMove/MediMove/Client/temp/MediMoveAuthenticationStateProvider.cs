using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Components;
using MediatR.NotificationPublishers;
using MediMove.Shared.Models.DTOs;
using System.Net.Http.Json;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace MediMove.Client.temp
{
    public class MediMoveAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jSRuntime;
        private readonly NavigationManager _navigationManager;

        public string Role { get; set; } = "";
        public string Name { get; set; } = "";


        public MediMoveAuthenticationStateProvider( HttpClient httpClient, IJSRuntime jSRuntime, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _jSRuntime = jSRuntime;
            _navigationManager = navigationManager;
            AuthenticationStateChanged += OnAuthenticationStateChanged;
        }

        public async Task<MediMoveResponse<StandardResponse>> Register(RegisterAdminRequest content)
        {
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Accounts/Register/Admin");

            var uriBuilder = new UriBuilder(requestUri);
            Console.WriteLine("I'm here 1");
            var token = await _jSRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            var request = new HttpRequestMessage(HttpMethod.Post, uriBuilder.ToString());
            Console.WriteLine("I'm here 1");
            Console.WriteLine($"{request}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new ObjectContent<RegisterAdminRequest>(content, new JsonMediaTypeFormatter());

            Console.WriteLine("I'm here 2");
            var httpResponse = await _httpClient.SendAsync(request);
            Console.WriteLine("I'm here 3");


            var response = await CheckStandardResponse(httpResponse);

            Console.WriteLine($"{response.CorrectResponse}, {response.ErrorResponse}");
            return response;
        }
        public async Task<MediMoveResponse<StandardResponse>> Register(RegisterParamedicRequest content)
        {

            var token = await _jSRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/Accounts/Register/Paramedic");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new ObjectContent<RegisterParamedicRequest>(content, new JsonMediaTypeFormatter());

            var httpResponse = await _httpClient.SendAsync(request);


            var response = await CheckStandardResponse(httpResponse);

            return response;
        }
        public async Task<MediMoveResponse<StandardResponse>> Register(RegisterDispatcherRequest content)
        {

            var token = await _jSRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/Accounts/Register/Dispatcher");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new ObjectContent<RegisterDispatcherRequest>(content, new JsonMediaTypeFormatter());

            var httpResponse = await _httpClient.SendAsync(request);
           

            var response = await CheckStandardResponse(httpResponse);

            return response;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity();
            var token = await _jSRuntime.InvokeAsync<string>("localStorage.getItem", "token");
            if (!string.IsNullOrEmpty(token) && tokenHandler.CanReadToken(token))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token); 
                identity = new ClaimsIdentity(jwtSecurityToken.Claims, "MediMove");
            }
            
            var principal = new ClaimsPrincipal(identity);

            var authenticationState = new AuthenticationState(principal);

            return authenticationState;
        }

        public async Task<string> GetRole()  // Wrzucić Role do Enuma
        {
            var authenticationState = await GetAuthenticationStateAsync();
            var user = authenticationState.User;

            if (user.Identity.IsAuthenticated)
            {
                var roleClaim = user.FindFirst(ClaimTypes.Role);
                if (roleClaim != null)
                {
                    return roleClaim.Value;
                }
            }

            // Jeśli użytkownik nie jest uwierzytelniony lub nie ma przypisanej roli, zwróć null lub domyślną wartość
            return null;

            //zmienic na odczyt pola Role

        }
        public async Task Logout()
        {
            await _jSRuntime.InvokeVoidAsync("localStorage.removeItem", "token");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task<MediMoveResponse<StandardResponse>> CheckStandardResponse(HttpResponseMessage? httpResponse)
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            MediMoveResponse<StandardResponse> response = null;
            if (httpResponse.IsSuccessStatusCode)
            {
                if (!string.IsNullOrEmpty(responseContent))
                {

                    var standardResponse = new StandardResponse(httpResponse.StatusCode);
                    response = new(standardResponse);
                }
            }
            else
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                response = new(errorResponse);
            }

            return response;
        }


        public async Task<MediMoveResponse<HttpResponseMessage>> LoginAsync(LoginUserDTO content)
        {
            
            var httpResponse = await _httpClient.PostAsJsonAsync("/api/v1/Accounts/Login", content);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            MediMoveResponse<HttpResponseMessage> response = null;
            if (httpResponse.IsSuccessStatusCode)
            {
                if (!string.IsNullOrEmpty(responseContent))
                {
                    await _jSRuntime.InvokeVoidAsync("localStorage.setItem", "token", responseContent);
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    response = new(httpResponse);
                }
            }
            else
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                response = new(errorResponse);
            }
            

            return response;


        }

        private async void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            var authenticationState = await task;

            if (authenticationState is not null)
            {
                Role = authenticationState.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "";
                Name = authenticationState.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "";
            }
        }
        public void Dispose()
        {
            AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }

    }

}

