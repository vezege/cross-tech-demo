using AutoMapper;
using CrossTech.Application.Models;
using CrossTech.Domain.Entities;

namespace CrossTech.Application.MapperInfrastructure.Profiles
{
    /// <summary>
    /// Класс, содержащий профиль отображения сущности Position в модель PositionModel
    /// </summary>
    class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<Position, PositionModel>();
        }
    }
}
