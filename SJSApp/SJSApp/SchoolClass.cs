using System;
using System.Linq;

namespace SJSApp
{
    public class SchoolClass
    {
        public string Name { get; set; }
        public string StartTime {get; set;}
        public string EndTime {get; set;}
        public string Room {get; set;}
        public string Block {get; set;}
        public SchoolClass(string name, string startTime, string endTime, string room, string block)
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            Room = room;
            Block = block;
        }
        public string TimeDisplay()
        {
            return StartTime + " - " + EndTime;
        }
        public string NameDisplay()
        {
            return (Block.Equals(Name) ? "" : Block + " - ") + Name + (String.IsNullOrEmpty(Room) ? "" : " (" + Room + ")");
        }
        public static string FixName(string name)
        {
            return String.Join("-", name.Split('-').Take(name.Split('-').Length - 1).ToArray()).Trim();
        }
    }
}
