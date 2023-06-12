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

namespace MediMove.Client.temp
{
    public class MediMoveAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jSRuntime;
        //private readonly AuthenticationDataStorage _authenticationDataStorage;
        //private string loginResponse;
        //private ErrorResponse errorResponse;

        public string Role { get; set; } = "";
        public string Name { get; set; } = "";


        public MediMoveAuthenticationStateProvider( HttpClient httpClient, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _jSRuntime = jSRuntime;
            AuthenticationStateChanged += OnAuthenticationStateChanged;
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



/*
 * 
 * private readonly HttpClient _httpClient;
    private readonly AuthenticationDataMemoryStorage _authenticationDataMemoryStorage;

    public BlazorSchoolAuthenticationStateProvider(HttpClient httpClient, AuthenticationDataMemoryStorage authenticationDataMemoryStorage)
    {
        _httpClient = httpClient;
        _authenticationDataMemoryStorage = authenticationDataMemoryStorage;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var identity = new ClaimsIdentity();

        

        var principal = new ClaimsPrincipal(identity);
        var authenticationState = new AuthenticationState(principal);
        var authenticationTask = Task.FromResult(authenticationState);

        return authenticationTask;
    }
 * 
 */
