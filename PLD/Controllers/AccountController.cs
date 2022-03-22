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
using System.Data;
using PLD.Models;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using PLD.Properties;
using System.Collections.Generic;
using PLD.EF;

namespace PLD.Controllers
{
    //[CustomAuthorize]
    public class AccountController : Controller
    {
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

        #region "Login"
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult TestControl()
        {

            ViewBag.Title = "Test Control";
            return View(CatalogosModels.Usuarios());
        }


        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //Buscamos usuario por nombre
                EF.AspNetUsers user = new EF.AspNetUsers();
                string message = string.Empty;



                using (EF.DB_Entities db = new EF.DB_Entities())
                {
                    if (!string.IsNullOrEmpty(model.Username))
                    {
                        user = db.AspNetUsers.FirstOrDefault(m => m.UserName == model.Username && m.EmailConfirmed == true);
                    }

                    if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                    {
                        // Don't reveal that the user does not exist or is not confirmed
                        message = string.Format("Usuario y/o contraseña incorrectos");
                        //ModelState.AddModelError("", "Usuario y/o contraseña incorrectos");
                    }
                    else
                    {

                        //Verificación de vigencia de contraseña
                        var LastPasswordChangedDate = UserRolesExtends.GetLastPasswordChangedDate(user.Id);
                        TimeSpan ts = DateTime.Today - LastPasswordChangedDate;
                        if (ts.TotalDays > Settings.Default.PASSWORD_VIGENCIA)
                        {
                            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                            return Redirect(callbackUrl);
                            //return RedirectToAction("ChangePassword", "Manage", ViewData = new ViewDataDictionary() { { "errorMessage", "Vigencia de contraseña expirada, favor de crear una nueva." } });
                        }

                        //Verificamos acceso
                        var validCredentials = await UserManager.FindAsync(model.Username, model.Password);
                        //bool boolEmail = await UserManager.IsEmailConfirmedAsync(user.Id);
                        //Se verifica si el usuario está bloqueado. Si es así sele muestra un mensaje indicando el tiempo por el que estará bloqueado
                        if (await UserManager.IsLockedOutAsync(user.Id))
                        {
                            message = string.Format("La cuenta ha sido bloqueada por {0} minutos.", UserManager.DefaultAccountLockoutTimeSpan.ToString());
                            //ModelState.AddModelError("", string.Format("La cuenta ha sido bloqueada por {0} minutos.", UserManager.DefaultAccountLockoutTimeSpan.ToString()));
                        }
                        //Si el usuario está sujeto a bloqueo e ingresÓ usuario o password de manera incorrecta
                        else if (await UserManager.GetLockoutEnabledAsync(user.Id) && validCredentials == null)
                        {
                            //Se registra en el contador de accesos fallidos
                            await UserManager.AccessFailedAsync(user.Id);
                            //Si cumplió con el límite de accesos y se registro el bloqueo
                            if (await UserManager.IsLockedOutAsync(user.Id))
                            {
                                message = string.Format("La cuenta ha sido bloqueada por {0} minutos.", UserManager.DefaultAccountLockoutTimeSpan.ToString());
                                //ModelState.AddModelError("", string.Format("La cuenta ha sido bloqueada por {0} minutos.", UserManager.DefaultAccountLockoutTimeSpan.ToString()));
                            }
                            else
                            {
                                //Contador de accesos fallidos
                                int accessFailedCount = await UserManager.GetAccessFailedCountAsync(user.Id);
                                //Contador de oportunidades
                                int attemptsLeft = UserManager.MaxFailedAccessAttemptsBeforeLockout - accessFailedCount;
                                message = string.Format("Usuario y/o contraseña incorrectos");
                                //ModelState.AddModelError("", "Usuario y/o contraseña incorrectos");
                            }
                        }
                        else if (validCredentials == null)
                        {
                            message = string.Format("Usuario y/o contraseña incorrectos");
                            //ModelState.AddModelError("", "Usuario y/o contraseña incorrectos");
                        }
                        //Loguear al usuario
                        if (string.IsNullOrWhiteSpace(message))
                        {
                            await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: true);
                            //Se reinicia el contador de accesos fallidos cuando el usuario accede correctamente
                            await UserManager.ResetAccessFailedCountAsync(user.Id);
                            user.UltimoAcceso = DateTime.Now;
                            db.SaveChanges();
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    ModelState.AddModelError("", message);
                }

            }

