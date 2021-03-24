using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Models.Hospital
{
    public class ListViewModel : ListBaseViewModel<HealthyDuty.Web.Business.Models.Hospital.HospitalWithDetail, ListFilterViewModel>
    {
        public List<SelectListItem> FilterCitySelectList { get; set; }
    }
}
