using AutoMapper;
using CrossTech.Application.Models;
using CrossTech.Domain.Entities;

namespace CrossTech.Application.MapperInfrastructure.Profiles
{
    /// <summary>
    /// Класс, содержащий профиль отображения сущности User в модель UserModel
    /// </summary>
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(model => model.Role, options => options.MapFrom(entity => entity.Role != null ? entity.Role.Name : "Роль не указана"));
        }
    }
}
