using CrossTech.Application.Interfaces.UseCases;
using FluentValidation;

namespace CrossTech.Application.UseCases.EmployeesUseCases.RemoveEmployee
{
    public class RemoveEmployeeUseCaseValidator : AbstractValidator<RemoveEmployeeUseCaseRequest>,
                                                  IUseCaseRequestValidator<RemoveEmployeeUseCaseRequest>
    {
        public RemoveEmployeeUseCaseValidator()
        {
            RuleFor(e => e.Id).NotEmpty().WithMessage("Не передан ID сотрудника");
        }
    }
}
