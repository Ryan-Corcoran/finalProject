using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CorkyID.Models
{
    public class Schemes
    {
        [DisplayName("Scheme ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SchemeID { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Valid From")]
        public DateTime ValidFromDate { get; set; }

        [DisplayName("Valid To")]
        public DateTime ValidToDate { get; set; }

        [DisplayName("Description")]
        public string SchemeDescription { get; set; }

        public Guid OwnerID { get; set;
        }
    }
}
