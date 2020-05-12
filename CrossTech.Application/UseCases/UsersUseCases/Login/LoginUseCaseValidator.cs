using CrossTech.Application.Interfaces.UseCases;
using FluentValidation;

namespace CrossTech.Application.UseCases.UsersUseCases.Login
{
    public class LoginUseCaseValidator : AbstractValidator<LoginUseCaseRequest>, IUseCaseRequestValidator<LoginUseCaseRequest>
    {
        public LoginUseCaseValidator()
        {
            RuleFor(r => r.Login).NotEmpty().WithMessage("Введите имя учетной записи")
                                 .MinimumLength(3).WithMessage("Минимальная длина поля {MinLength}")
                                 .MaximumLength(50).WithMessage("Максимальная длина поля {MaxLength}");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Введите пароль")
                                    .MaximumLength(50).WithMessage("Максимальная длина пароля {MaxLength}");
        }
    }
}
