using CrossTech.Application.Interfaces.UseCases;
using CrossTech.Application.ValidationRules;
using FluentValidation;

namespace CrossTech.Application.UseCases.EmployeesUseCases.UpdateEmployee
{
    public class UpdateEmployeeUseCaseValidator : AbstractValidator<UpdateEmployeeUseCaseRequest>,
                                                  IUseCaseRequestValidator<UpdateEmployeeUseCaseRequest>
    {
        public UpdateEmployeeUseCaseValidator()
        {
            RuleFor(e => e.Id).NotEmpty().WithMessage("ID сотрудника не передан");
            RuleFor(e => e.FirstName).NotEmpty().WithMessage("Имя обязательно к заполнению")
                                     .MaximumLength(50).WithMessage("Максимальная длина составляет {MaxLength}")
                                     .MinimumLength(2).WithMessage("Минимальная длина составляет {MinLength}");
            RuleFor(e => e.LastName).NotEmpty().WithMessage("Фамилия обязательна к заполнению")
                                    .MaximumLength(50).WithMessage("Максимальная длина составляет {MaxLength}");
            RuleFor(e => e.MiddleName).NotEmpty().WithMessage("Отчество обязательно к заполнению")
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
