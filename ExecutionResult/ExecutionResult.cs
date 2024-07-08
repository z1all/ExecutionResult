using System.Collections.Immutable;
using Z1all.ExecutionResult.Base;
using Z1all.ExecutionResult.Errors;

namespace Z1all.ExecutionResult
{
    public class ExecutionResult : BaseExecutionResult
    {
        public ExecutionResult() : base() { }
        public ExecutionResult(ImmutableDictionary<string, List<string>> errors) : base(errors) { }
        public ExecutionResult(string keyError, params string[] error) : base(keyError, error) { }

        public static ExecutionResult FromSuccess() => new();
        public static ExecutionResult FromError<TSuccessResult>(ExecutionResult<TSuccessResult> errorResult) => new(errorResult.Errors);
        public static ExecutionResult FromError(string keyError, params string[] error) => new(keyError, error);
        public static ExecutionResult FromError(ImmutableDictionary<string, List<string>> errors) => new(errors);
        public static ExecutionResult FromError(BaseError error) => new(error.KeyError, error.KeyError);
    }

    public class ExecutionResult<TSuccessResult> : BaseExecutionResult<TSuccessResult>
    {
        public ExecutionResult(TSuccessResult result) : base(result) { }
        public ExecutionResult(ImmutableDictionary<string, List<string>> errors) : base(errors) { }
        public ExecutionResult(string keyError, params string[] error) : base(keyError, error) { }

        public static ExecutionResult<TSuccessResult> FromSuccess(TSuccessResult value) => new(value);
        public static ExecutionResult<TSuccessResult> FromError(ExecutionResult errorResult) => new(errorResult.Errors);
        public static ExecutionResult<TSuccessResult> FromError(string keyError, params string[] error) => new(keyError, error);
        public static ExecutionResult<TSuccessResult> FromError(ImmutableDictionary<string, List<string>> errors) => new(errors);
        public static ExecutionResult<TSuccessResult> FromError(BaseError error) => new(error.KeyError, error.KeyError);

        public static implicit operator ExecutionResult<TSuccessResult>(TSuccessResult value) => new(value);
    }
}
