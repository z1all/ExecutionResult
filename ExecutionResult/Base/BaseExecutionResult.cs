using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Z1all.ExecutionResult.Interfaces;

namespace Z1all.ExecutionResult.Base
{
    public abstract class BaseExecutionResult : IExecutionResult
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

        public BaseExecutionResult()
        {
            IsSuccess = true;
        }
        public BaseExecutionResult(string keyError, params string[] error)
        {
            IsSuccess = false;
            _errors = _errors.Add(keyError, error.ToList());
        }
        public BaseExecutionResult(ImmutableDictionary<string, List<string>> errors)
        {
            IsSuccess = false;
            _errors = errors;
        }
    }

    public class BaseExecutionResult<TSuccessResult> : BaseExecutionResult, IExecutionResult<TSuccessResult>
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

        public BaseExecutionResult(TSuccessResult result) : base()
        {
            if (result is null) throw new ArgumentNullException();
            Result = result;
        }
        public BaseExecutionResult(string keyError, params string[] error) : base(keyError, error) { }
        public BaseExecutionResult(ImmutableDictionary<string, List<string>> errors) : base(errors) { }
    }
}
