using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Models.City
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [Required]
        public int Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }


        //edit 
        //not delete 
        public string SubmitType { get; set; }
    }
}
