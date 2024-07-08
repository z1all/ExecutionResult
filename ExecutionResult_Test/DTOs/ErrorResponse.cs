using System.Collections.Immutable;

namespace ExecutionResult_Test.DTOs
{
    public class ErrorResponse
    {
        public required string Title { get; set; } = null!;
        public required int Status { get; set; }
        public required ImmutableDictionary<string, List<string>> Errors { get; set; } = null!;
    }
}
