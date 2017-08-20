using System;
using System.Collections.Generic;
using System.Text;

namespace SJSApp
{
    public class DayAndAnnouncement
    {
        public DayAndAnnouncement()
        {
            schoolDay = null;
            announcement = null;
            realDate = DateTime.Today;
        }
        public int? schoolDay { get; set; }
        public string announcement { get; set; }
        public DateTime realDate { get; set; }
    }
}
