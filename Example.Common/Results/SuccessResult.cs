using Example.Common.Helpers;
using Example.Common.Results.Abstract;

namespace Example.Common.Results
{
    public class SuccessResult<TModel> : IResult<TModel>
    {
        public int Code { get; set; } = 0;
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "Ok";
        public TModel Result { get; set; }

        public SuccessResult(TModel result, string message = "") : this(message)
        {
            Result = result;
        }

        public SuccessResult(string message)
        {
            if (message.IsNotEmpity())
                Message = message;
        }
    }
}
