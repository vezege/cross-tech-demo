using CrossTech.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Validators;

namespace CrossTech.Application.ValidationRules
{
    internal static class PhoneValidationExtension
    {
        public static IRuleBuilderOptions<T, string> IsValidPhone<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new PhoneValidator());
        }
    }

    /// <summary>
    /// Класс, реализующий проверку поступившей строки на соблюдение необходимого для номера телефона формату
    /// </summary>
    internal class PhoneValidator : PropertyValidator
    {
        internal PhoneValidator() : base("{ErrorText}")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            string phoneString = context.PropertyValue as string;
            try
            {
                Phone phone = Phone.For(phoneString);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
