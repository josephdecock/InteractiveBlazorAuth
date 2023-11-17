public interface ICallApi
{
    Task<ApiResult> CallApiAsync();
}

public record ApiResult(string Jti, DateTime TimeStamp);