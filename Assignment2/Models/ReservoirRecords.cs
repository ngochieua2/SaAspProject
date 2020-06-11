using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class ReservoirRecords
    {
        public int year { get; set; }
        public int records { get; set; }
        public decimal avgVolume { get; set; }
        public int firstMonthNo { get; set; }
        public int lastMonthNo { get; set; }
        public string firstMonthName { get; set; }
        public string lastMonthName { get; set; }
    }

}