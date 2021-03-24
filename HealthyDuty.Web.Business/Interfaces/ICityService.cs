using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.City;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Interfaces
{
    public interface ICityService
    {
        PaginatedList<City> GetAllPaginatedWithDetailBySearchFilter(CitySearchFilter searchFilter);
        List<City> GetAll();
        City GetById(int id);
        int Add(City record);
        int Update(City record);



    }
}
