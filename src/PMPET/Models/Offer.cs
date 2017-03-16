using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PMPET.Models
{
    public class Offer
    {
        public int ID { get; set; }

        [Display(Name = "Offer Made By")]
        public int PersonID { get; set; }
        public int RealEstateID { get; set; }

        [Required]
        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Offer Date")]
        public DateTime OfferDate { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:£#,###,###.##}")]
        [Display(Name = "Offer Amount")]
        public decimal OfferAmount { get; set; }

        [Required]
        [Display(Name = "Offer Type")]
        public string OfferType { get; set; }

        public Person Person { get; set; }
        public RealEstate RealEstate { get; set; }
    }
}
