namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_slide_require : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Slides", "Link", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Slides", "Link", c => c.String(maxLength: 50));
        }
    }
}
