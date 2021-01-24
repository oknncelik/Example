namespace Example.Common.Results.Abstract
{
    public interface IResult
    {
        public string ResultType { get; }
        int Code { get; set; }
        bool IsSuccess { get; }
        string Message { get; set; }
    }
}