namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_auditable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.About", "CreatedBy", c => c.String(maxLength: 50));
            AlterColumn("dbo.About", "UpdatedBy", c => c.String(maxLength: 50));
            AlterColumn("dbo.ProductCategories", "CreatedBy", c => c.String(maxLength: 50));
            AlterColumn("dbo.ProductCategories", "UpdatedBy", c => c.String(maxLength: 50));
            AlterColumn("dbo.Products", "CreatedBy", c => c.String(maxLength: 50));
            AlterColumn("dbo.Products", "UpdatedBy", c => c.String(maxLength: 50));
            AlterColumn("dbo.PostCategories", "CreatedBy", c => c.String(maxLength: 50));
            AlterColumn("dbo.PostCategories", "UpdatedBy", c => c.String(maxLength: 50));
            AlterColumn("dbo.Posts", "CreatedBy", c => c.String(maxLength: 50));
            AlterColumn("dbo.Posts", "UpdatedBy", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "CreatedBy", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "UpdatedBy", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UpdatedBy", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Users", "CreatedBy", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Posts", "UpdatedBy", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Posts", "CreatedBy", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.PostCategories", "UpdatedBy", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.PostCategories", "CreatedBy", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Products", "UpdatedBy", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Products", "CreatedBy", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.ProductCategories", "UpdatedBy", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.ProductCategories", "CreatedBy", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.About", "UpdatedBy", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.About", "CreatedBy", c => c.String(maxLength: 50, unicode: false));
        }
    }
}
