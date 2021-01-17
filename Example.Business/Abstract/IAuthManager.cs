using Example.Common.Results.Abstract;
using Example.Common.Security.Jwt.Models;
using Example.Entities.Dtos;
using System.Threading.Tasks;

namespace Example.Business.Abstract
{
    public interface IAuthManager
    {
        Task<IResult<AccessToken>> CreateAccessToken(UserInfoModel userModel);
        Task<IResult<UserInfoModel>> Login(LoginModel login);
        Task<IResult<UserInfoModel>> Register(RegisterModel register);
    }
}
