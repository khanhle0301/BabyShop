namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MetaKeyword_Post : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "Metakeyword", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.ProductCategories", "Description", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.PostCategories", "Metakeyword", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.PostCategories", "Description", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.Posts", "Metakeyword", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Slides", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "Metakeyword", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Metakeyword", c => c.String(maxLength: 200));
            DropColumn("dbo.Slides", "Name");
            DropColumn("dbo.Posts", "Metakeyword");
            DropColumn("dbo.PostCategories", "Description");
            DropColumn("dbo.PostCategories", "Metakeyword");
            DropColumn("dbo.ProductCategories", "Description");
            DropColumn("dbo.ProductCategories", "Metakeyword");
        }
    }
}
