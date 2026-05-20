using AutoMapper;
using Example.Entities.Dtos;
using Example.Entities.Entities;

namespace Example.Business.Mappings.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<User, UserInfoModel>().ReverseMap();
            CreateMap<RegisterModel, User>();
            CreateMap<UserOperationClaim, UserOperationClaimModel>().ReverseMap();
        }
    }
}
