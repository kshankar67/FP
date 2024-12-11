using FP.Manager;
using FP.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static FP.Manager.Enums;

namespace FP.Controllers
{
    public class AchievementController : Controller
    {
        // GET: Achievement
        public ActionResult Index()
        {
            // SanitizePath("");
            return View();
        }
        #region Achievement Plan (CNRP Level)
        public ActionResult AddAchievePlan()
        {
            AchvPlanModel model = new AchvPlanModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddAchievePlan(AchvPlanModel model)
        {
            var results = 0;
            FP_DBEntities db_ = new FP_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                var resAchvPlanlist = this.Request.Unvalidated.Form["AVPlanModel"];

                if (resAchvPlanlist != null)
                {
                    var mlist = JsonConvert.DeserializeObject<List<AVPlanModel>>(resAchvPlanlist);
                    if (mlist != null)
                    {
                        if (mlist.Count() > 0 //&& mlist.Count <= 10
                            )
                        {
                            tbl_AchievementPlan tbl;
                            List<tbl_AchievementPlan> tbl_list = new List<tbl_AchievementPlan>();
                            if (model.DistrictId_fk != null && model.BlockId_fk != null && model.ClusterId_fk != null
                                && model.PanchayatId_fk != null && model.PlanYear != null && model.PlanMonth != null)
                            {
                                var exisgingItems = db_.tbl_AchievementPlan.Where(x => x.DistrictId_fk == model.DistrictId_fk && x.BlockId_fk == model.BlockId_fk
                                          && x.ClusterId_fk == model.ClusterId_fk && x.PanchayatId_fk == model.PanchayatId_fk && x.PlanYear == model.PlanYear
                                          && x.PlanMonth == model.PlanMonth).ToList();
                                if (exisgingItems != null && exisgingItems.Any())
                                {
                                    var deleteItems = exisgingItems.Where(x => !mlist.Any(y => y.AchieveId_pk == x.AchieveId_pk)).ToList();
                                    if (deleteItems!=null && deleteItems.Any())
                                    {
                                        db_.tbl_AchievementPlan.RemoveRange(deleteItems);
                                        db_.SaveChanges();
                                    }
                                }
                                foreach (var m in mlist)
                                {
                                    var existsRow = db_.tbl_AchievementPlan.FirstOrDefault(x => x.DistrictId_fk == model.DistrictId_fk && x.BlockId_fk == model.BlockId_fk
                                        && x.ClusterId_fk == model.ClusterId_fk && x.PanchayatId_fk == model.PanchayatId_fk && x.PlanYear == model.PlanYear
                                        && x.PlanMonth == model.PlanMonth && x.VoId_fk == m.VoId_fk && x.Meetingheld == m.Meetingheld);

                                    if (existsRow == null)
                                    {
                                        if (m.AchieveId_pk == Guid.Empty && m.Noofparticipant != null)
                                        {
                                            tbl = new tbl_AchievementPlan()
                                            {
                                                AchieveId_pk = Guid.NewGuid(),
                                                DistrictId_fk = model.DistrictId_fk,
                                                BlockId_fk = model.BlockId_fk,
                                                PanchayatId_fk = model.PanchayatId_fk,
                                                ClusterId_fk = model.ClusterId_fk,
                                                PlanYear = model.PlanYear,
                                                PlanMonth = model.PlanMonth,
                                                ActivityId_fk = m.ActivityId_fk,
                                                VoId_fk = m.VoId_fk,
                                                VoIds_fk = m.VoIds_fk,//for multiple VO selecection
                                                BfyIds = m.BfyIds,
                                                Meetingheld = m.Meetingheld,
                                                Noofparticipant = m.Noofparticipant,
                                                CreatedBy = MvcApplication.CUser.Id,
                                                CreatedOn = DateTime.Now,
                                                IsActive = true
                                            };
                                            tbl_list.Add(tbl);
                                        }
                                    }
                                    if (m.AchieveId_pk != Guid.Empty && m.Noofparticipant != null)
                                    {
                                        var tblu = db_.tbl_AchievementPlan.Find(m.AchieveId_pk);
                                        tblu.VoId_fk = m.VoId_fk;
                                        tblu.VoIds_fk = m.VoIds_fk;//for multiple VO selecection
                                        tblu.BfyIds = m.BfyIds;//for multiple VO selecection
                                        tblu.ActivityId_fk = m.ActivityId_fk;
                                        tblu.Meetingheld = m.Meetingheld;
                                        tblu.Noofparticipant = m.Noofparticipant;
                                        tblu.UpdatedBy = MvcApplication.CUser.Id;
                                        tblu.UpdatedOn = DateTime.Now;
                                        tblu.IsActive = true;
                                        results += db_.SaveChanges();
                                    }
                                }
                                if (tbl_list.Count > 0)
                                {
                                    db_.tbl_AchievementPlan.AddRange(tbl_list);
                                }
                                results += db_.SaveChanges();
                            }
                            else
                            {
                                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) + "\r\n", Data = null };
                                var resResponseerr = Json(response, JsonRequestBehavior.AllowGet);
                                resResponseerr.MaxJsonLength = int.MaxValue;
                                return resResponseerr;
                            }
                        }
                    }
                }
                response = results > 0 ? new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, Achievement Plan Submitted Successfully ! \r\n", Data = null }
                : new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.NotSubmitData), Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }
            catch (Exception ex)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
        }
        public ActionResult GetEditAchPlanList(AchvPlanModel model)
        {
            FP_DBEntities db_ = new FP_DBEntities();
            try
            {
                var items = db_.tbl_AchievementPlan.Where(x => x.DistrictId_fk == model.DistrictId_fk
                && x.BlockId_fk == model.BlockId_fk && x.ClusterId_fk == model.ClusterId_fk && x.PanchayatId_fk == model.PanchayatId_fk
                && x.PlanYear == model.PlanYear && x.PlanMonth == model.PlanMonth).ToList();
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        if (item.ActivityId_fk == 1 || item.ActivityId_fk == 2)
                        {
                            var VoIds_fk = item.VoIds_fk.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                            item.VONames = string.Join(",", db_.VO_Master.Where(x => VoIds_fk.Any(s => s == x.Void_pk)).Select(x => x.Village_Organization));
                        }
                    }
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = Enums.GetEnumDescription(eReturnReg.RecordNotFound) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error!." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAchPlanList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_AchvPlanList(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_AchvPlanData", tbllist);
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
        #endregion

        public ActionResult GetBFYListByVOIds(string voIds)
        {
            FP_DBEntities db_ = new FP_DBEntities();
            try
            {
                var VoIds_fk = voIds.Trim(',').Split(',').Select(x => Convert.ToInt32(x)).ToList();
                var items = db_.TBL_Beneficiary.Where(x => VoIds_fk.Any(s => s == x.VillageOId_fk)).ToList();
                if (items != null && items.Count > 0)
                {
                    // var data = JsonConvert.SerializeObject(items);
                    var html = ConvertViewToString("_BFList", items);
                    return Json(new { IsSuccess = true, Data = html }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, Data = Enums.GetEnumDescription(eReturnReg.RecordNotFound) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, Data = "There was a communication error!." }, JsonRequestBehavior.AllowGet);
            }
        }

        //#region 2nd Level Approved Planning (CC Level)
        //public ActionResult LevelSecAchvApprove()
        //{
        //    AchvPlanModel model = new AchvPlanModel();
        //    return View(model);
        //}
        //public ActionResult GetAchApvSecPlanList(FilterModel model)
        //{
        //    try
        //    {
        //        bool IsCheck = false;
        //        var tbllist = SP_Model.SP_AchvPlanApv2ndlevel(model);
        //        if (tbllist.Rows.Count > 0)
        //        {
        //            IsCheck = true;
        //        }
        //        var html = ConvertViewToString("_AchvPlanApvLevelSec", tbllist);
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
        //[HttpPost]
        //public ActionResult AchvPlanApvLevSec(AchvPlanModel model)
        //{
        //    var results = 0; var results_Reject = 0;
        //    FP_DBEntities db_ = new FP_DBEntities();
        //    JsonResponseData response = new JsonResponseData();
        //    try
        //    {
        //        var resAchvPlanlist = this.Request.Unvalidated.Form["AVPlanModel"];

        //        if (resAchvPlanlist != null)
        //        {
        //            var mlist = JsonConvert.DeserializeObject<List<AVPlanModel>>(resAchvPlanlist);
        //            if (mlist != null)
        //            {
        //                if (mlist.Count() > 0)
        //                {
        //                    tbl_AchievementPlan tbl;
        //                    List<tbl_AchievementPlan> tbl_list = new List<tbl_AchievementPlan>();
        //                    if (model.DistrictId_fk != null && model.BlockId_fk != null && model.ClusterId_fk != null
        //                       && model.PlanYear != null && model.PlanMonth != null)
        //                    {
        //                        foreach (var m in mlist)
        //                        {
        //                            if (m.AchieveId_pk != Guid.Empty &&
        //                                m.DistrictId != null && m.BlockId != null && m.ClusterId != null &&
        //                                m.PanchayatId != null && m.VoId_fk != null && string.IsNullOrWhiteSpace(m.Remark2))
        //                            {
        //                                //MRP Approve
        //                                var tblu = db_.tbl_AchievementPlan.Find(m.AchieveId_pk);
        //                                if (tblu != null && m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve))
        //                                {
        //                                    var cdt = DateTime.Now;
        //                                    tbl_Achievement_Log tblLog = new tbl_Achievement_Log();
        //                                    tblLog.LogId_pk = Guid.NewGuid();
        //                                    tblLog.AchieveId_fk = m.AchieveId_pk;
        //                                    tblLog.PlanStatus = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? Convert.ToInt16(eTypeApprove.Approve) : 0;
        //                                    tblLog.PlanStatusDate = cdt;
        //                                    tblLog.CreatedBy = MvcApplication.CUser.Id;
        //                                    tblLog.CreatedOn = DateTime.Now;
        //                                    db_.tbl_Achievement_Log.Add(tblLog);
        //                                    db_.SaveChanges();

        //                                    tblu.FinalApproved = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? Convert.ToInt16(eTypeApprove.Approve) : 0;
        //                                    tblu.FinalApprovedDate = cdt;
        //                                    tblu.FinalApprovedBy = MvcApplication.CUser.Id;

        //                                    tblu.IsLevel2Approve = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? true : false;
        //                                    tblu.Level2ApproveDt = cdt;
        //                                    tblu.Level2ApproveBy = MvcApplication.CUser.Id;
        //                                    results += db_.SaveChanges();
        //                                }
        //                            }
        //                            else if (m.AchieveId_pk != Guid.Empty && m.PanchayatId != null && m.VoId_fk != null && !string.IsNullOrWhiteSpace(m.Remark2))
        //                            {
        //                                //MRP Reject
        //                                var tblu = db_.tbl_AchievementPlan.Find(m.AchieveId_pk);
        //                                if (tblu != null && m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject))
        //                                {
        //                                    var cdt = DateTime.Now;
        //                                    tbl_Achievement_Log tblLog = new tbl_Achievement_Log();
        //                                    tblLog.LogId_pk = Guid.NewGuid();
        //                                    tblLog.AchieveId_fk = m.AchieveId_pk;
        //                                    tblLog.PlanStatus = m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) ? Convert.ToInt16(eTypeApprove.Reject) : 0;
        //                                    tblLog.PlanStatusDate = cdt;
        //                                    tblLog.CreatedBy = MvcApplication.CUser.Id;
        //                                    tblLog.CreatedOn = DateTime.Now;
        //                                    db_.tbl_Achievement_Log.Add(tblLog);
        //                                    db_.SaveChanges();

        //                                    tblu.FinalApproved = m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) ? Convert.ToInt16(eTypeApprove.Reject) : 0;
        //                                    tblu.FinalApprovedDate = cdt;
        //                                    tblu.FinalApprovedBy = MvcApplication.CUser.Id;

        //                                    tblu.Remark2 = m.Remark2.Trim();
        //                                    tblu.IsLevel2Reject = true;
        //                                    tblu.Level2RejectDt = cdt;
        //                                    tblu.Level2RejectBy = MvcApplication.CUser.Id;
        //                                    results_Reject += db_.SaveChanges();
        //                                }
        //                            }
        //                        }
        //                        // results += db_.SaveChanges();
        //                    }
        //                    else
        //                    {
        //                        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) + "\r\n", Data = null };
        //                        var resResponseerr = Json(response, JsonRequestBehavior.AllowGet);
        //                        resResponseerr.MaxJsonLength = int.MaxValue;
        //                        return resResponseerr;
        //                    }
        //                }
        //            }
        //        }
        //        response = results > 0 && results_Reject == 0 ? new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, Achievement Planning Approved Successfully ! \r\n", Data = null }
        //        : results == 0 && results_Reject > 0 ? new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = " Congratulations, Achievement Planning Rejected Successfully ! \r\n", Data = null }
        //        : new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.NotSubmitData), Data = null };
        //        var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
        //        resResponse3.MaxJsonLength = int.MaxValue;
        //        return resResponse3;
        //    }
        //    catch (Exception)
        //    {
        //        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
        //        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
        //        resResponse1.MaxJsonLength = int.MaxValue;
        //        return resResponse1;
        //    }
        //}
        //#endregion
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