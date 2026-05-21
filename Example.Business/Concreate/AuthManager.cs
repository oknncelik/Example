#region

using System.Threading.Tasks;
using AutoMapper;
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

#endregion

namespace Example.Business.Concreate
{
    [Log]
    public class AuthManager : IAuthManager
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthManager(IUserRepository userRepository,
            ITokenHelper tokenHelper, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        public async Task<IResult> Register(RegisterModel register)
        {
            HashHelpers.CreatePasswordHash(register.Password, out var passwordHash, out var passwordSalt);
            var user = _mapper.Map<User>(register);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ActiveFlg = true;
            
            user = await _userRepository.Add(user);
            var result = _mapper.Map<UserInfoModel>(user);
            return new SuccessResult<UserInfoModel>(result, Messages.UserRegistered);
        }


        public async Task<IResult> Login(LoginModel login)
        {
            var user = await _userRepository.Get(x => x.UserName == login.UserName || x.EMail == login.UserName);
            if (user == null)
                return new ErrorResult(Messages.UserNotFound);

            if (!HashHelpers.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                return new ErrorResult(Messages.PasswordError);

            var result = _mapper.Map<UserInfoModel>(user);
            return new SuccessResult<UserInfoModel>(result, Messages.SuccessfulLogin);
        }

        public async Task<IResult> CreateAccessToken(IResult userModel)
        {
            if (userModel.GetType() == typeof(SuccessResult<UserInfoModel>))
            {
                var model = userModel as SuccessResult<UserInfoModel>;
                var user = await _userRepository.Get(x => x.Id == model.Result.Id);
                if (user == null) return new ErrorResult(Messages.AccessTokenNotCreated);
                var accessToken = _tokenHelper.CreateToken(user);
                return new SuccessResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
            }

            return new ErrorResult(Messages.AccessTokenNotCreated);
        }
    }
}