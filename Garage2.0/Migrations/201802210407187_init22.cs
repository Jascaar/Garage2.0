namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init22 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "FirstName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "FirstName", c => c.String());
        }
    }
}
