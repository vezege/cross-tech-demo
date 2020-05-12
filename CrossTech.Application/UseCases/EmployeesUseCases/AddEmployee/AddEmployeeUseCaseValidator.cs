using CrossTech.Application.Interfaces.UseCases;
using CrossTech.Application.ValidationRules;
using FluentValidation;

namespace CrossTech.Application.UseCases.EmployeesUseCases.AddEmployee
{
    public class AddEmployeeUseCaseValidator : AbstractValidator<AddEmployeeUseCaseRequest>,
                                               IUseCaseRequestValidator<AddEmployeeUseCaseRequest>
    {
        public AddEmployeeUseCaseValidator()
        {
            RuleFor(e => e.Login).NotEmpty().WithMessage("Имя пользователя обязательно к заполнению")
                                 .MaximumLength(50).WithMessage("Максимальная длина составляет {MaxLength}")
                                 .MinimumLength(5).WithMessage("Минимальная длина составляет {MinLength}");
            RuleFor(e => e.Password).NotEmpty().WithMessage("Пароль обязателен к заполнению")
                                    .MaximumLength(50).WithMessage("Максимальная длина составляет {MaxLength}")
                                    .MinimumLength(6).WithMessage("Минимальная длина составляет {MinLength}");
            RuleFor(e => e.FirstName).NotEmpty().WithMessage("Имя обязательно к заполнению")
                                     .IsCyrillic().WithMessage("Имя может содержать только кириллические символы")
                                     .MaximumLength(50).WithMessage("Максимальная длина составляет {MaxLength}")
                                     .MinimumLength(2).WithMessage("Минимальная длина составляет {MinLength}");
            RuleFor(e => e.LastName).NotEmpty().WithMessage("Фамилия обязательна к заполнению")
                                    .IsCyrillic().WithMessage("Фамилия может содержать только кириллические символы")
                                    .MaximumLength(50).WithMessage("Максимальная длина составляет {MaxLength}");
            RuleFor(e => e.MiddleName).NotEmpty().WithMessage("Отчество обязательно к заполнению")
                                      .IsCyrillic().WithMessage("Отчество может содержать только кириллические символы")
                                      .MaximumLength(50).WithMessage("Максимальная длина составляет {MaxLength}")
                                      .MinimumLength(2).WithMessage("Минимальная длина составляет {MinLength}");
            RuleFor(e => e.Phone).NotEmpty().WithMessage("Телефон обязателен к заполнению")
                                 .IsValidPhone().WithMessage("Телефон должен иметь вид +7 (xxx) xxx-xx-xx");
            RuleFor(e => e.PositionId).NotEmpty().WithMessage("Пожалуйста, выберите должность сотрудника");
            RuleFor(e => e.Gender).NotNull().WithMessage("Пожалуйста, выберите пол сотрудника");
            RuleFor(e => e.Birthdate).NotEmpty().WithMessage("Пожалуйста, выберите дату рождения сотрудника")
                                     .IsValidBirthdate().WithMessage("Дата рождения должна находиться в промежутке 01.01.1940 - 31.12.2005");
        }
    }
}
