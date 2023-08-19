namespace LearnMediatr;

public class Result<TValue, TError>
{
    public bool IsError { get; }

    public bool IsSuccess => !IsError;

    private readonly TValue? _value;
    private readonly TError? _error;

    public Result(TValue val)
    {
        _value = val;
        IsError = false;
        _error = default;
    }

    public Result(TError err)
    {
        _value = default;
        IsError = false;
        _error = err;
    }

    public T Match<T>(Func<TValue, T> successCb, Func<TError, T> errorCb)
    {
        if (IsError)
        {
            return errorCb(_error!);
        }
        else
        {
            return successCb(_value!);
        }
    }
}
