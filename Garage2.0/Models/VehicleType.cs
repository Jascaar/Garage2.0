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
    public class VehicleType
    {


        public int Id { get; set; }
        protected int vehicleTypeId;
        public int VehicleTypeId
        {
            get { return vehicleTypeId; }
            set { vehicleTypeId = Id; }
        }

        protected string typeOfVehicle = "";
        [Required(ErrorMessage = "Required field")]
        public String TypeOfVehicle
        {
            get { return typeOfVehicle; }
            set
            {
                value = value.Trim().ToLower();
                if (value.Length == 0)
                    typeOfVehicle = "";
                else typeOfVehicle = value.Substring(0, 1).ToUpper();
                if (value.Length > 1)
                    typeOfVehicle = value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
            }
        }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive integers are valid")]
        [Required(ErrorMessage = "Required field")]
        public int SpacesNeededDividend { get; set; } = 1;
        [Required(ErrorMessage = "Required field")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive integers are valid")]
        public int SpacesNeededDivisor { get; set; } = 1;
        protected decimal spacesNeeded;
        public decimal SpacesNeeded
        {
            get { return spacesNeeded; }
            protected set
            {
                spacesNeeded =
     (decimal)SpacesNeededDividend / (decimal)SpacesNeededDivisor;
            }
        }

        //navigational property
        public virtual ICollection<PSlot> ParkedVehicles { get; set; }







        /*    public enum TypeOfVehicle
            {
                Airplane = 0,
                Bicycle = 1,
                Boat = 2,
                Bus = 3,
                Car = 4,
                Motorcycle = 5,
            }
            */









    }
}

