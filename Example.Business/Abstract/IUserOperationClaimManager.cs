using System.Threading.Tasks;
using Example.Common.Results.Abstract;
using Example.Entities.Dtos;

namespace Example.Business.Abstract;

public interface IUserOperationClaimManager
{
    Task<IResult> GetList();
    Task<IResult> GetByUserId(int userId);
    Task<IResult> Add(UserOperationClaimModel userOperationClaim);
    Task<IResult> Update(UserOperationClaimModel userOperationClaim);
    Task<IResult> Delete(UserOperationClaimModel userOperationClaim);
}
