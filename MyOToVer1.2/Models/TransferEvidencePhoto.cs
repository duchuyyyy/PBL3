using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOToVer1._2.Models
{
    public class TransferEvidencePhoto
    {
        [ForeignKey("CarRental")]
        public int rental_id { get; set; }

        public virtual CarRental CarRental { get; set; }

        public string name_img { get; set; }
    }
}
