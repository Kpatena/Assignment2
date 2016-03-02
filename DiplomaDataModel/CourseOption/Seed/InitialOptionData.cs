using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.CourseOption.Seed
{
    public class InitialOptionData
    {
        public static List<YearTerm> GetYearTerm()
        {
            List<YearTerm> yearTerms = new List<YearTerm>()
            {
                new YearTerm { Year = 2015, Term = 20, IsDefault = false},
                new YearTerm { Year = 2015, Term = 30, IsDefault = false},
                new YearTerm { Year = 2016, Term = 10, IsDefault = false},
                new YearTerm { Year = 2016, Term = 30, IsDefault = true}
            };

            return yearTerms;
        }

        public static List<Option> GetOption()
        {
            List<Option> options = new List<Option>()
            {
                new Option { Title = "Data Communications", IsActive = true},
                new Option { Title = "Client Server", IsActive = true},
                new Option { Title = "Digital Processing", IsActive = true},
                new Option { Title = "Information Systems", IsActive = true},
                new Option { Title = "Database", IsActive = false},
                new Option { Title = "Web & Mobile", IsActive = true},
                new Option { Title = "Tech Pro", IsActive = false}
            };

            return options;
        }
    }
}
