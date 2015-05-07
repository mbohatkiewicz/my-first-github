namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maps : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressTypeId = c.Int(nullable: false),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        City = c.String(),
                        PostalCode = c.String(),
                        Province = c.String(),
                        Region = c.String(),
                        Country = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Profile_ProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Profile", t => t.Profile_ProfileId)
                .Index(t => t.Profile_ProfileId);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        LicenseId = c.Int(nullable: false),
                        Profile_ProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.CompanyId)
                .ForeignKey("dbo.Profile", t => t.Profile_ProfileId)
                .Index(t => t.Profile_ProfileId);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        AccessLevelId = c.Int(nullable: false),
                        IsOwner = c.Boolean(nullable: false),
                        Company_CompanyId = c.Int(),
                        Profile_ProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Company", t => t.Company_CompanyId)
                .ForeignKey("dbo.Profile", t => t.Profile_ProfileId)
                .Index(t => t.Company_CompanyId)
                .Index(t => t.Profile_ProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "Profile_ProfileId", "dbo.Profile");
            DropForeignKey("dbo.User", "Company_CompanyId", "dbo.Company");
            DropForeignKey("dbo.Company", "Profile_ProfileId", "dbo.Profile");
            DropForeignKey("dbo.Address", "Profile_ProfileId", "dbo.Profile");
            DropIndex("dbo.User", new[] { "Profile_ProfileId" });
            DropIndex("dbo.User", new[] { "Company_CompanyId" });
            DropIndex("dbo.Company", new[] { "Profile_ProfileId" });
            DropIndex("dbo.Address", new[] { "Profile_ProfileId" });
            DropTable("dbo.User");
            DropTable("dbo.Profile");
            DropTable("dbo.Company");
            DropTable("dbo.Address");
        }
    }
}
