using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HealthyDuty.Data.Entity
{
    [Table("City")]
    public class City
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        public int Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
