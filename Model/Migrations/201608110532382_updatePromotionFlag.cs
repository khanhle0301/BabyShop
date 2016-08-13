namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePromotionFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PromotionFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PromotionFlag");
        }
    }
}
