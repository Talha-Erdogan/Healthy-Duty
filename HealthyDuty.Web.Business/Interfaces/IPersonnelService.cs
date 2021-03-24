using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.Personnel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Interfaces
{
    public interface IPersonnelService
    {
        PaginatedList<PersonnelWithDetail> GetAllPaginatedWithDetailBySearchFilter(PersonnelSearchFilter searchFilter);
        List<Personnel> GetAll();
        Personnel GetById(int id);
        Personnel GetByUserName(string userName);
        Personnel GetByUserNameAndPassword(string userName, string password);
        int Add(Personnel record);
        int Update(Personnel record);
    }
}
