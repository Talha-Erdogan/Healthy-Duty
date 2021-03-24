using HealthyDuty.Web.Business.Common.Extensions;
using HealthyDuty.Web.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Business.Common
{
    public static class SessionHelper
    {

        // A Better Approach To Access HttpContext Outside A Controller In .Net Core 2.1
        // https://www.c-sharpcorner.com/article/a-better-approach-to-access-httpcontext-outside-a-controller-in-net-core-2-1/
        // Accessing HttpContext outside of framework components in ASP.NET Core
        // https://www.strathweb.com/2016/12/accessing-httpcontext-outside-of-framework-components-in-asp-net-core/


        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext CurrentHttpContext => _httpContextAccessor.HttpContext;


        public static SessionUser CurrentUser
        {
            get
            {

                if (CurrentHttpContext.Session.Get<SessionUser>("HealthyDuty_CurrentUser") == null)
                {
                    return null;
                }
                else
                {
                    return CurrentHttpContext.Session.Get<SessionUser>("HealthyDuty_CurrentUser");
                }
            }

            set
            {
                CurrentHttpContext.Session.Set<SessionUser>("HealthyDuty_CurrentUser", value);
            }
        }

        public static bool IsAuthenticated
        {
            get
            {
                //if (CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.TID))
                if (CurrentUser != null && CurrentUser.Id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static string CurrentLanguage
        {
            get
            {
                if (CurrentHttpContext.Session.GetString("CurrentLanguage") == null || string.IsNullOrEmpty(CurrentHttpContext.Session.GetString("CurrentLanguage").ToString()))
                {
                    string language = "tr-tr";
                    CurrentHttpContext.Session.SetString("CurrentLanguage", language);

                    return language;
                }
                else
                {
                    return CurrentHttpContext.Session.GetString("CurrentLanguage").ToString();
                }
            }

            set
            {
                CurrentHttpContext.Session.SetString("CurrentLanguage", value);
            }
        }

        public static string CurrentLanguageTwoChar
        {
            get
            {
                if (CurrentLanguage == "tr-tr")
                {
                    return "tr";
                }
                else if (CurrentLanguage == "en-us")
                {
                    return "en";
                }
                else
                {
                    return "tr";
                }
            }
        }

        public static void SetCurrentThreadCulture(string language)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        }

        // todo: Login metodu yeni projeye gore kodlanacak
        /*
        public static SessionLoginResult Login(string userRegNum, string userPasswordClear, bool isWindowsAuthentication, IUserService userService, IBshCasAuthService bshCasAuthService, ICustomAppUserService customAppUserService, IBshCasProfileService bshCasProfileService, IDepartmentService departmentService)
        */

        public static SessionLoginResult Login(string userName, string userPasswordClear,
            IAuthenticationService authenticationService, IProfileDetailService profileDetailService,
            IProfilePersonnelService profilePersonnelService)
        {
            try
            {
                var existUser = authenticationService.Login(userName, userPasswordClear);
                if (existUser.IsValid == false)
                {
                    return new SessionLoginResult(false, existUser.ErrorMessage);
                }

                SessionUser currentUser = new SessionUser();
                currentUser.Id = existUser.Personnel.Id;
                currentUser.TC = existUser.Personnel.TC;
                currentUser.Name = existUser.Personnel.Name;
                currentUser.LastName = existUser.Personnel.LastName;
                currentUser.Phone = existUser.Personnel.Phone;
                currentUser.HospitalId = existUser.Personnel.HospitalId;
                currentUser.AuthList = profileDetailService.GetAllAuthByCurrentUser(existUser.Personnel.Id);
                currentUser.ProfileList = profilePersonnelService.GetAllProfileByCurrentUser(existUser.Personnel.Id);
                CurrentUser = currentUser;

                return new SessionLoginResult(true, null);
            }
            catch 
            {
                return new SessionLoginResult(false, "Error");
            }
        }

        public static bool Logout()
        {
            var currentLanguage = SessionHelper.CurrentLanguage;
            CurrentHttpContext.Session.Clear();
            foreach (var item in CurrentHttpContext.Session.Keys)
            {
                CurrentHttpContext.Session.Remove(item);
            }
            SessionHelper.CurrentLanguage = currentLanguage;
            return true;
        }

        public static void SetCurrentLanguageAndCurrentThreadCultureByLanguageTwoChar(string languageTwoChar)
        {
            if (string.IsNullOrEmpty(languageTwoChar))
            {
                languageTwoChar = "tr";
            }
            string newLanguage = languageTwoChar.Equals("tr") ? "tr-tr" : "en-us";
            SessionHelper.CurrentLanguage = newLanguage;
            SessionHelper.SetCurrentThreadCulture(newLanguage);
        }

        public static string GetCurrentRequestIpAddress()
        {
            string requestIpAddress = "";

            /*
            if (!string.IsNullOrEmpty(CurrentHttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
            {
                requestIpAddress = CurrentHttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else if (!string.IsNullOrEmpty(CurrentHttpContext.Request.ServerVariables["REMOTE_ADDR"]))
            {
                requestIpAddress = CurrentHttpContext.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                requestIpAddress = CurrentHttpContext.Request.UserHostAddress;
            }
            */

            if (CurrentHttpContext.Connection.RemoteIpAddress != null)
            {
                requestIpAddress = CurrentHttpContext.Connection.RemoteIpAddress.ToString();
            }

            return requestIpAddress;
        }


        public static bool CheckAuthForCurrentUser(params string[] authCodeList)
        {
            bool result = false;

            if (CurrentUser == null || CurrentUser.AuthList == null || CurrentUser.AuthList.Count == 0)
            {
                return result;
            }

            if (CurrentUser.AuthList.Where(r => authCodeList.Contains(r.Code)).Any())
            {
                result = true;
            }

            // todo: yeni projeye gore kodlanacak

            return result;
        }




        public static void SetObject(string key, object value)
        {
            var str = JsonConvert.SerializeObject(value);
            CurrentHttpContext.Session.SetString(key, str);
        }


        public static T GetObject<T>(string key) where T : class
        {
            string objectString = CurrentHttpContext.Session.GetString(key);
            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }
            else
            {
                T value = JsonConvert.DeserializeObject<T>(objectString);
                return value;
            }
        }
    }
}
