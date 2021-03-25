using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Models.User
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public int SexId { get; set; }


        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        public int BloodGroupId { get; set; }

        [Required]
        public int CityCode { get; set; }



        //select list
        public List<SelectListItem> CitySelectList { get; set; }
        public List<SelectListItem> BloodGroupSelectList { get; set; }
        public List<SelectListItem> SexSelectList { get; set; }


        //edit or delete 
        public string SubmitType { get; set; }
    }
}
