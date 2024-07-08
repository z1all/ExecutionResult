using System.Collections.Immutable;
using Z1all.ExecutionResult.StatusCode.Base;
using Z1all.ExecutionResult.StatusCode.Errors;

namespace Z1all.ExecutionResult.StatusCode
{
    public class ExecutionResult : BaseExecutionResult<StatusCodeExecutionResult>
    {
        public override StatusCodeExecutionResult DefaultStatusCode => StatusCodeExecutionResult.NoContent;

        protected ExecutionResult() { }
        protected ExecutionResult(StatusCodeExecutionResult status, ImmutableDictionary<string, List<string>> errors) : base(status, errors) { }
        protected ExecutionResult(StatusCodeExecutionResult status, string keyError, params string[] error) : base(status, keyError, error) { }

        public static ExecutionResult FromSuccess() => new();
        public static ExecutionResult FromError<TSuccessResult>(ExecutionResult<TSuccessResult> errorResult) => new ExecutionResult(errorResult.StatusCode, errorResult.Errors);
        public static ExecutionResult FromError(StatusCodeExecutionResult status, string keyError, params string[] error) => new(status, keyError, error);
        public static ExecutionResult FromError(StatusCodeExecutionResult status, ImmutableDictionary<string, List<string>> errors) => new(status, errors);
        public static ExecutionResult FromError(BaseError error) => new(error.Status, error.KeyError, error.KeyError);

        public static ExecutionResult FromBadRequest(string keyError, params string[] error) => FromError(StatusCodeExecutionResult.BadRequest, keyError, error);
        public static ExecutionResult FromForbid(string keyError, params string[] error) => FromError(StatusCodeExecutionResult.Forbid, keyError, error);
        public static ExecutionResult FromNotFound(string keyError, params string[] error) => FromError(StatusCodeExecutionResult.NotFound, keyError, error);
        public static ExecutionResult FromInternalServer(string keyError, params string[] error) => FromError(StatusCodeExecutionResult.InternalServer, keyError, error);
    }

    public class ExecutionResult<TSuccessResult> : BaseExecutionResult<TSuccessResult, StatusCodeExecutionResult>
    {
        public override StatusCodeExecutionResult DefaultStatusCode => StatusCodeExecutionResult.Ok;

        protected ExecutionResult(TSuccessResult result) : base(result) { }
        protected ExecutionResult(StatusCodeExecutionResult status, ImmutableDictionary<string, List<string>> errors) : base(status, errors) { }
        protected ExecutionResult(StatusCodeExecutionResult status, string keyError, params string[] error) : base(status, keyError, error) { }

        public static ExecutionResult<TSuccessResult> FromSuccess(TSuccessResult value) => new ExecutionResult<TSuccessResult>(value);
        public static ExecutionResult<TSuccessResult> FromError(ExecutionResult errorResult) => new ExecutionResult<TSuccessResult>(errorResult.StatusCode, errorResult.Errors);
        public static ExecutionResult<TSuccessResult> FromError(StatusCodeExecutionResult status, string keyError, params string[] error) => new(status, keyError, error);
        public static ExecutionResult<TSuccessResult> FromError(StatusCodeExecutionResult status, ImmutableDictionary<string, List<string>> errors) => new(status, errors);
        public static ExecutionResult<TSuccessResult> FromError(BaseError error) => new(error.Status, error.KeyError, error.KeyError);

        public static ExecutionResult<TSuccessResult> FromBadRequest(string keyError, params string[] error) => FromError(StatusCodeExecutionResult.BadRequest, keyError, error);
        public static ExecutionResult<TSuccessResult> FromForbid(string keyError, params string[] error) => FromError(StatusCodeExecutionResult.Forbid, keyError, error);
        public static ExecutionResult<TSuccessResult> FromNotFound(string keyError, params string[] error) => FromError(StatusCodeExecutionResult.NotFound, keyError, error);
        public static ExecutionResult<TSuccessResult> FromInternalServer(string keyError, params string[] error) => FromError(StatusCodeExecutionResult.InternalServer, keyError, error);

        public static implicit operator ExecutionResult<TSuccessResult>(TSuccessResult value) => new ExecutionResult<TSuccessResult>(value);
    }
}
