using Greggs.Core.Enums;

namespace Greggs.Core.Models
{
    public class ErrorModel
    {
        public ErrorCode ErrorCode { get; set; }

        public int Code => (int)ErrorCode;

        public string Message { get; set; }

        public override string ToString()
        {
            return $"[{ErrorCode}({Code})] - {Message}";
        }

    }
}
