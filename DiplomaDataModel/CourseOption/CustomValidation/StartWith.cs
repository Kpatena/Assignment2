using System.ComponentModel.DataAnnotations;

namespace DiplomaDataModel.CourseOption.CustomValidation
{
    class StartWith : ValidationAttribute
    {
        private readonly string _chars;

        public StartWith(string chars) : base("Student ID needs to start with" + chars)
        {
            _chars = chars;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueAsString = value.ToString().Trim();

                //If string doesn't start with A00
                if (!valueAsString.StartsWith(_chars))
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
