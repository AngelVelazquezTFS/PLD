using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PLD.Models.IdentityExt
{
    public class CustomizePasswordValidation : IIdentityValidator<string>
    {
        public bool RequireDigit { get; set; }
        public int RequiredLength { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireNonLetterOrDigit { get; set; }
        public bool RequireUppercase { get; set; }

        public CustomizePasswordValidation(int length)
        {
            RequiredLength = length;
        }

        public virtual Task<IdentityResult> ValidateAsync(string item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(item) || item.Length < RequiredLength)
            {
                errors.Add(String.Format(CultureInfo.CurrentCulture, "La longitud de la contraseña debe ser de {0} caracteres.", RequiredLength));
            }
            if (RequireNonLetterOrDigit && item.All(IsLetterOrDigit))
            {
                errors.Add("Las contraseñas deben tener al menos un carácter que no sea letra o dígito.");
            }
            if (RequireDigit && item.All(c => !IsDigit(c)))
            {
                errors.Add("La contraseña debe tener al menos un dígito ('0'-'9').");
            }
            if (RequireLowercase && item.All(c => !IsLower(c)))
            {
                errors.Add("La contraseña debe tener al menos una letra minúscula('a'-'z').");
            }
            if (RequireUppercase && item.All(c => !IsUpper(c)))
            {
                errors.Add("La contraseña debe tener al menos una letra mayúscula ('A'-'Z').");
            }
            if (errors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed(String.Join(" ", errors)));
        }

        public virtual bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        public virtual bool IsLower(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        public virtual bool IsUpper(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        public virtual bool IsLetterOrDigit(char c)
        {
            return IsUpper(c) || IsLower(c) || IsDigit(c);
        }
    }
}