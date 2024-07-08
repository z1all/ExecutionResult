using Z1all.ExecutionResult.StatusCode;
using Z1all.ExecutionResult.StatusCode.Errors;

namespace ExecutionResult_Test.Erros
{
    public record class EmailAlreadyInUse(string email)
        : BaseError(StatusCodeExecutionResult.BadRequest, "EmailAlreadyInUse", $"Email {email} is already in use!");

    public record class LoginFail()
        : BaseError(StatusCodeExecutionResult.BadRequest, "LoginFail", $"Invalid username or password!");

    public record class TokenNotFound()
        : BaseError(StatusCodeExecutionResult.NotFound, "TokenNotFound", $"Token not found!");
}
