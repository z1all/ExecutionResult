using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using ExecutionResult.Interfaces;

namespace ExecutionResult
{
    public class ExecutionResult : IExecutionResult
    {
        private ImmutableDictionary<string, List<string>> _errors = ImmutableDictionary<string, List<string>>.Empty;
        public ImmutableDictionary<string, List<string>> Errors
        {
            get { return _errors; }
            protected set
            {
                _errors = value;
                IsSuccess = false;
            }
        }

        private bool _isSuccess = false;
        public bool IsSuccess
        {
            get { return _isSuccess; }
            protected set
            {
                _isSuccess = value;
            }
        }
        public bool IsNotSuccess { get => !_isSuccess; }

        protected ExecutionResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public ExecutionResult(string keyError, params string[] error)
        {
            IsSuccess = false;
            _errors = _errors.Add(keyError, error.ToList());
        }
        public ExecutionResult(ImmutableDictionary<string, List<string>> errors)
        {
            IsSuccess = false;
            _errors = errors;
        }

        public static ExecutionResult FromSuccess() => new(isSuccess: true);
    }

    public class ExecutionResult<TSuccessResult> : ExecutionResult, IExecutionResult<TSuccessResult>
        where TSuccessResult : class
    {
        private TSuccessResult? _result;
        public TSuccessResult Result
        {
            get 
            {
                if (_result is null) throw new NullReferenceException();
                return _result; 
            }
            protected set
            {
                _result = value;
                IsSuccess = true;
            }
        }

        public bool TryGetResult([NotNullWhen(true)] out TSuccessResult? result)
        {
            result = default;
            if (IsNotSuccess || Result is null) return false;

            result = Result;
            return true;
        }

        protected ExecutionResult(TSuccessResult result) : base(isSuccess: true) 
        {
            if (result is null) throw new ArgumentNullException();
            Result = result;
        }
        protected ExecutionResult(string keyError, params string[] error) : base(keyError, error) { }
        protected ExecutionResult(ImmutableDictionary<string, List<string>> errors) : base(errors) { }

        private static new ExecutionResult FromSuccess() => throw new NotImplementedException();
        private static ExecutionResult<TSuccessResult> FromSuccess(TSuccessResult value) => new ExecutionResult<TSuccessResult>(value);
        public static ExecutionResult<TSuccessResult> FromError(ExecutionResult errorResult) => new ExecutionResult<TSuccessResult>(errorResult.Errors);

        public static implicit operator ExecutionResult<TSuccessResult>(TSuccessResult value) => new ExecutionResult<TSuccessResult>(value);
    }
}
