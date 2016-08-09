namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserGroups", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserGroups", "Name", c => c.String(maxLength: 50));
        }
    }
}
