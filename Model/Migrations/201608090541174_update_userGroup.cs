namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_userGroup : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Credentials", name: "AdminGroupID", newName: "UserGroupID");
            RenameIndex(table: "dbo.Credentials", name: "IX_AdminGroupID", newName: "IX_UserGroupID");
            AddColumn("dbo.UserGroups", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.Contacts", "Status");
            DropColumn("dbo.Footers", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Footers", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Contacts", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserGroups", "Status");
            RenameIndex(table: "dbo.Credentials", name: "IX_UserGroupID", newName: "IX_AdminGroupID");
            RenameColumn(table: "dbo.Credentials", name: "UserGroupID", newName: "AdminGroupID");
        }
    }
}
