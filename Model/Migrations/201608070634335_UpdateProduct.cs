namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "HomeFlag", c => c.Boolean(nullable: false));
            AlterColumn("dbo.OrderDetails", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Products", "PromotionPrice", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "PromotionPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "HomeFlag");
        }
    }
}
