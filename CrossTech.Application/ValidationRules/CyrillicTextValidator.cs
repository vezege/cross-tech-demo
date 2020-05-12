using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace CrossTech.Application.ValidationRules
{
    internal static class CyrillicTextValidationExtension
    {
        public static IRuleBuilderOptions<T, string> IsCyrillic<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CyrillicTextValidator());
        }
    }

    /// <summary>
    /// Класс, реализующий проверку поступившей строки на наличие только кириллических символов
    /// </summary>
    internal class CyrillicTextValidator : PropertyValidator
    {
        internal CyrillicTextValidator() : base("{ErrorText}")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            string text = context.PropertyValue as string;
            if (text == null)
            {
                return false;
            }

            Regex cyrillicPattern = new Regex(@"^([А-Яа-я]|\s)*?$");
            return cyrillicPattern.IsMatch(text);
        }
    }
}
