namespace Z1all.ExecutionResult.StatusCode.Errors
{
    public abstract record class BaseError(StatusCodeExecutionResult Status, string KeyError, params string[] Error) 
        : Z1all.ExecutionResult.Errors.BaseError(KeyError, Error);
}
