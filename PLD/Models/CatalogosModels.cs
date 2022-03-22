using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLD.Models
{
    public class CatalogosModels
    {
        public static List<RegisterViewModel> Usuarios()
        {
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                //List<RegisterViewModel> Lista = new List<RegisterViewModel>();
                return db.AspNetUsers.Select(m => new RegisterViewModel
                {
                    Id = m.Id,
                    UserName = m.UserName,
                    Email = m.Email,
                    EmailConfirmed = m.EmailConfirmed,
                    Paterno = m.LastName,
                    //Materno = m.Materno,
                    Nombre = m.FirstName,
                    FechaAlta = m.FechaAlta,
                    FechaBaja = m.FechaBaja,
                    IdStatus = m.IdStatus,
                    UltimoAcceso = m.UltimoAcceso
                }).ToList();
            }
        }

        public static List<RegisterRolViewModel> Roles()
        {
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                //List<RegisterViewModel> Lista = new List<RegisterViewModel>();
                return db.AspNetRoles.Select(m => new RegisterRolViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Activo = m.Activo
                }).ToList();
            }
        }
    }

    public class SelectListModels
    {
        public static List<SelectListItem> PermisosRol(string IdRol)
        {
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                List<SelectListItem> Lista = new List<SelectListItem>();
                string[] PermisosRol;

                if (string.IsNullOrEmpty(IdRol))
                {
                    Lista = db.Permisos.Where(x => x.Activo == true).OrderBy(m => m.Nombre).Select(m => new SelectListItem { Text = m.Nombre, Value = m.IdMenu.ToString() }).ToList();
                }
                else
                {
                    //RolesUsuario = db.AspNetRoles.Where(x => x.AspNetUsers.Any(r => IdUsuario == r.Id)).Select(o => o.Id).ToArray();
                    PermisosRol = db.Permisos.Where(x => x.PermisosRoles.Any(r => IdRol == r.IdRole.ToString() && r.Activo == true)).Select(o => o.IdMenu.ToString()).ToArray();
                    Lista = db.Permisos.Where(x => x.Activo == true).OrderBy(m => m.Nombre).Select(m => new SelectListItem { Text = m.Nombre, Value = m.IdMenu.ToString(), Selected = (PermisosRol.Contains(m.IdMenu.ToString()) ? true : false) }).ToList();
                }
                return Lista;
            }
        }

        public static List<SelectListItem> RolesUsuario(string IdUsuario)
        {
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                List<SelectListItem> Lista = new List<SelectListItem>();
                string[] RolesUsuario;

                if (string.IsNullOrEmpty(IdUsuario))
                {
                    Lista = db.AspNetRoles.Where(x => x.Activo == true).OrderBy(m => m.Name).Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() }).ToList();
                }
                else
                {
                    //RolesUsuario = db.AspNetRoles.Where(x => x.AspNetUsers.Any(r => IdUsuario == r.Id)).Select(o => o.Id).ToArray();
                    RolesUsuario = db.AspNetRoles.Where(x => x.AspNetUserRoles.Any(r => IdUsuario == r.UserId.ToString() && r.Activo == true)).Select(o => o.Id.ToString()).ToArray();
                    Lista = db.AspNetRoles.Where(x => x.Activo == true).OrderBy(m => m.Name).Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString(), Selected = (RolesUsuario.Contains(m.Id.ToString()) ? true : false) }).ToList();
                }
                return Lista;
            }
        }
    }

    public class BitacoraModels
    {
        //public DateTime BIT_FE_FECHA { get; set; }
        //public int PRM_FL_CVE { get; set; }
        //public string BIT_DS_DESCRIPCION { get; set; }
        //public string BIT_DS_RESUMEN { get; set; }
        //public string USR_CL_CVE { get; set; }

        //public BitacoraModels()
        //{
        //    PRM_FL_CVE = 0;
        //    BIT_DS_DESCRIPCION = string.Empty;
        //    BIT_DS_RESUMEN = string.Empty;
        //    USR_CL_CVE = string.Empty;
        //}

        public static void guardaBitacora(int IdMenu, string Descripcion,  string Valores, string Usuario)
        {
            try
            {
                using (EF.DB_Entities db = new EF.DB_Entities())
                {
                    db.KBITACORA_AML.Add(new EF.KBITACORA_AML()
                    {
                        BIT_FE_FECHA = DateTime.Now,
                        PRM_FL_CVE = IdMenu,
                        BIT_DS_DESCRIPCION = Descripcion,
                        BIT_DS_RESUMEN = Valores,
                        USR_CL_CVE = Usuario
                    });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logs.Log("--> GuardaBítacora :: --> EXCEPTION: " + ex.ToString(), true);
            }
        }
    }

}