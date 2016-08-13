namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_about : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.About", "Name", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.About", "Name");
        }
    }
}
