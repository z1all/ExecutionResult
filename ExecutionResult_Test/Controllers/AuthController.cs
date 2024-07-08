using Microsoft.AspNetCore.Mvc;
using ExecutionResult_Test.DTOs;
using ExecutionResult_Test.Interfaces;
using Z1all.ExecutionResult.StatusCode;

namespace ExecutionResult_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult<TokenResponseDTO> Login([FromBody] LoginDTO login)
        {
            return ExecutionResultHandler(() => _authService.Login(login));
        }

        [HttpPost("registration")]
        public ActionResult<TokenResponseDTO> Registration([FromBody] RegistrationDTO registration)
        {
            return ExecutionResultHandler(() => _authService.Registration(registration));
        }

        [HttpPost("logout")]
        public ActionResult Logout([FromBody] LogoutDTO logout)
        {
            return ExecutionResultHandler(() => _authService.Logout(logout));
        }

        protected ActionResult<TResult> ExecutionResultHandler<TResult>(Func<ExecutionResult<TResult>> operation)
        {
            ExecutionResult<TResult> response = operation();

            if (!response.IsSuccess) return ExecutionResultHandler(response);
            return Ok(response.Result);
        }
        protected ObjectResult ExecutionResultHandler<TResult>(ExecutionResult<TResult> executionResult, string? otherMassage = null)
        {
            return StatusCode((int)executionResult.StatusCode, new ErrorResponse()
            {
                Title = otherMassage ?? "One or more errors occurred.",
                Status = (int)executionResult.StatusCode,
                Errors = executionResult.Errors,
            });
        }
        protected ActionResult ExecutionResultHandler(Func<ExecutionResult> operation)
        {
            ExecutionResult response = operation();

            if (!response.IsSuccess) return ExecutionResultHandler(response);
            return Ok();
        }
        protected ObjectResult ExecutionResultHandler(ExecutionResult executionResult, string? otherMassage = null)
        {
            return StatusCode((int)executionResult.StatusCode, new ErrorResponse()
            {
                Title = otherMassage ?? "One or more errors occurred.",
                Status = (int)executionResult.StatusCode,
                Errors = executionResult.Errors,
            });
        }
    }
}
