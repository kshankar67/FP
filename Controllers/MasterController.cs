using System.Linq;
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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace FP.Controllers
{
    // [Authorize]
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
        public ActionResult GetCLFList(int DistrictId, int BlockId)
        {
            try
            {
                var items = SP_Model.SPCLF(DistrictId, BlockId);
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
        public ActionResult GetPanchayatList(int DistrictId, int BlockId, int CLFId)
        {
            try
            {
                var items = SP_Model.SPPanchayat(DistrictId, BlockId, CLFId);
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
        public ActionResult GetVillageList(int DistrictId, int BlockId, int CLFId, int PanchayatId)
        {
            try
            {
                var items = SP_Model.SPVillage(DistrictId, BlockId, CLFId, PanchayatId);
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
        public ActionResult GetCMList(int DistrictId, int BlockId, int CLFId, int PanchayatId, int VOId)
        {
            try
            {
                var items = SP_Model.SPCM(DistrictId, BlockId, CLFId, PanchayatId, VOId);
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
        public ActionResult GetActivityList()
        {
            try
            {
                var items = CommonModel.GetActivity();
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
        public ActionResult GetContraceptiveList()
        {
            try
            {
                var items = CommonModel.GetContraceptive();
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
        public ActionResult GetContraceptiveChildList(int CID)
        {
            try
            {
                var items = CommonModel.GetContraceptive_Child(CID);
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
        public ActionResult GetYearList(int IsAll = 0)
        {
            try
            {
                var items = CommonModel.GetYear(IsAll);
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
        public ActionResult GetMonthList(int IsAll = 0)
        {
            try
            {
                var items = CommonModel.GetMonth(IsAll);
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
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetUserDetailData(string Roles = "")
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SpUserDetails(Roles, User.Identity.Name);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_UserDData", tbllist);
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

        public ActionResult GetCLFMasterllist(int DistrictId = 0, int BlockId = 0)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SPCLFMasterlist(DistrictId, BlockId);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_CLFData", tbllist);
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
        public ActionResult CLFMaster(int id = 0)
        {
            FP_DBEntities db_ = new FP_DBEntities();
            CLFModel model = new CLFModel();
            if (id > 0)
            {
                var tbl = db_.CLF_Master.Find(id);
                if (tbl != null)
                {
                    model.CLF_ID_pk = tbl.CLF_ID_pk;
                    model.DistrictId_fk = tbl.DistrictId_fk;
                    model.BlockId_fk = tbl.BlockId_fk;
                    model.CLFName = tbl.CLFName;
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        [EnableCors("*")]
        public JsonResult CLFMaster(CLFModel model)
        {
            JsonResponseData response = new JsonResponseData();
            var isExists = db.CLF_Master.FirstOrDefault(x => x.DistrictId_fk == model.DistrictId_fk && x.BlockId_fk == model.BlockId_fk && string.Compare(x.CLFName.Trim(), model.
                CLFName.Trim(), true) == 0 && x.CLF_ID_pk != model.CLF_ID_pk);
            if (isExists == null)
            {
                var tbl = model.CLF_ID_pk != 0 ? db.CLF_Master.Find(model.CLF_ID_pk) : new CLF_Master();
                if (tbl != null && model != null)
                {
                    tbl.CLFName = model.CLFName.Trim();
                    tbl.IsActive = true;
                    if (model.CLF_ID_pk == 0)
                    {
                        tbl.DistrictId_fk = model.DistrictId_fk;
                        tbl.BlockId_fk = model.BlockId_fk;
                        tbl.CreatedBy = MvcApplication.CUser.Id;
                        tbl.CreatedOn = DateTime.Now;
                        db.CLF_Master.Add(tbl);
                    }
                    else
                    {
                        tbl.UpdatedBy = MvcApplication.CUser.Id;
                        tbl.UpdatedOn = DateTime.Now;
                    }
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Record Submitted Successfully!!!", Data = null };
                        var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse3.MaxJsonLength = int.MaxValue;
                        return resResponse3;
                        //ModelState.AddModelError("", Record Submitted Successfully!!!");
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "CLF already exists!!!", Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                return resResponse3;
            }
            return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            //return Json();
            //   return View();
        }
        public ActionResult GetVillagedetaillist(int DistrictId = 0, int BlockId = 0, int CLFId = 0, int PanchayatId = 0)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SPVillagelist(DistrictId, BlockId, CLFId, PanchayatId);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_VillageData", tbllist);
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
        public ActionResult VillMaster(int id = 0)
        {
            FP_DBEntities db_ = new FP_DBEntities();
            VillageModel model = new VillageModel();
            if (id > 0)
            {
                var tbl = db_.VO_Master.Find(id);
                if (tbl != null)
                {
                    model.Void_pk = tbl.Void_pk;
                    model.DistrictId_fk = tbl.DistrictId_fk;
                    model.BlockId_fk = tbl.BlockId_fk;
                    model.CLF_Id_fk = tbl.CLF_Id_fk;
                    model.Panchayatid_fk = tbl.Panchayatid_fk;
                    model.Village_Organization = tbl.Village_Organization;
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        [EnableCors("*")]
        public JsonResult VillMaster(VillageModel model)
        {
            JsonResponseData response = new JsonResponseData();
            //var MS_model = this.Request.Unvalidated.Form["v_model"];
            //model = JsonConvert.DeserializeObject<VillageModel>(MS_model);

            var isExists = db.VO_Master.FirstOrDefault(x => x.DistrictId_fk == model.DistrictId_fk && x.BlockId_fk == model.BlockId_fk && x.Panchayatid_fk == model.Panchayatid_fk && string.Compare(x.Village_Organization.Trim(), model.
                 Village_Organization.Trim(), true) == 0 && x.Void_pk != model.Void_pk);
            if (isExists == null)
            {
                var tbl = model.Void_pk != 0 ? db.VO_Master.Find(model.Void_pk) : new VO_Master();
                if (tbl != null && model != null)
                {
                    tbl.Village_Organization = model.Village_Organization.Trim();
                    tbl.IsActive = true;
                    if (model.Void_pk == 0)
                    {
                        tbl.DistrictId_fk = model.DistrictId_fk;
                        tbl.BlockId_fk = model.BlockId_fk;
                        tbl.CLF_Id_fk = model.CLF_Id_fk;
                        tbl.Panchayatid_fk = model.Panchayatid_fk;
                        tbl.CreatedBy = MvcApplication.CUser.Id;
                        tbl.CreatedOn = DateTime.Now;
                        db.VO_Master.Add(tbl);
                    }
                    else
                    {
                        tbl.UpdatedBy = MvcApplication.CUser.Id;
                        tbl.UpdatedOn = DateTime.Now;
                    }
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Record Submitted Successfully!!!", Data = null };
                        var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse3.MaxJsonLength = int.MaxValue;
                        return resResponse3;
                        //ModelState.AddModelError("", Record Submitted Successfully!!!");
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Village Organization already exists!!!", Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                return resResponse3;
            }
            return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            //return Json();
            //   return View();
        }


        public ActionResult GetPanchayatdetaillist(int DistrictId = 0, int BlockId = 0, int CLFId = 0)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SPPanchayatList(DistrictId, BlockId, CLFId);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_PanchayatData", tbllist);
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

        public ActionResult PanchayatMaster(int id = 0)
        {
            FP_DBEntities _db = new FP_DBEntities();
            PanchayatModel model = new PanchayatModel();
            if (id > 0)
            {
                var tbl = _db.Panchayat_Master.Find(id);
                if (tbl != null)
                {
                    model.Panchayatid_pk = tbl.Panchayatid_pk;
                    model.DistrictId_fk = tbl.DistrictId_fk;
                    model.Blockid_fk = tbl.Blockid_fk;
                    model.CLF_Id_fk = tbl.CLF_Id_fk;
                    model.Panchayat = tbl.Panchayat;
                }
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult PanchayatMaster(PanchayatModel model)
        {
            FP_DBEntities _db = new FP_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                if (!ModelState.IsValid)
                {
                    response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "All required fields!!!", Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }

                var isExists = db.Panchayat_Master.FirstOrDefault(x => x.DistrictId_fk == model.DistrictId_fk && x.Blockid_fk == model.Blockid_fk && string.Compare(x.Panchayat.Trim(), model.
                 Panchayat.Trim(), true) == 0 && x.Panchayatid_pk != model.Panchayatid_pk);
                if (isExists == null)
                {
                    var tbl = model.Panchayatid_pk != 0 ? db.Panchayat_Master.Find(model.Panchayatid_pk) : new Panchayat_Master();

                    if (tbl != null)
                    {
                        tbl.DistrictId_fk = model.DistrictId_fk;
                        tbl.Blockid_fk = model.Blockid_fk;
                        tbl.CLF_Id_fk = model.CLF_Id_fk;
                        tbl.Panchayat = !string.IsNullOrWhiteSpace(model.Panchayat) ? model.Panchayat.Trim() : null;
                        if (model.Panchayatid_pk == 0)
                        {
                            db.Panchayat_Master.Add(tbl);
                        }
                    }

                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Record Submitted Successfully!!!", Data = null };
                        var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse3.MaxJsonLength = int.MaxValue;
                        return resResponse3;
                        //ModelState.AddModelError("", Record Submitted Successfully!!!");
                    }
                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Panchayat already exists!!!", Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    return resResponse3;
                }
            }

            catch (Exception ex)
            {
                string strerror = ex.Message;
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
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