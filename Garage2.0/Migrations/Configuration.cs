namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Garage2._0.Models;


    internal sealed class Configuration : DbMigrationsConfiguration<Models.Garage2_0Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Models.Garage2_0Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.VehicleTypes.AddOrUpdate(
                 p => p.Id,
                 new VehicleType { TypeOfVehicle="Car", SpacesNeededDividend=1, SpacesNeededDivisor=1 },
                 new VehicleType { TypeOfVehicle = "Bus", SpacesNeededDividend = 2, SpacesNeededDivisor = 1 },
                 new VehicleType { TypeOfVehicle = "Cycle", SpacesNeededDividend = 1, SpacesNeededDivisor = 1 },
                 new VehicleType { TypeOfVehicle = "Boat", SpacesNeededDividend = 3, SpacesNeededDivisor = 1 },
                 new VehicleType { TypeOfVehicle = "Motor", SpacesNeededDividend = 1, SpacesNeededDivisor = 1 }
                 );

    }      
    }
}
