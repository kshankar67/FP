﻿using System.Linq;
using System.Web;
using System.Web.Mvc;
using FP.Models;
using FP.Manager;
using Newtonsoft.Json;
using static FP.Manager.Enums;
using System.IO;
using System.Data;
using System;
using System.EnterpriseServices;
using FP.Controllers;
using System.Web.UI;
using System.Xml.Linq;
//using FP.Helpers;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Collections.Generic;
//using FileInfo = FP.Models.FileInfo;

namespace FP.Controllers
{
    [Authorize]
    public class MasterController : BaseController
    {
        FP_DBEntities db = new FP_DBEntities();
        JsonResponseData response = new JsonResponseData();
        int result = 0; bool CheckStatus = false;
        string MSG = string.Empty;

        #region Reg Details Create,List,Update
        //public ActionResult CourseDetail()
        //{
        //    CoursesDModel model = new CoursesDModel();
        //    return View(model);
        //}
        //public ActionResult GetCourseDetail()
        //{
        //    try
        //    {
        //        bool IsCheck = false;
        //        var tbllist = SP_Model.GetSPCourseEdit();
        //        if (tbllist != null)
        //        {
        //            IsCheck = true;
        //        }
        //        var html = ConvertViewToString("_CourseEdit", tbllist);
        //        var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
        //        res.MaxJsonLength = int.MaxValue;
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        string er = ex.Message;
        //        return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
        //    }
        //}
        //public ActionResult CourseD(int Id = 0)
        //{
        //    CoursesDModel model = new CoursesDModel();
        //    if (Id > 0)
        //    {
        //        var tbl = db.tbl_CoursesDetail.Find(Id);
        //        if (tbl != null && Id > 0)
        //        {
        //            model.NameCourseEng = tbl.NameCourseEng;
        //            model.NameCourseHindi = tbl.NameCourseHindi;
        //            model.CourseTypeEng = tbl.CourseTypeEng;
        //            model.CourseTypeHindi = tbl.CourseTypeHindi;
        //            model.JobOpportunityEng = tbl.JobOpportunityEng;
        //            model.JobOpportunityHindi = tbl.JobOpportunityHindi;
        //            model.CourseDurationEng = tbl.CourseDurationEng;
        //            model.CourseDurationHindi = tbl.CourseDurationHindi;
        //            model.ShortDescriptionCourseEng = tbl.ShortDescriptionCourseEng;
        //            model.ShortDescriptionCourseHindi = tbl.ShortDescriptionCourseHindi;
        //            model.EligibilityEng = tbl.EligibilityEng;
        //            model.EligibilityHindi = tbl.EligibilityHindi;
        //            model.MarksCriteriaEng = tbl.MarksCriteriaEng;
        //            model.MarksCriteriaHindi = tbl.MarksCriteriaHindi;
        //            model.AdmissionProcessEng = tbl.AdmissionProcessEng;
        //            model.AdmissionProcessHindi = tbl.AdmissionProcessHindi;
        //            model.MediumInstructionEng = tbl.MediumInstructionEng;
        //            model.MediumInstructionHindi = tbl.MediumInstructionHindi;
        //            model.HostelAvailabilityEng = tbl.HostelAvailabilityEng;
        //            model.HostelAvailabilityHindi = tbl.HostelAvailabilityHindi;
        //            model.AvailableScholarshipEng = tbl.AvailableScholarshipEng;
        //            model.AvailableScholarshipHindi = tbl.AvailableScholarshipHindi;
        //            model.CategoryEng = tbl.CategoryEng;
        //            model.CategoryHindi = tbl.CategoryHindi;
        //            model.CategoryOtherEng = tbl.CategoryOtherEng;
        //            model.CategoryOtherHindi = tbl.CategoryOtherHindi;
        //            model.College_Unvty_InstEng = tbl.College_Unvty_InstEng;
        //            model.College_Unvty_InstHindi = tbl.College_Unvty_InstHindi;
        //            model.FeeStructureEng = tbl.FeeStructureEng;
        //            model.FeeStructureHindi = tbl.FeeStructureHindi;
        //            model.StatusInstitutionEng = tbl.StatusInstitutionEng;
        //            model.StatusInstitutionHindi = tbl.StatusInstitutionHindi;
        //            model.DistrictEng = tbl.DistrictEng;
        //            model.DistrictHindi = tbl.DistrictHindi;
        //        }
        //    }
        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult CourseD(CoursesDModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        Danger("Required!", true);
        //        return View(model);
        //    }
        //    var tbl = model.ID != 0 ? db.tbl_CoursesDetail.Find(model.ID) : new tbl_CoursesDetail();
        //    if (tbl != null)
        //    {
        //        tbl.NameCourseEng = model.NameCourseEng;
        //        tbl.NameCourseHindi = model.NameCourseHindi;
        //        tbl.CourseTypeEng = model.CourseTypeEng;
        //        tbl.CourseTypeHindi = model.CourseTypeHindi;
        //        tbl.JobOpportunityEng = model.JobOpportunityEng;
        //        tbl.JobOpportunityHindi = model.JobOpportunityHindi;
        //        tbl.CourseDurationEng = model.CourseDurationEng;
        //        tbl.CourseDurationHindi = model.CourseDurationHindi;
        //        tbl.ShortDescriptionCourseEng = model.ShortDescriptionCourseEng;
        //        tbl.ShortDescriptionCourseHindi = model.ShortDescriptionCourseHindi;
        //        tbl.EligibilityEng = model.EligibilityEng;
        //        tbl.EligibilityHindi = model.EligibilityHindi;
        //        tbl.MarksCriteriaEng = model.MarksCriteriaEng;
        //        tbl.MarksCriteriaHindi = model.MarksCriteriaHindi;
        //        tbl.AdmissionProcessEng = model.AdmissionProcessEng;
        //        tbl.AdmissionProcessHindi = model.AdmissionProcessHindi;
        //        tbl.MediumInstructionEng = model.MediumInstructionEng;
        //        tbl.MediumInstructionHindi = model.MediumInstructionHindi;
        //        tbl.HostelAvailabilityEng = model.HostelAvailabilityEng;
        //        tbl.HostelAvailabilityHindi = model.HostelAvailabilityHindi;
        //        tbl.AvailableScholarshipEng = model.AvailableScholarshipEng;
        //        tbl.AvailableScholarshipHindi = model.AvailableScholarshipHindi;
        //        tbl.CategoryEng = model.CategoryEng;
        //        tbl.CategoryHindi = model.CategoryHindi;
        //        tbl.CategoryOtherEng = model.CategoryOtherEng;
        //        tbl.CategoryOtherHindi = model.CategoryOtherHindi;
        //        tbl.College_Unvty_InstEng = model.College_Unvty_InstEng;
        //        tbl.College_Unvty_InstHindi = model.College_Unvty_InstHindi;
        //        tbl.FeeStructureEng = model.FeeStructureEng;
        //        tbl.FeeStructureHindi = model.FeeStructureHindi;
        //        tbl.StatusInstitutionEng = model.StatusInstitutionEng;
        //        tbl.StatusInstitutionHindi = model.StatusInstitutionHindi;
        //        tbl.DistrictEng = model.DistrictEng;
        //        tbl.DistrictHindi = model.DistrictHindi;
        //        tbl.IsActive = true;
        //        if (model.ID == 0)
        //        {
        //            tbl.CreatedBy = User.Identity.Name;
        //            tbl.CreatedDt = DateTime.Now;
        //            db.tbl_CoursesDetail.Add(tbl);
        //        }
        //        else
        //        {
        //            tbl.UpdatedBy = User.Identity.Name;
        //            tbl.UpdatedDt = DateTime.Now;
        //        }
        //        var res = db.SaveChanges();
        //        if (res > 0)
        //        {
        //            Success("Added Successfully !", true);
        //            return RedirectToAction("CourseD", new { id = tbl.ID });
        //        }
        //    }
        //    return View(model);
        //}
        #endregion

        #region Master
        public ActionResult GetDistList()
        {
            try
            {
                var items = SP_Model.SPDistrict();
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetBlckList(int DistrictId)
        {
            try
            {
                var items = SP_Model.SPBlock(DistrictId);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPanchayatList(int DistrictId, int BlockId)
        {
            try
            {
                var items = SP_Model.SPPanchayat(DistrictId, BlockId);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetVillageList(int DistrictId, int BlockId,int PanchayatId)
        {
            try
            {
                var items = SP_Model.SPVillage(DistrictId, BlockId, PanchayatId);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        // GET: Master
        public ActionResult UserDetaillist()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }
        public ActionResult GetUserDetailData()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = CommonModel.GetSPCutUserlist();
                if (tbllist != null)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_UserDetailData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }

        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
    }
}