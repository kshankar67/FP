using FP.Manager;
using FP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static FP.Manager.Enums;

namespace FP.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        FP_DBEntities db = new FP_DBEntities();
        JsonResponseData response = new JsonResponseData();
        int result = 0; bool CheckStatus = false;
        string MSG = string.Empty;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetPlanBFYList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_PlanBFYAddServiceList(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_ServiceBFYData", tbllist);
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
        public PartialViewResult AchvpopForm()
        {
            ServiceBFYModel model = new ServiceBFYModel();
            return PartialView("_BFYPOP", model);
        }
        public PartialViewResult AchvpopView(Guid BFYId, Guid FollowId_fk, Guid ServiceBFYId_pk)
        {
            ServiceBFYModel model = new ServiceBFYModel();
            model.ServiceBFYId_pk = ServiceBFYId_pk != Guid.Empty ? ServiceBFYId_pk : Guid.Empty;
            model.FollowId_fk = FollowId_fk;
            model.BFYId_fk = BFYId;
            return PartialView("_BFYPOPView", model);
        }
        public ActionResult GetBFYServiceView(FilterModel model)
        {
            bool IsCheck = false;
            try
            {
                var tbllist = SP_Model.SPCNRPServiceBFYView(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    var html = ConvertViewToString("_BFYPOPView", tbllist);
                    var res1 = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                    res1.MaxJsonLength = int.MaxValue;
                    return res1;
                }
                var res = Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.RecordNotFound) }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError) }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddServiceCNRP()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddServiceCNRP(ServiceBFYModel model)
        {
            int res = 0;
            try
            {
                FP_DBEntities _db = new FP_DBEntities();
                JsonResponseData response = new JsonResponseData();

                if (!ModelState.IsValid)
                {
                    var d = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired);
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
                if (!CommonModel.GetValidTillMonth(model.ServiceMonthId.Value, model.ServiceYearId.Value))
                {
                    var d = Enums.GetEnumDescription(Enums.eReturnReg.VaildMonth);
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.VaildMonth), Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
                //if (_db.tbl_BFYService.Any(x => x.ServiceMonthId == model.ServiceMonthId && x.ServiceYearId == model.ServiceYearId && (x.ServiceBFYId_pk != model.ServiceBFYId_pk && model.ServiceBFYId_pk == Guid.Empty)
                // && x.BFYId_fk == model.BFYId_fk))
                //{
                //    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.Already), Data = null };
                //    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                //    resResponse3.MaxJsonLength = int.MaxValue;
                //    return resResponse3;
                //}
                if (model != null)
                {
                    var dt = DateTime.Now;
                    var tbl = model.ServiceBFYId_pk != Guid.Empty ? _db.tbl_BFYService.Find(model.ServiceBFYId_pk) : new tbl_BFYService();
                    if (tbl != null)
                    {
                        tbl.IsPeerPresent = model.IsPeerPresent;
                        tbl.IsFollowUpHV = model.IsFollowUpHV;
                        if (model.IsContraception == true)
                        {
                            tbl.IsContraception = model.IsContraception;
                            tbl.ContraceptionId_fk = (model.IsContraception == true && model.ContraceptionId_fk != null) ? model.ContraceptionId_fk : null;
                            tbl.ContraceptionOther = (model.IsContraception == true && model.ContraceptionId_fk == 4) ? model.ContraceptionOther : null;
                            tbl.UseMethodId_fk = (model.IsContraception == true && model.ContraceptionId_fk != null && model.UseMethodId_fk != null) ? model.UseMethodId_fk : null;

                            if (model.ContraceptionId_fk != 3)
                            {
                                tbl.Isservice = model.ContraceptionId_fk != 3 ? model.Isservice == true : false;
                                tbl.ServiceProvider = model.Isservice == true ? model.ServiceProvider : null;
                                tbl.ServiceRevcDt = model.Isservice == true ? model.ServiceRevcDt : null;
                                tbl.Location = model.Isservice == true ? model.Location : null;
                                tbl.AashaName = model.Isservice == true ? model.AashaName : null;
                            }
                        }
                        else
                        {
                            tbl.IsContraception = null;
                            tbl.ContraceptionId_fk = null;
                            tbl.ContraceptionOther = null;
                            tbl.UseMethodId_fk = null;
                            tbl.Location = null;
                            tbl.Isservice = null;
                            tbl.ServiceProvider = null;
                            tbl.ServiceRevcDt = null;
                        }
                        tbl.IsActive = true;
                        if (model.ServiceBFYId_pk == Guid.Empty)
                        {
                            tbl.ServiceBFYId_pk = Guid.NewGuid();
                            tbl.ServiceYearId = model.ServiceYearId;
                            tbl.ServiceMonthId = model.ServiceMonthId;
                            var bfytbl = _db.TBL_Beneficiary.Where(x => x.Beneficiary_Id_pk == model.BFYId_fk )?.FirstOrDefault();
                            tbl.BFYId_fk = model.BFYId_fk;
                            tbl.DistrictId_fk = bfytbl.DistrictId_fk;
                            tbl.BlockId_fk = bfytbl.BlockId_fk;
                            tbl.ClusterId_fk = bfytbl.CLFId_fk;
                            tbl.PanchayatId_fk = bfytbl.PanchayatId_fk;
                            tbl.VoId_fk = bfytbl.VillageOId_fk;

                            var FollowId = _db.tbl_BFYFollowup.Where(x => x.BFYID_fk == model.BFYId_fk && x.FMonth == model.ServiceMonthId && x.FYear == model.ServiceYearId)
                                .OrderByDescending(x => x.CreatedOn).Take(1)?.FirstOrDefault()?.FollowupID_pk;
                            tbl.FollowId_fk = FollowId;

                            tbl.CreatedBy = MvcApplication.CUser.Id;
                            tbl.CreatedOn = dt;
                            if (model.IsContraception == true)
                            {
                                if (model.ContraceptionId_fk != 3 && model.ContraceptionId_fk != 4)
                                {
                                    if (model.UseMethodId_fk > 0)
                                    {
                                        if (model.UseMethodId_fk == 5 || model.UseMethodId_fk == 6
                                            || model.UseMethodId_fk == 7 || model.UseMethodId_fk == 8)
                                        {
                                            tbl.CMEligible = CommonModel.GetClaimMobilization().CM;
                                            tbl.CNRPEligible = CommonModel.GetClaimMobilization().CNRP;
                                        }
                                    }
                                }
                            }
                            db.tbl_BFYService.Add(tbl);
                            res = db.SaveChanges();

                            TBL_Beneficiary tblBFY = _db.TBL_Beneficiary.Find(model.BFYId_fk);
                            if (model.IsContraception == true && model.ContraceptionId_fk > 0)
                            {
                                tblBFY.ServiceBFYId_fk = tbl.ServiceBFYId_pk;
                                tblBFY.ServicedBy = MvcApplication.CUser.Id;
                                tblBFY.ServicedOn = dt;
                                tblBFY.ServiceBFYDate = model.Isservice == true ? model.ServiceRevcDt : null;
                                tblBFY.Q15 = model.ContraceptionId_fk;
                                tblBFY.Q16 = model.ContraceptionId_fk == 1 ? model.UseMethodId_fk : null;
                                tblBFY.Q17 = model.ContraceptionId_fk == 2 ? model.UseMethodId_fk : null;
                                tblBFY.Q18 = model.ContraceptionId_fk == 4 ? model.ContraceptionOther : null;
                                res += _db.SaveChanges();
                            }
                        }
                        else
                        {
                            tbl.UpdatedBy = MvcApplication.CUser.Id;
                            tbl.UpdatedOn = DateTime.Now;
                            res += _db.SaveChanges();
                        }
                    }
                    if (res > 0)
                    {
                        var AchModel = new ServiceBFYModel();
                        var Achvmtpopmodal = ConvertViewToString("_BFYPOP", AchModel);
                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.Insert), Data = Achvmtpopmodal };
                        var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse3.MaxJsonLength = int.MaxValue;
                        return resResponse3;
                    }
                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.Error), Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }

            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
            var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
            resResponse4.MaxJsonLength = int.MaxValue;
            return resResponse4;
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