using ExecutionResult_Test.Models;

namespace ExecutionResult_Test.DbContexts
{
    public class DbContext
    {
        public List<User> Users { get; set; } = [];
        public List<string> Tokens { get; set; } = [];
    }
}
