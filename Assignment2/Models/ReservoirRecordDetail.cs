using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class ReservoirRecordDetail
    {
        public int dayNo { get; set; }
        public int monthNo { get; set; }
        public string monthName { get; set; }
        public int year { get; set; }
        public DateTime recordDate { get; set; }
        public decimal volumeToday { get; set; }
        public decimal volumeLastYear { get; set; }
        public decimal avgVolume { get; set; }
    }
}