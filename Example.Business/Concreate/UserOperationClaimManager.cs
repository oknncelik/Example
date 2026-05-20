using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Example.Business.Abstract;
using Example.Common.Attributes;
using Example.Common.Results;
using Example.Common.Results.Abstract;
using Example.Dal.Abstract.Repositories;
using Example.Entities.Dtos;
using Example.Entities.Entities;

namespace Example.Business.Concreate;

public class UserOperationClaimManager : IUserOperationClaimManager
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IMapper _mapper;

    public UserOperationClaimManager(IUserOperationClaimRepository userOperationClaimRepository, IMapper _mapper)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        this._mapper = _mapper;
    }

    [Auth("UserOperationClaim.List")]
    public async Task<IResult> GetList()
    {
        var result = await _userOperationClaimRepository.GetList();
        var data = _mapper.Map<List<UserOperationClaimModel>>(result);
        return new SuccessResult<List<UserOperationClaimModel>>(data);
    }

    [Auth("UserOperationClaim.List")]
    public async Task<IResult> GetByUserId(int userId)
    {
        var result = await _userOperationClaimRepository.GetList(x => x.UserId == userId);
        var data = _mapper.Map<List<UserOperationClaimModel>>(result);
        return new SuccessResult<List<UserOperationClaimModel>>(data);
    }

    [Auth("UserOperationClaim.Add")]
    public async Task<IResult> Add(UserOperationClaimModel userOperationClaim)
    {
        var entity = _mapper.Map<UserOperationClaim>(userOperationClaim);
        var result = await _userOperationClaimRepository.Add(entity);
        var data = _mapper.Map<UserOperationClaimModel>(result);
        return new SuccessResult<UserOperationClaimModel>(data);
    }

    [Auth("UserOperationClaim.Update")]
    public async Task<IResult> Update(UserOperationClaimModel userOperationClaim)
    {
        var entity = _mapper.Map<UserOperationClaim>(userOperationClaim);
        var result = await _userOperationClaimRepository.Update(entity);
        var data = _mapper.Map<UserOperationClaimModel>(result);
        return new SuccessResult<UserOperationClaimModel>(data);
    }

    [Auth("UserOperationClaim.Delete")]
    public async Task<IResult> Delete(UserOperationClaimModel userOperationClaim)
    {
        var entity = _mapper.Map<UserOperationClaim>(userOperationClaim);
        var result = await _userOperationClaimRepository.Delete(entity);
        return new SuccessResult<bool>(result);
    }
}
