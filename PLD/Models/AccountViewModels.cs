using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PLD.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "¿Recordar este explorador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        //[Required]
        //[Display(Name = "Correo electrónico")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordar cuenta?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "Id Usuario")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "A. Paterno")]
        public string Paterno { get; set; }

        [Display(Name = "A. Materno")]
        public string Materno { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Display(Name = "Último Acceso")]
        public DateTime? UltimoAcceso { get; set; }

        [Display(Name = "Fecha Alta")]
        public DateTime? FechaAlta { get; set; }

        [Display(Name = "Fecha Baja")]
        public DateTime? FechaBaja { get; set; }

        [Display(Name = "Usuario Alta")]
        public string UsuarioAlta { get; set; }

        [Display(Name = "Usuario Baja")]
        public string UsuarioBaja { get; set; }

        [Display(Name = "Email Confirmado")]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "Estatus")]
        public int IdStatus { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 10)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Contraseña")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirmar Contraseña")]
        //[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Roles de Usuario")]
        public IList<SelectListItem> SelectedRoles { get; set; }
    }

    public class RegisterRolViewModel
    {
        [Display(Name = "IdRol")]
        public int Id { get; set; }

        [Display(Name = "Descripcion")]
        public string Name { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        [Display(Name = "Permisos")]
        public IList<SelectListItem> SelectedPermisos { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }

    public class PermisosModels
    {
        public int IdMenu { get; set; }
        public string Accion { get; set; }
        public string Nombre { get; set; }
        public string Controlador { get; set; }
        public int IdMenuPadre { get; set; }
        public byte? Nivel { get; set; }
        public string Icono { get; set; }
        public bool Checked { get; set; }
        public virtual PermisosModels Parent { get; set; }
        public virtual List<PermisosModels> Children { get; set; }

    }

    public class DatosPersonales
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Materno { get; set; }
        public string Correo { get; set; }
        public string UserName { get; set; }
        public DateTime? FechaAlta { get; set; }
        public int IdStatus { get; set; }
        public bool? LockoutEnabled { get; set; }
    }


    public class UserRolesExtends
    {
        public static IdentityResult addToRoles(int userId, int[] rolesId, int usrAlta)
        {
            try
            {
                using (EF.DB_Entities db = new EF.DB_Entities())
                {
                    foreach (int rolId in rolesId)
                    {
                        db.AspNetUserRoles.Add(new EF.AspNetUserRoles()
                        {
                            UserId = userId,
                            RoleId = rolId,
                            FechaAlta = DateTime.Now,
                            UsuarioAlta = usrAlta,
                            FechaBaja = null,
                            UsuarioBaja = null,
                            Activo = true
                        });
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(ex.Message);
            }
            return IdentityResult.Success;
        }

        public static IdentityResult addToPermisos(int IdRol, int[] PermisosId, int usrAlta)
        {
            try
            {
                using (EF.DB_Entities db = new EF.DB_Entities())
                {
                    foreach (int MenuId in PermisosId)
                    {
                        db.PermisosRoles.Add(new EF.PermisosRoles()
                        {
                            IdMenu = MenuId,
                            IdRole = IdRol,
                            Activo = true
                        });
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(ex.Message);
            }
            return IdentityResult.Success;
        }

        public static IList<PermisosModels> MenuUsuario(System.Security.Principal.IPrincipal user)
        {
            var userId = user.Identity.GetUserId<int>();
            //IList<PermisosModels> Permisos = new List<PermisosModels>();
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                return db.sp_MenuUsuario(userId).Select(m => new PermisosModels
                {
                    IdMenu = m.IdMenu,
                    Nombre = m.Nombre,
                    Controlador = m.Controlador,
                    Accion = m.Accion,
                    IdMenuPadre = m.IdPadre,
                    Nivel = m.Nivel,
                    Icono = m.Icon
                }).ToList();
            }
        }

        public static IList<PermisosModels> getOpcionesMenu()
        {
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                return db.Permisos.Select(m => new PermisosModels
                {
                    IdMenu = m.IdMenu,
                    Nombre = m.Nombre,
                    Controlador = m.Controlador,
                    Accion = m.Accion,
                    IdMenuPadre = m.IdPadre.HasValue ? m.IdPadre.Value : 0,
                    Nivel = m.Nivel,
                    Icono = m.Icon
                }).ToList();
            }
        }

        //public static DateTime GetLastPasswordChangedDate(System.Security.Principal.IPrincipal user)

        //public static DatosPersonales GetInfoUsuario(int userId)
        public static DatosPersonales GetInfoUsuario(System.Security.Principal.IPrincipal user)
        {
            var userId = user.Identity.GetUserId<int>();
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                var usr = db.AspNetUsers.FirstOrDefault(m => m.Id == userId);
                return new DatosPersonales() { Id = usr.Id, Nombre = usr.FirstName, Apellidos = usr.LastName, Materno = usr.Materno, UserName = usr.UserName, Correo = usr.Email, FechaAlta = usr.FechaAlta, IdStatus = usr.IdStatus, LockoutEnabled = usr.LockoutEnabled };
            }
        }

        public static DateTime GetLastPasswordChangedDate(int userId)
        {
            //var userId = user.Identity.GetUserId<int>();
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                var usr = db.AspNetUsers.FirstOrDefault(m => m.Id == userId);
                return usr.PasswordHistory.OrderByDescending(x => x.CreateDate).Take(1).Select(x => x.CreateDate).FirstOrDefault();
            }
        }
    }

}
