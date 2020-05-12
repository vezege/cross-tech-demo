using CrossTech.Application.Interfaces.UseCases;
using FluentValidation;

namespace CrossTech.Application.UseCases
{
    /// <summary>
    /// Класс, реализующий базу для валидаторов запросов к UseCase, содержащих информацию о запрашивающем пользователе
    /// </summary>
    /// <typeparam name="TUserUseCaseRequest"></typeparam>
    public class BasicUserUseCaseRequestValidator<TUserUseCaseRequest> : AbstractValidator<TUserUseCaseRequest>,
                                                                         IUseCaseRequestValidator<TUserUseCaseRequest>
        where TUserUseCaseRequest : IUserUseCaseRequest
    {
        public BasicUserUseCaseRequestValidator()
        {
            RuleFor(r => r.Requester).NotEmpty().WithMessage("Не передан ID запрашивающего пользователя");
            InitializeRules();
        }

        protected virtual void InitializeRules()
        {
            //
        }
    }
}
