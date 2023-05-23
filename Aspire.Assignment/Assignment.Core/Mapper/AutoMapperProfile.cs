using AutoMapper;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;

namespace Assignment.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<App, AppDTO>();
            CreateMap<User, UserDTO>()
                 .AfterMap((entity, src) => src.Operations = entity.Operation.OperationAccess.Split(","))
                 .ForMember(dest => dest.Rolename, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<Product, ProductDTO>();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<ProductDTO, CreateProductDTO>();
            CreateMap<ProductDTO,Product>();
            CreateMap<UserDTO, User>()
                     .ForMember(dest => dest.LastPasswordChange, opt => opt.MapFrom(src => src.LastPasswordChange??DateTime.Now));
            CreateMap<Role,RoleDTO>();
            CreateMap<UserDTO, CreateUserDTO>();
        }
    }
}
