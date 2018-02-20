using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;



namespace Garage2._0.Models
{
    public class PSlot
    {
        [Key]
        public int Id { get; set; }

        //Navigational property
        public virtual Member Member { get; set; }

        [Required(ErrorMessage = "Required field")]
        public int MemberId { get; set; }

        //Navigational property
        public virtual VehicleType VehicleType { get; set; }
        [Required(ErrorMessage = "Required field")]
        public int VehicleTypeId { get; set; }


        [Range(1, 100, ErrorMessage = "Not a valid parking slot. Please choose a free slot between 1 and 100")]
        [Required(ErrorMessage = "Required field")]
        public int ParkingSlot { get; set; }

        string vehicleRegistrationNumber;

        [DisplayName("Registration number")]
        [RNValidation]
        [Required(ErrorMessage = "Required field")]
        public string VehicleRegistrationNumber
        {
            get { return vehicleRegistrationNumber; }
            set
            {
                value = value.ToUpper().Trim().Replace(" ", "").Replace("-", "");
                vehicleRegistrationNumber = value.Substring(0, 3) + " " + value.Substring(3, 3);
            }
        }
        [DisplayName("Brand")]
        [Required(ErrorMessage = "Required field")]
        public string VehicleBrand { get; set; }
        [DisplayName("Model")]
        [Required(ErrorMessage = "Required field")]
        public string VehicleModel { get; set; }
        [DisplayName("Color")]
        [Required(ErrorMessage = "Required field")]
        public ConsoleColor Color { get; set; }
        [DisplayName("Number of wheels")]
        [Range(0, 10, ErrorMessage = "Only vehicles with 0-10 wheels are allowed to park in this garage")]
        [Required(ErrorMessage = "Required field")]
        public int TiresOnVehicle { get; set; }

        [DisplayName("Parking start time")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Required(ErrorMessage = "Required field")]
        public DateTime StartParking
        { get; set; } = DateTime.Now;


    }

 /*   public enum TypeOfVehicle
    {
        Airplane = 0,
        Bicycle = 1,
        Boat = 2,
        Bus = 3,
        Car = 4,
        Motorcycle = 5,

    }

    */
    public class RNValidation : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Required field");

            string compare = ((string)value).ToUpper().Trim().Replace(" ", "").Replace("-", "");
            if (compare.Length != 6) return new ValidationResult("Not a valid Swedish vehicle registration number");
            Regex regex = new Regex(@"[A-Z]{3}[0-9]{3}");

