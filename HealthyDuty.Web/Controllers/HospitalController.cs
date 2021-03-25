using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyDuty.Web.Business.Common;
using HealthyDuty.Web.Business.Enums;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.Hospital;
using HealthyDuty.Web.Filters;
using HealthyDuty.Web.Models.Hospital;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthyDuty.Web.Controllers
{
    public class HospitalController : Controller
    {
        private readonly IHospitalService _hospitalService;
        private readonly ICityService _cityService;

        public HospitalController(IHospitalService hospitalService, ICityService cityService)
        {
            _hospitalService = hospitalService;
            _cityService = cityService;
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_HOSPITAL_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();
            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            HospitalSearchFilter searchFilter = new HospitalSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_CityCode = model.Filter.Filter_CityCode;
            model.FilterCitySelectList = GetCitySelectList();

            try
            {
                model.DataList = _hospitalService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<HospitalWithDetail>(new List<HospitalWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_HOSPITAL_LIST)]
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

            HospitalSearchFilter searchFilter = new HospitalSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_CityCode = model.Filter.Filter_CityCode;
            model.FilterCitySelectList = GetCitySelectList();


            try
            {
                model.DataList = _hospitalService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<HospitalWithDetail>(new List<HospitalWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_HOSPITAL_ADD)]
        public ActionResult Add()
        {
            Models.Hospital.AddViewModel model = new AddViewModel();
            //select list
            model.CitySelectList = GetCitySelectList();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_HOSPITAL_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Hospital.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select list
                model.CitySelectList = GetCitySelectList();
                return View(model);
            }
            //select list
            model.CitySelectList = GetCitySelectList();

            HealthyDuty.Data.Entity.Hospital hospital = new HealthyDuty.Data.Entity.Hospital();
            hospital.Name = model.Name;
            hospital.Phone = model.Phone;
            hospital.Address = model.Address;
            hospital.CityCode = model.CityCode;
            hospital.IsDeleted = false;

            try
            {
                _hospitalService.Add(hospital);
                return RedirectToAction(nameof(HospitalController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_HOSPITAL_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Hospital.AddViewModel model = new AddViewModel();

            //select list
            model.CitySelectList = GetCitySelectList();

            try
            {
                var result = _hospitalService.GetById(id);
                if (result == null)
                {
                    return View("_ErrorNotExist");
                }
                model.Id = result.Id;
                model.Name = result.Name;
                model.Phone = result.Phone;
                model.Address = result.Address;
                model.CityCode = result.CityCode;
            }
            catch
            {
                ViewBag.ErrorMessage = "Record Not Found";
                return View("_ErrorNotExist");
            }

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_HOSPITAL_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Hospital.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select list
                model.CitySelectList = GetCitySelectList();
                return View(model);
            }

            //select list
            model.CitySelectList = GetCitySelectList();

            try
            {
                var hospital = _hospitalService.GetById(model.Id);
                if (hospital == null)
                {
                    return View("_ErrorNotExist");
                }
                
                hospital.Name = model.Name;
                hospital.Phone = model.Phone;
                hospital.Address = model.Address;
                hospital.CityCode = model.CityCode;
                
                if (model.SubmitType == "Edit")
                {
                    _hospitalService.Update(hospital);
                }
                if (model.SubmitType == "Delete")
                {
                    _hospitalService.Delete(hospital.Id, SessionHelper.CurrentUser.Id);
                }
                return RedirectToAction(nameof(HospitalController.List));
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
  
    }
}
