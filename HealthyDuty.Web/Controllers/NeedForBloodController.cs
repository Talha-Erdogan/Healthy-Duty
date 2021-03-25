using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyDuty.Web.Business.Common;
using HealthyDuty.Web.Business.Enums;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.NeedForBlood;
using HealthyDuty.Web.Filters;
using HealthyDuty.Web.Models.NeedForBlood;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthyDuty.Web.Controllers
{
    public class NeedForBloodController : Controller
    {
        private readonly INeedForBloodService _needForBloodService;
        private readonly IBloodGroupService _bloodGroupService;
        public NeedForBloodController(INeedForBloodService needForBloodService, IBloodGroupService bloodGroupService)
        {
            _needForBloodService = needForBloodService;
            _bloodGroupService = bloodGroupService;
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_NEEDFORBLOOD_LISTFORCURRENTUSER)]
        public ActionResult ListForCurrentUser()
        {
            ListForCurrentUserViewModel model = new ListForCurrentUserViewModel();

            model.Filter = new ListForCurrentUserFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            NeedForBloodSearchFilter searchFilter = new NeedForBloodSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_BloodGroupId = model.Filter.Filter_BloodGroupId;
            model.Filter.Filter_IsFound = 0;//default
            searchFilter.Filter_IsFound = model.Filter.Filter_IsFound;
            searchFilter.Filter_HospitalId = SessionHelper.CurrentUser.HospitalId;
            
            model.FilterBloodGroupSelectList = GetBloodGroupSelectList();
            model.FilterIsFoundSelectList = GetIsFoundSelectList();

            try
            {
                model.DataList = _needForBloodService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<NeedForBloodWithDetail>(new List<NeedForBloodWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_NEEDFORBLOOD_LISTFORCURRENTUSER)]
        [HttpPost]
        public ActionResult ListForCurrentUser(ListForCurrentUserViewModel model)
        {
            // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            if (model.Filter == null)
            {
                model.Filter = new ListForCurrentUserFilterViewModel();
            }

            if (!model.CurrentPage.HasValue)
            {
                model.CurrentPage = 1;
            }

            if (!model.PageSize.HasValue)
            {
                model.PageSize = 10;
            }

            NeedForBloodSearchFilter searchFilter = new NeedForBloodSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_BloodGroupId = model.Filter.Filter_BloodGroupId;
            searchFilter.Filter_IsFound = model.Filter.Filter_IsFound;
            searchFilter.Filter_HospitalId = SessionHelper.CurrentUser.HospitalId;
            model.FilterBloodGroupSelectList = GetBloodGroupSelectList();
            model.FilterIsFoundSelectList = GetIsFoundSelectList();

            try
            {
                model.DataList = _needForBloodService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<NeedForBloodWithDetail>(new List<NeedForBloodWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_NEEDFORBLOOD_ADDFORCURRENTUSER)]
        public ActionResult AddForCurrentUser()
        {
            Models.NeedForBlood.AddForCurrentUserViewModel model = new AddForCurrentUserViewModel();
            model.BloodGroupSelectList = GetBloodGroupSelectList();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_NEEDFORBLOOD_ADDFORCURRENTUSER)]
        [HttpPost]
        public ActionResult AddForCurrentUser(Models.NeedForBlood.AddForCurrentUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.BloodGroupSelectList = GetBloodGroupSelectList();
                return View(model);
            }
            model.BloodGroupSelectList = GetBloodGroupSelectList();

            HealthyDuty.Data.Entity.NeedForBlood needForBlood = new HealthyDuty.Data.Entity.NeedForBlood();
            needForBlood.BloodGroupId = model.BloodGroupId;
            needForBlood.HospitalId = SessionHelper.CurrentUser.HospitalId;
            needForBlood.IsFound = false;

            try
            {
                _needForBloodService.Add(needForBlood);
                return RedirectToAction(nameof(NeedForBloodController.ListForCurrentUser));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_NEEDFORBLOOD_EDITFORCURRENTUSER)]
        public ActionResult EditForCurrentUser(int id)
        {
            Models.NeedForBlood.AddForCurrentUserViewModel model = new AddForCurrentUserViewModel();
            try
            {
                var result = _needForBloodService.GetById(id);
                if (result == null || result.IsFound ==true || result.HospitalId != SessionHelper.CurrentUser.HospitalId)
                {
                    return View("_ErrorNotExist");
                }
                model.Id = result.Id;
                model.BloodGroupId = result.BloodGroupId;
                model.HospitalId = result.HospitalId;
                model.BloodGroupSelectList = GetBloodGroupSelectList();
            }
            catch
            {
                ViewBag.ErrorMessage = "Record Not Found";
                return View("_ErrorNotExist");
            }

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_NEEDFORBLOOD_EDITFORCURRENTUSER)]
        [HttpPost]
        public ActionResult EditForCurrentUser(Models.NeedForBlood.AddForCurrentUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var needForBlood = _needForBloodService.GetById(model.Id);
                if (needForBlood == null || needForBlood.IsFound == true || needForBlood.HospitalId != SessionHelper.CurrentUser.HospitalId)
                {
                    return View("_ErrorNotExist");
                }
                needForBlood.BloodGroupId = model.BloodGroupId;
                needForBlood.HospitalId = SessionHelper.CurrentUser.HospitalId;
                needForBlood.DonorName = model.DonorName;
                needForBlood.DonorLastName = model.DonorLastName;
                needForBlood.DonorPhone = model.DonorPhone;
                needForBlood.IsFound = true;
                needForBlood.FoundDateTime = DateTime.Now;
                model.BloodGroupSelectList = GetBloodGroupSelectList();


                if (model.SubmitType == "Edit")
                {
                    _needForBloodService.Update(needForBlood);
                }
                
                return RedirectToAction(nameof(NeedForBloodController.ListForCurrentUser));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Operation.";
                return View(model);
            }
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
        private List<SelectListItem> GetIsFoundSelectList()
        {
            // found select list
            List<SelectListItem> resultList = new List<SelectListItem>();
            resultList.AddRange(new List<SelectListItem>() {
                new SelectListItem() { Value="0", Text = "No" },
                new SelectListItem() { Value="1", Text = "Yes" }
            });
            return resultList;
        }
    }
}
