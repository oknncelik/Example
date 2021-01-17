using Example.Common.Results.Abstract;

namespace Example.Common.Results
{
    public class ErrorResult<TModel> : IResult<TModel>
    {
        public int Code { get; set; } = -1;
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
        public TModel Result { get; set; }

        public ErrorResult(int code, string message) : this(message)
        {
            Code = code;
        }

        public ErrorResult(string message)
        {
            Message = message;
        }
    }
}
