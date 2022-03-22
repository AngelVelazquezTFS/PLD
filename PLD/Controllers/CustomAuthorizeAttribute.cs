using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PLD.EF;
using PLD.Models;

namespace PLD.Controllers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string UserRole { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            AspNetUsers U = new AspNetUsers();
            bool Allowed = false;

            using (PLD.EF.DB_Entities db = new PLD.EF.DB_Entities())
            {
                bool isAuthorized = base.AuthorizeCore(httpContext);

                if (!isAuthorized)
                {
                    return false;
                }
                // Obtiene el Usuario que se Autentifico
                string CurrentUser = HttpContext.Current.User.Identity.Name;
                // Obtiene el parametros de la solicitud
                System.Web.Routing.RouteData rd = httpContext.Request.RequestContext.RouteData;
                string CurrentAction = rd.GetRequiredString("action");
                string CurrentController = rd.GetRequiredString("controller");

                // Obtiene el Usuario
                var usr = UserRolesExtends.GetInfoUsuario(HttpContext.Current.User);

                Allowed = db.sp_AccesoUsuario(usr.Id, CurrentAction, CurrentController).FirstOrDefault() ?? false;

                //if (CurrentAction != "Index" && CurrentController != "Home") {
                //    // Consulta si El Usuario tiene acceso a la "Accion" del "Controlador"
                //    Allowed = db.PermiteAccesoUsuario(usr.Id, CurrentAction, CurrentController).FirstOrDefault() ?? false;
                //} else {
                //    Allowed = usr.IdStatus == 1 ? true: false;
                //} 

                if (Allowed)
                {
                    U.UltimoAcceso = DateTime.Now;
                    db.SaveChanges();
                }
            }

            return Allowed;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string[] acceptedTypes = filterContext.HttpContext.Request.AcceptTypes;
                foreach (string type in acceptedTypes)
                {
                    if (type.Contains("html") || type == "*/*")
                    {
                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                            filterContext.Result = new PartialViewResult { ViewName = "AccessDeniedPartial" };
                        else
                            filterContext.Result = new ViewResult { ViewName = "AccessDenied" };
                        break;
                    }
                    else if (type.Contains("javascript"))
                    {
                        filterContext.Result = new JsonResult { Data = new { success = false, message = "Acceso Denegado." } };
                        break;
                    }
                    else if (type.Contains("xml"))
                    {
                        filterContext.Result = new HttpUnauthorizedResult(); //this will redirect to login page with forms auth you could instead serialize a custom xml payload and return here.
                    }
                }
            }
            else
            {
                //filterContext.HttpContext.Request.RequestContext = new Web.Routing.RequestContext();

                //filterContext.HttpContext.Response.Clear();
                //filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Account", Action = "Login" }));
                filterContext.HttpContext.Response.Redirect("~/Account/Login");
                //filterContext.Result = new RedirectResult("/Account/Login");
                //filterContext.Result = new HttpStatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }

    public class DeleteFileAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Flush();

            //convert the current filter context to file and get the file path
            string filePath = (filterContext.Result as FilePathResult).FileName;

            //delete the file after download
            System.IO.File.Delete(filePath);
        }
    }
}