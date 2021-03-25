using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Enums;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.BloodGroup;
using HealthyDuty.Web.Filters;
using HealthyDuty.Web.Models.BloodGroup;
using Microsoft.AspNetCore.Mvc;

namespace HealthyDuty.Web.Controllers
{
    public class BloodGroupController : Controller
    {
        private readonly IBloodGroupService _bloodGroupService;
        public BloodGroupController(IBloodGroupService bloodGroupService)
        {
            _bloodGroupService = bloodGroupService;
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_BLOODGROUP_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            BloodGroupSearchFilter searchFilter = new BloodGroupSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            try
            {
                model.DataList = _bloodGroupService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<BloodGroup>(new List<BloodGroup>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_BLOODGROUP_LIST)]
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

            BloodGroupSearchFilter searchFilter = new BloodGroupSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            try
            {
                model.DataList = _bloodGroupService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<BloodGroup>(new List<BloodGroup>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_BLOODGROUP_ADD)]
        public ActionResult Add()
        {
            Models.BloodGroup.AddViewModel model = new AddViewModel();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_BLOODGROUP_ADD)]
        [HttpPost]
        public ActionResult Add(Models.BloodGroup.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HealthyDuty.Data.Entity.BloodGroup bloodGroup = new HealthyDuty.Data.Entity.BloodGroup();
            bloodGroup.Name = model.Name;
            try
            {
                _bloodGroupService.Add(bloodGroup);
                return RedirectToAction(nameof(BloodGroupController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_BLOODGROUP_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.BloodGroup.AddViewModel model = new AddViewModel();
            try
            {
                var result = _bloodGroupService.GetById(id);
                if (result == null)
                {
                    return View("_ErrorNotExist");
                }
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

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_BLOODGROUP_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.BloodGroup.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var bloodGroup = _bloodGroupService.GetById(model.Id);
                if (bloodGroup == null)
                {
                    return View("_ErrorNotExist");
                }
                bloodGroup.Name = model.Name;
                if (model.SubmitType == "Edit")
                {
                    _bloodGroupService.Update(bloodGroup);
                }
                
                return RedirectToAction(nameof(BloodGroupController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Operation.";
                return View(model);
            }

        }
    }
}
