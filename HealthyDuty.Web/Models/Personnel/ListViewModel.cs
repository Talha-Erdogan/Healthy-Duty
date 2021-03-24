using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Models.Personnel
{
    public class ListViewModel : ListBaseViewModel<HealthyDuty.Web.Business.Models.Personnel.PersonnelWithDetail, ListFilterViewModel>
    {
        public List<SelectListItem> FilterHospitalSelectList { get; set; }
    }
}
