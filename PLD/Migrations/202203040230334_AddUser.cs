namespace PLD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 40));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 40));
            AddColumn("dbo.AspNetUsers", "UsuarioAlta", c => c.Int());
            AddColumn("dbo.AspNetUsers", "UsuarioBaja", c => c.Int());
            AddColumn("dbo.AspNetUsers", "FechaAlta", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "FechaBaja", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "UltimoAcceso", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "IdStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IdStatus");
            DropColumn("dbo.AspNetUsers", "UltimoAcceso");
            DropColumn("dbo.AspNetUsers", "FechaBaja");
            DropColumn("dbo.AspNetUsers", "FechaAlta");
            DropColumn("dbo.AspNetUsers", "UsuarioBaja");
            DropColumn("dbo.AspNetUsers", "UsuarioAlta");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
