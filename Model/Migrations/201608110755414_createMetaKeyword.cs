namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createMetaKeyword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Metakeyword", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Metakeyword");
        }
    }
}
