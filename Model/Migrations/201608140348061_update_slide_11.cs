namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_slide_11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Slides", "Link", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Slides", "Link");
        }
    }
}
