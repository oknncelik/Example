using System.Xml.XPath;
using Example.Common.Results.Abstract;

namespace Example.Common.Results
{
    public class WarningResult : IResult
    {
        public string ResultType { get; } = "Warning";
        public int Code { get; set; }
        public bool IsSuccess { get; } = false;
        public string Message { get; set; }
        
        public WarningResult(int code, string message) : this(message)
        {
            Code = code;
        }
        
        public WarningResult(string message)
        {
            Message = message;
        }
    }
}