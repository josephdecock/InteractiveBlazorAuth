
public class CallApiFromServer : ICallApi
{
    public Task<string> CallApiAsync()
    {
        return Task.FromResult("Not implemented server side yet");
    }
}