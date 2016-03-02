using System.ComponentModel.DataAnnotations;

namespace DiplomaDataModel.CourseOption
{
    public class YearTerm
    {
        
        [Key]
        public int YearTermId { get; set; }

        public int Year { get; set; }

        public int Term { get; set; }

        public bool IsDefault { get; set; }
    }
}
