using FP.Manager;
using FP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static FP.Manager.Enums;

namespace FP.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        // GET: Approve
        public ActionResult Index()
        {
            return View();
        }

        #region CNR Monthly Incentive 1st Monthly Payment Level Approved Planning (MRP Level)
        public ActionResult LevelOnePayment()
        {
            AchvPlanModel model = new AchvPlanModel();
            return View(model);
        }
        public ActionResult GetAchApproveOnePlanList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                model.TypeLayer = (int)Enums.eTypeLayer.MRP;
                var tbllist = SP_Model.SP_AchvPlanApprove(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_LevelOnePaymentData", tbllist);
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
        //PostLevelOnePayment
        [HttpPost]
        public ActionResult PostLevelOnePayment(AchvPlanModel model)
        {
            var results = 0; var results_Reject = 0;
            FP_DBEntities db_ = new FP_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                var resAchvPlanlist = this.Request.Unvalidated.Form["AVPlanModel"];

                if (resAchvPlanlist != null)
                {
                    var mlist = JsonConvert.DeserializeObject<List<AVPlanModel>>(resAchvPlanlist);
                    if (mlist != null && mlist.Any())
                    {
                        List<tbl_AchievementPlan> tbl_list = new List<tbl_AchievementPlan>();
                        if (model.DistrictId_fk != null && model.BlockId_fk != null && model.ClusterId_fk != null
                           && model.PlanYear != null && model.PlanMonth != null)
                        {
                            foreach (var m in mlist)
                            {
                                if (m.AchieveId_pk != Guid.Empty &&
                                    m.DistrictId != null && m.BlockId != null && m.ClusterId != null &&
                                    m.PanchayatId != null && m.VoId_fk != null)
                                {
                                    //MRP Approve
                                    var tblu = db_.tbl_AchievementPlan.Find(m.AchieveId_pk);
                                    if (tblu != null)
                                    {
                                        var cdt = DateTime.Now;
                                        tbl_Achievement_Log tblLog = new tbl_Achievement_Log();
                                        tblLog.LogId_pk = Guid.NewGuid();
                                        tblLog.AchieveId_fk = m.AchieveId_pk;
                                        tblLog.PlanStatusDate = cdt;
                                        tblLog.CreatedBy = MvcApplication.CUser.Id;
                                        tblLog.CreatedOn = DateTime.Now;

                                        if (m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve))
                                        {
                                            if (tblu.IsLevel1Approve != true)
                                            {
                                                tblLog.PlanStatus = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? Convert.ToInt16(eTypeApprove.Approve) : 0;
                                                db_.tbl_Achievement_Log.Add(tblLog);
                                                db_.SaveChanges();

                                                /*Reject Status set NULL Started*/
                                                tblu.Remark1 = null;
                                                tblu.IsLevel1Reject = null;
                                                tblu.Level1RejectDt = null;
                                                tblu.Level1RejectBy = null;
                                                /*Reject Status set NULL end*/

                                                tblu.FinalApproved = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? Convert.ToInt16(eTypeApprove.Approve) : 0;
                                                tblu.FinalApprovedDate = cdt;
                                                tblu.FinalApprovedBy = MvcApplication.CUser.Id;

                                                tblu.Remark1 = !(string.IsNullOrWhiteSpace(m.Remark1)) ? m.Remark1.Trim() : null;
                                                tblu.IsLevel1Approve = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? true : false;
                                                tblu.Level1ApproveDt = cdt;
                                                tblu.Level1ApproveBy = MvcApplication.CUser.Id;

                                                tblu.CLFValidation = model.CLFValidation;
                                                tblu.CLFValidationDate = model.CLFValidationDate;
                                                tblu.CLFLeadersPresident = model.CLFLeadersPresident;
                                                tblu.CLFLeadersSecretary = model.CLFLeadersSecretary;
                                                tblu.CLFLeadersTreasurer = model.CLFLeadersTreasurer;
                                                results += db_.SaveChanges();
                                            }
                                        }
                                        else if (m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) && !string.IsNullOrWhiteSpace(m.Remark1))
                                        {
                                            //MRP Reject
                                            if (m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject))
                                            {
                                                tblLog.PlanStatus = m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) ? Convert.ToInt16(eTypeApprove.Reject) : 0;
                                                db_.tbl_Achievement_Log.Add(tblLog);
                                                db_.SaveChanges();

                                                tblu.FinalApproved = m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) ? Convert.ToInt16(eTypeApprove.Reject) : 0;
                                                tblu.FinalApprovedDate = cdt;
                                                tblu.FinalApprovedBy = MvcApplication.CUser.Id;
                                                /*Approved Status set NULL Started*/
                                                tblu.IsLevel1Approve = null;
                                                tblu.Level1ApproveBy = null;
                                                tblu.Level1ApproveDt = null;
                                                /*Approved Status set NULL end*/

                                                tblu.Remark1 = m.Remark1.Trim();
                                                tblu.IsLevel1Reject = true;
                                                tblu.Level1RejectDt = cdt;
                                                tblu.Level1RejectBy = MvcApplication.CUser.Id;

                                                tblu.CLFValidation = model.CLFValidation;
                                                tblu.CLFValidationDate = model.CLFValidationDate;
                                                tblu.CLFLeadersPresident = model.CLFLeadersPresident;
                                                tblu.CLFLeadersSecretary = model.CLFLeadersSecretary;
                                                tblu.CLFLeadersTreasurer = model.CLFLeadersTreasurer;

                                                results_Reject += db_.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                            // results += db_.SaveChanges();
                            var groups = mlist.GroupBy(x => x.UserId);
                            if (groups != null && (results > 0 || results_Reject > 0))
                            {
                                foreach (var group in groups)
                                {
                                    var appovlist = group.Where(x => x.PlanApprove == Convert.ToInt16(Enums.eTypeApprove.Approve)).Select(x => x.AchieveId_pk).ToList();
                                    var rejectlist = group.Where(x => x.PlanApprove == Convert.ToInt16(Enums.eTypeApprove.Reject)).Select(x => x.AchieveId_pk).ToList();
                                    tbl_PaymentHistory tblpay = new tbl_PaymentHistory();
                                    tblpay.PaymentHistoryId_pk = Guid.NewGuid();
                                    tblpay.ApprovedAchvId = string.Join(",", appovlist).ToUpper();
                                    tblpay.RejectedAchvId = string.Join(",", rejectlist).ToUpper();
                                    tblpay.NoofApproved = appovlist.Count;
                                    tblpay.NoofRejected = rejectlist.Count;
                                    tblpay.TypeofPayment = Enums.GetEnumDescription(eTypeOfPayment.MonthlyCNRP);
                                    tblpay.VerifyUserTypeId = Guid.Parse(MvcApplication.CUser.RoleId);
                                    tblpay.TargetUserTypeId = Guid.Parse(db_.AspNetRoles.First(x => x.Name == CommonModel.RoleNameCont.CNRP).Id);
                                    tblpay.TargetUserId = group.Key;
                                    tblpay.ClaimAmount = CommonModel.GetClaimApprove(appovlist.Count, CommonModel.RoleNameCont.CNRP);
                                    tblpay.ApprovedAmount = CommonModel.GetClaimApprove(rejectlist.Count, CommonModel.RoleNameCont.CNRP);
                                    tblpay.PayMonth = model.PlanMonth;
                                    tblpay.PayYear = model.PlanYear;
                                    tblpay.IsActive = true;
                                    tblpay.CreatedBy = MvcApplication.CUser.Id;
                                    tblpay.UpdatedBy = MvcApplication.CUser.Id;
                                    tblpay.CreatedOn = tblpay.UpdatedOn = DateTime.Now;
                                    db_.tbl_PaymentHistory.Add(tblpay);
                                }
                                db_.SaveChanges();
                            }
                            var empId = mlist.First().empId;
                            var emp = db_.TBL_Emp.FirstOrDefault(x => x.EmpID_pk == empId);
                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = $"Successfully, Validated for {emp?.EmpName}!", Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }
                        else
                        {
                            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) + "\r\n", Data = null };
                            var resResponseerr = Json(response, JsonRequestBehavior.AllowGet);
                            resResponseerr.MaxJsonLength = int.MaxValue;
                            return resResponseerr;
                        }
                    }
                    else
                    {
                        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Please selected any activities!", Data = null };
                        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse1.MaxJsonLength = int.MaxValue;
                        return resResponse1;
                    }
                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Issue in data submition!", Data = null };
                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse1.MaxJsonLength = int.MaxValue;
                    return resResponse1;
                }
            }
            catch (Exception ex)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
        }




        #endregion

        #region CNR Monthly Incentive 2nd Monthly Payment Level Approved Planning (CC Level Two)
        public ActionResult LevelTwoPayment()
        {
            AchvPlanModel model = new AchvPlanModel();
            return View(model);
        }
        public ActionResult GetAchApproveTwoPlanList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                model.TypeLayer = (int)Enums.eTypeLayer.CC;
                var tbllist = SP_Model.SP_AchvPlanApprove(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_LevelTwoPaymentData", tbllist);
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
        [HttpPost]
        public ActionResult PostLevelTwoPayment(AchvPlanModel model)
        {
            var results = 0; var results_Reject = 0;
            FP_DBEntities db_ = new FP_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                var resAchvPlanlist = this.Request.Unvalidated.Form["AVPlanModel"];
                if (resAchvPlanlist != null)
                {
                    var mlist = JsonConvert.DeserializeObject<List<AVPlanModel>>(resAchvPlanlist);
                    if (mlist != null && mlist.Any())
                    {
                        List<tbl_AchievementPlan> tbl_list = new List<tbl_AchievementPlan>();
                        if (model.DistrictId_fk != null && model.BlockId_fk != null && model.ClusterId_fk != null
                           && model.PlanYear != null && model.PlanMonth != null)
                        {
                            foreach (var m in mlist)
                            {
                                if (m.AchieveId_pk != Guid.Empty &&
                                    m.DistrictId != null && m.BlockId != null && m.ClusterId != null &&
                                    m.PanchayatId != null && m.VoId_fk != null)
                                {
                                    //MRP Approve
                                    var tblu = db_.tbl_AchievementPlan.Find(m.AchieveId_pk);
                                    if (tblu != null)
                                    {
                                        var cdt = DateTime.Now;
                                        tbl_Achievement_Log tblLog = new tbl_Achievement_Log();
                                        tblLog.LogId_pk = Guid.NewGuid();
                                        tblLog.AchieveId_fk = m.AchieveId_pk;
                                        tblLog.PlanStatusDate = cdt;
                                        tblLog.CreatedBy = MvcApplication.CUser.Id;
                                        tblLog.CreatedOn = DateTime.Now;

                                        if (m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve))
                                        {
                                            if (tblu.IsLevel2Approve != true)
                                            {
                                                tblLog.PlanStatus = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? Convert.ToInt16(eTypeApprove.Approve) : 0;
                                                db_.tbl_Achievement_Log.Add(tblLog);
                                                db_.SaveChanges();

                                                /*Reject Status set NULL Started*/
                                                tblu.Remark2 = null;
                                                tblu.IsLevel2Reject = null;
                                                tblu.Level2RejectDt = null;
                                                tblu.Level2RejectBy = null;
                                                /*Reject Status set NULL end*/

                                                tblu.FinalApproved = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? Convert.ToInt16(eTypeApprove.Approve) : 0;
                                                tblu.FinalApprovedDate = cdt;
                                                tblu.FinalApprovedBy = MvcApplication.CUser.Id;

                                                tblu.Remark2 = !(string.IsNullOrWhiteSpace(m.Remark1)) ? m.Remark1.Trim() : null;
                                                tblu.IsLevel2Approve = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? true : false;
                                                tblu.Level2ApproveDt = cdt;
                                                tblu.Level2ApproveBy = MvcApplication.CUser.Id;
                                                results += db_.SaveChanges();
                                            }
                                        }
                                        else if (m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) && !string.IsNullOrWhiteSpace(m.Remark1))
                                        {
                                            //MRP Reject
                                            if (m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject))
                                            {
                                                tblLog.PlanStatus = m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) ? Convert.ToInt16(eTypeApprove.Reject) : 0;
                                                db_.tbl_Achievement_Log.Add(tblLog);
                                                db_.SaveChanges();

                                                tblu.FinalApproved = m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) ? Convert.ToInt16(eTypeApprove.Reject) : 0;
                                                tblu.FinalApprovedDate = cdt;
                                                tblu.FinalApprovedBy = MvcApplication.CUser.Id;
                                                /*Approved Status set NULL Started*/
                                                tblu.IsLevel2Approve = null;
                                                tblu.Level2ApproveBy = null;
                                                tblu.Level2ApproveDt = null;
                                                /*Approved Status set NULL end*/

                                                tblu.Remark2 = m.Remark1.Trim();
                                                tblu.IsLevel2Reject = true;
                                                tblu.Level2RejectDt = cdt;
                                                tblu.Level2RejectBy = MvcApplication.CUser.Id;
                                                results_Reject += db_.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                            // results += db_.SaveChanges();
                            var groups = mlist.GroupBy(x => x.UserId);
                            if (groups != null && (results > 0 || results_Reject > 0))
                            {
                                foreach (var group in groups)
                                {
                                    var appovlist = group.Where(x => x.PlanApprove == Convert.ToInt16(Enums.eTypeApprove.Approve)).Select(x => x.AchieveId_pk).ToList();
                                    var rejectlist = group.Where(x => x.PlanApprove == Convert.ToInt16(Enums.eTypeApprove.Reject)).Select(x => x.AchieveId_pk).ToList();
                                    tbl_PaymentHistory tblpay = new tbl_PaymentHistory();
                                    tblpay.PaymentHistoryId_pk = Guid.NewGuid();
                                    tblpay.ApprovedAchvId = string.Join(",", appovlist).ToUpper();
                                    tblpay.RejectedAchvId = string.Join(",", rejectlist).ToUpper();
                                    tblpay.NoofApproved = appovlist.Count;
                                    tblpay.NoofRejected = rejectlist.Count;
                                    tblpay.TypeofPayment = Enums.GetEnumDescription(eTypeOfPayment.MonthlyCNRP);
                                    tblpay.VerifyUserTypeId = Guid.Parse(MvcApplication.CUser.RoleId);
                                    tblpay.TargetUserTypeId = Guid.Parse(db_.AspNetRoles.First(x => x.Name == CommonModel.RoleNameCont.CNRP).Id);
                                    tblpay.TargetUserId = group.Key;
                                    tblpay.ClaimAmount = CommonModel.GetClaimApprove(appovlist.Count, CommonModel.RoleNameCont.CNRP);
                                    tblpay.ApprovedAmount = CommonModel.GetClaimApprove(rejectlist.Count, CommonModel.RoleNameCont.CNRP);
                                    tblpay.PayMonth = model.PlanMonth;
                                    tblpay.PayYear = model.PlanYear;
                                    tblpay.IsActive = true;
                                    tblpay.CreatedBy = MvcApplication.CUser.Id;
                                    tblpay.UpdatedBy = MvcApplication.CUser.Id;
                                    tblpay.CreatedOn = tblpay.UpdatedOn = DateTime.Now;
                                    db_.tbl_PaymentHistory.Add(tblpay);
                                }
                                db_.SaveChanges();
                            }
                            var empId = mlist.First().empId;
                            var emp = db_.TBL_Emp.FirstOrDefault(x => x.EmpID_pk == empId);
                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = $"Successfully, Checked-In for {emp?.EmpName}!", Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }
                        else
                        {
                            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) + "\r\n", Data = null };
                            var resResponseerr = Json(response, JsonRequestBehavior.AllowGet);
                            resResponseerr.MaxJsonLength = int.MaxValue;
                            return resResponseerr;
                        }
                    }
                    else
                    {
                        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Please selected any activities!", Data = null };
                        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse1.MaxJsonLength = int.MaxValue;
                        return resResponse1;
                    }
                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Issue in data submition!", Data = null };
                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse1.MaxJsonLength = int.MaxValue;
                    return resResponse1;
                }
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
        }
        #endregion

        #region CNR Monthly Incentive 3th Monthly Payment Level Approved Planning (BPM Level Two)
        public ActionResult LevelThreePayment()
        {
            AchvPlanModel model = new AchvPlanModel();
            return View(model);
        }
        public ActionResult GetAchApproveThreePlanList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                model.TypeLayer = (int)Enums.eTypeLayer.BPIU;
                var tbllist = SP_Model.SP_AchvPlanApprove(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_LevelThreePaymentData", tbllist);
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
        [HttpPost]
        public ActionResult PostLevelThreePayment(AchvPlanModel model)
        {
            var results = 0; var results_Reject = 0;
            FP_DBEntities db_ = new FP_DBEntities();
            JsonResponseData response = new JsonResponseData();
            var cdt = DateTime.Now;
            try
            {
                var resAchvPlanlist = this.Request.Unvalidated.Form["AVPlanModel"];
                if (resAchvPlanlist != null)
                {
                    var mlist = JsonConvert.DeserializeObject<List<AVPlanModel>>(resAchvPlanlist);
                    if (mlist != null && mlist.Any())
                    {
                        List<tbl_AchievementPlan> tbl_list = new List<tbl_AchievementPlan>();
                        if (model.DistrictId_fk != null && model.BlockId_fk != null
                           && model.PlanYear != null && model.PlanMonth != null)
                        {
                            foreach (var m in mlist)
                            {
                                if (m.AchieveId_pk != Guid.Empty &&
                                    m.DistrictId != null && m.BlockId != null && m.ClusterId != null &&
                                    m.PanchayatId != null && m.VoId_fk != null)
                                {
                                    //MRP Approve
                                    var tblu = db_.tbl_AchievementPlan.Find(m.AchieveId_pk);
                                    if (tblu != null)
                                    {

                                        tbl_Achievement_Log tblLog = new tbl_Achievement_Log();
                                        tblLog.LogId_pk = Guid.NewGuid();
                                        tblLog.AchieveId_fk = m.AchieveId_pk;
                                        tblLog.PlanStatusDate = cdt;
                                        tblLog.CreatedBy = MvcApplication.CUser.Id;
                                        tblLog.CreatedOn = DateTime.Now;

                                        if (m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve))
                                        {
                                            if (tblu.IsLevel3Approve != true)
                                            {
                                                tblLog.PlanStatus = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? Convert.ToInt16(eTypeApprove.Approve) : 0;
                                                db_.tbl_Achievement_Log.Add(tblLog);
                                                db_.SaveChanges();

                                                /*Reject Status set NULL Started*/
                                                tblu.Remark3 = null;
                                                tblu.IsLevel3Reject = null;
                                                tblu.Level3RejectDt = null;
                                                tblu.Level3RejectBy = null;
                                                /*Reject Status set NULL end*/

                                                tblu.FinalApproved = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? Convert.ToInt16(eTypeApprove.Approve) : 0;
                                                tblu.FinalApprovedDate = cdt;
                                                tblu.FinalApprovedBy = MvcApplication.CUser.Id;

                                                tblu.Remark3 = !(string.IsNullOrWhiteSpace(m.Remark1)) ? m.Remark1.Trim() : null;
                                                tblu.IsLevel3Approve = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? true : false;
                                                tblu.Level3ApproveDt = cdt;
                                                tblu.Level3ApproveBy = MvcApplication.CUser.Id;
                                                tblu.ClaimedAmount = CommonModel.GetClaimApprove(1, CommonModel.RoleNameCont.CNRP);
                                                results += db_.SaveChanges();
                                            }
                                        }
                                        else if (m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) && !string.IsNullOrWhiteSpace(m.Remark1))
                                        {
                                            //MRP Reject
                                            if (m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject))
                                            {
                                                tblLog.PlanStatus = m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) ? Convert.ToInt16(eTypeApprove.Reject) : 0;
                                                db_.tbl_Achievement_Log.Add(tblLog);
                                                db_.SaveChanges();

                                                tblu.FinalApproved = m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) ? Convert.ToInt16(eTypeApprove.Reject) : 0;
                                                tblu.FinalApprovedDate = cdt;
                                                tblu.FinalApprovedBy = MvcApplication.CUser.Id;

                                                /*Approved Status set NULL Started*/
                                                tblu.IsLevel3Approve = null;
                                                tblu.Level3ApproveBy = null;
                                                tblu.Level3ApproveDt = null;
                                                /*Approved Status set NULL end*/

                                                tblu.Remark3 = m.Remark1.Trim();
                                                tblu.IsLevel3Reject = true;
                                                tblu.Level3RejectDt = cdt;
                                                tblu.Level3RejectBy = MvcApplication.CUser.Id;
                                                results_Reject += db_.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                            // results += db_.SaveChanges();
                            var groups = mlist.GroupBy(x => x.UserId);
                            if (groups != null && (results > 0 || results_Reject > 0))
                            {
                                foreach (var group in groups)
                                {
                                    var appovlist = group.Where(x => x.PlanApprove == Convert.ToInt16(Enums.eTypeApprove.Approve)).Select(x => x.AchieveId_pk).ToList();
                                    var rejectlist = group.Where(x => x.PlanApprove == Convert.ToInt16(Enums.eTypeApprove.Reject)).Select(x => x.AchieveId_pk).ToList();
                                    tbl_PaymentHistory tblpay = new tbl_PaymentHistory();
                                    tblpay.PaymentHistoryId_pk = Guid.NewGuid();
                                    tblpay.ApprovedAchvId = string.Join(",", appovlist).ToUpper();
                                    tblpay.RejectedAchvId = string.Join(",", rejectlist).ToUpper();
                                    tblpay.NoofApproved = appovlist.Count;
                                    tblpay.NoofRejected = rejectlist.Count;
                                    tblpay.TypeofPayment = Enums.GetEnumDescription(eTypeOfPayment.MonthlyCNRP);
                                    tblpay.VerifyUserTypeId = Guid.Parse(MvcApplication.CUser.RoleId);
                                    tblpay.TargetUserTypeId = Guid.Parse(db_.AspNetRoles.First(x => x.Name == CommonModel.RoleNameCont.CNRP).Id);
                                    tblpay.TargetUserId = group.Key;
                                    tblpay.ClaimAmount = CommonModel.GetClaimApprove(appovlist.Count, CommonModel.RoleNameCont.CNRP);
                                    tblpay.ApprovedAmount = CommonModel.GetClaimApprove(rejectlist.Count, CommonModel.RoleNameCont.CNRP);
                                    tblpay.PayMonth = model.PlanMonth;
                                    tblpay.PayYear = model.PlanYear;
                                    tblpay.IsActive = true;
                                    tblpay.CreatedBy = MvcApplication.CUser.Id;
                                    tblpay.UpdatedBy = MvcApplication.CUser.Id;
                                    tblpay.CreatedOn = cdt;
                                    tblpay.UpdatedOn = cdt;
                                    db_.tbl_PaymentHistory.Add(tblpay);
                                }
                                db_.SaveChanges();
                            }
                            var empId = mlist.First().empId;
                            var emp = db_.TBL_Emp.FirstOrDefault(x => x.EmpID_pk == empId);
                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = $"Successfully, Recommended for {emp?.EmpName}!", Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }
                        else
                        {
                            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) + "\r\n", Data = null };
                            var resResponseerr = Json(response, JsonRequestBehavior.AllowGet);
                            resResponseerr.MaxJsonLength = int.MaxValue;
                            return resResponseerr;
                        }
                    }
                    else
                    {
                        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Please selected any activities!", Data = null };
                        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse1.MaxJsonLength = int.MaxValue;
                        return resResponse1;
                    }
                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Issue in data submition!", Data = null };
                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse1.MaxJsonLength = int.MaxValue;
                    return resResponse1;
                }
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
        }
        #endregion


        public ActionResult GetAchievementDetailsByUser(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_GetAchvPlanApproveChild(model.UserID, model.EmpId, model.MonthId, model.YearId, model.TypeLayer);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = "";
                switch (model.TypeLayer)
                {
                    case (int)Enums.eTypeLayer.MRP:
                        html = ConvertViewToString("_LevelOneAchvDetails", tbllist);
                        break;
                    case (int)Enums.eTypeLayer.CC:
                        html = ConvertViewToString("_LevelTwoAchvDetails", tbllist);
                        break;
                    case (int)Enums.eTypeLayer.BPIU:
                        html = ConvertViewToString("_LevelThreeAchvDetails", tbllist);
                        break;
                    default:
                        html = ConvertViewToString("_LevelOneAchvDetails", tbllist);
                        break;
                }
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


        public ActionResult AppPlanList()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetAppPlanList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_AppPlanList(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_AppPlanData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet);
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