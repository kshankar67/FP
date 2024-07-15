using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FP.Models;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using FP.Helpers;

namespace FP.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        FP_DBEntities db_ = new FP_DBEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Session["CUser"] = null;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    //return RedirectToLocal(returnUrl);
                    return RedirectToAction("BFYList", "Beneficiary");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register(string Roles = "", string id = "", string empid = "")
        {
            //B00E969C-54F6-4578-A84C-E3550AB6F73D//Admin
            RegisterViewModel model = new RegisterViewModel();
            model.Roles = Roles;
            if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(empid))
            {
                var tblu = db_.AspNetUsers.Find(id);
                var tblemp = db_.TBL_Emp.Find(Guid.Parse(empid));
                model.RoleID_fk = tblemp.RoleID_fk;
                model.UserID_fk = tblu.Id;
                model.MobileNo = tblemp.MobileNo;
                model.EmpID_pk = tblemp.EmpID_pk;
                model.DistrictId = tblemp.DistrictID;
                model.BlockId = tblemp.BlockID;
                //if (tblemp.CLFId_fk != null)
                //{
                //    var list = tblemp.CLFId_fk.Split(',');
                //    foreach (var item in list)
                //    {
                //        model.CLFId_fks.Add(item);
                //    }
                //}
                model.CLFId_fk = tblemp.CLFId_fk;
                model.PanchayatId = tblemp.PanchayatId;
                model.VOId_fk = tblemp.VOId_fk;
                model.NoOfSHG = tblemp.NoOfSHG;
                model.NameOfSHGs = tblemp.NameOfSHGs;
                model.VillageName = tblemp.VillageName;
                model.EmpName = tblemp.EmpName;
            }
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            FP_DBEntities dbe = new FP_DBEntities();
            int res = 0; var u_n = "";
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(model.UserID_fk) && model.EmpID_pk != Guid.Empty)
                {
                    var tbLu = dbe.AspNetUsers.Find(model.UserID_fk);
                    var tbLe = dbe.TBL_Emp.Find(model.EmpID_pk);

                    tbLe.DistrictID = model.DistrictId;
                    tbLe.BlockID = model.BlockId;
                    //tbLe.CLFId_fk = model.CLFId_fks != null ? string.Join(",", model.CLFId_fks) : null;
                    tbLe.CLFId_fk = model.CLFId_fk;
                    tbLe.PanchayatId = model.PanchayatId;
                    tbLe.VOId_fk = model.VOId_fk;
                    tbLe.NoOfSHG = model.NoOfSHG;
                    tbLe.NameOfSHGs = model.NameOfSHGs;

                    tbLe.EmpName = model.EmpName;
                    tbLe.Gender = model.Gender;
                    tbLe.MobileNo = model.MobileNo;
                    tbLe.VillageName = model.VillageName;
                    tbLe.UpdatedBy = MvcApplication.CUser.Id;
                    tbLe.UpdatedOn = DateTime.Now;
                    tbLu.Email = model.MobileNo + "@gmail.com";
                    tbLu.PhoneNumber = model.MobileNo.Trim();
                    var Passwordh = !string.IsNullOrEmpty(model.Password) ? model.Password : model.MobileNo.Trim();
                    var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();
                    model.Password = passwordHasher.HashPassword(Passwordh);
                    var tblrole_name = dbe.AspNetRoles.Where(x => x.Id == model.Roles)?.FirstOrDefault().Name;
                    if (FP.Manager.CommonModel.RoleNameCont.State == tblrole_name
                        || FP.Manager.CommonModel.RoleNameCont.Admin == tblrole_name
                        || FP.Manager.CommonModel.RoleNameCont.Viewer == tblrole_name)
                    {
                        tbLu.UserName = model.EmpName.Trim();
                    }
                    else
                    {
                        tbLu.UserName = model.MobileNo.Trim();
                    }
                    res = dbe.SaveChanges();

                    if (model.CLFId_fks != null && model.CLFId_fks.Count() > 0)
                    {
                        var aspid = Guid.Parse(tbLu.Id);
                        var tblclf = dbe.tbl_CLF_Emp.Where(x => x.UserId_fk == aspid && x.EmpId_fk == tbLe.EmpID_pk);
                        dbe.tbl_CLF_Emp.AddRange(tblclf);
                        dbe.SaveChanges();
                        foreach (var item in model.CLFId_fks.ToList())
                        {
                            if (!string.IsNullOrWhiteSpace(item))
                            {
                                tbl_CLF_Emp tblclfemp = new tbl_CLF_Emp();
                                tblclfemp.EmpId_fk = tbLe.EmpID_pk;
                                tblclfemp.UserId_fk = Guid.Parse(tbLu.Id);
                                tblclfemp.CLFIdfk = Convert.ToInt32(item);
                                dbe.tbl_CLF_Emp.Add(tblclfemp);
                                dbe.SaveChanges();
                            }
                        }
                    }
                    if (res > 0)
                    {
                        return RedirectToAction("UserDetaillist", "Master");
                    }
                }
                else
                {
                  
                    var tblrole_name = dbe.AspNetRoles.Where(x=>x.Id==model.Roles)?.FirstOrDefault().Name;
                    if (FP.Manager.CommonModel.RoleNameCont.State== tblrole_name 
                        || FP.Manager.CommonModel.RoleNameCont.Admin == tblrole_name
                        || FP.Manager.CommonModel.RoleNameCont.Viewer == tblrole_name)
                    {
                        u_n = model.EmpName.Trim();
                    }
                    else
                    {
                        u_n = model.MobileNo.Trim();
                    }
                    var user = new ApplicationUser { PhoneNumber = model.MobileNo.Trim(), UserName = u_n, Email = model.MobileNo + "@gmail.com" };
                    model.Password = !string.IsNullOrEmpty(model.Password) ? model.Password : model.MobileNo.Trim();
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                        var rolename = db_.AspNetRoles.Find(model.Roles).Name;
                        var result1 = UserManager.AddToRole(user.Id, rolename);
                        if (!dbe.TBL_Emp.Any(x => x.MobileNo == user.PhoneNumber.Trim()))
                        {
                            TBL_Emp tbl = new TBL_Emp();
                            tbl.EmpID_pk = Guid.NewGuid();
                            tbl.UserID_fk = user.Id;
                            tbl.RoleID_fk = model.Roles;
                            tbl.DistrictID = model.DistrictId;
                            tbl.BlockID = model.BlockId;
                            //tbl.CLFId_fk = model.CLFId_fks != null ? string.Join(",", model.CLFId_fks) : null;
                            tbl.CLFId_fk = model.CLFId_fk != null ? model.CLFId_fk : null;
                            tbl.PanchayatId = model.PanchayatId;
                            tbl.VOId_fk = model.VOId_fk;
                            tbl.NoOfSHG = model.NoOfSHG;
                            tbl.NameOfSHGs = model.NameOfSHGs;
                            tbl.EmpName = model.EmpName.Trim();
                            tbl.VillageName = model.VillageName;
                            tbl.Gender = model.Gender;
                            tbl.MobileNo = model.MobileNo.Trim();
                            tbl.IsActive = true;
                            tbl.CreatedBy = MvcApplication.CUser.Id;
                            tbl.CreatedOn = DateTime.Now;
                            db_.TBL_Emp.Add(tbl);
                            res = db_.SaveChanges();

                            var ap = dbe.AspNetUsers.Find(user.Id);
                            ap.CreatedOn = DateTime.Now;
                            ap.Emp_ID = tbl.EmpID_pk;
                            //ap.PhoneNumber = model.MobileNo;
                            dbe.SaveChanges();

                            //if (model.CLFId_fks != null)
                            //{
                            //    foreach (var item in model.CLFId_fks.ToList())
                            //    {
                            //        if (!string.IsNullOrWhiteSpace(item))
                            //        {
                            //            tbl_CLF_Emp tblclfemp = new tbl_CLF_Emp();
                            //            tblclfemp.EmpId_fk = tbl.EmpID_pk;
                            //            tblclfemp.UserId_fk = Guid.Parse(user.Id);
                            //            tblclfemp.CLFIdfk = Convert.ToInt32(item);
                            //            dbe.tbl_CLF_Emp.Add(tblclfemp);
                            //            dbe.SaveChanges();
                            //        }
                            //    }
                            //}
                        }
                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        // return RedirectToAction("Index", "Home");
                        if (res > 0)
                        {
                            return RedirectToAction("UserDetaillist", "Master");
                        }
                    }
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}