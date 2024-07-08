using ExecutionResult_Test.DTOs;
using Z1all.ExecutionResult.StatusCode;

namespace ExecutionResult_Test.Interfaces
{
    public interface IAuthService
    {
        ExecutionResult<TokenResponseDTO> Login(LoginDTO login);
        ExecutionResult<TokenResponseDTO> Registration(RegistrationDTO registration);
        ExecutionResult Logout(LogoutDTO logout);
    }
}
