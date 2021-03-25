using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyDuty.Web.Business.Enums;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.User;
using HealthyDuty.Web.Filters;
using HealthyDuty.Web.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthyDuty.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBloodGroupService _bloodGroupService;
        private readonly ICityService _cityService;
        private readonly ISexService _sexService;
        public UserController(IUserService userService, IBloodGroupService bloodGroupService, ICityService cityService, ISexService sexService)
        {
            _userService = userService;
            _bloodGroupService = bloodGroupService;
            _cityService = cityService;
            _sexService = sexService;
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_USER_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            UserSearchFilter searchFilter = new UserSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_LastName = model.Filter.Filter_LastName;
            searchFilter.Filter_BloodGroupId = model.Filter.Filter_BloodGroupId;
            searchFilter.Filter_CityCode = model.Filter.Filter_CityCode; 
            model.FilterCitySelectList = GetCitySelectList();
            model.FilterBloodGroupSelectList = GetBloodGroupSelectList();
            model.FilterSexSelectList = GetSexSelectList();

            try
            {
                model.DataList = _userService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<UserWithDetail>(new List<UserWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_USER_LIST)]
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

            UserSearchFilter searchFilter = new UserSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_LastName = model.Filter.Filter_LastName;
            searchFilter.Filter_BloodGroupId = model.Filter.Filter_BloodGroupId;
            searchFilter.Filter_CityCode = model.Filter.Filter_CityCode;
            model.FilterCitySelectList = GetCitySelectList();
            model.FilterBloodGroupSelectList = GetBloodGroupSelectList();
            model.FilterSexSelectList = GetSexSelectList();

            try
            {
                model.DataList = _userService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<UserWithDetail>(new List<UserWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_USER_ADD)]
        public ActionResult Add()
        {
            Models.User.AddViewModel model = new AddViewModel();
            model.CitySelectList = GetCitySelectList();
            model.BloodGroupSelectList = GetBloodGroupSelectList();
            model.SexSelectList = GetSexSelectList();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_USER_ADD)]
        [HttpPost]
        public ActionResult Add(Models.User.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select list
                model.CitySelectList = GetCitySelectList();
                model.BloodGroupSelectList = GetBloodGroupSelectList();
                model.SexSelectList = GetSexSelectList();
                return View(model);
            }
            //select list
            model.CitySelectList = GetCitySelectList();
            model.BloodGroupSelectList = GetBloodGroupSelectList();
            model.SexSelectList = GetSexSelectList();

            HealthyDuty.Data.Entity.User user = new HealthyDuty.Data.Entity.User();
            user.Name = model.Name;
            user.LastName = model.LastName;
            user.Phone = model.Phone;
            user.Age = model.Age;
            user.Email = model.Email;
            user.SexId = model.SexId;
            user.UserName = model.UserName;
            user.Password = model.Password;
            user.BloodGroupId = model.BloodGroupId;
            user.CityCode = model.CityCode;

            try
            {
                var existUser = _userService.GetByUserName(model.UserName);
                if (existUser != null)
                {
                    ViewBag.ErrorMessage = "Exist User.";
                    return View(model);
                }
                _userService.Add(user);
                return RedirectToAction(nameof(UserController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_USER_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.User.AddViewModel model = new AddViewModel();

            //select list
            model.CitySelectList = GetCitySelectList();
            model.BloodGroupSelectList = GetBloodGroupSelectList();
            model.SexSelectList = GetSexSelectList();

            try
            {
                var result = _userService.GetById(id);
                if (result == null)
                {
                    return View("_ErrorNotExist");
                }
              
                model.Id = result.Id;
                model.Name = result.Name;
                model.LastName = result.LastName;
                model.Phone = result.Phone;
                model.Age = result.Age;
                model.Email = result.Email;
                model.SexId = result.SexId;
                model.UserName = result.UserName;
                model.Password = result.Password;
                model.BloodGroupId = result.BloodGroupId;
                model.CityCode = result.CityCode;
            }
            catch
            {
                ViewBag.ErrorMessage = "Record Not Found";
                return View("_ErrorNotExist");
            }

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_USER_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.User.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select list
                model.CitySelectList = GetCitySelectList();
                model.BloodGroupSelectList = GetBloodGroupSelectList();
                model.SexSelectList = GetSexSelectList();

                return View(model);
            }

            //select list
            model.CitySelectList = GetCitySelectList();
            model.BloodGroupSelectList = GetBloodGroupSelectList();
            model.SexSelectList = GetSexSelectList();

            try
            {
                var user = _userService.GetById(model.Id);
                if (user == null)
                {
                    return View("_ErrorNotExist");
                }
                user.Name = model.Name;
                user.LastName = model.LastName;
                user.Phone = model.Phone;
                user.Age = model.Age;
                user.Email = model.Email;
                user.SexId = model.SexId;
                user.UserName = model.UserName;
                user.Password = model.Password;
                user.BloodGroupId = model.BloodGroupId;
                user.CityCode = model.CityCode;

                if (model.SubmitType == "Edit")
                {
                    var existUser = _userService.GetByUserName(model.UserName);
                    if (existUser != null)
                    {
                        if (existUser.Id != model.Id)
                        {
                            ViewBag.ErrorMessage = "Exist User.";
                            return View(model);
                        }
                    }
                    _userService.Update(user);
                }
                return RedirectToAction(nameof(UserController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Operation.";
                return View(model);
            }

        }

        [NonAction]
        private List<SelectListItem> GetCitySelectList()
        {
            // city select list 
            List<SelectListItem> resultList = new List<SelectListItem>();
            try
            {
                resultList = _cityService.GetAll().OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Code.ToString(), Text = r.Name }).ToList();
            }
            catch
            {
                resultList = new List<SelectListItem>();
            }
            return resultList;
        }

        [NonAction]
        private List<SelectListItem> GetBloodGroupSelectList()
        {
            // blood group select list 
            List<SelectListItem> resultList = new List<SelectListItem>();
            try
            {
                resultList = _bloodGroupService.GetAll().OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            }
            catch
            {
                resultList = new List<SelectListItem>();
            }
            return resultList;
        }

        [NonAction]
        private List<SelectListItem> GetSexSelectList()
        {
            // sex select list 
            List<SelectListItem> resultList = new List<SelectListItem>();
            try
            {
                resultList = _sexService.GetAll().OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            }
            catch
            {
                resultList = new List<SelectListItem>();
            }
            return resultList;
        }

    }
}
