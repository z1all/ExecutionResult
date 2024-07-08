namespace ExecutionResult_Test.Helper
{
    /// <summary>
    /// Так как это пример использование библиотеки ExecutionResult, то хэширование пароля опущено
    /// </summary>
    public static class PasswordHelper
    {
        public static bool ComparePasswordsHash(string password, string hashedPassword)
        {
            return password.Equals(hashedPassword);
        }

        public static string GetPasswordsHash(string password)
        {
            return password;
        }
    }
}
