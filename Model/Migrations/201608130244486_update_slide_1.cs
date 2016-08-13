namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_slide_1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Slides", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Slides", "Content", c => c.String(storeType: "ntext"));
        }
    }
}
