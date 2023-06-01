using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("BlogId")]
        [Column(TypeName = "varchar(100)")]
        public Blog Blog { get; set; }
        
        public int BlogId { get; set; }
        //[ForeignKey("UserId")]
        //[Column(TypeName = "varchar(100)")]
        //public User User { get; set; }

        //public int UserId { get; set; }


    }
}
