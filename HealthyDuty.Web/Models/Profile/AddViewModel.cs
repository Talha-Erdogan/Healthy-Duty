using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Models.Profile
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Code { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }


        //edit or delete 
        public string SubmitType { get; set; }
    }
}
