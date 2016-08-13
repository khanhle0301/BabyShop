namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_about1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.About", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.About", "Name", c => c.String(maxLength: 50));
        }
    }
}
