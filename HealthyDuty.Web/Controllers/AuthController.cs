using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Common;
using HealthyDuty.Web.Business.Enums;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.Auth;
using HealthyDuty.Web.Filters;
using HealthyDuty.Web.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HealthyDuty.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            AuthSearchFilter searchFilter = new AuthSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Code = model.Filter.Filter_Code;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            try
            {
                model.DataList = _authService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<Auth>(new List<Auth>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_LIST)]
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

            AuthSearchFilter searchFilter = new AuthSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Code = model.Filter.Filter_Code;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            try
            {
                model.DataList = _authService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<Auth>(new List<Auth>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_ADD)]
        public ActionResult Add()
        {
            Models.Auth.AddViewModel model = new AddViewModel();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Auth.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HealthyDuty.Data.Entity.Auth auth = new HealthyDuty.Data.Entity.Auth();
            auth.Code = model.Code;
            auth.Name = model.Name;
            auth.CreatedBy = SessionHelper.CurrentUser.Id;
            auth.CreatedDateTime = DateTime.Now;
            auth.IsDeleted = false;
            try
            {
                _authService.Add(auth);
                return RedirectToAction(nameof(AuthController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Auth.AddViewModel model = new AddViewModel();
            try
            {
                var result = _authService.GetById(id);
                if (result ==null)
                {
                    return View("_ErrorNotExist");
                }
                model.Code = result.Code;
                model.Id = result.Id;
                model.Name = result.Name;
            }
            catch 
            {
                ViewBag.ErrorMessage = "Record Not Found";
                return View("_ErrorNotExist");
            }
            
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Auth.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var auth = _authService.GetById(model.Id);
                if (auth == null)
                {
                    return View("_ErrorNotExist");
                }
                auth.Code = model.Code;
                auth.Name = model.Name;
                if (model.SubmitType == "Edit")
                {
                   _authService.Update(auth);
                }
                if (model.SubmitType == "Delete")
                {
                   _authService.Delete(auth.Id,SessionHelper.CurrentUser.Id);
                }
                return RedirectToAction(nameof(AuthController.List));
            }
            catch 
            {
                ViewBag.ErrorMessage = "Not Operation.";
                return View(model);
            }
            
        }
    }
}
