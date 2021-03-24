using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Models.Hospital
{
    public class AddViewModel
    {
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

        //select list
        public List<SelectListItem> CitySelectList { get; set; }
        //edit or delete 
        public string SubmitType { get; set; }
    }
}
