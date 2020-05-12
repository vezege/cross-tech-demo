using CrossTech.Application.DTO;
using CrossTech.Application.Interfaces.UseCases;

namespace CrossTech.Application.UseCases
{
    /// <summary>
    /// Основной класс для запросов к UseCase, требующих информацию о запрашивающем пользователе
    /// </summary>
    public class BasicUserUseCaseRequest : IUserUseCaseRequest
    {
        public Requester Requester { get; set; }
    }
}
