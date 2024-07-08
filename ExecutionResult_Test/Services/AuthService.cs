using ExecutionResult_Test.DbContexts;
using ExecutionResult_Test.DTOs;
using ExecutionResult_Test.Interfaces;
using ExecutionResult_Test.Models;
using ExecutionResult_Test.Erros;
using ExecutionResult_Test.Helper;
using Z1all.ExecutionResult.StatusCode;

namespace ExecutionResult_Test.Services
{
    public class AuthService : IAuthService
    {
        private readonly DbContext _dbContext;

        public AuthService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ExecutionResult<TokenResponseDTO> Login(LoginDTO login)
        {
            User? user = _dbContext.Users.FirstOrDefault(user => user.Email == login.Email);
            if (user is null)
            {
                return ExecutionResult<TokenResponseDTO>.FromError(new LoginFail());
            }

            if (!PasswordHelper.ComparePasswordsHash(login.Password, user.Password))
            {
                return ExecutionResult<TokenResponseDTO>.FromError(new LoginFail());
            }

            return new TokenResponseDTO()
            {
                Token = GetToken(),
            };
        }

        public ExecutionResult<TokenResponseDTO> Registration(RegistrationDTO registration)
        {
            bool emailAlreadyInUse = _dbContext.Users.Any(user => user.Email == registration.Email);
            if (emailAlreadyInUse)
            {
                return ExecutionResult<TokenResponseDTO>.FromError(new EmailAlreadyInUse(registration.Email));
            }

            User newUser = new()
            {
                FullName = registration.FullName,
                Email = registration.Email,
                Password = PasswordHelper.GetPasswordsHash(registration.Password),
            };

            _dbContext.Users.Add(newUser);

            return new TokenResponseDTO()
            {
                Token = GetToken(),
            };
        }

        public ExecutionResult Logout(LogoutDTO logout)
        {
            int tokenIndex = _dbContext.Tokens.IndexOf(logout.Token);
            if (tokenIndex == -1)
            {
                return ExecutionResult.FromError(new TokenNotFound());
            }

            _dbContext.Tokens.RemoveAt(tokenIndex);

            return ExecutionResult.FromSuccess();
        }

        private string GetToken()
        {
            string token = TokenHelper.GenerateToken();

            _dbContext.Tokens.Add(token);

            return token;
        }
    }
}
