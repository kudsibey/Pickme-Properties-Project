using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMPET.Models
{
    public class Person
    {           
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Date Joined")]
        public DateTime JoinDate { get; set; }
        [Display(Name = "Address")]
        public string AddressLine1 { get; set; }
        [Display(Name = " ")]
        public string AddressLine2 { get; set; }
        [Display(Name = "Town")]
        public string Town { get; set; }
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
        [Display(Name = "Type")]
        public string MemberType { get; set; }

        [Display(Name = "Tel(Landline)")]
        public string TelephoneNo { get; set; }
        [Display(Name = "Tel(Mobile)")]
        public string MobileNo { get; set; }
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
        [Display(Name = "Notes")]
        public string GeneralNotes { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        public ICollection<RealEstate> RealEstates { get; set; }
        public ICollection<Viewing> Viewings { get; set; }
    }
}
