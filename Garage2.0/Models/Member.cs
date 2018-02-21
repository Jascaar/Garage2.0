using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Garage2._0.Models
{
    public class Member
    {
     
        public int Id { get; set; }

        [Key]
        protected string ssn;
        [Required(ErrorMessage = "Required field")]
        [SSNValidation]
        [RegularExpression(@"^(\d{6}|\d{8})[-\s]?\d{4}$", ErrorMessage = "Please use YYMMDD-XXXX or YYYYMMDD-XXXX formatting")]
        [DisplayName("Social security number")]
        public string SSN
        {
            get
            {
                return ssn;
            }
            set
            {
                value = value.Trim().Replace("-", "");

                if (value.Length == 10)
                {
                    var yearThisCentury = (DateTime.Now.Year).ToString().Substring(0, 2) + value.Substring(0, 2);
                    var yearLastCentury = (DateTime.Now.Year - 100).ToString().Substring(0, 2) + value.Substring(0, 2);
                    string year = "";

                    if (DateTime.Now.Year - Int32.Parse(yearLastCentury) < 115 && DateTime.Now.Year - Int32.Parse(yearLastCentury) >= 15)
                    { year = yearLastCentury; }
                    else year = yearThisCentury;

                    ssn = year + value.Substring(2, 4) + "-" + value.Substring(6, 4);
                }

                else
                    ssn = value.Substring(0, 8) + "-" + value.Substring(8, 4);

            }
        }


        protected string firstName;
        [Required(ErrorMessage = "Required field")]
        [DisplayName("First Name")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = FirstUpperCase(value); }
        }

        protected string lastName;
        [Required(ErrorMessage = "Required field")]
        [DisplayName("Surname")]
        public string LastName
        {
            get { return lastName; }
            set { lastName = FirstUpperCase(value); }
        }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Sign up date")]
        public DateTime SignUpTime
        { get; set; } = DateTime.Now;


        [RegularExpression("(^[0-9]{1,45}$)", ErrorMessage = "Mobile number should not contain letters or be more than 45 digits long")]
        [DisplayName("Cell phone number")]
        public string Cellular { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayName("Email address")]
        public string Email { get; set; }

        protected string street;
        [DisplayName("Street name")]
        public string Street
        {
            get { return street; }
            set { street = FirstUpperCase(value); }
        }


        [Range(1, int.MaxValue, ErrorMessage = "Only positive integers >0 are valid")]
        [Required(ErrorMessage = "Required field")]
        [DisplayName("Street number")]
        public int StreetNumber { get; set; }

        protected string streetNumberAppendix;
        [DisplayName("Street number appendix")]
        public string StreetNumberAppendix 
        {
            get { return streetNumberAppendix; }
    set { if (value is null || value.Length == 0) streetNumberAppendix = "";
                else streetNumberAppendix = FirstUpperCase(value);
}
        }


        [RegularExpression("(^[0-9]{4}$)", ErrorMessage = "Official apparment numbers are always 4 digits")]
        [DisplayName("Official apartment number")]
        //rename this field!
        public string OfficialApartmentNumber { get; set; }

        protected string postCode;
        //hantering så att all endast 5 siffror, visa dock med mellanslag mellan 3 och 2
        [RegularExpression("(^[0-9]{5}|([0-9]{3}[ X][0-9]{2}|)$)", ErrorMessage = "Post Code Should be 5 digits ([XXXXX] or [XXX XX]")]
        [DisplayName("Post code")]
        public string PostCode
        {
            get { return postCode; }
            set
            {if (value is null) postCode = "";
                postCode = value.Replace(" ","").ToString().Substring(0, 3) + " " + value.Replace(" ", "").ToString().Substring(3, 2);
            }
        }

        protected string city;
        public string City
        {
            get { return city; }
            set { city = FirstUpperCase(value); }
        }
        protected string country;
        public string Country
        {
            get { return country; }
            set { country = FirstUpperCase(value); }
        }
        protected Gender gender = Gender.Unknown;

        public Gender Gender
        {
            get { return gender; }
            set
            {
                if ((int)SSN.Replace("-","")[10] % 2 != 0)
                {
                    gender = Gender.Male;
                }
                else gender = Gender.Female;

            }
        }

        //navigational property
        public virtual ICollection<PSlot> ParkedVehicles { get; set; }


        protected Gender GetGender(string sSN)
        {
            return Gender.Unknown;  //The gender is indicated by the third digit, even for women, odd for men. 
        }
        protected string FirstUpperCase(string value)
        {
            value = value.Trim().ToLower();
            if (value.Length > 1)
                return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
            if (value.Length == 0)
                return "";
            else return value.Substring(0, 1).ToUpper();
        }
    }


    public enum Gender
    {
        Male = 0,
        Female = 1,
        Unknown = 2,
    }


    public class SSNValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            string RegExForValidation = @"^(?<date>\d{6}|\d{8})[-\s]?\d{4}$";
            string date = Regex.Match((string)value, RegExForValidation).Groups["date"].Value;
            if (DateTime.TryParseExact(date, new[] { "yyMMdd", "yyyyMMdd" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt) == false)
                return new ValidationResult("YYMMDD-XXXX or YYYYMMDD-XXXX. Dates need to be valid");


            string validationValue = ((string)value).Replace("-", "");
            if (validationValue.Length == 12)
                validationValue = validationValue.Substring(2, 10);
            int CheckSum = 0;
            int helper = 0;
            int helper2 = 0;
            for (int i = 1; i <= 9; i += 2)
            {
                helper = (int)(validationValue[i - 1] - '0');
                helper2 = (int)(validationValue[i] - '0');

                if (helper * 2 > 10)
                { CheckSum += helper * 2 - 9 + helper2; }
                else
                { CheckSum += helper * 2 + helper2; }

                if (i == 9)
                    CheckSum -= helper2;
            }

            decimal checkSumRoof = Math.Ceiling((decimal)CheckSum / 10) * 10;
            int controlNumber = Convert.ToInt32(checkSumRoof) - CheckSum;

            if ((int)(validationValue[9] - '0') == controlNumber)
                return ValidationResult.Success;
            else return new ValidationResult("Not a valid Swedish SSN number");
        }

    }


}