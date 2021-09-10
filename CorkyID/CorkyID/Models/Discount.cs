using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CorkyID.Models
{
    public class Discount 
    {
        [DisplayName("Discount ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid DiscountID { get; set; }

        [DisplayName("User ID")]
        public Guid UserID { get; set; }

        [DisplayName("Retailer Name")]
        [Required]
        public string RetailerName { get; set; }

        [DisplayName("Size")]
        [Required]
        public string DiscountPercentage { get; set; }

        [DisplayName("Description")]
        [Required]
        public string DiscountDescription { get; set; }

        [DisplayName("Valid From")]
        [Required]
        public DateTime ValidFrom { get; set; }

        [DisplayName("Valid To")]
        [Required]
        public DateTime ValidTo { get; set; }

        [DisplayName("Last Updated")]
        public DateTime LastUpdated { get; set; }
        
        [DisplayName("Logo URL")]
        [Required]
        public string LogoURL { get; set; }

        [DisplayName("Redirect URL")]
        [Required]
        public string RedirectURL { get; set; }

        [DisplayName("Category")]
        [Required]
        public string Category { get; set; }
    }
}
