namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Credential2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        AdminGroupID = c.String(nullable: false, maxLength: 20, unicode: false),
                        RoleID = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.AdminGroupID, t.RoleID })
                .ForeignKey("dbo.AdminGroups", t => t.AdminGroupID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.AdminGroupID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 50, unicode: false),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Credentials", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Credentials", "AdminGroupID", "dbo.AdminGroups");
            DropIndex("dbo.Credentials", new[] { "RoleID" });
            DropIndex("dbo.Credentials", new[] { "AdminGroupID" });
            DropTable("dbo.Roles");
            DropTable("dbo.Credentials");
        }
    }
}
