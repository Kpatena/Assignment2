using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace DiplomaDataModel.CourseOption.CustomValidation
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class CompareOptions : ValidationAttribute
    {
        private readonly string _field;

        public CompareOptions(string field) : base("Choices can't be similar to " + field)
        {
            _field = field;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {

                var property = validationContext.ObjectType.GetProperty(this._field);
                if (property == null)
                {
                    return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Unknown property {0}", this._field));
                }
                var otherValue = property.GetValue(validationContext.ObjectInstance, null) as string;

                if (string.Equals(value as string, otherValue))
                {
                    return new ValidationResult(this.FormatErrorMessage(otherValue));
                }

                //return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));


            }
            return ValidationResult.Success;
        }
    }
}
