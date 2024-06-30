using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace ExecutionResult.Interfaces
{
    public interface IExecutionResult
    {
        bool IsSuccess { get; }
        bool IsNotSuccess { get; }
        public ImmutableDictionary<string, List<string>> Errors { get; }
    }

    public interface IExecutionResult<TSuccessResult> : IExecutionResult
        where TSuccessResult : class
    {
        TSuccessResult Result { get; }
        bool TryGetResult ([NotNullWhen(true)] out TSuccessResult? result);
    }
}
