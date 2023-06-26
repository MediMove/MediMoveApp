using ErrorOr;
using MediatR;
using MediMove.Client.temp;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace MediMove.Client.Services;

public class BaseService
{
    protected static async Task<ErrorOr<T>> UnpackResponse<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            if (typeof(T) == typeof(Unit))
                return new ErrorOr<T>();

            var result = JsonConvert.DeserializeObject<T>(responseContent);
            return result;
        }
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

    protected UriBuilder GenerateUriBuilder(string endpointPath) => new(_navigationManager.BaseUri + endpointPath);

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

    protected async Task<HttpRequestMessage> GenerateRequestAsync<T>(string endpointPath, HttpMethod httpMethod, T content)
    {
        var uriBuilder = GenerateUriBuilder(endpointPath);
        var request = await GenerateRequestAsync(uriBuilder, httpMethod);
        request.Content = new ObjectContent<T>(content, new JsonMediaTypeFormatter());
        return request;
    }

    protected async Task<ErrorOr<T>> HandleRequestAsync<T>(string endpointPath, HttpMethod httpMethod)
    {
        var request = await GenerateRequestAsync(endpointPath, httpMethod);
        var response = await _httpClient.SendAsync(request);
        return await UnpackResponse<T>(response);
    }

    protected async Task<ErrorOr<N>> HandleRequestAsync<T, N>(string endpointPath, HttpMethod httpMethod, T content)
    {
        var request = await GenerateRequestAsync(endpointPath, httpMethod, content);
        var response = await _httpClient.SendAsync(request);
        return await UnpackResponse<N>(response);
    }

    protected async Task<ErrorOr<T>> HandleQueryAsync<T>(UriBuilder uriBuilder, HttpMethod httpMethod)
    {
        var request = await GenerateRequestAsync(uriBuilder, httpMethod);
        var response = await _httpClient.SendAsync(request);
        return await UnpackResponse<T>(response);
    }
}