            if (regex.IsMatch(compare))
            {
                if (
                    (compare.Substring(0, 3) != "APA") ||
                    (compare.Substring(0, 3) != "ARG") ||
                    (compare.Substring(0, 3) != "ASS") ||
                    (compare.Substring(0, 3) != "BAJ") ||
                    (compare.Substring(0, 3) != "BSS") ||
                    (compare.Substring(0, 3) != "CUC") ||
                    (compare.Substring(0, 3) != "CUK") ||
                    (compare.Substring(0, 3) != "DUM") ||
                    (compare.Substring(0, 3) != "ETA") ||
                    (compare.Substring(0, 3) != "ETT") ||
                    (compare.Substring(0, 3) != "FAG") ||
                    (compare.Substring(0, 3) != "FAN") ||
                    (compare.Substring(0, 3) != "FEG") ||
                    (compare.Substring(0, 3) != "FEL") ||
                    (compare.Substring(0, 3) != "FEM") ||
                    (compare.Substring(0, 3) != "FES") ||
                    (compare.Substring(0, 3) != "FET") ||
                    (compare.Substring(0, 3) != "FNL") ||
                    (compare.Substring(0, 3) != "FUC") ||
                    (compare.Substring(0, 3) != "FUK") ||
                    (compare.Substring(0, 3) != "FUL") ||
                    (compare.Substring(0, 3) != "GAM") ||
                    (compare.Substring(0, 3) != "GAY") ||
                    (compare.Substring(0, 3) != "GEJ") ||
                    (compare.Substring(0, 3) != "GEY") ||
                    (compare.Substring(0, 3) != "GHB") ||
                    (compare.Substring(0, 3) != "GUD") ||
                    (compare.Substring(0, 3) != "GYN") ||
                    (compare.Substring(0, 3) != "HAT") ||
                    (compare.Substring(0, 3) != "HBT") ||
                    (compare.Substring(0, 3) != "HKH") ||
                    (compare.Substring(0, 3) != "HOR") ||
                    (compare.Substring(0, 3) != "HOT") ||
                    (compare.Substring(0, 3) != "KGB") ||
                    (compare.Substring(0, 3) != "KKK") ||
                    (compare.Substring(0, 3) != "KUC") ||
                    (compare.Substring(0, 3) != "KUF") ||
                    (compare.Substring(0, 3) != "KUG") ||
                    (compare.Substring(0, 3) != "KUK") ||
                    (compare.Substring(0, 3) != "KYK") ||
                    (compare.Substring(0, 3) != "LAM") ||
                    (compare.Substring(0, 3) != "LAT") ||
                    (compare.Substring(0, 3) != "LEM") ||
                    (compare.Substring(0, 3) != "LOJ") ||
                    (compare.Substring(0, 3) != "LSD") ||
                    (compare.Substring(0, 3) != "LUS") ||
                    (compare.Substring(0, 3) != "MAD") ||
                    (compare.Substring(0, 3) != "MAO") ||
                    (compare.Substring(0, 3) != "MEN") ||
                    (compare.Substring(0, 3) != "MES") ||
                    (compare.Substring(0, 3) != "MLB") ||
                    (compare.Substring(0, 3) != "MUS") ||
                    (compare.Substring(0, 3) != "NAZ") ||
                    (compare.Substring(0, 3) != "NRP") ||
                    (compare.Substring(0, 3) != "NSF") ||
                    (compare.Substring(0, 3) != "NYP") ||
                    (compare.Substring(0, 3) != "OND") ||
                    (compare.Substring(0, 3) != "OOO") ||
                    (compare.Substring(0, 3) != "ORM") ||
                    (compare.Substring(0, 3) != "PAJ") ||
                    (compare.Substring(0, 3) != "PKK") ||
                    (compare.Substring(0, 3) != "PLO") ||
                    (compare.Substring(0, 3) != "PMS") ||
                    (compare.Substring(0, 3) != "PUB") ||
                    (compare.Substring(0, 3) != "RAP") ||
                    (compare.Substring(0, 3) != "RAS") ||
                    (compare.Substring(0, 3) != "ROM") ||
                    (compare.Substring(0, 3) != "RPS") ||
                    (compare.Substring(0, 3) != "RUS") ||
                    (compare.Substring(0, 3) != "SEG") ||
                    (compare.Substring(0, 3) != "SEX") ||
                    (compare.Substring(0, 3) != "SJU") ||
                    (compare.Substring(0, 3) != "SOS") ||
                    (compare.Substring(0, 3) != "SPY") ||
                    (compare.Substring(0, 3) != "SUG") ||
                    (compare.Substring(0, 3) != "SUP") ||
                    (compare.Substring(0, 3) != "SUR") ||
                    (compare.Substring(0, 3) != "TBC") ||
                    (compare.Substring(0, 3) != "TOA") ||
                    (compare.Substring(0, 3) != "TOK") ||
                    (compare.Substring(0, 3) != "TRE") ||
                    (compare.Substring(0, 3) != "TYP") ||
                    (compare.Substring(0, 3) != "UFO") ||
                    (compare.Substring(0, 3) != "USA") ||
                    (compare.Substring(0, 3) != "WAM") ||
                    (compare.Substring(0, 3) != "WAR") ||
                    (compare.Substring(0, 3) != "WWW") ||
                    (compare.Substring(0, 3) != "XTC") ||
                    (compare.Substring(0, 3) != "XTZ") ||
                    (compare.Substring(0, 3) != "XXL") ||
                    (compare.Substring(0, 3) != "XXX") ||
                    (compare.Substring(0, 3) != "ZEX") ||
                    (compare.Substring(0, 3) != "ZOG") ||
                    (compare.Substring(0, 3) != "ZPY") ||
                    (compare.Substring(0, 3) != "ZUG") ||
                    (compare.Substring(0, 3) != "ZUP") ||
                    (compare.Substring(0, 3) != "ZOO")
                    )
                {
                    foreach (char item in compare.Substring(0, 3))
                    {
                        if (item != 'P' || item != 'Q' || item != 'V') { return ValidationResult.Success; }
                        else return new ValidationResult("Not a valid Swedish vehicle registration number");
                    }
                }
            }
            return new ValidationResult("Not a valid Swedish vehicle registration number");
        }

    }









}

