namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_User : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AdminGroups", newName: "UserGroups");
            RenameTable(name: "dbo.Admins", newName: "Users");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Users", newName: "Admins");
            RenameTable(name: "dbo.UserGroups", newName: "AdminGroups");
        }
    }
}
