namespace ExecutionResult.Interfaces.StatusCode
{
    public interface IExecutionResult<TStatusEnum> : IExecutionResult
        where TStatusEnum : Enum
    {
        TStatusEnum StatusCode { get; }
    }

    public interface IExecutionResult<TSuccessResult, TStatusEnum> : Interfaces.IExecutionResult<TSuccessResult>, IExecutionResult<TStatusEnum>
        where TSuccessResult : class
        where TStatusEnum : Enum
    {
    }
}
