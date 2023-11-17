using System.Net.Http.Json;

public class CallApiFromClient(HttpClient http) : ICallApi
{
    private readonly HttpClient http = http;

    public async Task<ApiResult> CallApiAsync()
    {
        return await http.GetFromJsonAsync<ApiResult>("api/token-details") ?? throw new Exception("API Failure");
    }
}
