using ErrorOr;
using MediMove.Client.temp;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MediMove.Client.Services;

public class BaseService
{
    protected static async Task<List<Error>> DeserializeError(HttpResponseMessage? response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
        var errors = new List<Error>();
        foreach (var field in errorResponse.Errors)
            foreach (var error in field.Value)
                errors.Add(Error.Failure(field.Key, error));
                
        return errors;
    }

    protected readonly HttpClient _httpClient;
    protected readonly IJSRuntime _jsRuntime;
    protected readonly NavigationManager _navigationManager;

    public BaseService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
        _navigationManager = navigationManager;
    }

    protected async Task<HttpRequestMessage> generateRequestAsync(string endpointPath, HttpMethod httpMethod)
    {
        var requestUri = new Uri(new Uri(_navigationManager.BaseUri),
            endpointPath);

        var uriBuilder = new UriBuilder(requestUri);

        var request = new HttpRequestMessage(httpMethod, uriBuilder.ToString());

        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return request;
    }
}