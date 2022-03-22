using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PLD.Models;
using PLD.Models.IdentityExt;
using PLD.Properties;

namespace PLD
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            try
            {
                var email = new MailMessage(Convert.ToString(ConfigurationManager.AppSettings["MailFrom"]), message.Destination)
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                };

                var mailClient = new SmtpClient(Convert.ToString(ConfigurationManager.AppSettings["SmtpHost"]), Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]))
                {
                    Credentials = new NetworkCredential(
                            Convert.ToString(ConfigurationManager.AppSettings["SmtpUser"]),
                            Convert.ToString(ConfigurationManager.AppSettings["SmtpPass"])),
                    EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpSsl"])
                };

                return mailClient.SendMailAsync(email);
            }
            catch (Exception ex)
            {
                Logs.Log("--> EmailService Method: SendAsync :: --> EXCEPTION: " + ex.ToString(), true);
                throw ex;
            }
            // Plug in your email service here to send an email.
            //return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Conecte el servicio SMS aquí para enviar un mensaje de texto.
            return Task.FromResult(0);
        }
    }

    // Configure el administrador de usuarios de aplicación que se usa en esta aplicación. UserManager se define en ASP.NET Identity y se usa en la aplicación.
    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store) : base(store)
        {
            PasswordValidator = new CustomizePasswordValidation(Settings.Default.PASSWORD_REQUIRED_LENGTH); //NEW
        }

        //public ApplicationUserManager(IUserStore<ApplicationUser> store)
        //    : base(store)
        //{
        //}

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new CustomUserStore(context.Get<ApplicationDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // Configure validation logic for passwords
            /*manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = Settings.Default.PASSWORD_REQUIRED_LENGTH,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            }; */

            manager.PasswordValidator = new CustomizePasswordValidation(Settings.Default.PASSWORD_REQUIRED_LENGTH)
            {
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(30);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser, int>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.PasswordHasher = new ConfigurablePasswordHasher(); //Se modifica la forma de codificar la contraseña ARH
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    TokenLifespan = TimeSpan.FromHours(Convert.ToDouble(Settings.Default.IDENTITY_TOKEN_LIFE_SPAN))
                };
            }
            return manager;
        }

        public override async Task<IdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            if (await IsPreviousPassword(userId, newPassword))
            {
                return await Task.FromResult(IdentityResult.Failed(String.Format("Cambiar contraseña, no puedes reutilizar las últimas {0} contraseñas.", Settings.Default.PASSWORD_HISTORY_LIMIT)));
            }
            var result = await base.ChangePasswordAsync(userId, currentPassword, newPassword);

            if (result.Succeeded)
            {
                var store = Store as CustomUserStore;
                await store.AddToPreviousPasswordsAsync(await FindByIdAsync(userId), PasswordHasher.HashPassword(newPassword), DateTime.Now);
            }
            return result;
        }


        public override async Task<IdentityResult> ResetPasswordAsync(int userId, string token, string newPassword)
        {
            if (await IsPreviousPassword(userId, newPassword))
            {
                return await Task.FromResult(IdentityResult.Failed(String.Format("Cambiar contraseña, no puedes reutilizar las últimas {0} contraseñas.", Settings.Default.PASSWORD_HISTORY_LIMIT)));
            }
            var result = await base.ResetPasswordAsync(userId, token, newPassword);

            if (result.Succeeded)
            {
                var store = Store as CustomUserStore;
                await store.AddToPreviousPasswordsAsync(await FindByIdAsync(userId), PasswordHasher.HashPassword(newPassword), DateTime.Now);
            }
            return result;
        }


        private async Task<bool> IsPreviousPassword(int userId, string newPassword)
        {
            var user = await FindByIdAsync(userId);
            if (user.PreviousUserPasswords.OrderByDescending(x => x.CreateDate).
            Select(x => x.PasswordHash).Take(Settings.Default.PASSWORD_HISTORY_LIMIT).Where(x => PasswordHasher.VerifyHashedPassword(x, newPassword) != PasswordVerificationResult.Failed).
            Any())
            {
                return true;
            }
            return false;
        }
    }

    // Configure el administrador de inicios de sesión que se usa en esta aplicación.
    public class ApplicationRoleManager : RoleManager<CustomRole, int>
    {
        public ApplicationRoleManager(IRoleStore<CustomRole, int> roleStore) : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<CustomRole, int, CustomUserRole>(context.Get<ApplicationDbContext>()));
        }
    }


    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    /*
     * Clase para extender la funcionalidad de la validación de contraseñas
     */
}
