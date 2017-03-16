using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PMPET.Models
{
    public class Viewing
    {
        public int ID { get; set; }
        [Display(Name = "Person ID")]
        public int PersonID { get; set; }
        public int? RealEstateID { get; set; }
        [Required]
        [Display(Name = "Viewing Type")]
        public string ViewingType { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Date")]
        public DateTime ViewDate { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Time")]
        public DateTime ViewTime { get; set; }

        [Required]
        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public Person Person { get; set; }
        public RealEstate RealEstate { get; set; }

    }
}
