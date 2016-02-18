using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.OptionPicker
{
    public class Choice
    {
        [Key]
        public int ChoiceId { get; set; }

        [ForeignKey("YearTerm")]
        public int? YearTermId { get; set; }
        [ForeignKey("YearTermId")]
        public virtual YearTerm YearTerm { get; set; }

        [MaxLength(9)]
        [Display(Name = "Student ID")]
        public string StudentId { get; set; }

        [MaxLength(40)]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        public string StudentFirstName { get; set; }

        [MaxLength(40)]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        public string StudentLastName { get; set; }

        [UIHint("OptionDropDown")]
        [ForeignKey("FirstOption")]
        [Display(Name = "First Choice")]
        public int? FirstChoiceOptionId { get; set; }
        [ForeignKey("FirstChoiceOptionId")]
        public virtual Option FirstOption { get; set; }


        [UIHint("OptionDropDown")]
        [Display(Name = "Second Choice")]
        [ForeignKey("SecondOption")]
        public int? SecondChoiceOptionId { get; set; }
        [ForeignKey("SecondChoiceOptionId")]
        public virtual Option SecondOption { get; set; }


        [UIHint("OptionDropDown")]
        [ForeignKey("ThirdOption")]
        [Display(Name = "Third Choice")]
        public int? ThirdChoiceOptionId { get; set; }
        [ForeignKey("ThirdChoiceOptionId")]
        public virtual Option ThirdOption { get; set; }

        [UIHint("OptionDropDown")]
        [ForeignKey("FourthOption")]
        [Display(Name = "Fourth Choice")]
        public int? FourthChoiceOptionId { get; set; }
        [ForeignKey("FourthChoiceOptionId")]
        public virtual Option FourthOption { get; set; }


        private DateTime _SelectionDate = DateTime.MinValue;

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime SelectionDate
        {
            get
            {
                return (_SelectionDate == DateTime.MinValue) ? DateTime.Now : _SelectionDate;
            }
            set { _SelectionDate = value; }
        }
    }
}
