﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PLD.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DB_Entities : DbContext
    {
        public DB_Entities()
            : base("name=DB_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AccesosWS> AccesosWS { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CREMARK_PLANPISO_AML> CREMARK_PLANPISO_AML { get; set; }
        public virtual DbSet<Modulos> Modulos { get; set; }
        public virtual DbSet<PasswordHistory> PasswordHistory { get; set; }
        public virtual DbSet<Permisos> Permisos { get; set; }
        public virtual DbSet<PermisosRoles> PermisosRoles { get; set; }
        public virtual DbSet<KBITACORA_AML> KBITACORA_AML { get; set; }
        public virtual DbSet<KCONFIG_AML> KCONFIG_AML { get; set; }
    
        public virtual ObjectResult<Nullable<bool>> sp_AccesoUsuario(Nullable<int> idUsuario, string accion, string controlador)
        {
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("idUsuario", idUsuario) :
                new ObjectParameter("idUsuario", typeof(int));
    
            var accionParameter = accion != null ?
                new ObjectParameter("Accion", accion) :
                new ObjectParameter("Accion", typeof(string));
    
            var controladorParameter = controlador != null ?
                new ObjectParameter("Controlador", controlador) :
                new ObjectParameter("Controlador", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("sp_AccesoUsuario", idUsuarioParameter, accionParameter, controladorParameter);
        }
    
        public virtual ObjectResult<sp_MenuUsuario_Result> sp_MenuUsuario(Nullable<int> idUsuario)
        {
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("idUsuario", idUsuario) :
                new ObjectParameter("idUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_MenuUsuario_Result>("sp_MenuUsuario", idUsuarioParameter);
        }
    
        public virtual ObjectResult<string> sp_PasswordDec(string passwordCifrado)
        {
            var passwordCifradoParameter = passwordCifrado != null ?
                new ObjectParameter("PasswordCifrado", passwordCifrado) :
                new ObjectParameter("PasswordCifrado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_PasswordDec", passwordCifradoParameter);
        }
    
        public virtual ObjectResult<string> sp_PasswordEnc(string password)
        {
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_PasswordEnc", passwordParameter);
        }
    
        public virtual ObjectResult<ObtenerMatrizRiesgo_Result> ObtenerMatrizRiesgo(string rFC, string iDSOLICITUD, Nullable<System.DateTime> fECHA_INICIO, Nullable<System.DateTime> fECHA_FIN)
        {
            var rFCParameter = rFC != null ?
                new ObjectParameter("RFC", rFC) :
                new ObjectParameter("RFC", typeof(string));
    
            var iDSOLICITUDParameter = iDSOLICITUD != null ?
                new ObjectParameter("IDSOLICITUD", iDSOLICITUD) :
                new ObjectParameter("IDSOLICITUD", typeof(string));
    
            var fECHA_INICIOParameter = fECHA_INICIO.HasValue ?
                new ObjectParameter("FECHA_INICIO", fECHA_INICIO) :
                new ObjectParameter("FECHA_INICIO", typeof(System.DateTime));
    
            var fECHA_FINParameter = fECHA_FIN.HasValue ?
                new ObjectParameter("FECHA_FIN", fECHA_FIN) :
                new ObjectParameter("FECHA_FIN", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerMatrizRiesgo_Result>("ObtenerMatrizRiesgo", rFCParameter, iDSOLICITUDParameter, fECHA_INICIOParameter, fECHA_FINParameter);
        }
    }
}