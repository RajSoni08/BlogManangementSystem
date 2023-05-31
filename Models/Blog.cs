using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string BlogTitle { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string BlogContet { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string BlogCategory { get; set; }
        [Range(0,5)]
        public int NoofSubsciption { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }

    }
}