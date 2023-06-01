using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOToVer1._2.Models
{
    public class OwnerIdentityPhoto
    {
        [Key]
        public int Id { get; set; }
        public string NameImg { get; set; }

        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }
    }
}
