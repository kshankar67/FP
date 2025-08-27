using FP.Manager;
using FP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FP.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ServiceBFYList()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetServiceBFYList(FilterModel model)
        {
            bool IsCheck = false;
            try
            {
                DataTable tbllist = SP_Model.SPBFYServiceList(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_ServiceBFYListData", tbllist);
                var res1 = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res1.MaxJsonLength = int.MaxValue;
                return res1;
                //var res = Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.RecordNotFound) }, JsonRequestBehavior.AllowGet);
                //res.MaxJsonLength = int.MaxValue;
                //return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError) }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PaymentSummaryList()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetPaymentSummaryList(FilterModel model)
        {
            bool IsCheck = false;
            try
            {
                DataTable tbllist = SP_Model.SPCNRPPaymentSummary(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_PaymentSmyData", tbllist);
                var res1 = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res1.MaxJsonLength = int.MaxValue;
                return res1;
                //var res = Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.RecordNotFound) }, JsonRequestBehavior.AllowGet);
                //res.MaxJsonLength = int.MaxValue;
                //return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError) }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPaymentReport(FilterModel model)
        {
            bool IsCheck = false;
            try
            {
                DataTable tbllist = SP_Model.SP_GetPaymentReport(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_PaymentReportData", tbllist);
                var res1 = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res1.MaxJsonLength = int.MaxValue;
                return res1;
                //var res = Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.RecordNotFound) }, JsonRequestBehavior.AllowGet);
                //res.MaxJsonLength = int.MaxValue;
                //return res;
            }
            catch (Exception ex)
            {
                var errorMessage = JsonConvert.SerializeObject(ex);
                return Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError), Error = errorMessage }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PrapatraOne()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetPrapatraOne(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_BFYPrapatra_One(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_PrapatraOneData", tbllist);
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
        public ActionResult LetterTwo()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetLetterTwo(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SpLetterTwo(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_LetterTwoData", tbllist);
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
        public ActionResult GetDistrictGraph(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                DataSet ds = new DataSet();
                ds = SP_Model.SpDistrictGraph(model);
                if (ds.Tables.Count > 0)
                {
                    IsCheck = true;
                }
                var dslist = JsonConvert.SerializeObject(ds);
                //var html = ConvertViewToString("_BFYData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = dslist }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AchBFYList()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetAchBFYList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_PlanAchBFYList(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_AchBFYData", tbllist);
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

        public ActionResult AchivmentByParity()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public JsonResult GetAchivmentByParity(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_AchivmentByParity(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var res = Json(new { IsSuccess = IsCheck, Data = JsonConvert.SerializeObject(tbllist) }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Prapatra2()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetPrapatra2(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_Prapatra2(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_Prapatra2", tbllist);
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


        public ActionResult Prapatra8()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetPrapatra8(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_Prapatra8(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_Prapatra8", tbllist);
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