using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMPET.Models
{
    
    public class RealEstate
    {
        public int ID { get; set; }
        [Display(Name = "Person ID")]
        public int PersonID { get; set; }
        public int ServiceID { get; set; }
        [Required]
        [Display(Name = "Address")]
        [StringLength(30, ErrorMessage = "First line of address cannot be longer than 30 characters.")]
        public string AddressLine1 { get; set; }
        [Required]
        [Display(Name = " ")]
        [StringLength(40, ErrorMessage = "Second line of address cannot be longer than 40 characters.")]
        public string AddressLine2 { get; set; }
        [Required]
        [Display(Name = "Town")]
        [StringLength(30, ErrorMessage = "Town name cannot be longer than 30 characters.")]
        public string Town { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Post code cannot be longer than 30 characters.")]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "Property Type")]
        public string PropertyType { get; set; }
        [Required]
        [Display(Name = "No Of Bedrooms")]
        [Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int BedroomQty { get; set; }
        [Required]
        [Display(Name = "No Of Bathroom + WC")]
        [Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int BathroomWCQty { get; set; }
        [Required]
        [Display(Name = "No Of Bathrooms")]
        [Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int BathroomQty { get; set; }
        [Required]
        [Display(Name = "No WC")]
        [Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int WCQty { get; set; }
        [Required]
        [Display(Name = "Front Garden?")]
        public bool FrontGarden { get; set; }
        [Required]
        [Display(Name = "Back Garden?")]
        public bool BackGarden { get; set; }
        [Required]
        [Display(Name = "General Description")]
        public string GenDescription { get; set; }


        [Range(1, int.MaxValue,ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Display(Name = "Sale Price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:£#,###,###.##}")]
        public decimal SalePrice { get; set; }
        [Display(Name = "Rent per Month")]
        [DisplayFormat(DataFormatString = "{0:£#,###,###.##}")]
        [DataType(DataType.Currency)]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public decimal RentalPrice { get; set; }

        public Person Person { get; set; }
        public Service Service { get; set; }

        public ICollection<Viewing> Viewings { get; set; }
    }
}
