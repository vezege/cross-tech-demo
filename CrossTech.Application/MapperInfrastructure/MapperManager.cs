using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrossTech.Application.MapperInfrastructure
{
    /// <summary>
    /// Класс-одиночка для предоставления сервиса, отвечающего за отображение сущностей в модели приложения
    /// </summary>
    internal class MapperManager
    {
        private static MapperManager _manager;
        private IMapper _mapper;

        private MapperManager()
        {
        }

        internal static MapperManager GetManager()
        {
            if (_manager == null)
            {
                _manager = new MapperManager();
            }
            return _manager;
        }

        /// <summary>
        /// Создаем единственный экземпляр сервиса, отвечающего за отображение, регистрируем все профили отображения и записываем его в private-свойство
        /// </summary>
        internal IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    // ищем все профили отображения, заданные в этой сборке
                    Type mapperBaseProfileType = typeof(Profile);
                    Assembly applicationAssembly = Assembly.GetExecutingAssembly();
                    IEnumerable<TypeInfo> mapperProfiles = applicationAssembly.DefinedTypes
                        .Where(t =>
                        {
                            return t.IsClass && t.IsSubclassOf(mapperBaseProfileType);
                        });

                    // создаем конфигурационный файл сервиса
                    var cfg = new MapperConfigurationExpression();

                    // регистрируем все профили отображения
                    foreach (TypeInfo profileType in mapperProfiles)
                    {
                        cfg.AddProfile(profileType);
                    }
                    var config = new MapperConfiguration(cfg);

                    _mapper = new Mapper(config);
                }
                return _mapper;
            }
        }
    }
}
