#region

using Example.Common.Results.Abstract;

#endregion

namespace Example.Common.Results
{
    public class ErrorResult : IResult
    {
        public ErrorResult(int code, string message) : this(message)
        {
            Code = code;
        }

        public ErrorResult(string message)
        {
            Message = message;
        }

        public string ResultType { get; } = "Error";
        public int Code { get; set; }
        public bool IsSuccess { get; } = false;
        public string Message { get; set; }
    }
}