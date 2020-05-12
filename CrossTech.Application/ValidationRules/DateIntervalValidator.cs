using FluentValidation;
using FluentValidation.Validators;
using System;

namespace CrossTech.Application.ValidationRules
{
    public static class DateIntervalValidationExtension
    {
        public static IRuleBuilderOptions<T, DateTime> IsValidBirthdate<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new DateIntervalValidator(new DateTime(1940, 1, 1), new DateTime(2005, 12, 31)));
        }
    }

    /// <summary>
    /// Класс, реализующий проверку поступившей даты на нахождение в определенном периоде
    /// </summary>
    internal class DateIntervalValidator : PropertyValidator
    {
        private DateTime _startDate;
        private DateTime _endDate;

        internal DateIntervalValidator() : base("{ErrorText}")
        {
            _startDate = new DateTime(1900, 1, 1);
            _endDate = DateTime.Now;
        }

        internal DateIntervalValidator(DateTime startDate) : base("{ErrorText}")
        {
            _startDate = startDate;
            _endDate = DateTime.Now;
        }

        internal DateIntervalValidator(DateTime startDate, DateTime endDate) : base("{ErrorText}")
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            DateTime date = (DateTime)context.PropertyValue;
            return date >= _startDate && date <= _endDate;
        }
    }
}
