﻿
using Duende.AccessTokenManagement.OpenIdConnect;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components.Authorization;

public class CallApiFromServer : ICallApi
{

    private readonly HttpClient _client;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IUserTokenManagementService _tokenManagementService;


    public CallApiFromServer(
    IHttpClientFactory factory,
    AuthenticationStateProvider authenticationStateProvider,
    IUserTokenManagementService tokenManagementService)
    {
        _client = factory.CreateClient("server-side-api-client");
        _authenticationStateProvider = authenticationStateProvider;
        _tokenManagementService = tokenManagementService;
    }

    public async Task<ApiResult> CallApiAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "token-details");
        var response = await SendRequestAsync(request);
        return await response.Content.ReadFromJsonAsync<ApiResult>() ?? throw new Exception("API Failure");
    }

    private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var token = await _tokenManagementService.GetAccessTokenAsync(state.User);

        request.SetToken(token.AccessTokenType!, token.AccessToken!);
        return await _client.SendAsync(request);
    }
}