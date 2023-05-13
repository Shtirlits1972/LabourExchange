using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LabourExchange
{
    public class RangeValidationRule : ValidationRule
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int intValue;

            if (!int.TryParse(value.ToString(), out intValue))
            {
                return new ValidationResult(false, $"Пожалуйста укажите корректное значение ({Minimum} - {Maximum})");
            }

            if (intValue < Minimum || intValue > Maximum)
            {
                return new ValidationResult(false, $"Пожалуйста укажите корректное значение ({Minimum} - {Maximum})");
            }

            return ValidationResult.ValidResult;
        }
    }
}
