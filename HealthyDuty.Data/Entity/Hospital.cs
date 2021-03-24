using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HealthyDuty.Data.Entity
{
    [Table("Hospital")]
    public class Hospital
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }


        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        public int CityCode { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDateTime { get; set; }

    }
}
