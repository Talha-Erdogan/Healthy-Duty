using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Interfaces
{
    public interface IUserService
    {
        PaginatedList<UserWithDetail> GetAllPaginatedWithDetailBySearchFilter(UserSearchFilter searchFilter);
        List<User> GetAll();
        User GetById(int id);
        User GetByUserName(string userName);
        int Add(User record);
        int Update(User record);

    }
}
