namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PSlots", "VehicleRegistrationNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PSlots", "VehicleRegistrationNumber", c => c.String());
        }
    }
}
