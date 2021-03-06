﻿using System.Collections.Generic;
using Xamarin.Auth;
using Newtonsoft.Json;
using System.Text;
using System;
//using System.Collections.Specialized;

namespace SJSApp
{
    public class SJSManager
    {
        // Constants
        public const string SAC_SUGGESTIONS_LINK = "https://docs.google.com/a/sjs.org/forms/d/1bBOdqDCeVLpvA3GkW1WzxvkZhLm8NkHg-IXZP8jUo9I/viewform";
        public const string NAVIANCE_LINK = "https://connection.naviance.com/family-connection/auth/login/?hsid=sjs";

        // Private vars
        private SJSLoginManager LoginManager;

        private List<SchoolAssignment> AssignmentCache { get; set; }

        private List<SchoolClass> ScheduleCache { get; set; }
        private DateTime ScheduleCacheDate { get; set; }

        private DayAndAnnouncement DayAndAnnouncementCache { get; set; }
        private DateTime DayAndAnnouncementDate { get; set; }

        // Constructor
        private SJSManager()
        {
            LoginManager = new SJSLoginManager();
        }

        // Singleton setup
        private static SJSManager instance;
        public static SJSManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SJSManager();
                }
                return instance;
            }
        }

        // Methods
        public void SetCredentials(string username, string password, bool clearToken, Action callback)
        {
            LoginManager.SubmitCredentials(username, password, clearToken, callback);
        }
        public List<SchoolClass> GetSchedule(DateTime day, Action<List<SchoolClass>> callback)
        {
            string date = day.Month.ToString() + "%2F" + day.Day.ToString() + "%2F" + day.Year.ToString();
            LoginManager.MakeAPICall("schedule/MyDayCalendarStudentList/?scheduleDate=" + date + "&personaId=2", (Newtonsoft.Json.Linq.JArray o) =>
            {
                if (o != null)
                {
                    try
                    {
                        List<SchoolClass> classes = new List<SchoolClass>();
                        foreach (var schoolClass in o.Children())
                        {
                            classes.Add(new SchoolClass(SchoolClass.FixName(schoolClass["CourseTitle"].ToString()), schoolClass["MyDayStartTime"].ToString(), schoolClass["MyDayEndTime"].ToString(), schoolClass["RoomNumber"].ToString(), schoolClass["Block"].ToString()));
                        }
                        ScheduleCache = classes;
                        ScheduleCacheDate = day;
                        callback(classes);
                    }
                    catch
                    {
                        callback(null);
                    }
                }
                else
                {
                    callback(null);
                }
            });
            return (ScheduleCacheDate == day) ? ScheduleCache : null;
        }
        public List<SchoolAssignment> GetAssignments(DateTime start, DateTime end, Action<List<SchoolAssignment>> callback)
        {
            // %2F is the HTML encoding for a slash (/) which for some really weird reason myschoolapp uses
            // They also use M/D/YYYY which is just really strange and not standard at all
            string startDate = start.Month.ToString() + "%2F" + start.Day.ToString() + "%2F" + start.Year.ToString();
            string endDate = end.Month.ToString() + "%2F" + end.Day.ToString() + "%2F" + end.Year.ToString();
            LoginManager.MakeAPICall("DataDirect/AssignmentCenterAssignments/?format=json&filter=1&dateStart=" + startDate + "&dateEnd=" + endDate + "&persona=2&statusList=&sectionList=", (Newtonsoft.Json.Linq.JArray o) =>
            {
                if (o != null)
                {
                    try
                    {
                        List<SchoolAssignment> assignments = new List<SchoolAssignment>();
                        foreach (var schoolAssignment in o.Children())
                        {
                            //string a = SchoolClass.FixName(schoolAssignment["groupname"].ToString());
                            //string b = schoolAssignment["assignment_type"].ToString();
                            //string c = schoolAssignment["date_dueTicks"].ToString();
                            //string d = SchoolAssignment.GetFormattedDate(schoolAssignment["date_due"].ToString());
                            //string e = schoolAssignment["short_description"].ToString();
                            //int f = (int)schoolAssignment["date_dueTicks"];
                            assignments.Add(new SchoolAssignment(SchoolClass.FixName(schoolAssignment["groupname"].ToString()), schoolAssignment["assignment_type"].ToString(), Int64.Parse(schoolAssignment["date_dueTicks"].ToString()), SchoolAssignment.GetFormattedDate(schoolAssignment["date_due"].ToString()), schoolAssignment["short_description"].ToString()));
                        }
                        AssignmentCache = assignments;
                        callback(assignments);
                    }
                    catch
                    {
                        callback(null);
                    }
                }
                else
                {
                    callback(null);
                }
            });
            return AssignmentCache;
        }
        private int? DayToInt(string input)
        {
            return input.Length != 0 ? Int32.Parse(input.Substring(5)) : (int?)null;
        }
        public DayAndAnnouncement GetDayAndAnnouncement(DateTime day, Action<DayAndAnnouncement> callback)
        {
            string date = day.Month.ToString() + "%2F" + day.Day.ToString() + "%2F" + day.Year.ToString();
            LoginManager.MakeAPICall("schedule/ScheduleCurrentDayAnnouncmentParentStudent/?mydayDate=" + date + "&viewerId=1773520&viewerPersonaId=2", (Newtonsoft.Json.Linq.JArray o) =>
            {
                DayAndAnnouncement ret = new DayAndAnnouncement();
                if (o != null)
                {
                    try
                    {
                        ret.schoolDay = DayToInt((string)o[2]["DayLabel"]);
                        ret.announcement = o[2]["Announcement"].ToString();
                    }
                    catch
                    {
                        try
                        {
                            ret.schoolDay = DayToInt((string)o[1]["DayLabel"]);
                            ret.announcement = o[1]["Announcement"].ToString();
                        }
                        catch
                        {
                            //ret.schoolDay = null;
                            //ret.announcement = null;
                        }
                    }
                    DayAndAnnouncementCache = ret;
                    DayAndAnnouncementDate = day;
                }
                callback(ret);
            });
            return (DayAndAnnouncementDate == day) ? DayAndAnnouncementCache : null;
        }





        // TEMPORARY METHODS ONLY USED IN TESTING
        public void InvalidateToken()
        {
            LoginManager.InvalidateToken();
        }
        public void DeleteToken()
        {
            LoginManager.DeleteToken();
        }
    }
}

