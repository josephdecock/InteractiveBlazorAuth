public class CallApiFromClient(HttpClient http) : ICallApi
{
    private readonly HttpClient http = http;

    public async Task<string> CallApiAsync()
    {
        return await http.GetStringAsync("api/weatherforecast");
    }
}
