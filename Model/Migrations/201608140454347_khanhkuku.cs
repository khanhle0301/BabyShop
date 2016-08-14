namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class khanhkuku : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "TTTT", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "TTTT");
        }
    }
}
