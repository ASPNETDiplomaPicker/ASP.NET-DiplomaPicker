using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.OptionPicker.Seed
{
    class DummyData
    {
        public static List<YearTerm> GetYearTerm()
        {
            List<YearTerm> yearTerms = new List<YearTerm>()
            {
                new YearTerm { Year=2015,Term=20,isDefault=false},
                new YearTerm { Year=2015,Term=30,isDefault=false},
                new YearTerm { Year=2016,Term=10,isDefault=false},
                new YearTerm { Year=2016,Term=30,isDefault=true}
            };
            return yearTerms;
        }

        public static List<Option> GetOption()
        {
            List<Option> options = new List<Option>()
            {
                new Option {Title="Data Communications",isActive=true },
                new Option {Title="Client Server",isActive=true },
                new Option {Title="Digital Processing",isActive=true },
                new Option {Title="Information Systems",isActive=true },
                new Option {Title="Database",isActive=false },
                new Option {Title="Web & Mobile",isActive=true },
                new Option {Title="Tech Pro",isActive=false },
            };
            return options;
        }
    }
}
