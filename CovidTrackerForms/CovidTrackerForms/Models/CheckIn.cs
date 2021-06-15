using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTrackerForms.Models
{
   public class CheckIn
    {
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int placeID { get; set; }
        public int userCMEID { get; set; }
    }
}
