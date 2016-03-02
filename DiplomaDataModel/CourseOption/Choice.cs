using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using DiplomaDataModel.CourseOption.CustomValidation;
using System.ComponentModel;

namespace DiplomaDataModel.CourseOption
{
    public class Choice
    {

        [HiddenInput(DisplayValue = false)]
        public int ChoiceId { get; set; }

        [ForeignKey("YearTerm")]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Year Term Id: ")]
        public int? YearTermId { get; set; }

        [ForeignKey("YearTermId")]
        public virtual YearTerm YearTerm { get; set; }

        [UIHint("Default")]
        [Display(Name = "Student Number: ")]
        [StartWith("A00", ErrorMessage = "Student ID needs to start with A00")]
        [MaxLength(9, ErrorMessage = "Max Length 9 Characters")]
        [Required(ErrorMessage = "Student number is required.")]
        public string StudentId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name: ")]
        public string StudentFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name: ")]
        public string StudentLastName { get; set; }

        [UIHint("FirstChoiceDropDown")]
        [Required(ErrorMessage = "First Choice is required.")]
        [Display(Name = "First Choice: ")]
        [ForeignKey("FirstOption")]
        public int? FirstChoiceOptionId { get; set; }
        [ForeignKey("FirstChoiceOptionId")]
        public virtual Option FirstOption { get; set; }

        [UIHint("SecondChoiceDropDown")]
        [Required(ErrorMessage = "Second Choice is required.")]
        [Display(Name = "Second Choice: ")]
        [ForeignKey("SecondOption")]
        public int? SecondChoiceOptionId { get; set; }
        [ForeignKey("SecondChoiceOptionId")]
        public virtual Option SecondOption { get; set; }

        [UIHint("ThirdChoiceDropDown")]
        [Required(ErrorMessage = "Third Choice is required.")]
        [Display(Name = "Third Choice: ")]
        [ForeignKey("ThirdOption")]
        public int? ThirdChoiceOptionId { get; set; }
        [ForeignKey("ThirdChoiceOptionId")]
        public virtual Option ThirdOption { get; set; }

        [UIHint("FourthChoiceDropDown")]
        [Required(ErrorMessage = "Fourth Choice is required.")]
        [Display(Name = "Fourth Choice: ")]
        [ForeignKey("FourthOption")]
        public int? FourthChoiceOptionId { get; set; }
        [ForeignKey("FourthChoiceOptionId")]
        public virtual Option FourthOption { get; set; }

        [Display(Name = "Selection Date: ")]
        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.Date)]
        public DateTime SelectionDate { get; set; }

        public ICollection<Option> Options { get; set; }
        public ICollection<YearTerm> YearTerms { get; set; }
    }
}
