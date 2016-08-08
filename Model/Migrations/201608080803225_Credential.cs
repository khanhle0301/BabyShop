namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Credential : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminGroups",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 20, unicode: false),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Admins", "GroupID", c => c.String(maxLength: 20, unicode: false));
            CreateIndex("dbo.Admins", "GroupID");
            AddForeignKey("dbo.Admins", "GroupID", "dbo.AdminGroups", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admins", "GroupID", "dbo.AdminGroups");
            DropIndex("dbo.Admins", new[] { "GroupID" });
            DropColumn("dbo.Admins", "GroupID");
            DropTable("dbo.AdminGroups");
        }
    }
}
