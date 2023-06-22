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
            CreateMap<SkillSet, SkillSetDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Type))
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill.Name));
            CreateMap<CreateSkillSetDTO, SkillSet>();
            CreateMap<SkillSetDTO, SkillSet>();
            CreateMap<SkillSetDTO, CreateSkillSetDTO>();
            CreateMap<Skill,SkillDTO>();
            CreateMap<Category, CategoryDTO>();
        }
    }
}
