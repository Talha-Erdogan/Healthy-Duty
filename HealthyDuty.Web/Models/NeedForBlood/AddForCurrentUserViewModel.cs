using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Models.NeedForBlood
{
    public class AddForCurrentUserViewModel
    {
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


        //select list
        public List<SelectListItem> BloodGroupSelectList { get; set; }

        //edit or delete 
        public string SubmitType { get; set; }
    }
}
