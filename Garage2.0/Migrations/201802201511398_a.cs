namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SSN = c.String(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(nullable: false),
                        SignUpTime = c.DateTime(nullable: false),
                        Cellular = c.String(),
                        Email = c.String(nullable: false),
                        Street = c.String(),
                        StreetNumber = c.Int(nullable: false),
                        OfficialApartmentNumber = c.String(),
                        PostCode = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PSlots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        VehicleTypeId = c.Int(nullable: false),
                        ParkingSlot = c.Int(nullable: false),
                        VehicleRegistrationNumber = c.String(nullable: false),
                        VehicleBrand = c.String(nullable: false),
                        VehicleModel = c.String(nullable: false),
                        Color = c.Int(nullable: false),
                        TiresOnVehicle = c.Int(nullable: false),
                        StartParking = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.VehicleTypeId);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleTypeId = c.Int(nullable: false),
                        TypeOfVehicle = c.String(nullable: false),
                        SpacesNeededDividend = c.Int(nullable: false),
                        SpacesNeededDivisor = c.Int(nullable: false),
                        SpacesNeeded = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PSlots", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.PSlots", "MemberId", "dbo.Members");
            DropIndex("dbo.PSlots", new[] { "VehicleTypeId" });
            DropIndex("dbo.PSlots", new[] { "MemberId" });
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.PSlots");
            DropTable("dbo.Members");
        }
    }
}
