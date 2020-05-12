using AutoMapper;
using CrossTech.Application.Interfaces.UseCases;
using CrossTech.Application.MapperInfrastructure;

namespace CrossTech.Application.UseCases
{
    /// <summary>
    /// Основной класс UseCase, содержащий экземпляр сервиса, позволяющего совершать отображения экземпляров сущностей в экземпляры моделей
    /// </summary>
    public abstract class BasicUseCase : IUseCase
    {
        protected IMapper _mapper;

        public BasicUseCase()
        {
            MapperManager mapperManager = MapperManager.GetManager();
            _mapper = mapperManager.Mapper;
        }
    }
}
