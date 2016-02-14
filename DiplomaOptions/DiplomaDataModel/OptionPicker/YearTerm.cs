using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.OptionPicker
{
    public class YearTerm
    {
        [Key]
        public int YearTermId { get; set; }
        public int Year { get; set; }
        public int Term { get; set; }
        public bool isDefault { get; set; }

        public List<YearTerm> YearTerms { get; set; }
    }
}
