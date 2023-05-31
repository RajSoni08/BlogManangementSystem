using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BlogDTO
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string BlogTitle { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string BlogContet { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string BlogCategory { get; set; }

        public int NoofSubsciption { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
    }
}
