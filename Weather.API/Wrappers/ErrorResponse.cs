using System.Collections.Generic;

namespace Weather.API.Wrappers
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}