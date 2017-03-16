using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PMPET.Models
{
    public class Service
    {   [Display(Name ="Service ID")]
        public int ID { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "Marketing descripton cannot be longer than 40 characters.")]
        public string Description { get; set; } // can be anything to describe the service 
        [Required]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; } // sale/rent/manage
        [Required]
        [Display(Name = "Fee Type")]
        public string FeeType { get; set; } // fixed or commision
        [Required]
        [Display(Name = "Sale % ")]
        [Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public float SaleCommission { get; set; } // eg. 20%
        [Required]
        [Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Display(Name = "Rent %")]
        public float RentCommision { get; set; }// eg. 20%
        [Range(0, 2000000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Display(Name = "Fixed Fee (Let)")]
        [DisplayFormat(DataFormatString = "{0:£#,###,###.##}")]
        [DataType(DataType.Currency)]
        public float FixedLetFee { get; set; }
        [Range(0, 2000000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Display(Name = "Fixed Sale Fee")]
        [DisplayFormat(DataFormatString = "{0:£#,###,###.##}")]
        [DataType(DataType.Currency)]
        public float FixedSaleFee { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

    
    }
}
