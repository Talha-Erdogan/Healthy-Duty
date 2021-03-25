using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HealthyDuty.Web.Business.Common;
using HealthyDuty.Web.Business.Enums;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.Personnel;
using HealthyDuty.Web.Filters;
using HealthyDuty.Web.Models.Personnel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthyDuty.Web.Controllers
{
    public class PersonnelController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IProfileDetailService _profileDetailService;
        private readonly IProfilePersonnelService _profilePersonnelService;
        private readonly IPersonnelService _personnelService;
        private readonly IHospitalService _hospitalService;

        public PersonnelController(IAuthenticationService authenticationService, IProfileDetailService profileDetailService, IProfilePersonnelService profilePersonnelService, IPersonnelService personnelService, IHospitalService hospitalService)
        {
            _authenticationService = authenticationService;
            _profileDetailService = profileDetailService;
            _profilePersonnelService = profilePersonnelService;
            _personnelService = personnelService;
            _hospitalService = hospitalService;
        }


        #region Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            GetCaptchaImage();
            return View(model);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            bool isValidCaptcha = CaptchaHelper.ValidateCaptchaCode(model.CaptchaCode, HttpContext);

            if (!isValidCaptcha)
            {
                ViewBag.Error = "Captcha Code Is Not Valid";
                return View(model);
            }

            SessionLoginResult result = SessionHelper.Login(model.UserName, model.Password, _authenticationService, _profileDetailService, _profilePersonnelService);
            if (result.IsSuccess)
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            else
            {
                ViewBag.Error = result.Message;
                return View(model);
            }
        }

        // oturum açmadan erişilecek
        [AllowAnonymous]
        public ActionResult Logout()
        {
            // session logout olmalı
            string _UserSessionTrace_SessionTraceGuid = "UserSessionTrace_SessionTraceGuid elde edilemedi";
            if (SessionHelper.CurrentUser != null)
            {

            }
            SessionHelper.Logout();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult NotAuthorized()
        {
            return View();
        }

        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 120;
            int height = 36;
            var captchaCode = CaptchaHelper.GenerateCaptchaCode();
            var result = CaptchaHelper.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }
        #endregion

        #region List,Add and Edit
        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();
            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            PersonnelSearchFilter searchFilter = new PersonnelSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_LastName = model.Filter.Filter_LastName;
            searchFilter.Filter_HospitalId = model.Filter.Filter_HospitalId;
            model.FilterHospitalSelectList = GetHospitalSelectList();

            try
            {
                model.DataList = _personnelService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<PersonnelWithDetail>(new List<PersonnelWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_LIST)]
        [HttpPost]
        public ActionResult List(ListViewModel model)
        {
            // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            if (model.Filter == null)
            {
                model.Filter = new ListFilterViewModel();
            }

            if (!model.CurrentPage.HasValue)
            {
                model.CurrentPage = 1;
            }

            if (!model.PageSize.HasValue)
            {
                model.PageSize = 10;
            }

            PersonnelSearchFilter searchFilter = new PersonnelSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_LastName = model.Filter.Filter_LastName;
            searchFilter.Filter_HospitalId = model.Filter.Filter_HospitalId;
            model.FilterHospitalSelectList = GetHospitalSelectList();

            try
            {
                model.DataList = _personnelService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<PersonnelWithDetail>(new List<PersonnelWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_ADD)]
        public ActionResult Add()
        {
            Models.Personnel.AddViewModel model = new AddViewModel();
            //select list
            model.HospitalSelectList = GetHospitalSelectList();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Personnel.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select list
                model.HospitalSelectList = GetHospitalSelectList();
                return View(model);
            }
            //select list
            model.HospitalSelectList = GetHospitalSelectList();

            HealthyDuty.Data.Entity.Personnel personnel = new HealthyDuty.Data.Entity.Personnel();
            personnel.TC = model.TC;
            personnel.Name = model.Name;
            personnel.LastName = model.LastName;
            personnel.Phone = model.Phone;
            personnel.Address = model.Address;
            personnel.UserName = model.UserName;
            personnel.Password = model.Password;
            personnel.HospitalId = model.HospitalId;

            try
            {
                var existPersonnel= _personnelService.GetByUserName(model.UserName);
                if (existPersonnel !=null)
                {
                    ViewBag.ErrorMessage = "Exist Personnel.";
                    return View(model);
                }
                _personnelService.Add(personnel);
                return RedirectToAction(nameof(PersonnelController.List));
            }
            catch 
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Personnel.AddViewModel model = new AddViewModel();
            
            //select list
            model.HospitalSelectList = GetHospitalSelectList();

            try
            {
                var result = _personnelService.GetById(id);
                if (result == null)
                {
                    return View("_ErrorNotExist");
                }
                model.Id = result.Id;
                model.TC = result.TC;
                model.Name = result.Name;
                model.LastName = result.LastName;
                model.Phone = result.Phone;
                model.Address = result.Address;
                model.UserName = result.UserName;
                model.Password = result.Password;
                model.HospitalId = result.HospitalId;
            }
            catch
            {
                ViewBag.ErrorMessage = "Record Not Found";
                return View("_ErrorNotExist");
            }

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Personnel.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select list
                model.HospitalSelectList = GetHospitalSelectList();
                return View(model);
            }

            //select list
            model.HospitalSelectList = GetHospitalSelectList();

            try
            {
                var personnel = _personnelService.GetById(model.Id);
                if (personnel == null)
                {
                    return View("_ErrorNotExist");
                }
                personnel.TC = model.TC;
                personnel.Name = model.Name;
                personnel.LastName = model.LastName;
                personnel.Phone = model.Phone;
                personnel.Address = model.Address;
                personnel.UserName = model.UserName;
                personnel.Password = model.Password;
                personnel.HospitalId = model.HospitalId;

                if (model.SubmitType == "Edit")
                {
                    var existPersonnel = _personnelService.GetByUserName(model.UserName);
                    if (existPersonnel != null)
                    {
                        if (existPersonnel.Id != model.Id)
                        {
                            ViewBag.ErrorMessage = "Exist Personnel.";
                            return View(model);
                        }
                    }
                    _personnelService.Update(personnel);
                }
                return RedirectToAction(nameof(PersonnelController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Operation.";
                return View(model);
            }
        }

        [NonAction]
        private List<SelectListItem> GetHospitalSelectList()
        {
            // hospital select list 
            List<SelectListItem> resultList = new List<SelectListItem>();
            try
            {
                resultList = _hospitalService.GetAll().OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            }
            catch
            {
                resultList = new List<SelectListItem>();
            }
            return resultList;
        }
        #endregion
    }
}
