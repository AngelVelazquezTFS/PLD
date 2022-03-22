using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;
using PLD.Properties;

namespace PLD.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        [Required]
        [StringLength(40)]
        public string FirstName{ get; set; }
        [Required]
        [StringLength(40)]
        public string LastName { get; set; }
        //[StringLength(40)]
        //public string Materno { get; set; }
        public int? UsuarioAlta { get; set; }
        public int? UsuarioBaja { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        [Required]
        public int IdStatus { get; set; }


        public ApplicationUser() : base()
        {
            PreviousUserPasswords = new List<PasswordHistory>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual IList<PasswordHistory> PreviousUserPasswords { get; set; }
    }

    [Table("PasswordHistory")]
    public class PasswordHistory
    {
        public PasswordHistory()
        {
            //CreateDate = DateTime.Now;
        }

        [Key, Column(Order = 0)]
        public string PasswordHash { get; set; }

        public DateTime CreateDate { get; set; }

        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }

    public class ConfigurablePasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                return Utilities.ObtenerString(db.sp_PasswordEnc(password));
            }
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                string R = Utilities.ObtenerString(db.sp_PasswordDec(hashedPassword));
                if (R.Equals(providedPassword))
                    return PasswordVerificationResult.Success;
                else
                    return PasswordVerificationResult.Failed;
            }
        }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("PLD_DBConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class CustomUserRole : IdentityUserRole<int> {
        public CustomUserRole() : base() {            
        }

        //public CustomUserRole(DateTime fechaAlta, DateTime fechaBaja, int usuarioAlta, int usuarioBaja, bool activo) : base() {
        //    FechaAlta = fechaAlta;
        //    FechaBaja = fechaBaja;
        //    UsuarioAlta = usuarioAlta;
        //    UsuarioBaja = usuarioBaja;
        //    Activo = activo;
        //}

        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public int? UsuarioAlta { get; set; }
        public int? UsuarioBaja { get; set; }
        public bool Activo { get; set; }

        //public CustomUserRole(ApplicationDbContext context) : base() {
        //}
    }
    public class CustomUserClaim : IdentityUserClaim<int> { }

    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        //public CustomRole() : base() { }
        //public CustomRole(string name, bool Activo) : base() {
        //    this.Name = name;
        //    this.Activo = Activo;
        //}
        //public bool Activo { get; set; }

        public CustomRole() { }
        public CustomRole(string name) { Name = name; }

        public bool Activo { get; set; }

    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task CreateAsync(ApplicationUser user)
        {
            await base.CreateAsync(user);

            await AddToPreviousPasswordsAsync(user, user.PasswordHash, DateTime.Now.AddMonths(-1 * Settings.Default.PASSWORD_VIGENCIA)); //El envío de la fecha obliga al cambio de contraseña al crear la cuenta
        }

        public Task AddToPreviousPasswordsAsync(ApplicationUser user, string password, DateTime createDate)
        {
            user.PreviousUserPasswords.Add(new PasswordHistory() { UserId = user.Id, PasswordHash = password, CreateDate = createDate });
            return UpdateAsync(user);
        }

    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context) : base(context)
        {
        }
    }

}