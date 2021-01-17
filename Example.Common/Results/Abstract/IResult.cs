namespace Example.Common.Results.Abstract
{
    public interface IResult<TModel>
    {
        int Code { get; set; } 
        bool IsSuccess { get; set; } 
        string Message { get; set; }
        TModel Result { get; set; }
    }
}
