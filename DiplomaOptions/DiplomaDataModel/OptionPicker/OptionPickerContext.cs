using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.OptionPicker
{
    public class OptionPickerContext : DbContext
    {
        public OptionPickerContext() : base("DefaultConnection") { }

        public IDbSet<Choice> Choices { get; set; }
        public IDbSet<Option> Options { get; set; }
        public IDbSet<YearTerm> YearTerms { get; set; }
    }
}
