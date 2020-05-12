using AutoMapper;
using CrossTech.Application.Models;
using CrossTech.Domain.Entities;

namespace CrossTech.Application.MapperInfrastructure.Profiles
{
    /// <summary>
    /// Класс, содержащий профиль отображения сущности Employee в модель EmployeeModel
    /// </summary>
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeModel>()
                .ForMember(model => model.Phone, o => o.MapFrom(entity => entity.Profile != null ? entity.Profile.Phone.Number : "Номер не указан"))
                .ForMember(model => model.Position, o => o.MapFrom(entity => entity.Position != null ? entity.Position.Name : "Должность не указана"))
                .ForMember(model => model.Birthdate, o => o.MapFrom(entity => entity.Profile != null ? entity.Profile.Birthdate.ToString("yyyy-MM-dd") : ""))
                .ForMember(model => model.Gender, o => o.MapFrom(entity => entity.Profile != null ? entity.Profile.Gender.ToString() : "Пол не указан"))
                .ForMember(model => model.FirstName, o => o.MapFrom(entity => entity.Profile != null ? entity.Profile.FirstName : "Имя не указано"))
                .ForMember(model => model.LastName, o => o.MapFrom(entity => entity.Profile != null ? entity.Profile.LastName : "Фамилия не указана"))
                .ForMember(model => model.MiddleName, o => o.MapFrom(entity => entity.Profile != null ? entity.Profile.MiddleName : "Отчество не указано"))
                .ForMember(model => model.FullName, o => o.MapFrom(entity => entity.Profile != null ? entity.Profile.FullName : "Имя не указано"));
        }
    }
}
