using HealthyDuty.Web.Business.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Interfaces
{
    public interface IAuthenticationService
    {
        EmployeeLoginResponse Login(string userName, string password);
    }
}
