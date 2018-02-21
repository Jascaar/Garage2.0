namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage2._0.Models.Garage2_0Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Garage2._0.Models.Garage2_0Context context)
        {
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
            p => p.VehicleTypeId,
            new VehicleType { TypeOfVehicle = "Car", SpacesNeededDividend = 1, SpacesNeededDivisor = 1 },
            new VehicleType { TypeOfVehicle = "Motorcycle", SpacesNeededDividend = 1, SpacesNeededDivisor = 1 },
            new VehicleType { TypeOfVehicle = "Bus", SpacesNeededDividend = 2, SpacesNeededDivisor = 1 },
            new VehicleType { TypeOfVehicle = "Boat", SpacesNeededDividend = 2, SpacesNeededDivisor = 1 },
            new VehicleType { TypeOfVehicle = "Cycle", SpacesNeededDividend = 1, SpacesNeededDivisor = 1 }
            );
            context.Members.AddOrUpdate(
            e => e.Id,
            new Member { FirstName = "Dany", LastName = "KassDaoud", SSN = "770701-7898", Email = "danydaood@yahoo.com", Cellular = "0720158892", City = "Eskilstuna", Street = "Sommarrogatan", StreetNumber = 1, PostCode = "63226", OfficialApartmentNumber = "1306", Country = "Sweden", SignUpTime = DateTime.Now, StreetNumberAppendix="A" },
            new Member { FirstName = "Fadi", LastName = "Georgr", SSN = "881120-3853", Email = "fadi.gogo@hotmail.com", Cellular = "0700417329", City = "Eskilstuna", Street = "Belmansplan", StreetNumber = 3, PostCode = "63326", OfficialApartmentNumber = "2520", Country = "Sweden", SignUpTime = DateTime.Now, StreetNumberAppendix ="A" },
            new Member { FirstName = "Ingerd", LastName = "Andersson", SSN = "740612-9481", Email = "ingerd.andersson@hotmail.com", Cellular = "0700412316", City = "Lund", Street = "Goto", StreetNumber = 1, PostCode = "51226", OfficialApartmentNumber = "3701", Country = "Sweden", SignUpTime = DateTime.Now, StreetNumberAppendix = "A" },
            new Member { FirstName = "Gostav", LastName = "Ericsson", SSN = "751213-8533", Email = "gostav.ericsson@hotmail.com", Cellular = "0741223954", City = "Stockholm", Street = "Liljholmen", StreetNumber = 2, PostCode = "19225", OfficialApartmentNumber = "7503", Country = "Sweden", SignUpTime = DateTime.Now , StreetNumberAppendix = "A" },
            new Member { FirstName = "Gostav", LastName = "Ericsson", SSN = "751213-8533", Email = "ingerd.andersson@hotmail.com", Cellular = "0741623454", City = "Stockholm", Street = "Liljholmen", StreetNumber = 1, PostCode = "19236", OfficialApartmentNumber = "5310", Country = "Sweden", SignUpTime = DateTime.Now , StreetNumberAppendix = "A" }
            );
        }
    }
}
