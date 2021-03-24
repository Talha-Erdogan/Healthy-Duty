using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.Hospital;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Interfaces
{
    public interface IHospitalService
    {
        PaginatedList<HospitalWithDetail> GetAllPaginatedWithDetailBySearchFilter(HospitalSearchFilter searchFilter);
        List<Hospital> GetAll();
        Hospital GetById(int id);
        int Add(Hospital record);
        int Update(Hospital record);
        int Delete(int id, int deletedBy);
    }
}
