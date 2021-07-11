using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorkyID.Models
{
    [Keyless]
    public class SchemeUsers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AssignmentID { get; set; }
        public Guid UserID { get; set; }
        public Guid SchemeID { get; set; }
    }

    
}
