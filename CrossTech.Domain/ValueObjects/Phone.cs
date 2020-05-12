using CrossTech.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CrossTech.Domain.ValueObjects
{
    public class Phone : ValueObject
    {
        private Phone()
        {
        }

        public static Phone For(string phoneString)
        {
            var phone = new Phone();

            Regex phonePattern = new Regex(@"^\+7\s\(\d{3}\)\s\d{3}-\d{2}-\d{2}");
            if (!phonePattern.IsMatch(phoneString))
            {
                throw new FormatException("Номер телефона указан в некорректном формате");
            }

            phone.Number = phoneString;

            return phone;
        }

        public string Number { get; private set; }

        public static implicit operator string(Phone phone)
        {
            return phone.ToString();
        }

        public static explicit operator Phone(string phoneString)
        {
            return For(phoneString);
        }

        public override string ToString()
        {
            return $"{Number}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Number;
        }
    }
}
