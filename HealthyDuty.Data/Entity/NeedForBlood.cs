using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HealthyDuty.Data.Entity
{
    [Table("NeedForBlood")] 
    public class NeedForBlood
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        public int BloodGroupId { get; set; }

        [Required]
        public int HospitalId { get; set; }

        [StringLength(100)]
        public string DonorName { get; set; }

        [StringLength(100)]
        public string DonorLastName { get; set; }

        [StringLength(100)]
        public string DonorPhone { get; set; }

        [Required]
        public bool IsFound { get; set; }

        public DateTime? FoundDateTime { get; set; }
    }
}
