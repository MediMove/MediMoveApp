using ErrorOr;
using MediMove.Client.temp;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MediMove.Client.Services;

public class BaseService
{
    protected static async Task<List<Error>> DeserializeError(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
        List<Error> errors = new();
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

    protected UriBuilder GenerateUriBuilder(string endpointPath)
    {
        Console.WriteLine($"URI: {_navigationManager.BaseUri + endpointPath}");
        return new UriBuilder(_navigationManager.BaseUri + endpointPath);
    }

    protected async Task<HttpRequestMessage> GenerateRequestAsync(UriBuilder uriBuilder, HttpMethod httpMethod)
    {
        var request = new HttpRequestMessage(httpMethod, uriBuilder.ToString());
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return request;
    }

    protected async Task<HttpRequestMessage> GenerateRequestAsync(string endpointPath, HttpMethod httpMethod)
    {
        var uriBuilder = GenerateUriBuilder(endpointPath);
        return await GenerateRequestAsync(uriBuilder, httpMethod);
    }
}