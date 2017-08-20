using Foundation;
using System.Collections.Generic;
using System.Linq;
using System;
using UIKit;
using Newtonsoft.Json;

namespace SJSApp.iOS
{
    public partial class ScheduleViewController : UIViewController
    {
        public ScheduleViewController(IntPtr handle) : base(handle)
        {

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            GetAndDisplaySchedule();
        }
        private void GetAndDisplaySchedule()
        {
            List<SchoolClass> cached = SJSManager.Instance.GetSchedule(new DateTime(2017, 8, 23), (List<SchoolClass> o) =>
            {
                if (o == null)
                {
                    LoginViewController loginView = this.Storyboard.InstantiateViewController("LoginViewController") as LoginViewController;
                    if (loginView != null)
                    {
                        loginView.ret = () => { GetAndDisplaySchedule(); };
                        //this.NavigationController.PushViewController(loginView, true);
                        this.PresentViewController(loginView, true, () => { });
                    }

                }
                else
                {
                    DisplaySchedule(o);
                }
            });
            DisplaySchedule(cached);
            // temp
            //ResultLabel.Text = "loadin";
        }
        private void DisplaySchedule(List<SchoolClass> o)
        {
            scheduleTableView.LeftEntries.Clear();
            scheduleTableView.RightEntries.Clear();
            try
            {
                foreach (SchoolClass schoolClass in o)
                {
                    scheduleTableView.LeftEntries.Add(schoolClass.TimeDisplay());
                    scheduleTableView.RightEntries.Add(schoolClass.NameDisplay());
                }
                scheduleTableView.ReloadData();
            }
            catch (Exception e)
            {

            }
        }
    }
}