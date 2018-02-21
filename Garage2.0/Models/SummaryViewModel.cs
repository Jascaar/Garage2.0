using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garage2._0.Models;
using System.ComponentModel;

namespace Garage2._0.Models
{
    public class SummaryViewModel
    {
        [DisplayName("Vehicle Type")]
        public string Name { get; set; }

        [DisplayName("#Instances")]
        public int Count { get; set; }

        [DisplayName("#Tires")]
        public int SumTires { get; set; }

        [DisplayName("Acc. parking time")]
        public int? ParkingTime { get; set; }

        [DisplayName("Acc. revenue")]
        public int? AccumulatedRevenue { get; set; }

        public string TotalName = "Total";

        public int TotalCount { get; set; }
        public int TotalSumTires { get; set; }
        public int? TotalParkingTime { get; set; }
        public int? TotalAccumulatedRevenue { get; set; }
    }
}