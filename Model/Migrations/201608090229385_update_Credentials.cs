namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_Credentials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Credentials", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Credentials", "Status");
        }
    }
}