            return View(model);
            /*if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
            */
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Requerir que el usuario haya iniciado sesión con nombre de usuario y contraseña o inicio de sesión externo
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

            // El código siguiente protege de los ataques por fuerza bruta a los códigos de dos factores. 
            // Si un usuario introduce códigos incorrectos durante un intervalo especificado de tiempo, la cuenta del usuario 
            // se bloqueará durante un período de tiempo especificado. 
            // Puede configurar el bloqueo de la cuenta en IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Código no válido.");
                    return View(model);
            }
        }

        private string VerificaUSer(RegisterViewModel model)
        {
            string cambios = string.Empty;
            ApplicationUser user;
            user = UserManager.FindById(model.Id);

            if (user.FirstName.ToString() != model.Nombre.ToString())
                cambios += "NombreAnt:" + user.FirstName.ToString() + "; Nombre:" + model.Nombre.ToString().ToUpper() + "; ";
            if (user.LastName.ToString() != model.Paterno.ToString())
                cambios += "PaternoAnt:" + user.LastName.ToString() + "; Paterno:" + model.Paterno.ToString().ToUpper() + "; ";
            //if (user.Materno.ToString() != model.Materno.ToString())
            //    cambios += "Materno:" + user.Materno.ToString() + "MaternoAnt:" + model.Materno.ToString();
            if (user.Email.ToString() != model.Email.ToString())
                cambios += "EmailAnt:" + user.Email.ToString() + "; Email:" + model.Email.ToString().ToUpper() + "; ";

            return cambios;
        }

        private string strPermisos(int[] rolesId)
        {
            string permisos = "";
            int cont = 0;
            foreach (int rolId in rolesId)
            {
                if (cont == 0)
                {
                    permisos = "Roles: ";
                }
                cont++;
                permisos += rolId + ", ";
            }

            return permisos;
        }

        // GET: /Account/DesbloquearAcceso
        public ActionResult DesbloquearAcceso(string username)
        {
            ResultadoModels R = new ResultadoModels();
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    using (EF.DB_Entities db = new EF.DB_Entities())
                    {
                        EF.AspNetUsers usuario = db.AspNetUsers.Where(m => m.UserName == username).FirstOrDefault();
                        if (usuario.LockoutEndDateUtc == null)
                        {
                            R = new ResultadoModels { NotifyMsg = "El usuario NO tiene el acceso bloqueado", NotifyType = Enums.eNotify_Type.warning };
                        }
                        else
                        {
                            usuario.LockoutEndDateUtc = null;
                            usuario.AccessFailedCount = 0;
                            db.SaveChanges();
                            R = new ResultadoModels { NotifyMsg = "El acceso del usuario " + username + " ha sido desbloqueado.", NotifyType = Enums.eNotify_Type.success };
                        }
                    }
                }
                else
                {
                    R = new ResultadoModels { NotifyMsg = "Usuario no válido", NotifyType = Enums.eNotify_Type.success };
                }
            }
            catch (Exception ex)
            {
                Logs.Log("--> DesbloquearAcceso :: " + username + " --> EXCEPTION: " + ex.Message, true);
                R = new ResultadoModels { NotifyMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message, NotifyType = Enums.eNotify_Type.danger };
            }
            return PartialView("_Resultado", R);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResendEmailConfirmation(int userId)
        {
            ResultadoModels R = new ResultadoModels();
            try
            {
                ApplicationUser user = UserManager.FindById(userId);
                if (!user.EmailConfirmed)
                {
                    string password = string.Empty;
                    using (EF.DB_Entities db = new EF.DB_Entities())
                    {
                        password = Utilities.ObtenerString(db.sp_PasswordDec(user.PasswordHash));
                    }

                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "PLD :: Acceso", "Se reenvian los datos de accesos de " + string.Concat(user.FirstName, " ", user.LastName) + " <br/><br/>Usuario: <strong>" + user.UserName + "</strong><br/>Contraseña: <strong>" + password + "</strong><br/><br/> <h3>Da clic en el siguiente enlace para activar tu cuenta y poder ingresar al sistema: <a style='color:##D71921;' href=\"" + callbackUrl + "\">¡CONFIRMAR!</a></h3> <p style='color:red;'>Este link vence en " + Settings.Default.IDENTITY_TOKEN_LIFE_SPAN + " horas.</p>");

                    R = new ResultadoModels { NotifyMsg = "Se ha reenviado correo para confirmación de la cuenta: " + user.UserName, NotifyType = Enums.eNotify_Type.success };
                }
            }
            catch (Exception ex)
            {
                Logs.Log("--> ResendEmailConfirmation :: --> EXCEPTION: " + ex.ToString(), true);
                R = new ResultadoModels { NotifyMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message, NotifyType = Enums.eNotify_Type.danger };

            }
            return PartialView("_Resultado", R);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            if (userId == default(int) || code == null)
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
                //var user = await UserManager.FindByNameAsync(model.Email);
                var user = await UserManager.FindByNameAsync(model.Username);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);



                //MailMessage mail = new MailMessage();
                //mail.To.Add("ing.christian.velazquez@gmail.com");
                //mail.From = new MailAddress("servicioalcliente@toyota.com");
                //mail.Subject = "Soporte TFSM :: Cambio de Contraseña de " + string.Concat(user.FirstName, " ", user.LastName);

                //mail.Body = " Por favor ingresa <a href=\"" + callbackUrl + "\">Aqui</a> para cambiar tu contraseña";

                //mail.IsBodyHtml = true;

                //SmtpClient smtp = new SmtpClient("10.60.17.155", 28);
                //smtp.Credentials = new NetworkCredential { UserName = "", Password = "" };
                //smtp.Send(mail);

                ////Or your Smtp Email ID and Password
                //smtp.EnableSsl = true;
                //smtp.Send(mail);
                //await UserManager.SendEmailAsync(user.Id, "AppBuroC:: Cambio de Contraseña de " + string.Concat(user.FirstName, " ", user.LastName), "Por favor ingresa <a href=\"" + callbackUrl + "\">Aqui</a> para cambiar tu contraseña");
                // Send an email with this link
                await UserManager.SendEmailAsync(user.Id, "PLD :: Reestablecer Contraseña", "Usuario: " + user.UserName + "<br/><br/>Por favor reestablece tu contraseña dando clic <a href=\"" + callbackUrl + "\"><strong>aquí</strong></a>  ");

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
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
        public ActionResult ResetPassword(int userId, string code)
        {
            if (UserManager.VerifyUserToken(userId, "ResetPassword", code))
            {
                var user = UserManager.FindById(userId);
                // If the password request is valid, displays the password reset form
                var model = new ResetPasswordViewModel
                {
                    Code = code,
                    UserName = user.UserName
                };

                return View(model);
            }
            else
            {
                return View("Error", new HandleErrorInfo(new Exception("Token no válido."), "Account", "ResetPassword"));
            }
            //return code == null ? View("Error", new HandleErrorInfo(new Exception("Token no válido."), "Account", "ResetPassword")) : View();                      
        }

        ////
        //// POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await UserManager.FindByNameAsync(model.Email);
        //    if (user == null)
        //    {
        //        // No revelar que el usuario no existe
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    AddErrors(result);
        //    return View();
        //}

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
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                await UserManager.SendEmailAsync(user.Id, "PLD :: Contraseña Actualizada", "La contraseña de la cuenta <strong>" + user.UserName + "</strong> ha sido actualizada.<br/><br/>Nueva Contraseña: <strong>" + model.Password + "</strong><br/>");
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
            // Solicitar redireccionamiento al proveedor de inicio de sesión externo
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == default(int))
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

            // Generar el token y enviarlo
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

            // Si el usuario ya tiene un inicio de sesión, iniciar sesión del usuario con este proveedor de inicio de sesión externo
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
                    // Si el usuario no tiene ninguna cuenta, solicitar que cree una
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
                // Obtener datos del usuario del proveedor de inicio de sesión externo
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        #endregion

        #region "Roles"
        

        // GET: /Account/RolesList
        public ActionResult ListaRoles()
        {
            ViewBag.Title = "Registrar Rol";
            return PartialView(CatalogosModels.Roles());
            //return Json(CatalogosModels.Roles(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPermisos()
        {
            List<PermisosModels> Permisos;
            List<Models.DTO.PermisosModels> Registros;

            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                //List<RegisterViewModel> Lista = new List<RegisterViewModel>();
                Permisos = db.Permisos.Select(m => new PermisosModels
                {
                    IdMenu = m.IdMenu,
                    Accion = m.Accion,
                    Nombre = m.Nombre,
                    Controlador = m.Controlador,
                    IdMenuPadre = m.IdPadre.HasValue ? m.IdPadre.Value : 0,
                    Nivel = m.Nivel
                }).ToList();

                Registros = Permisos.Where(l => l.IdMenuPadre == 0).Select(n => new PLD.Models.DTO.PermisosModels
                {
                    id = n.IdMenu,
                    text = n.Nombre,
                    accion = n.Accion,
                    @checked = n.Checked,
                    controlador = n.Controlador,
                    children = GetChildren(Permisos, n.IdMenu),
                    nivel = n.Nivel
                }).ToList();
            }

            return this.Json(Registros, JsonRequestBehavior.AllowGet);
        }

        private List<PLD.Models.DTO.PermisosModels> GetChildren(List<PermisosModels> Permisos, int parentId)
        {
            return Permisos.Where(l => l.IdMenuPadre == parentId).Select(l => new PLD.Models.DTO.PermisosModels
            {
                id = l.IdMenu,
                accion = l.Accion,
                text = l.Nombre,
                @checked = l.Checked,
                controlador = l.Controlador,
                children = GetChildren(Permisos, l.IdMenu),
                nivel = l.Nivel
            }).ToList();
        }

        public ActionResult RegisterRol(string IdRol)
        {
            RegisterRolViewModel Rol;
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                if (!string.IsNullOrEmpty(IdRol))
                {
                    Rol = db.AspNetRoles.Where(m => m.Id == Convert.ToInt32(IdRol)).Select(m => new RegisterRolViewModel
                    {
                        Id = m.Id,
                        Name = m.Name
                    }).Single();

                    Rol.SelectedPermisos = SelectListModels.PermisosRol(IdRol);

                    //ViewBag.TitleRegEdit = "Editar Usuario: " + Usuario.UserName;
                }
                else
                {
                    //Usuario = new RegisterViewModel { UserName = username };
                    ViewBag.TitleRegEdit = "Registrar Usuario";
                }
            }
            return PartialView("Register");
        }

        [HttpPost]
        public ActionResult SaveCheckedNodes(List<int> checkedIds, string descripcion)
        {
            ViewBag.TitleRegEdit = "Registrar Perfil";
            string Permisos = string.Empty;
            ResultadoModels R = new ResultadoModels();
            try
            {
                if (ModelState.IsValid)
                {

                    if (checkedIds == null)
                    {
                        checkedIds = new List<int>();
                    }
                    using (DB_Entities db = new DB_Entities())
                    {
                        var AspNetRole = new AspNetRoles()
                        {
                            Name = descripcion,
                            Activo = true
                        };

                        db.AspNetRoles.Add(AspNetRole);
                        db.SaveChanges();

                        int idRol = AspNetRole.Id;

                        var PermisosModels = db.PermisosRoles.ToList();
                        foreach (var PermisoModel in PermisosModels)
                        {
                            if (checkedIds.Contains(PermisoModel.IdMenu))
                            {
                                db.PermisosRoles.Add(new EF.PermisosRoles()
                                {
                                    IdMenu = PermisoModel.IdMenu,
                                    IdRole = idRol,
                                    Activo = true
                                });
                            }

                        }
                        db.SaveChanges();
                    }
                }
                else
                {
                    R.ModelState = ModelState.Values.Where(e => e.Errors.Count() > 0).ToList();
                    //R = new ResultadoModels { NotifyType = Enums.eNotify_Type.warning };
                }

            }
            catch (Exception ex)
            {
                Logs.Log("--> Register :: --> EXCEPTION: " + ex.ToString(), true);
                R = new ResultadoModels { NotifyMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message, NotifyType = Enums.eNotify_Type.danger };
            }

            return PartialView("_Resultado", R);
        }


        #endregion

        #region "Usuario"

        // GET: /Account/UsersList
        public ActionResult ListaUsuarios()
        {
            ViewBag.Title = "Registrar Usuario";
            return PartialView(CatalogosModels.Usuarios());
        }

        //
        // GET: /Account/EditarUsuario
        public ActionResult Register(string username)
        {
            RegisterViewModel Usuario;
            string pass = System.Web.Security.Membership.GeneratePassword(10, 2);
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                if (!string.IsNullOrEmpty(username))
                {
                    Usuario = db.AspNetUsers.Where(m => m.UserName == username).Select(m => new RegisterViewModel
                    {
                        Id = m.Id,
                        UserName = m.UserName,
                        Email = m.Email,
                        EmailConfirmed = m.EmailConfirmed,
                        Nombre = m.FirstName,
                        Paterno = m.LastName,
                        Materno = m.Materno,
                        UltimoAcceso = m.UltimoAcceso,
                        Password = m.PasswordHash,
                        IdStatus = m.IdStatus
                        //Password = db.getPasswordDec(m.PasswordHash),
                        //ConfirmPassword = db.getPasswordDec(m.PasswordHash)
                    }).Single();

                    //Usuario.Password = db.getPasswordDec(Usuario.Password);
                    //Usuario.ConfirmPassword = Usuario.Password;

                    Usuario.SelectedRoles = SelectListModels.RolesUsuario(Usuario.Id.ToString());

                    ViewBag.TitleRegEdit = "Editar Usuario: " + Usuario.UserName;
                }
                else
                {
                    Usuario = new RegisterViewModel { UserName = username };
                    ViewBag.TitleRegEdit = "Registrar Usuario";
                }
            }
            return PartialView("Register", Usuario);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            ViewBag.TitleRegEdit = "Registrar Usuario";
            string Permisos = string.Empty;
            ResultadoModels R = new ResultadoModels();
            if (ModelState.IsValid)
            {
                try
                {

                    /*
                     * 0 = USUARIO NUEVO
                     * != 0 USUARIO EXISTENTE
                     */
                    if (model.Id == 0)
                    {

                        //Generamos una contraseña aleatoria con Membership
                        model.Password = System.Web.Security.Membership.GeneratePassword(Settings.Default.PASSWORD_REQUIRED_LENGTH, 2) + "xU" + new Random().Next(10);
                        model.ConfirmPassword = model.Password;

                        var user = new ApplicationUser
                        {
                            UserName = model.UserName.ToUpper().Trim(),
                            Email = model.Email.Trim().ToLower(),
                            FirstName = model.Nombre.ToUpper(),
                            LastName = model.Paterno.ToUpper(),
                            //Materno = model.Materno.ToUpper(),
                            FechaAlta = DateTime.Now,
                            UsuarioAlta = User.Identity.GetUserId<int>(),
                            FechaBaja = null,
                            UltimoAcceso = DateTime.Now,
                            IdStatus = model.IdStatus
                        };

                        var result = await UserManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            // Get user roles
                            string[] allUserRoles = UserManager.GetRoles(user.Id).ToArray();
                            UserManager.RemoveFromRoles(user.Id, allUserRoles);

                            // Add all of the roles returned from the webpage
                            if (model.SelectedRoles.Count(m => m.Selected == true) > 0)
                            {
                                UserRolesExtends.addToRoles(user.Id, model.SelectedRoles.Where(m => m.Selected == true).Select(m => int.Parse(m.Value)).ToArray(), User.Identity.GetUserId<int>());
                                //UserManager.AddToRoles(user.Id, model.SelectedRoles.Where(m => m.Selected == true).Select(m => m.Text).ToArray());
                                Permisos = strPermisos(model.SelectedRoles.Where(m => m.Selected == true).Select(m => int.Parse(m.Value)).ToArray());

                            }

                            //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                            // Send an email with this link
                            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                            await UserManager.SendEmailAsync(user.Id, "PLD :: Acceso", "Se ha creado el usuario para " + string.Concat(user.FirstName, " ", user.LastName) + " <br/><br/>Usuario: <strong>" + user.UserName.ToUpper() + "</strong><br/>Contraseña: <strong>" + model.Password + "</strong><br/><br/> <h3>Da clic en el siguiente enlace para activar tu cuenta y poder ingresar al sistema: <a style='color:##D71921;' href=\"" + callbackUrl + "\">¡CONFIRMAR!</a></h3> <p style='color:red;'>Este link vence en " + Settings.Default.IDENTITY_TOKEN_LIFE_SPAN + " horas.</p>");

                            string valores = "Usuario: " + user.UserName + ";Nombre: " + user.FirstName + " " + user.LastName + ";" + "Email:" + user.Email;
                            BitacoraModels.guardaBitacora(6, "Se registró Usuario", valores + " " + Permisos, User.Identity.GetUserName());

                            //R = new ResultadoModels { NotifyMsg = user.FirstName + " " + user.LastName + " ha sido registrado", NotifyType = Enums.eNotify_Type.success };
                            R = new ResultadoModels { NotifyMsg = "El usuario " + user.UserName + " ha sido registrado.", NotifyType = Enums.eNotify_Type.success };
                            //return RedirectToAction("ListaUsuarios", "Account");
                        }
                        else
                        {
                            AddErrors(result);
                            R.ModelState = ModelState.Values.Where(e => e.Errors.Count() > 0).ToList();
                            //R = new ResultadoModels { NotifyType = Enums.eNotify_Type.warning };
                        }
                    }
                    else
                    {

                        //Obtenemos lo que se actualizó de la tabla usuario
                        string valores = string.Empty;
                        valores = VerificaUSer(model);
                        if (valores == string.Empty)
                            valores = "No se actualizaron datos";

                        ApplicationUser user;



                        user = UserManager.FindById(model.Id);

                        string[] allselectRoles = UserManager.GetRoles(user.Id).ToArray();


                        user.UserName = model.UserName.Trim().ToUpper();
                        user.FirstName = model.Nombre.Trim().ToUpper();
                        user.LastName = model.Paterno.Trim().ToUpper();
                        //user.Materno = model.Materno.Trim().ToUpper();
                        user.Email = model.Email.Trim().ToLower();
                        user.IdStatus = model.IdStatus;
                        user.FechaBaja = model.IdStatus != 1 ? DateTime.Now : (DateTime?)null;
                        user.UsuarioBaja = model.IdStatus != 1 ? User.Identity.GetUserId<int>() : (int?)null;

                        user.PasswordHash = model.Password;
                        /*
                        using(EF.BuroCreditoEntities db = new EF.BuroCreditoEntities()){
                            user.PasswordHash = db.getPasswordEnc(model.Password);
                        }   
                        */


                        var result = await UserManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            // Get user roles
                            string[] allUserRoles = UserManager.GetRoles(user.Id).ToArray();
                            UserManager.RemoveFromRoles(user.Id, allUserRoles);

                            // Add all of the roles returned from the webpage
                            if (model.SelectedRoles.Count(m => m.Selected == true) > 0)
                            {
                                UserRolesExtends.addToRoles(user.Id, model.SelectedRoles.Where(m => m.Selected == true).Select(m => int.Parse(m.Value)).ToArray(), User.Identity.GetUserId<int>());
                                Permisos = strPermisos(model.SelectedRoles.Where(m => m.Selected == true).Select(m => int.Parse(m.Value)).ToArray());
                            }

                            // Send an email with this link
                            //await UserManager.SendEmailAsync(user.Id, "TFSM Buró Crédito :: Datos actualizados", "Se ha actualizado tu usuario " + string.Concat(user.FirstName, " ", user.LastName) + " <br/><br/>Usuario: <strong>" + user.UserName + "</strong><br/>Contraseña: <strong>" + model.Password + "</strong><br/><br/>");
                            await UserManager.SendEmailAsync(user.Id, "PLD :: Datos actualizados", "Se ha actualizado tu usuario " + string.Concat(user.FirstName, " ", user.LastName) + " <br/><br/>Usuario: <strong>" + user.UserName + "</strong><br/><br/>");

                            BitacoraModels.guardaBitacora(6, "Se actualizó usuario " + user.UserName, valores + " " + Permisos, User.Identity.GetUserName());

                            //R = new ResultadoModels { NotifyMsg = user.FirstName + " " + user.LastName + " ha sido actualizado", NotifyType = Enums.eNotify_Type.success };
                            R = new ResultadoModels { NotifyMsg = "El usuario " + user.UserName + " ha sido actualizado.", NotifyType = Enums.eNotify_Type.success };
                            //return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            AddErrors(result);
                            R.ModelState = ModelState.Values.Where(e => e.Errors.Count() > 0).ToList();
                            //R = new ResultadoModels { NotifyType = Enums.eNotify_Type.warning };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logs.Log("--> Register :: --> EXCEPTION: " + ex.ToString(), true);
                    R = new ResultadoModels { NotifyMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message, NotifyType = Enums.eNotify_Type.danger };
                }
            }
            else
            {
                R.ModelState = ModelState.Values.Where(e => e.Errors.Count() > 0).ToList();
            }
            return PartialView("_Resultado", R);
            // If we got this far, something failed, redisplay form
            //return View(model);
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertTest(RegisterViewModel model)
        {
            ViewBag.TitleRegEdit = "Registrar Usuario";
            ResultadoModels R = new ResultadoModels();
            if (ModelState.IsValid)
            {
                try
                {

                    /*
                     * 0 = USUARIO NUEVO
                     * != 0 USUARIO EXISTENTE
                     */
                    if (model.Id == 0)
                    {

                        //Generamos una contraseña aleatoria con Membership
                        model.Password = System.Web.Security.Membership.GeneratePassword(Settings.Default.PASSWORD_REQUIRED_LENGTH, 2) + "xU" + new Random().Next(10);
                        model.ConfirmPassword = model.Password;

                        var user = new ApplicationUser
                        {
                            UserName = model.UserName.ToUpper().Trim(),
                            Email = model.Email.Trim().ToLower(),
                            FirstName = model.Nombre.ToUpper(),
                            LastName = model.Paterno.ToUpper(),
                            //Materno = model.Materno.ToUpper(),
                            FechaAlta = DateTime.Now,
                            UsuarioAlta = User.Identity.GetUserId<int>(),
                            FechaBaja = null,
                            UltimoAcceso = DateTime.Now,
                            IdStatus = model.IdStatus
                        };

                        var result = await UserManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            // Get user roles
                            string[] allUserRoles = UserManager.GetRoles(user.Id).ToArray();
                            UserManager.RemoveFromRoles(user.Id, allUserRoles);

                            // Add all of the roles returned from the webpage
                            if (model.SelectedRoles.Count(m => m.Selected == true) > 0)
                            {
                                UserRolesExtends.addToRoles(user.Id, model.SelectedRoles.Where(m => m.Selected == true).Select(m => int.Parse(m.Value)).ToArray(), User.Identity.GetUserId<int>());
                                //UserManager.AddToRoles(user.Id, model.SelectedRoles.Where(m => m.Selected == true).Select(m => m.Text).ToArray());
                            }

                            //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                            // Send an email with this link
                            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                            await UserManager.SendEmailAsync(user.Id, "PLD :: Acceso", "Se ha creado el usuario para " + string.Concat(user.FirstName, " ", user.LastName) + " <br/><br/>Usuario: <strong>" + user.UserName.ToUpper() + "</strong><br/>Contraseña: <strong>" + model.Password + "</strong><br/><br/> <h3>Da clic en el siguiente enlace para activar tu cuenta y poder ingresar al sistema: <a style='color:##D71921;' href=\"" + callbackUrl + "\">¡CONFIRMAR!</a></h3> <p style='color:red;'>Este link vence en " + Settings.Default.IDENTITY_TOKEN_LIFE_SPAN + " horas.</p>");

                            //R = new ResultadoModels { NotifyMsg = user.FirstName + " " + user.LastName + " ha sido registrado", NotifyType = Enums.eNotify_Type.success };
                            R = new ResultadoModels { NotifyMsg = "El usuario " + user.UserName + " ha sido registrado.", NotifyType = Enums.eNotify_Type.success };
                            //return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            AddErrors(result);
                            R.ModelState = ModelState.Values.Where(e => e.Errors.Count() > 0).ToList();
                            //R = new ResultadoModels { NotifyType = Enums.eNotify_Type.warning };
                        }
                    }
                    else
                    {
                        ApplicationUser user;

                        user = UserManager.FindById(model.Id);
                        user.UserName = model.UserName.Trim().ToUpper();
                        user.FirstName = model.Nombre.Trim().ToUpper();
                        user.LastName = model.Paterno.Trim().ToUpper();
                        //user.Materno = model.Materno.Trim().ToUpper();
                        user.Email = model.Email.Trim().ToLower();
                        //user.UsuarioAlta = User.Identity.GetUserId<int>();
                        user.IdStatus = model.IdStatus;
                        user.FechaBaja = model.IdStatus != 1 ? DateTime.Now : (DateTime?)null;
                        user.UsuarioBaja = model.IdStatus != 1 ? User.Identity.GetUserId<int>() : (int?)null;

                        user.PasswordHash = model.Password;
                        /*
                        using(EF.BuroCreditoEntities db = new EF.BuroCreditoEntities()){
                            user.PasswordHash = db.getPasswordEnc(model.Password);
                        }   
                        */

                        var result = await UserManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            // Get user roles
                            string[] allUserRoles = UserManager.GetRoles(user.Id).ToArray();
                            UserManager.RemoveFromRoles(user.Id, allUserRoles);

                            // Add all of the roles returned from the webpage
                            if (model.SelectedRoles.Count(m => m.Selected == true) > 0)
                            {
                                UserRolesExtends.addToRoles(user.Id, model.SelectedRoles.Where(m => m.Selected == true).Select(m => int.Parse(m.Value)).ToArray(), User.Identity.GetUserId<int>());
                            }

                            // Send an email with this link
                            //await UserManager.SendEmailAsync(user.Id, "TFSM Buró Crédito :: Datos actualizados", "Se ha actualizado tu usuario " + string.Concat(user.FirstName, " ", user.LastName) + " <br/><br/>Usuario: <strong>" + user.UserName + "</strong><br/>Contraseña: <strong>" + model.Password + "</strong><br/><br/>");
                            await UserManager.SendEmailAsync(user.Id, "PLD :: Datos actualizados", "Se ha actualizado tu usuario " + string.Concat(user.FirstName, " ", user.LastName) + " <br/><br/>Usuario: <strong>" + user.UserName + "</strong><br/><br/>");

                            //R = new ResultadoModels { NotifyMsg = user.FirstName + " " + user.LastName + " ha sido actualizado", NotifyType = Enums.eNotify_Type.success };
                            R = new ResultadoModels { NotifyMsg = "El usuario " + user.UserName + " ha sido actualizado.", NotifyType = Enums.eNotify_Type.success };
                            //return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            AddErrors(result);
                            R.ModelState = ModelState.Values.Where(e => e.Errors.Count() > 0).ToList();
                            //R = new ResultadoModels { NotifyType = Enums.eNotify_Type.warning };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logs.Log("--> Register :: --> EXCEPTION: " + ex.ToString(), true);
                    R = new ResultadoModels { NotifyMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message, NotifyType = Enums.eNotify_Type.danger };
                }
            }
            else
            {
                R.ModelState = ModelState.Values.Where(e => e.Errors.Count() > 0).ToList();
            }
            return PartialView("_Resultado", R);
            // If we got this far, something failed, redisplay form
            //return View(model);
        }
        #endregion

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

        #region Aplicaciones auxiliares
        // Se usa para la protección XSRF al agregar inicios de sesión externos
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