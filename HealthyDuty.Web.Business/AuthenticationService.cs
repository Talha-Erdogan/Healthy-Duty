using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business
{
    public  class AuthenticationService : IAuthenticationService
    {
        private readonly IPersonnelService _personnelService;
        public AuthenticationService(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        public EmployeeLoginResponse Login(string userName, string password)
        {
            EmployeeLoginResponse result = new EmployeeLoginResponse();

            var personnel = _personnelService.GetByUserNameAndPassword(userName, password);
            if (personnel != null)
            {
                result.Personnel = personnel;
                result.IsValid = true;
                result.ErrorMessage = "";
            }
            else
            {
                result.IsValid = false;
                result.ErrorMessage = "Personnel Not Found.";
            }
            return result;
        }
    }
}
