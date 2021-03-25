using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Common;
using HealthyDuty.Web.Business.Enums;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.Profile;
using HealthyDuty.Web.Filters;
using HealthyDuty.Web.Models.Profile;
using Microsoft.AspNetCore.Mvc;

namespace HealthyDuty.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            ProfileSearchFilter searchFilter = new ProfileSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Code = model.Filter.Filter_Code;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            try
            {
                model.DataList = _profileService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<Profile>(new List<Profile>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_LIST)]
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

            ProfileSearchFilter searchFilter = new ProfileSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Code = model.Filter.Filter_Code;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            try
            {
                model.DataList = _profileService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<Profile>(new List<Profile>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_ADD)]
        public ActionResult Add()
        {
            Models.Profile.AddViewModel model = new AddViewModel();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Profile.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HealthyDuty.Data.Entity.Profile profile = new HealthyDuty.Data.Entity.Profile();
            profile.Code = model.Code;
            profile.Name = model.Name;
            profile.CreatedBy = SessionHelper.CurrentUser.Id;
            profile.CreatedDateTime = DateTime.Now;
            profile.IsDeleted = false;
            try
            {
                _profileService.Add(profile);
                return RedirectToAction(nameof(ProfileController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Profile.AddViewModel model = new AddViewModel();
            try
            {
                var result = _profileService.GetById(id);
                if (result == null)
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

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Profile.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var profile = _profileService.GetById(model.Id);
                if (profile == null)
                {
                    return View("_ErrorNotExist");
                }
                profile.Code = model.Code;
                profile.Name = model.Name;
                if (model.SubmitType == "Edit")
                {
                    _profileService.Update(profile);
                }
                if (model.SubmitType == "Delete")
                {
                    _profileService.Delete(profile.Id, SessionHelper.CurrentUser.Id);
                }
                return RedirectToAction(nameof(ProfileController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Operation.";
                return View(model);
            }

        }
    }
}
