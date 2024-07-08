using System.Text;

namespace ExecutionResult_Test.Helper
{
    public static class TokenHelper
    {
        private static Random random = new Random();

        public static string GenerateToken()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var token = new StringBuilder(64);
            for (int i = 0; i < 64; i++)
            {
                token.Append(chars[random.Next(chars.Length)]);
            }
            return token.ToString();
        }
    }
}
