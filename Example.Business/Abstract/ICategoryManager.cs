#region

using System.Threading.Tasks;
using Example.Common.Results.Abstract;

#endregion

namespace Example.Business.Abstract
{
    public interface ICategoryManager
    {
        Task<IResult> GetCategories();
        Task<IResult> AddCategory(string name);
    }
}