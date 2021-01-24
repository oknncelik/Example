namespace Example.Common.Results.Abstract
{
    public interface IResultModel<TModel> : IResult
    {
        TModel Result { get; set; }
    }
}