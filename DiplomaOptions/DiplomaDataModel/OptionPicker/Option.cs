using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.OptionPicker
{
    public class Option
    {
        [Key]
        public int OptionId { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [Display(Name = "Is Active")]
        public bool isActive { get; set; }

        public List<Choice> choices { get; set; }
    }
}
