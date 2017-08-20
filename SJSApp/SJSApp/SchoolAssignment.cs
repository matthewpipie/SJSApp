using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SJSApp
{
    public class SchoolAssignment
    {
        public string ClassName { get; set; }
        public string AssignmentType { get; set; }
        public long DueTick { get; set; }
        public string DueDate { get; set; }
        public string AssignmentDescription { get; set; }

        public SchoolAssignment(string className, string assignmentType, long dueTick, string dueDate, string assignmentDescription)
        {
            ClassName = className;
            AssignmentType = assignmentType;
            DueTick = dueTick;
            DueDate = dueDate;
            AssignmentDescription = assignmentDescription;
        }
        public string DisplayClass()
        {
            string uppercaseLetters = GetUppercaseLettersInString(ClassName);
            string ret = uppercaseLetters.Substring(0, Math.Min(3, uppercaseLetters.Length)).Trim();
            string[] split = SplitByUppercase(ClassName);
            foreach (string i in split)
            {
                if (i.Length > 1)
                {
                    ret += i.Substring(1, Math.Max(0, 3 - ret.Length));
                }
                if (ret.Length >= 3)
                {
                    break;
                }
            }
            return ret;
        }
        public string DisplayAssignmentType()
        {
            string ret = GetUppercaseLettersInString(AssignmentType).Substring(0, Math.Min(3, GetUppercaseLettersInString(AssignmentType).Length)).Trim();
            string[] split = SplitByUppercase(AssignmentType);
            foreach (string i in split)
            {
                if (i.Length > 1)
                {
                    ret += i.Substring(1, Math.Max(0, 3 - ret.Length));
                }
                if (ret.Length >= 3)
                {
                    break;
                }
            }
            return ret;
        }
        public string DisplayDueDate()
        {
            return DueDate;
        }
        public string DisplayDescription()
        {
            return AssignmentDescription;
        }
        private static string[] SplitByUppercase(string source)
        {
            return Regex.Split(source, @"(?<!^)(?=[A-Z])");
        }
        private static string GetUppercaseLettersInString(string source)
        {
            string[] split = SplitByUppercase(source);
            string ret = "";
            foreach (string i in split)
            {
                if (i.Length != 0)
                {
                    ret += i[0];
                }
            }
            return ret;
        }
        public static string GetFormattedDate(string oldDate)
        {
            string[] split = oldDate.Split('/');
            try
            {
                return split[0] + "/" + split[1];
            }
            catch
            {
                return oldDate;
            }
        }
    }
}
