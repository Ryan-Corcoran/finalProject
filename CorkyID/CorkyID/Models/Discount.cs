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
        public string RetailerName { get; set; }

        [DisplayName("Discount Size")]
        public string DiscountPercentage { get; set; }

        [DisplayName("Discount Description")]
        public string DiscountDescription { get; set; }

        [DisplayName("Valid From")]
        public DateTime ValidFrom { get; set; }

        [DisplayName("Valid To")]
        public DateTime ValidTo { get; set; }

        [DisplayName("Last Updated Date")]
        public DateTime LastUpdated { get; set; }
        
        [DisplayName("Logo URL")]
        public string LogoURL { get; set; }

        [DisplayName("Redirect URL")]
        public string RedirectURL { get; set; }
    }
}
