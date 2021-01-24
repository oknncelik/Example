#region

using System.Threading.Tasks;
using Example.Common.Results.Abstract;
using Example.Entities.Dtos;

#endregion

namespace Example.Business.Abstract
{
    public interface IAuthManager
    {
        Task<IResult> CreateAccessToken(IResult userModel);
        Task<IResult> Login(LoginModel login);
        Task<IResult> Register(RegisterModel register);
    }
}