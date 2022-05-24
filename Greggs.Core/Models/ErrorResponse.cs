namespace Greggs.Core.Models
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; }

        public ErrorResponse()
        {
            Errors = new List<ErrorModel>();
        }

        public override string ToString()
        {
            return string.Join(",", Errors);
        }
    }
}
