namespace Vehiculos.Api;

public class Result<TValue>
{
    public TValue Value { get; }
    public string Error { get; }
    public bool IsSuccess { get; }

    private Result(TValue value, string error, bool isSuccess)
    {
        Value = value;
        Error = error;
        IsSuccess = isSuccess;
    }

    public static Result<TValue> Success(TValue value) => new Result<TValue>(value, null, true);
    public static Result<TValue> Fail(string error) => new Result<TValue>(default, error, false);

}