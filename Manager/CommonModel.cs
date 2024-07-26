using FP.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SubSonic.Schema;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Reflection;
using SubSonic.Extensions;
using System.ComponentModel.DataAnnotations;
using FP.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FP.Manager
{
    public static class CommonModel
    {
        private static FP_DBEntities dbe = new FP_DBEntities();
        //private static FP_DBEntities dbe;
        //public CommonModel()
        //{
        //    if (dbe==null)
        //    {
        //        dbe = new FP_DBEntities();
        //    }
        //}

        #region BaseUrl
        public static string GetBaseUrl()
        {
            var str = HttpContext.Current.Request.Url.Host;
            //return str;
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            string host = HttpContext.Current.Request.Url.Host;
            if (host == "localhost")
            {
                host = HttpContext.Current.Request.Url.Authority;
                return HttpContext.Current.Request.Url.Scheme + "://" + host;
            }
            //return urlHelper.Content("~/");
            return HttpContext.Current.Request.Url.Scheme + "://" + str;
        }
        public static string GetWebUrl()
        {
            return ConfigurationManager.AppSettings["WebUrl"];
        }

        public static bool IsEmailConfiguredToLive()
        {
            var isLive = Convert.ToBoolean(ConfigurationManager.AppSettings["IsEmailSetLive"].ToString());
            return isLive;
        }
        public static string GetLocalEmailAddress()
        {
            var emailAddr = ConfigurationManager.AppSettings["LocalEmailAddress"].ToString();
            return emailAddr;
        }

        public static string GetFileUrl(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
                return CommonModel.GetBaseUrl() + filePath.ToString().Replace("~", "");
            else
                return string.Empty;
        }

        public static string GetMultipleFileUrlFromComma(string filePaths)
        {
            //string filePath = string.Empty;
            //var filePathArray = filePaths.Split(',');
            List<string> filePathList = new List<string>();
            foreach (var path in filePaths.Split(','))
            {
                if (!string.IsNullOrEmpty(path))
                {
                    //return CommonModel.GetBaseUrl() + path.ToString().Replace("~", "");
                    filePathList.Add(CommonModel.GetBaseUrl() + path.Trim().ToString().Replace("~", ""));
                }
                //else
                //    return string.Empty;

            }
            //filePathList=.Join(',');
            return string.Join(",", filePathList);
        }

        public static string GetHeaderUSLogo(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
                return filePath.ToString().Replace("src=\"..//Content/images/USAID_Template.png\"", "src=\"" + CommonModel.GetWebUrl() + "/Content/images/USAID_Template.png\"");
            else
                return string.Empty;
        }
        public static string GetHeaderCareLogo(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
                return filePath.ToString().Replace("src=\"..//Content/images/Care_Template.png\"", "src=\"" + CommonModel.GetWebUrl() + "/Content/images/Care_Template.png\"");
            else
                return string.Empty;
        }
        #endregion

        #region Get User Role 
        public static string GetUserRoleLogin()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.IsInRole("Admin") || HttpContext.Current.User.IsInRole("State"))
                {
                    return "all";
                }
                else if (HttpContext.Current.User.IsInRole("CNRP"))
                {
                    return "CNRP";
                }
                else if (HttpContext.Current.User.IsInRole("CM"))
                {
                    return "CM";
                }
                else if (HttpContext.Current.User.IsInRole("BPIU"))
                {
                    return "BPIU";
                }
                else if (HttpContext.Current.User.IsInRole("BPMU"))
                {
                    return "BPMU";
                }
                else if (HttpContext.Current.User.IsInRole("Viewer"))
                {
                    return "Viewer";
                }
                else if (HttpContext.Current.User.IsInRole("District"))
                {
                    return "District";
                }
            }
            return "all";
        }
        public static string GetUserRoleConsultantDist()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.IsInRole("Consultant"))
                {
                    return "Consultant";
                }
            }
            return "All";
        }
        public static string GetUserRole()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //if (HttpContext.Current.User.IsInRole("Admin"))
                //{
                //    return "Admin";
                //}
                //else if (HttpContext.Current.User.IsInRole("State"))
                //{
                //    return "State";
                //}
                if (HttpContext.Current.User.IsInRole("Teacher"))
                {
                    return "Teacher";
                }
                //else if (HttpContext.Current.User.IsInRole("PCI_Representative"))
                //{
                //    return "PCI_Representative";
                //}
                //else if (HttpContext.Current.User.IsInRole("Viewer"))
                //{
                //    return "Viewer";
                //}
            }
            return "All";
        }
        //public static UserViewModel Get_IsRole()
        //{
        //    var un = dbe.AspNetRoles.Where(x=>x.Name == CUser.Role);
        //    return role;
        //}



        //public static class SessionLog
        //{
        //    public static int EmployeeId { get { return MvcApplication.Emplog().EmpId_pk; } }
        //    public static int SchoolId_fk { get { return MvcApplication.Emplog().SchoolId_fk == null ? 0 : MvcApplication.Emplog().SchoolId_fk.Value; } }
        //    public static int DistrictId { get { return MvcApplication.Emplog().DistrictId_fk ?? 0; } }
        //    public static int BlockId { get { return MvcApplication.Emplog().BlockId_fk ?? 0; } }
        //    //public static int? EmployeeCode { get { return MvcApplication.Emplog().EmpCode; } }
        //    public static string Name { get { return MvcApplication.Emplog().Name; } }
        //    public static string Email { get { return MvcApplication.Emplog().Email; } }
        //    public static string EmpGuid { get { return MvcApplication.Emplog().EmpGuid; } }
        //    //  public static int Hubid { get { return MvcApplication.Emplog().Hubid_fk.Value; } }

        //    // public static int? SupervisorId { get { return MvcApplication.Emplog().SupervisorId; } }
        //    public static int? StateId { get { return MvcApplication.Emplog().StateId_fk; } }
        //    //  public static string SupervisorName { get { return MvcApplication.Emplog().SupervisorName; } }
        //    //public static string SupervisorEmail { get { return MvcApplication.Emplog().SupervisorEmail; } }
        //}
        //public static List<SelectListItem> GetRole()
        //{
        //    try
        //    {
        //        var dis = new SelectList(dbe.AspNetRoles, "Name", "Name").ToList();
        //        return dis.OrderBy(x => x.Text).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}
        //public static string GetLeadRoleName()
        //{
        //    string str = string.Empty;
        //    if (HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        if (CommonModel.User.Role == "SPMU-Member")
        //        {
        //            str = "SPMU-Lead";
        //        }
        //        if (CommonModel.User.Role == "SPMU-Lead")
        //        {
        //            str = "CPMU";
        //        }
        //    }
        //    return str;
        //}




        //public static DataTable GetSPUserDetail()
        //{
        //    StoredProcedure sp = new StoredProcedure("SP_UserDetail");
        //    sp.Command.AddParameter("@User", HttpContext.Current.User.Identity.Name, DbType.String);
        //    DataTable dt = sp.ExecuteDataSet().Tables[0];
        //    return dt;
        //}
        public static DataTable GetSPCutUserlist()
        {
            StoredProcedure sp = new StoredProcedure("SPGetCutUserlist");
            sp.Command.AddParameter("@User", HttpContext.Current.User.Identity.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet GetSPCourselist(string Parame)
        {
            StoredProcedure sp = new StoredProcedure("SP_Courselist");
            sp.Command.AddParameter("@Parame", Parame, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            if (ds.Tables.Count > 0)
            {
                return ds;
            }
            return ds;
        }
        public class User_Model
        {
            public int StateId { get; set; }
            public int DistrictId { get; set; }
            public int BlockId { get; set; }
            public int PanchayatId { get; set; }
            public int VoId { get; set; }
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string DistrictName { get; set; }
            public string BlockName { get; set; }
            public string PanchayatName { get; set; }
            public string VillageOrgName { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public bool IsStatus { get; set; }
        }
        public class UserName_Model
        {
            public int StateId { get; set; }
            public int DistrictId { get; set; }
            public int BlockId { get; set; }
            public int ClusterId { get; set; }
            public string DistrictName { get; set; }
            public string BlockName { get; set; }
            public string ClusterName { get; set; }
            public string UserName { get; set; }
        }

        public static bool ValidateFilterModelByRole(FilterModel model)
        {
            if (model.Month == "0")
            {
                model.Month = string.Empty;
            }
            var isSuccess = false;
            switch (MvcApplication.CUser.Role)
            {
                case RoleNameCont.Admin:
                    isSuccess = true;
                    break;
                case RoleNameCont.BPIU:
                case RoleNameCont.BPM:
                    if (!string.IsNullOrWhiteSpace(model.DistrictId) && !string.IsNullOrWhiteSpace(model.BlockId))
                    {
                        isSuccess = true;
                    }
                    break;
                case RoleNameCont.CC:
                    if (!string.IsNullOrWhiteSpace(model.DistrictId) && !string.IsNullOrWhiteSpace(model.BlockId) && !string.IsNullOrWhiteSpace(model.CLFId))
                    {
                        isSuccess = true;
                    }
                    break;
                case RoleNameCont.CLF:
                    if (!string.IsNullOrWhiteSpace(model.DistrictId) && !string.IsNullOrWhiteSpace(model.BlockId) && !string.IsNullOrWhiteSpace(model.CLFId))
                    {
                        isSuccess = true;
                    }
                    break;
                case RoleNameCont.CM:
                    if (!string.IsNullOrWhiteSpace(model.DistrictId) && !string.IsNullOrWhiteSpace(model.BlockId) && !string.IsNullOrWhiteSpace(model.CLFId) && !string.IsNullOrWhiteSpace(model.PanchayatId) && !string.IsNullOrWhiteSpace(model.VOId) && !string.IsNullOrWhiteSpace(model.CMID))
                    {
                        isSuccess = true;
                    }
                    break;
                case RoleNameCont.CNRP:
                    if (!string.IsNullOrWhiteSpace(model.DistrictId) && !string.IsNullOrWhiteSpace(model.BlockId) && !string.IsNullOrWhiteSpace(model.CLFId) && !string.IsNullOrWhiteSpace(model.PanchayatId))
                    {
                        isSuccess = true;
                    }
                    break;
                case RoleNameCont.District:
                    if (!string.IsNullOrWhiteSpace(model.DistrictId))
                    {
                        isSuccess = true;
                    }
                    break;
                case RoleNameCont.MRP:
                    if (!string.IsNullOrWhiteSpace(model.DistrictId) && !string.IsNullOrWhiteSpace(model.BlockId) && !string.IsNullOrWhiteSpace(model.CLFId))
                    {
                        isSuccess = true;
                    }
                    break;
                case RoleNameCont.State:
                    isSuccess = true;
                    break;
                case RoleNameCont.Viewer:
                    isSuccess = true;
                    break;
                default:
                    isSuccess = true;
                    break;
            }
            return isSuccess;
        }
        #endregion

        #region Master 
        public static string GetEnumDisplayName(Enum enumValue)
        {
            return enumValue.GetType()
              .GetMember(enumValue.ToString())
              .First()
              .GetCustomAttribute<DisplayAttribute>()
              ?.GetName();
        }
        public static List<SelectListItem> GetGender()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem { Value = "", Text = "Select" });
            list.Add(new SelectListItem { Value = "Male", Text = "Male" });
            list.Add(new SelectListItem { Value = "Female", Text = "Female" });
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetRole(bool IsAll = false)
        {
            FP_DBEntities db_ = new FP_DBEntities();
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                if (HttpContext.Current.User.IsInRole(RoleNameCont.CNRP))
                {
                    items = new SelectList(db_.AspNetRoles.Where(x => x.Name == RoleNameCont.CM), "ID", "Name").OrderBy(x => x.Text).ToList();
                }
                else if (HttpContext.Current.User.IsInRole(RoleNameCont.BPIU) || HttpContext.Current.User.IsInRole(RoleNameCont.BPM))
                {
                    items = new SelectList(db_.AspNetRoles.Where(x => x.Name == RoleNameCont.CNRP || x.Name == RoleNameCont.CM || x.Name == RoleNameCont.BPIU), "ID", "Name").OrderBy(x => x.Text).ToList();
                }
                else if (HttpContext.Current.User.IsInRole(RoleNameCont.District))
                {
                    items = new SelectList(db_.AspNetRoles.Where(x => x.Name != RoleNameCont.Admin && x.Name != RoleNameCont.State && x.Name != RoleNameCont.District), "ID", "Name").OrderBy(x => x.Text).ToList();
                }
                else
                {
                    items = new SelectList(db_.AspNetRoles, "ID", "Name").OrderBy(x => x.Text).ToList();
                }

                if (IsAll)
                {
                    items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                return items;
            }
            catch (Exception)
            {
                //DO To
                throw;
            }
        }
        public static List<SelectListItem> GetState(bool IsAll = false)
        {
            try
            {
                var items = new SelectList(dbe.State_Master.Where(x => x.LGD_State_Code == 10), "ID", "Name").OrderBy(x => x.Text).ToList();
                if (IsAll)
                {
                    items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                return items;
            }
            catch (Exception)
            {
                //DO To
                throw;
            }
        }
        public static List<SelectListItem> GetDistrict(bool IsAll = false, int StateId = 0)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = SP_Model.SPDistrict();
                var listitem = ConvertDataTable<SelectListItem>(dt);
                //var items = new SelectList(dbe.Dist_Mast.Where(x => x.IsActive == true && x.StateId == StateId), "ID", "DistName").OrderBy(x => x.Text).ToList();
                var items = new SelectList(listitem, "Value", "Text").OrderBy(x => x.Text).ToList();
                if (IsAll)
                {
                    items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                return items;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<SelectListItem> GetBlock(bool IsAll = false, int DistrictId = 0)
        {
            try
            {
                var items = new SelectList(dbe.Block_Master.Where(x => x.IsActive == true && x.DistrictId_fk == DistrictId), "BlockId_pk", "Block").OrderBy(x => x.Text).ToList();
                if (IsAll)
                {
                    items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                return items;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<SelectListItem> GetCLF(bool IsAll = false, int DistrictId = 0, int BlockId = 0)
        {
            try
            {
                var items = new SelectList(dbe.CLF_Master.Where(x => x.IsActive == true && x.DistrictId_fk == DistrictId && x.DistrictId_fk == BlockId), "CLF_ID_pk", "CLFName").OrderBy(x => x.Text).ToList();
                if (IsAll)
                {
                    items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                return items;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<SelectListItem> GetPanchayat(bool IsAll = false, int DistrictId = 0, int BlockId = 0, int CLFId = 0)
        {
            try
            {
                var items = new SelectList(dbe.Panchayat_Master.Where(x => x.IsActive == true && x.DistrictId_fk == DistrictId && x.Blockid_fk == BlockId && x.CLF_Id_fk == CLFId), "Panchayatid_pk", "Panchayat").OrderBy(x => x.Text).ToList();
                if (IsAll)
                {
                    items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                return items;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<SelectListItem> GetVOrg(bool IsAll = false, int DistrictId = 0, int BlockId = 0, int CLFId = 0, int PanchayatId = 0)
        {
            try
            {
                var items = new SelectList(dbe.VO_Master.Where(x => x.IsActive == true && x.DistrictId_fk == DistrictId && x.BlockId_fk == BlockId && x.CLF_Id_fk == CLFId && x.Panchayatid_fk == PanchayatId), "Void_pk", "Village_Organization").OrderBy(x => x.Text).ToList();
                if (IsAll)
                {
                    items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                return items;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        //public static List<SelectListItem> GetState()
        //{
        //    try
        //    {
        //        var dis = new SelectList(dbe.MST_State, "StateId", "StateName").ToList();
        //        return dis.OrderBy(x => x.Text).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetDistrict(int StateId)
        //{
        //    try
        //    {
        //        if (StateId != 0)
        //        {
        //            var dis = new SelectList(dbe.MST_District.Where(x => (x.IDState == StateId)), "DistrictId", "DistrictName").ToList();
        //            return dis.OrderBy(x => x.Text).ToList();
        //        }
        //        else
        //        {
        //            var dis = new SelectList(dbe.MST_District, "DistrictId", "DistrictName").ToList();
        //            return dis.OrderBy(x => x.Text).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetBlock(int? DistrictId)
        //{
        //    try
        //    {
        //        if (DistrictId != 0)
        //        {
        //            var dis = new SelectList(dbe.MST_Block.Where(x => (x.IDDistrict == DistrictId)), "BlockId", "BlockName").ToList();
        //            return dis.OrderBy(x => x.Text).ToList();
        //        }
        //        else
        //        {
        //            var dis = new SelectList(dbe.MST_Block, "BlockId", "BlockName").ToList();
        //            return dis.OrderBy(x => x.Text).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}

        //public static List<LeadModal> GetLeadList(int locationid, string Rolen)
        //{
        //    LeadModal LDetail;
        //    List<LeadModal> list = new List<LeadModal>();
        //    if (!string.IsNullOrEmpty(Rolen))
        //    {
        //        StoredProcedure sp = new StoredProcedure("SP_GetLeadDetails");
        //        sp.Command.AddParameter("@LocationId", locationid, DbType.Int16);
        //        sp.Command.AddParameter("@RoleName", Rolen, DbType.String);
        //        DataTable dt = sp.ExecuteDataSet().Tables[0];
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            LDetail = new LeadModal()
        //            {
        //                LeadID = dr["UserId"].ToString(),
        //                LeadName = dr["Name"].ToString(),
        //                LeadEmailID = dr["Email"].ToString(),
        //                LeadLocationID = dr["LocationID"].ToString(),
        //                LeadLocation = dr["Locaton"].ToString(),
        //                LeadRoleID = dr["RoleId"].ToString(),
        //                LeadRoleName = dr["RoleName"].ToString()
        //            };
        //            list.Add(LDetail);
        //        }
        //        return list.ToList();
        //    }
        //    return list;

        //}

        #endregion

        #region Document Upload
        public static string GetFilePath(HttpPostedFileBase file, string Module, string RegNo, string Ques_fk, string Folder)
        {
            var url = string.Empty;
            try
            {
                string file_extension = Path.GetExtension(file.FileName).ToLower();
                Stream strmStream = file.InputStream;
                if (IsValidImage(strmStream) == true || file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".pptx")
                {
                    url = "~/Uploads/" + Module + "/" + RegNo + "/" + Folder + "/";
                    string extension = Path.GetExtension(file.FileName);

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(url)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(url));
                    }
                    int index = 1;
                    string filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                    string fname = filenamewithoutext + extension;
                    while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(url, fname))))
                    {
                        index++;
                        fname = file.FileName + "(" + index.ToString() + ")" + extension;
                    }
                    //url = HttpContext.Current.Server.MapPath(url + Ques_fk + "_" + fname);
                    url = url + Ques_fk + "_" + fname;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return url;
        }
        public static string SaveFileDynamically(HttpPostedFileBase[] files, string Module, string RegNo, string Ques_fk, string Folder)
        {
            string URL = "";
            try
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string file_extension = Path.GetExtension(file.FileName).ToLower();
                        Stream strmStream = file.InputStream;
                        if (IsValidImage(strmStream) == true || file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".pptx")
                        {
                            URL = "~/Uploads/" + Module + "/" + RegNo + "/" + Folder + "/";
                            string extension = Path.GetExtension(file.FileName);

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(URL)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(URL));
                            }
                            int index = 1;
                            string filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                            string fname = filenamewithoutext + extension;
                            while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(URL, fname))))
                            {
                                index++;
                                fname = file.FileName + "(" + index.ToString() + ")" + extension;
                            }
                            file.SaveAs(HttpContext.Current.Server.MapPath(URL + Ques_fk + "_" + fname));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return URL;
        }
        public static string SaveFile(HttpPostedFileBase[] files, string Module, string RegNo, string Folder)
        {
            string URL = "";
            try
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string file_extension = Path.GetExtension(file.FileName).ToLower();
                        Stream strmStream = file.InputStream;
                        if (IsValidImage(strmStream) == true || file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".pptx")
                        {
                            //URL = "~/Uploads/" + Module + "/" + RegNo + "/" + Folder + "/";
                            URL = "~/Uploads/" + RegNo + " / ";
                            string extension = Path.GetExtension(file.FileName);

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(URL)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(URL));
                            }
                            int index = 1;
                            string filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                            string fname = filenamewithoutext + extension;
                            while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(URL, fname))))
                            {
                                index++;
                                fname = file.FileName + "(" + index.ToString() + ")" + extension;
                            }
                            file.SaveAs(HttpContext.Current.Server.MapPath(URL + fname));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return URL;
        }
        public static string SaveSingleFile(HttpPostedFileBase files, string Module, string RegNo)
        {
            string URL = "";
            string filepath = string.Empty;

            if (files != null && files.ContentLength > 0)
            {
                string file_extension = Path.GetExtension(files.FileName).ToLower();
                string filenamewithoutext = Path.GetFileNameWithoutExtension(files.FileName);
                Stream strmStream = files.InputStream;
                if (IsValidImage(strmStream) == true || file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".doc" || file_extension == ".dotx" || file_extension == ".dot" || file_extension == ".pptx" || file_extension == ".ppt" || file_extension == ".rar" || file_extension == ".zip" || file_extension == ".xls" || file_extension == ".xlsx" || file_extension == ".jpeg" || file_extension == ".png" || file_extension == ".jpg")
                {
                    //URL = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/" + Module + "/" + RegNo + "/"));
                    URL = "~/Uploads/" + RegNo + "/";
                    string extension = Path.GetExtension(files.FileName);

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(URL)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(URL));
                    }
                    int index = 1;
                    string fname = files.FileName;
                    while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(URL, fname))))
                    {
                        index++;
                        fname = filenamewithoutext + "(" + index.ToString() + ")" + extension;
                    }
                    files.SaveAs(HttpContext.Current.Server.MapPath(URL + (Module + "-" + fname)));
                    filepath = URL + (Module + "-" + fname);
                }
            }

            return filepath;
        }
        public static string DeleteSingleFile(HttpPostedFileBase files, string Module, string RegNo)
        {
            //ToDo: Add code to delete single file from directory
            string URL = "";
            string filepath = string.Empty;



            return filepath;
        }
        public static string SaveSingleFile(HttpPostedFileBase files, string Module, string RegNo, string CustomFileName)
        {
            string URL = "";
            string filepath = string.Empty;

            if (files != null && files.ContentLength > 0)
            {
                string file_extension = Path.GetExtension(files.FileName).ToLower();
                string filenamewithoutext = Path.GetFileNameWithoutExtension(files.FileName);
                Stream strmStream = files.InputStream;
                if (IsValidImage(strmStream) == true || file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".pptx")
                {
                    //URL = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/" + Module + "/" + RegNo + "/"));
                    URL = "~/Uploads/" + RegNo + "/";
                    string extension = Path.GetExtension(files.FileName);

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(URL)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(URL));
                    }
                    int index = 1;
                    string fname = CustomFileName + extension; // files.FileName;
                    while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(URL, fname))))
                    {
                        index++;
                        fname = filenamewithoutext + "(" + index.ToString() + ")" + extension;
                    }
                    files.SaveAs(HttpContext.Current.Server.MapPath(URL + (Module + "-" + fname)));
                    filepath = URL + (Module + "-" + fname);
                }
            }

            return filepath;
        }
        public static string GetFileName(HttpPostedFileBase files)
        {
            string filename = string.Empty;

            if (files != null && files.ContentLength > 0)
            {
                string file_extension = Path.GetExtension(files.FileName).ToLower();
                string filenamewithoutext = Path.GetFileNameWithoutExtension(files.FileName);
                Stream strmStream = files.InputStream;
                if (IsValidImage(strmStream) == true || file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".pptx")
                {
                    string extension = Path.GetExtension(files.FileName);
                    int index = 1;
                    string fname = files.FileName;
                    fname = filenamewithoutext + "(" + index.ToString() + ")" + extension;
                    filename = fname;
                }
            }
            return filename;
        }
        public static bool IsValidImage(Stream stream)
        {
            try
            {
                //Read an image from the stream...
                var i = Image.FromStream(stream);
                //Move the pointer back to the beginning of the stream
                stream.Seek(0, SeekOrigin.Begin);

                if (ImageFormat.Jpeg.Equals(i.RawFormat))
                    return true;
                return ImageFormat.Png.Equals(i.RawFormat) || ImageFormat.Gif.Equals(i.RawFormat);
            }
            catch
            {
                return false;
            }
        }
        //public static Binary BinarySaveSingleFile(HttpPostedFileBase files)
        //{
        //    byte[] Databytes;
        //    //try
        //    //{
        //    string empFilename = Path.GetFileName(files.FileName);
        //    string FilecontentType = files.ContentType;
        //    using (var reader = new BinaryReader(files.InputStream))
        //    {
        //        Databytes = reader.ReadBytes(files.ContentLength);
        //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    string error = ex.Message;
        //    //}
        //    return Databytes;
        //}
        public static string GetFileExt(string filename)
        {
            string myFilePath = filename;
            string ext = Path.GetExtension(myFilePath);
            return ext;
        }
        #endregion

        #region Monthly,Year
        public static List<SelectListItem> GetTypeOfDateList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "1", Text = "DOB" });
            list.Add(new SelectListItem { Value = "2", Text = "Age in Year,Month" });
            return list.OrderBy(x => x.Text).ToList();
        }
        public static string CalculateAgeFromDOB(DateTime p_Dob)
        {
            // Method to Calculate age from Date of Birth in C#
            DateTime Today = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(p_Dob).Ticks).Year - 1;
            DateTime PastYearDate = p_Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Today)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Today)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Today.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Today.Subtract(PastYearDate).Hours;
            int Minutes = Today.Subtract(PastYearDate).Minutes;
            int Seconds = Today.Subtract(PastYearDate).Seconds;
            return Years + "_" + Months;
            //    String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            //Years, Months, Days, Hours, Seconds);
        }
        public static List<SelectListItem> GetFunMonthList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 1; i <= 11; i++)
            {
                list.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            return list.ToList();
        }
        public static List<SelectListItem> GetFunYearList(int isAddedSelect = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            DateTime dt = DateTime.Now;
            var year = 0;
            if (isAddedSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            for (int i = 1; i <= 20; i++) //ToDo: Sunil - Change to 17 years after all backlog entry
            {
                dt = dt.AddYears(-1);
                if (dt.AddYears(1).Year == DateTime.Now.Year)
                {
                    year = DateTime.Now.Year;
                }
                else
                {

                    year = dt.Year;
                }
                list.Add(new SelectListItem { Value = year.ToString(), Text = year.ToString() });
            }
            return list.ToList();
        }
        public static List<SelectListItem> GetEnumYesNoList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            DateTime dt = DateTime.Now;
            foreach (var item in Enum.GetValues(typeof(Enums.OptionYesNo)))
            {
                list.Add(new SelectListItem { Value = item.ToString(), Text = item.ToString() });

            }

            return list.ToList();
        }
        #endregion

        #region Other Think
        public static List<SelectListItem> GetIsPlanAchieved()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "1", Text = "Achieved", Selected = true });
            list.Add(new SelectListItem { Value = "0", Text = "No Achieved" });
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetLanguangeType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem { Value = "", Text = "Select" });
            list.Add(new SelectListItem { Value = "1", Text = "English" });
            list.Add(new SelectListItem { Value = "2", Text = "Hindi" });
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetSexType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem { Value = "", Text = "Select" });
            list.Add(new SelectListItem { Value = "Female", Text = "Female" });
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetCastType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem { Value = "", Text = "Select" });
            list.Add(new SelectListItem { Value = "1", Text = "Gen" });
            list.Add(new SelectListItem { Value = "2", Text = "SC" });
            list.Add(new SelectListItem { Value = "3", Text = "ST" });
            list.Add(new SelectListItem { Value = "4", Text = "OBC" });
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetYesNo()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem { Value = "", Text = "Select" });
            list.Add(new SelectListItem { Value = "0", Text = "No" });
            list.Add(new SelectListItem { Value = "1", Text = "Yes" });
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetYesNotrue()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem { Value = "", Text = "Select" });
            list.Add(new SelectListItem { Value = "false", Text = "No" });
            list.Add(new SelectListItem { Value = "true", Text = "Yes" });
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetServiceProvider()
        {
            FP_DBEntities fP_ = new FP_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = new SelectList(fP_.ServiceProvider_Master.Where(x => x.IsActive == true), "ID", "ServiceProvider").OrderBy(x => x.Text).ToList();
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetBoyGirl()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem { Value = "", Text = "Select" });
            list.Add(new SelectListItem { Value = "Boy", Text = "Boy", Selected = true });
            list.Add(new SelectListItem { Value = "Girl", Text = "Girl" });
            list.Add(new SelectListItem { Value = "No", Text = "No" });
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetDOMYear()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem { Value = "", Text = "Select" });
            list.Add(new SelectListItem { Value = "1", Text = "Date of marriage", Selected = true });
            list.Add(new SelectListItem { Value = "2", Text = "Year of marriage" });
            return list.OrderByDescending(x => x.Text).ToList();
        }
        //public static List<SelectListItem> GetDOBYear()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    //list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    list.Add(new SelectListItem { Value = "1", Text = "Date of Birth", Selected = true });
        //    list.Add(new SelectListItem { Value = "2", Text = "Year of Birth" });
        //    return list.OrderByDescending(x => x.Text).ToList();
        //}
        public static List<SelectListItem> GetActivity()
        {
            FP_DBEntities dBE = new FP_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = new SelectList(dBE.Activities_Master.Where(x => x.IsActive == true), "ID", "Activity").ToList();
            //return list.OrderByDescending(x => x.Text).ToList();
            return list;
        }
        public static List<SelectListItem> GetModuleRollout()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = new SelectList(dbe.ModuleRollout_Master.Where(x => x.IsActive == true), "ID", "ModuleName").OrderBy(x => x.Text).ToList();
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetSubject(int Isval = 1)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = new SelectList(dbe.Subject_Master.Where(x => x.IsActive == true), "ID", "Subject").OrderBy(x => x.Text).ToList();
            if (Isval == 1)
            {
                list.Add(new SelectListItem { Value = "", Text = "Select" });
            }
            if (Isval == 2)
            {
                list.Add(new SelectListItem { Value = "all", Text = "all" });
            }
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetHVPMG(int Isval = 1)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (Isval == 1)
            {
                list.Add(new SelectListItem { Value = "", Text = "Select" });
            }
            if (Isval == 2)
            {
                list.Add(new SelectListItem { Value = "all", Text = "all" });
            }
            list.Add(new SelectListItem { Value = "1", Text = "Peer Group Meeting" });
            list.Add(new SelectListItem { Value = "2", Text = "Home Visit" });
            list.Add(new SelectListItem { Value = "3", Text = "Peer Group Meeting & Home Visit" });
            return list.OrderBy(x => x.Value).ToList();
        }
        public static List<SelectListItem> GetSHGAffiliation()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem { Value = "", Text = "Select" });
            list.Add(new SelectListItem { Value = "1", Text = "Self" });
            list.Add(new SelectListItem { Value = "2", Text = "Any Other family member" });
            return list.OrderBy(x => x.Value).ToList();
        }
        public static List<SelectListItem> GetContraceptive()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = new SelectList(dbe.Contraceptive_Master.Where(x => x.IsActive == true), "ID", "CName").OrderBy(x => x.Text).ToList();
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetContraceptive_Child(int CID)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (CID == 1 || CID == 2)
            {
                list = new SelectList(dbe.Contraceptive_Child_Master.Where(x => x.IsActive == true && x.CID == CID), "ID", "MethodName").OrderBy(x => x.Text).ToList();
            }
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetYear(int isAddedSelect = 0)
        {
            FP_DBEntities fP_DB = new FP_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = new SelectList(fP_DB.Year_Master, "ID", "Year").OrderBy(x => x.Text).ToList();
            if (isAddedSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (isAddedSelect == 2)
            {
                list.Insert(0, new SelectListItem { Value = "all", Text = "All" });
            }
            return list.OrderByDescending(x => x.Text).ToList();
        }
        public static List<SelectListItem> GetMonth(int isAddedSelect = 0)
        {
            FP_DBEntities _db = new FP_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = new SelectList(_db.Month_Master, "ID", "MonthName").OrderBy(x => Convert.ToInt16(x.Value)).ToList();
            if (isAddedSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (isAddedSelect == 2)
            {
                list.Insert(0, new SelectListItem { Value = "all", Text = "All" });
            }
            return list.ToList();
        }
        #endregion

        public static Boolean GetValidTillMonth(int MonthId, int YearId)
        {
            var year = YearId + 2022;
            var dt = new DateTime(year, MonthId, 1);
            if ((dt.Year == DateTime.Now.Year && dt.Month <= DateTime.Now.Month) || dt.Year < DateTime.Now.Year)
            {
                return true;
            }
            return false;
        }


        //public int OnlyContraUseMethod(int ContraId,int UseMethodId)
        //{
        //    if (ContraId > 0)
        //    {
        //        var usemethod = ContraId == (int)Enums.eContraceptive.Temporary
        //                        ? UseMethodId : ContraId == (int)Enums.eContraceptive.Permanent
        //                        ? UseMethodId : ContraId == (int)Enums.eContraceptive.OtherMethod:"";

        //    }

        //}
        //public static List<SelectListItem> GetSupportOrganization(bool isAddSel = false)
        //{
        //    try
        //    {
        //        var dis = new SelectList(dbe.MST_Organization.Where(x => x.IsActive == true && x.ID != User.IDOrganization), "ID", "Name").OrderBy(x => x.Text).ToList();
        //        if (isAddSel)
        //        {
        //            dis.Insert(0, new SelectListItem { Value = "", Text = "Select" });
        //        }
        //        return dis;
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetLocation(bool isAddAll = false)
        //{
        //    try
        //    {
        //        var dis = new SelectList(dbe.MST_Location.Where(x => x.IsActive == true), "ID", "Name").OrderBy(x => x.Text).ToList();
        //        if (isAddAll)
        //        {
        //            dis.Insert(0, new SelectListItem { Value = "0", Text = "All" });
        //        }
        //        return dis;
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}

        public static List<SelectListItem> GetRolesByLoggedinUser(bool isAddedSelect = true)
        {
            try
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var items = new SelectList(dbe.AspNetRoles.Where(x => x.Id.ToLower() == "2CAC5128-08C7-4B95-8C6A-DB61E6B40376" || x.Id.ToLower() == "D3CE11F6-847E-4158-91DB-C4EB555A9BD4" || x.Id.ToLower() == "BAA881FB-F8BD-4199-A6CA-6FCAF7E3A0C2"), "Name", "Name").OrderBy(x => x.Text).ToList();
                    if (isAddedSelect)
                    {
                        items.Insert(0, new SelectListItem { Value = "", Text = "Select" });
                    }
                    return items;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
        //public static List<SelectListItem> GetLocationsByLoggedinUser()
        //{
        //    try
        //    {
        //        if (HttpContext.Current.User.Identity.IsAuthenticated)
        //        {
        //            var dis = new SelectList(dbe.MST_Location.Where(x => x.IsActive == true), "ID", "Name").OrderBy(x => x.Text).ToList();
        //            if (User.LocationID > 0)
        //            {
        //                dis = dis.Where(x => x.Value == User.LocationID.ToString()).ToList();
        //                dis[0].Selected = true;
        //            }
        //            return dis;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //    return null;
        //}
        //public static List<SelectListItem> GetOrganizationByLoggedinUser()
        //{
        //    try
        //    {
        //        var dis = new SelectList(dbe.MST_Organization.Where(x => x.IsActive == true), "ID", "Name").OrderBy(x => x.Text).ToList();
        //        if (User.LocationID > 0)
        //        {
        //            dis = dis.Where(x => x.Value == User.IDOrganization.ToString()).ToList();
        //            dis[0].Selected = true;
        //        }
        //        return dis;
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetOutcomeVT()
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        var dis = new SelectList(_db.MST_Outcome, "ID", "Name").ToList();
        //        return dis.OrderBy(x => x.Text).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetOutcomeVT(Guid? ComponentID)
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        var list = new List<SelectListItem>();
        //        if (ComponentID != Guid.Empty)
        //        {
        //            list = new SelectList(_db.MST_Outcome.Where(x => x.IDComponent == ComponentID), "ID", "Name").ToList();
        //        }
        //        else
        //        {
        //            list = new SelectList(_db.MST_Outcome, "ID", "Name").ToList();
        //        }

        //        //var dis = new SelectList(_db.MST_Outcome, "ID", "Name").ToList();
        //        return list.OrderBy(x => x.Text).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetddlOutputVT(Guid? OutcomeID, Guid? OutputID)
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        if (OutputID != null)
        //        {
        //            var dis = new SelectList(_db.MST_Output.Where(x => x.IDOutcome == OutcomeID && x.ID == OutputID), "ID", "Name").ToList();
        //            return dis.OrderBy(x => x.Text).ToList();
        //        }
        //        else
        //        {
        //            var dis = new SelectList(_db.MST_Output.Where(x => x.IDOutcome == OutcomeID), "ID", "Name").ToList();
        //            return dis.OrderBy(x => x.Text).ToList();
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}

        //public static List<SelectListItem> GetDDLComponent(bool checkidcomponent = false)
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        if (checkidcomponent)
        //        {
        //            var dis = new SelectList(_db.MST_Component.OrderBy(x => x.DisplayOrder), "ID", "Name").ToList();
        //            return dis.ToList();
        //        }
        //        else
        //        {
        //            var dis = new SelectList(_db.MST_Component.Where(x => x.IsActive == true).OrderBy(x => x.DisplayOrder), "ID", "Name").ToList();
        //            return dis.ToList();
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetDDLPlanningMonth()
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        var dis = new SelectList(_db.MST_Month.OrderBy(x => x.DisplayOrder), "MonthID", "MonthName").ToList();
        //        return dis.ToList();
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}

        //public static List<SelectListItem> GetDDLPlanningYear()
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        var dis = new SelectList(_db.MST_Year, "ID", "PlanningYear").ToList();
        //        return dis.OrderByDescending(x => x.Value).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetDDLPlannYear()
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        var dis = new SelectList(_db.MST_Year, "ID", "PlanningYear").ToList();
        //        return dis.OrderByDescending(x => x.Value).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}

        //public static List<SelectListItem> GetLevelActivity()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    //list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    list.Add(new SelectListItem { Value = "National", Text = "National" });
        //    list.Add(new SelectListItem { Value = "State", Text = "State" });
        //    list.Add(new SelectListItem { Value = "District", Text = "District" });
        //    list.Add(new SelectListItem { Value = "Block", Text = "Block" });
        //    list.Add(new SelectListItem { Value = "Panchayat", Text = "Panchayat" });
        //    list.Add(new SelectListItem { Value = "School", Text = "School" });
        //    return list.ToList();//OrderBy(x => x.Text)
        //}
        //public static List<SelectListItem> GetStatusActivity(bool Status = false)
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    //list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    list.Add(new SelectListItem { Value = "Started", Text = "Started" });
        //    list.Add(new SelectListItem { Value = "Ongoing", Text = "Ongoing" });
        //    list.Add(new SelectListItem { Value = "Completed", Text = "Completed" });
        //    if (Status)
        //    {
        //        list.Add(new SelectListItem { Value = "Not Started", Text = "Not Started" });
        //        list.Add(new SelectListItem { Value = "Upcoming", Text = "Upcoming" });
        //    }
        //    return list.OrderBy(x => x.Text).ToList();
        //}
        //public static List<SelectListItem> GetTypeofActivity()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    ////list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    //list.Add(new SelectListItem { Value = "Training", Text = "Training" });
        //    //list.Add(new SelectListItem { Value = "Reach", Text = "Reach" });
        //    //list.Add(new SelectListItem { Value = "Meeting", Text = "Meeting" });
        //    //list.Add(new SelectListItem { Value = "Workshop", Text = "Workshop" });
        //    //return list.OrderBy(x => x.Text).ToList();
        //    IPELEntities _db = new IPELEntities();
        //    // List<MST_TypeActivity> tbllist = new List<MST_TypeActivity>();
        //    try
        //    {
        //        //var res=(from t1 in dbe.MST_TypeActivity 
        //        //        join t2 in dbe.MAP_PlanParticipantType on t1.ID equals t2.TypeofActivityID select new { t1.ID,t1.TypeActivity,t2.IDActivity }).Where(x=>x.IDActivity=);


        //        list = new SelectList(_db.MST_TypeActivity.Where(x => (x.IsActive == true)).OrderBy(y => y.TypeActivity), "ID", "TypeActivity").ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return list.ToList();

        //}
        //public static List<SelectListItem> GetTypeofActivityList(Guid ActivityID)
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    ////list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    //list.Add(new SelectListItem { Value = "Training", Text = "Training" });
        //    //list.Add(new SelectListItem { Value = "Reach", Text = "Reach" });
        //    //list.Add(new SelectListItem { Value = "Meeting", Text = "Meeting" });
        //    //list.Add(new SelectListItem { Value = "Workshop", Text = "Workshop" });
        //    //return list.OrderBy(x => x.Text).ToList();
        //    IPELEntities _db = new IPELEntities();
        //    // List<MST_TypeActivity> tbllist = new List<MST_TypeActivity>();
        //    try
        //    {
        //        if (ActivityID != Guid.Empty)
        //        {
        //            var res = (from t1 in dbe.MST_TypeActivity
        //                       join t2 in dbe.MAP_PlanParticipantType on t1.ID equals t2.TypeofActivityID
        //                       select new { t1.ID, t1.TypeActivity, t1.IsActive, t2.IDActivity }).Where(x => x.IDActivity == ActivityID && x.IsActive == true).OrderBy(x => x.TypeActivity).ToList();

        //            //list = new SelectList(_db.MST_TypeActivity.Where(x => (x.IsActive == true)).OrderBy(y => y.TypeActivity), "ID", "TypeActivity").ToList();
        //            //list = new SelectList((res.GroupBy(
        //            //        p => p.ID,
        //            //        p => p.TypeActivity,(key, g) => new { ID = key, TypeActivity = g.Select(x=>x.ty) })), "ID", "TypeActivity").ToList();
        //            //var reslist = (from p in res
        //            //                    group p by p.ID into g
        //            //                    select new
        //            //                    {
        //            //                        ID = g.Key,
        //            //                        TypeActivity = g.Select(c => c.TypeActivity)
        //            //                    }).ToList();

        //            var query = res.GroupBy(s => new { s.ID, s.TypeActivity })
        //                       .Select(g =>
        //                                    new
        //                                    {
        //                                        ID = g.Key.ID,
        //                                        TypeActivity = g.Key.TypeActivity,
        //                                    }
        //                              );

        //            list = new SelectList(query, "ID", "TypeActivity").ToList();
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return list.ToList();

        //}
        //public static List<SelectListItem> GetGenderDisable()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    //list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    list.Add(new SelectListItem { Value = "1", Text = "Male" });
        //    list.Add(new SelectListItem { Value = "2", Text = "Female" });
        //    list.Add(new SelectListItem { Value = "3", Text = "Both" });
        //    list.Add(new SelectListItem { Value = "4", Text = "Total" });
        //    return list.OrderBy(x => x.Text).ToList();
        //}
        //public static List<PartiPfmModel> GetParticipant(string TypeofActivityID)
        //{
        //    List<PartiPfmModel> list = new List<PartiPfmModel>();
        //    ////list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    //list.Add(new PartiPfmModel { TypeParticipantID = 1, TypeParticipantName = "State Edu Leadership" });
        //    //list.Add(new PartiPfmModel { TypeParticipantID = 2, TypeParticipantName = "SS Officials" });
        //    //list.Add(new PartiPfmModel { TypeParticipantID = 3, TypeParticipantName = "SCERT Officials" });
        //    //list.Add(new PartiPfmModel { TypeParticipantID = 4, TypeParticipantName = "SRG" });
        //    //list.Add(new PartiPfmModel { TypeParticipantID = 5, TypeParticipantName = "DRG" });
        //    //list.Add(new PartiPfmModel { TypeParticipantID = 6, TypeParticipantName = "MT" });
        //    //list.Add(new PartiPfmModel { TypeParticipantID = 7, TypeParticipantName = "BRG" });
        //    //list.Add(new PartiPfmModel { TypeParticipantID = 8, TypeParticipantName = "Teachers" });
        //    //list.Add(new PartiPfmModel { TypeParticipantID = 9, TypeParticipantName = "Others" });
        //    //list.Add(new PartiPfmModel { TypeParticipantID = 10, TypeParticipantName = "NA" });
        //    //return list.OrderBy(x => x.TypeParticipantID).ToList();
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        var tblist = _db.MST_ParticipantType.Where(x => (x.IsActive == true) && (x.TypeofActivityID.ToString() == TypeofActivityID)).OrderBy(y => y.ParticipantTypeName.ToString()).ToList();
        //        foreach (var item in tblist)
        //        {
        //            list.Add(new PartiPfmModel
        //            {
        //                TypeParticipantID = item.TypeParticipantID,
        //                TypeParticipantName = item.ParticipantTypeName,
        //                Gender = item.Gender,
        //                TypeDesc = item.TypeDesc,
        //            });
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return list.ToList();

        //}
        //public static DataTable SPGetParticipant(string PlanID, string ActivityID, string TypeofActivityID)
        //{
        //    StoredProcedure sp = new StoredProcedure("SP_GetParticipantType");
        //    sp.Command.AddParameter("@PlanID", PlanID, DbType.String);
        //    sp.Command.AddParameter("@ActivityID", ActivityID, DbType.String);
        //    sp.Command.AddParameter("@TypeofActivityID", TypeofActivityID, DbType.String);
        //    DataTable dt = sp.ExecuteDataSet().Tables[0];
        //    return dt;
        //}

        //public static DataTable SPGetParticipantTypeForReview(string PerformID, string PlanID, string ActivityID, string TypeofActivityID)
        //{
        //    StoredProcedure sp = new StoredProcedure("SP_GetParticipantTypeForReview");
        //    sp.Command.AddParameter("@PlanID", PlanID, DbType.String);
        //    sp.Command.AddParameter("@ActivityID", ActivityID, DbType.String);
        //    sp.Command.AddParameter("@TypeofActivityID", TypeofActivityID, DbType.String);
        //    sp.Command.AddParameter("@PerformID", PerformID, DbType.String);
        //    DataTable dt = sp.ExecuteDataSet().Tables[0];
        //    return dt;
        //}

        //public static DataTable SPGetParticipantTypeForWeeklyCollation(string PerformID, string PlanID, string ActivityID, string TypeofActivityID)
        //{
        //    StoredProcedure sp = new StoredProcedure("SP_GetParticipantTypeForWeeklyCollation");
        //    sp.Command.AddParameter("@PlanID", PlanID, DbType.String);
        //    sp.Command.AddParameter("@ActivityID", ActivityID, DbType.String);
        //    sp.Command.AddParameter("@TypeofActivityID", TypeofActivityID, DbType.String);
        //    sp.Command.AddParameter("@PerformID", PerformID, DbType.String);
        //    DataTable dt = sp.ExecuteDataSet().Tables[0];
        //    return dt;
        //}

        //public static List<PartiPfmModel> GetAlldataParticipant(string TypeofActivityID)
        //{
        //    List<PartiPfmModel> list = new List<PartiPfmModel>();
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        var tblist = _db.MST_ParticipantType.Where(x => (x.IsActive == true) && (x.TypeofActivityID.ToString() == TypeofActivityID)).OrderBy(y => y.ParticipantTypeName.ToString()).ToList();
        //        foreach (var item in tblist)
        //        {
        //            list.Add(new PartiPfmModel {

        //                TypeParticipantID = item.TypeParticipantID, 
        //                TypeParticipantName = item.ParticipantTypeName 

        //            });
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return list.ToList();
        //}

        //public static List<tbl_PartiPerfom> GetTypePartilist(Guid? PFMID)
        //{
        //    IPELEntities _db = new IPELEntities();
        //    List<tbl_PartiPerfom> tbllist = new List<tbl_PartiPerfom>();
        //    try
        //    {
        //        if (PFMID != Guid.Empty && PFMID != null)
        //        {
        //            tbllist = _db.tbl_PartiPerfom.Where(x => (x.IsActive == true && x.IsDeleted == false) && (x.IDPerform == PFMID)).OrderBy(y => y.TypeParticipants.ToString()).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return tbllist.ToList();
        //}
        //public static List<tbl_Lead_ActivityReviewParticipants> GetActivityReviewParticipantList(Guid? PFMID)
        //{
        //    IPELEntities _db = new IPELEntities();
        //    List<tbl_Lead_ActivityReviewParticipants> tbllist = new List<tbl_Lead_ActivityReviewParticipants>();
        //    try
        //    {
        //        if (PFMID != Guid.Empty && PFMID != null)
        //        {
        //            tbllist = _db.tbl_Lead_ActivityReviewParticipants.Where(x => (x.IsActive == true && x.IsDeleted == false) && (x.IDPerform == PFMID)).OrderBy(y => y.TypeParticipants.ToString()).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return tbllist.ToList();
        //}
        //public static List<SelectListItem> GetQuarter(bool isAddAll = false)
        //{
        //    try
        //    {
        //        var dis = new SelectList(dbe.MST_Quarter, "ID", "Name").OrderBy(x => x.Text).ToList();
        //        if (isAddAll)
        //        {
        //            dis.Insert(0, new SelectListItem { Value = "", Text = "All", Selected = true });
        //        }
        //        return dis.ToList();
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}

        //public static DataTable GetSP_CompiledLeadsForRevision(string CompiledLeadId)
        //{
        //    StoredProcedure sp = new StoredProcedure("SP_GetLeadsForRevision");
        //    sp.Command.AddParameter("@CompiledLeadId", CompiledLeadId, DbType.String);
        //    //sp.Command.AddParameter("@FromDate", FromDate, DbType.String);
        //    //sp.Command.AddParameter("@ToDate", ToDate, DbType.String);
        //    DataTable dt = sp.ExecuteDataSet().Tables[0];
        //    return dt;
        //}

        //public static List<SelectListItem> GetComponents()
        //{
        //    try
        //    {
        //        var dis = new SelectList(dbe.MST_Component.Where(x => x.IsActive == true).OrderBy(x => x.DisplayOrder), "ID", "Name").ToList();
        //        return dis.ToList();
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}

        //public static List<SelectListItem> GetOrganization()
        //{
        //    try
        //    {
        //        var dis = new SelectList(dbe.MST_Organization.Where(x => x.IsActive == true), "ID", "Name").ToList();
        //        return dis.OrderBy(x => x.Text).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetLocation()
        //{
        //    try
        //    {
        //        var dis = new SelectList(dbe.MST_Location.Where(x => x.IsActive == true), "ID", "Name").ToList();
        //        return dis.OrderBy(x => x.Text).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}

        //public static List<SelectListItem> GetOutcomeListByComponents(List<string> componentIds)
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        var list = new List<SelectListItem>();
        //        if (componentIds != null && componentIds.Count > 0)
        //        {
        //            list = new SelectList(_db.MST_Outcome.Where(x => componentIds.Contains(x.IDComponent.ToString())), "ID", "Name").ToList();
        //        }
        //        //else
        //        //{
        //        //    list = new SelectList(_db.MST_Outcome, "ID", "Name").ToList();
        //        //}

        //        //var dis = new SelectList(_db.MST_Outcome, "ID", "Name").ToList();
        //        return list.OrderBy(x => x.Text).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}

        //#region
        //public static List<MST_Outcome> GetOutcomelist()
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        var tbllist = _db.MST_Outcome.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(y => y.DisplayOrder).ToList();
        //        return tbllist;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //public static List<MST_Output> GetOutputlist(Guid? OutcomeID)
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        if (OutcomeID != Guid.Empty && OutcomeID != null)
        //        {
        //            var tbllist = _db.MST_Output.Where(x => (x.IsActive == true && x.IsDeleted == false) && (x.IDOutcome == OutcomeID)).OrderBy(y => y.DisplayOrder).ToList();
        //            return tbllist;
        //        }
        //        var tbllists = _db.MST_Output.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(y => y.DisplayOrder).ToList();
        //        return tbllists;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //public static List<MST_Activity> GetActivitylist(Guid? OutcomeID, Guid? OutputID)
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        if ((OutcomeID != Guid.Empty && OutcomeID != null) && (OutputID != Guid.Empty && OutputID != null))
        //        {
        //            var tbllist = _db.MST_Activity.Where(x => (x.IsActive == true && x.IsDeleted == false) && (x.IDOutcome == OutcomeID && x.IDOutput == OutputID)).OrderBy(y => y.DisplayOrder).ToList();
        //            return tbllist;
        //        }
        //        else if ((OutcomeID != Guid.Empty && OutcomeID != null) && (OutputID == Guid.Empty || OutputID == null))
        //        {
        //            var tbllist = _db.MST_Activity.Where(x => (x.IsActive == true && x.IsDeleted == false) && (x.IDOutcome == OutcomeID)).OrderBy(y => y.DisplayOrder).ToList();
        //            return tbllist;
        //        }
        //        var tbllists = _db.MST_Activity.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(y => y.DisplayOrder).ToList();
        //        return tbllists;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public static List<MST_TypeActivity> GetActivityTypeList()
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        var tbllist = _db.MST_TypeActivity.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(y => y.DisplayOrder).ToList();
        //        return tbllist;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// Outcome Monitoring Presentation List for Admin to Modify
        ///// </summary>
        ///// <returns></returns>
        //public static List<MST_OMPresentation> GetOMPresentationMasterList()
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        var tbllist = _db.MST_OMPresentation.Where(x => x.IsDeleted == false).OrderBy(y => y.DisplayOrder).ToList();
        //        return tbllist;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// Outcome Monitoring Presentation List for Front End Users
        ///// </summary>
        ///// <returns></returns>
        //public static List<MST_OMPresentation> GetOMPresentationList()
        //{
        //    IPELEntities _db = new IPELEntities();
        //    try
        //    {
        //        var tbllist = _db.MST_OMPresentation.Where(x => x.FilePath != null && x.IsActive == true && x.IsDeleted == false).OrderBy(y => y.DisplayOrder).ToList();
        //        return tbllist;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //#endregion

        #region Date Formats Functions
        public static DateTime FormateDt(DateTime date)
        {
            return date.Date;
        }
        public static DateTime? TruncateTime(DateTime? original)
        {
            if (!original.HasValue) return null;
            return original.Value.Date;
        }
        public static string FormateDtMDY(string date)
        {
            string dt = "";
            if (!string.IsNullOrEmpty(date))
            {
                dt = Convert.ToDateTime(date).ToString("MM-dd-yyyy");
            }
            return dt;
        }
        public static string FormateDtDMY(string date)
        {
            string dt = "";
            if (!string.IsNullOrEmpty(date))
            {
                dt = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
            }
            return dt;
        }
        public static string FormateDtMY(DateTime date)
        {
            string dt = "";
            if (date != null)
            {
                string m = date.Month.ToString();
                string y = date.Year.ToString();
                dt = Convert.ToDateTime(date).ToString("MMM-yyyy");
            }
            return dt;
        }
        #endregion

        #region Sending Email
        //public static string SendMail(string To, string Subject, string Body, string ReceiverName, string SenderName)
        //{
        //    try
        //    {
        //        string bodydata = string.Empty;
        //        string bodyTemplate = string.Empty;
        //        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Views/Shared/MailTemplate.html")))
        //        {
        //            bodyTemplate = reader.ReadToEnd();
        //        }

        //        MailMessage mail = new MailMessage();
        //        //mail.To.Add("bindu@careindia.org");
        //        mail.To.Add(To);
        //        mail.From = new MailAddress("careindiabtsp@gmail.com");
        //        mail.Subject = Subject + " ( " + SenderName + " )";
        //        //mail.Body = Body;
        //        bodydata = bodyTemplate.Replace("{Dearusername}", ReceiverName).Replace("{bodytext}", Body).Replace("{newusername}", SenderName);
        //        //bodydata = bodyTemplate.Replace("{bodytext}", Body);
        //        mail.Body = bodydata;
        //        mail.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.Port = 587;
        //        smtp.UseDefaultCredentials = false;
        //        smtp.Credentials = new System.Net.NetworkCredential("careindiabtsp@gmail.com", "gupczsbvzinhivzw");//Pasw-Care@321 // Enter seders User name and password       
        //        smtp.EnableSsl = true;
        //        smtp.Send(mail);
        //        tbl_SendMail tbl = new tbl_SendMail();
        //        tbl.Id = Guid.NewGuid();
        //        tbl.MTo = To;
        //        tbl.MFrom = "careindiabtsp@gmail.com";
        //        tbl.Subject = Subject + " ( " + SenderName + " )";
        //        tbl.Boby = bodydata;
        //        tbl.ReceiverName = ReceiverName;
        //        tbl.SenderName = SenderName;
        //        tbl.IsSented = true;
        //        tbl.CreatedBy = CommonModel.User.Id;
        //        tbl.CreatedOn = DateTime.Now;
        //        dbe.tbl_SendMail.Add(tbl);
        //        dbe.SaveChanges();
        //        return "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        tbl_SendMail tbl = new tbl_SendMail();
        //        tbl.Id = Guid.NewGuid();
        //        tbl.MTo = To;
        //        tbl.MFrom = "careindiabtsp@gmail.com";
        //        tbl.Subject = Subject + " ( " + SenderName + " )";
        //        tbl.Boby = Body;
        //        tbl.ReceiverName = ReceiverName;
        //        tbl.SenderName = SenderName;
        //        tbl.IsSented = false;
        //        dbe.tbl_SendMail.Add(tbl);
        //        dbe.SaveChanges();
        //        return "Error :" + ex.Message;
        //    }
        //}

        #endregion

        #region Payment Module Calucation Part CNRP && CM Monthly Incentive Payment
        public static int GetClaimApprove(int NoofPlan = 0, string TypeOfRole = "")
        {
            if (!string.IsNullOrWhiteSpace(TypeOfRole))
            {
                if (TypeOfRole == CommonModel.RoleNameCont.CNRP)
                {
                    var am = Convert.ToInt32(Enums.eAmount.CNRMonthly) * NoofPlan;
                    return am;
                }
                if (TypeOfRole == CommonModel.RoleNameCont.CM)
                {
                    var am = Convert.ToInt32(Enums.eAmount.CMMonthly) * NoofPlan;
                    return am;
                }
            }
            return 0;
        }
        #endregion
        #region Payment Module Calucation Part CNRP && CM Monthly mobilization incentive Payment
        public static MobilizationAmount GetClaimMobilization()
        {
            MobilizationAmount mobn = new MobilizationAmount();
            mobn.CNRP = Convert.ToInt32(Enums.eAmount.CNPMobilization) * 1;
            mobn.CM = Convert.ToInt32(Enums.eAmount.CMMobilization) * 1;
            return mobn;
        }
        public class MobilizationAmount
        {
            public int CNRP { get; set; }
            public int CM { get; set; }
        }
        #endregion
        public static decimal ToDecimal(string str)
        {
            return Decimal.TryParse(str, out decimal res) ? res : 0M;
        }
        public class LevelConst
        {
            public const string Validate = "Validate";
            public const string Checked = "Checked";
            public const string Recommend = "Recommend";
        }
        public class RoleNameCont
        {
            public const string Admin = "Admin";
            public const string State = "State";
            public const string Viewer = "Viewer";
            public const string District = "District";
            public const string BPM = "BPM";
            public const string BPIU = "BPIU";
            public const string CLF = "CLF";//1st Level Parallel Cluster
            public const string MRP = "MRP-CLF";//2nd Level Parallel  Cluster
            public const string CC = "AC-CC";//3rd Level Parallel Cluster
            public const string CNRP = "CNRP";
            public const string CM = "CM";
        }
        public static class DispLevel
        {
            public const string District = "District";
            public const string Block = "Block";
            public const string Cluster = "Cluster";
            public const string Panchayat = "Panchayat";
            public const string VO = "Village Org.";
            public const string Activities = "Activities";
            public const string VOFull = "Village Organization";
            public const string NoOfSHG = "No of SHG";
            public const string NameOfSHGs = "SHG Names";
            public const string CMID = "CM";
            public const string Month = "Month";
            public const string Year = "Year";
            public const string FromDate = "From Date";
            public const string ToDate = "To Date";
            public const string Role = "Role";
            public const string Name = "Name";
            public const string Achieved = "Achieved";
        }
        public static class DisAchvlbl
        {
            public const string Name = "CNRP";//MeetingHeld
            public const string MeetingDate = "Meeting Conducted On";//MeetingHeld
            public const string Activities = "Activities";//\r\n
            public const string TotalNoofParticipant = "No of Plan";
            public const string NoofParticipant = "No of Participant";
            public const string AchvPlanDate = "Planning Month\r\nYear ";//
            public const string AchvPlanMRPRemark = "Remark";//\r\n
            public const string AchvPlanMRPDate = "MRP \r\nApproved\r\n Date";//\r\n
            public const string AchvPlanCCRemark = "Remark";//\r\n
            public const string AchvPlanBPIURemark = "Remark";//\r\n
            public const string AchvPlanCCDate = "CC \r\nApproved\r\n Date";//\r\n
            public const string ClaimAmount = "Claimed \r\nAmount";//\r\n
            public const string ApproveAmount = "Approved \r\n Amount";//\r\n
            public const string ApprovedByMRP = "Approved MRP";//\r\n
            public const string ApprovedByCC = "Approved CC";//\r\n
            public const string ApprovedBy3 = "Approved BPM";//\r\n
            public const string ApprovedDTMRP = "Validate MRP";//\r\n
            public const string ApprovedDTCC = "Checked CC";//\r\n
            public const string ApprovedD3 = "Approved BPM";//\r\n

        }
    }
}