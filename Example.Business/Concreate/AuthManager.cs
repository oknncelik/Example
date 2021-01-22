using System.Threading.Tasks;
using Example.Business.Abstract;
using Example.Common.Attributes;
using Example.Common.Constants;
using Example.Common.Helpers;
using Example.Common.Results;
using Example.Common.Results.Abstract;
using Example.Common.Security.Jwt.Abstract;
using Example.Common.Security.Jwt.Models;
using Example.Dal.Abstract.Repositories;
using Example.Entities.Dtos;
using Example.Entities.Entities;

namespace Example.Business.Concreate
{
    public class AuthManager : IAuthManager
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserRepository _userRepository;

        public AuthManager(IUserRepository userRepository,
            ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<IResult<UserInfoModel>> Register(RegisterModel register)
        {
            HashHelpers.CreatePasswordHash(register.Password, out var passwordHash, out var passwordSalt);
            var user = new User
            {
                UserName = register.UserName,
                EMail = register.EMail,
                FirstName = register.FirstName,
                LastName = register.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                ActiveFlg = true
            };
            user = await _userRepository.Add(user);
            var result = new UserInfoModel
            {
                Id = user.Id,
                EMail = user.EMail,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return new SuccessResult<UserInfoModel>(result, Messages.UserRegistered);
        }

        [Log]
        [Cache]
        public async Task<IResult<UserInfoModel>> Login(LoginModel login)
        {
            var user = await _userRepository.Get(x => x.UserName == login.UserName || x.EMail == login.UserName);
            if (user == null)
                return new ErrorResult<UserInfoModel>(Messages.UserNotFound);

            if (!HashHelpers.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                return new ErrorResult<UserInfoModel>(Messages.PasswordError);

            var result = new UserInfoModel
            {
                Id = user.Id,
                EMail = user.EMail,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return new SuccessResult<UserInfoModel>(result, Messages.SuccessfulLogin);
        }

        public async Task<IResult<AccessToken>> CreateAccessToken(UserInfoModel userModel)
        {
            var user = await _userRepository.Get(x => x.Id == userModel.Id);
            if (user != null)
            {
                var claims = await _userRepository.GetClaims(user);
                var accessToken = _tokenHelper.CreateToken(user, claims);
                return new SuccessResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
            }

            return new ErrorResult<AccessToken>(Messages.AccessTokenNotCreated);
        }
    }
}