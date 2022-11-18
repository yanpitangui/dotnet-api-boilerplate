using Boilerplate.Api.IntegrationTests.Helpers;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace Boilerplate.Api.IntegrationTests.Common;

[Collection("Test collection")]
public abstract class BaseTest : IAsyncLifetime
{
    protected CustomWebApplicationFactory App { get; }
    protected HttpClient Client { get; }
    
    public virtual async Task InitializeAsync()
    {
        await TestingDatabase.SeedDatabase(App.CreateContext);
    }

    public async Task DisposeAsync()
    {
        await App.ResetDatabaseAsync();
    }

    protected BaseTest(CustomWebApplicationFactory apiFactory)
    {
        App = apiFactory;

        Client = App.Client;
    }
    
    protected async Task<T?> GetAsync<T>(string address, object? query = null)
    {
        if (query != null)
        {
            address += $"?{query.ToQueryString()}";
        }

        return await Client.GetFromJsonAsync<T>(address);
    }
    
    protected async Task<HttpResponseMessage> GetAsync(string address, object? query = null)
    {
        if (query != null)
        {
            address += $"?{query.ToQueryString()}";
        }
        return await Client.GetAsync(address);
    }
    
    protected async Task<T?> PostAsync<T>(string address, object? data = null)
    {
        var response = await RequestAsync(address, data, Client.PostAsync);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<HttpResponseMessage> PostAsync(string address, object? data = null, bool ensureSuccess = false)
    {
        var response = await RequestAsync(address, data, Client.PostAsync);
        if (ensureSuccess)
        {
            response.EnsureSuccessStatusCode();
        }
        return response;
    }
    
    protected async Task<T?> PutAsync<T>(string address, object? data = null)
    {
        var response = await RequestAsync(address, data, Client.PutAsync);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<HttpResponseMessage> PutAsync(string address, object? data = null, bool ensureSuccess = false)
    {
        var response = await RequestAsync(address, data, Client.PutAsync);
        if (ensureSuccess)
        {
            response.EnsureSuccessStatusCode();
        }
        return response;
    }
    
    protected async Task<T?> PatchAsync<T>(string address, object? data = null)
    {
        var response = await RequestAsync(address, data, Client.PatchAsync);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<HttpResponseMessage> PatchAsync(string address, object? data = null, bool ensureSuccess = false)
    {
        var response = await RequestAsync(address, data, Client.PatchAsync);
        if (ensureSuccess)
        {
            response.EnsureSuccessStatusCode();
        }
        return response;
    }
    
    protected async Task<HttpResponseMessage> DeleteAsync(string address, bool ensureSuccess = false)
    {
        var response = await Client.DeleteAsync(address);
        if (ensureSuccess)
        {
            response.EnsureSuccessStatusCode();
        }
        return response;
    }
    
    
    
    protected static async Task<HttpResponseMessage> RequestAsync(string address, object? data, Func<string, HttpContent, Task<HttpResponseMessage>> verb)
    {
        var json = JsonSerializer.Serialize(data);

        HttpResponseMessage response;
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        if (data is HttpContent httpContent)
        {
            response = await verb(address, httpContent);
        }
        else
        {
            response = await verb(address, content);
        }

        return response;
    }
    
}