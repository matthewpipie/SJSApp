using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace SJSApp.iOS
{
    public partial class AssignmentsViewController : UIViewController
    {
        public AssignmentsViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            GetAndDisplayAssignments();
        }
        private void GetAndDisplayAssignments()
        {
            List<SchoolAssignment> cached = SJSManager.Instance.GetAssignments(new DateTime(2017, 8, 24), new DateTime(2017, 8, 24), (List<SchoolAssignment> o) =>
            {
                if (o == null)
                {
                    LoginViewController loginView = this.Storyboard.InstantiateViewController("LoginViewController") as LoginViewController;
                    if (loginView != null)
                    {
                        loginView.ret = () => { GetAndDisplayAssignments(); };
                        //this.NavigationController.PushViewController(loginView, true);
                        this.PresentViewController(loginView, true, () => { });
                    }

                }
                else
                {
                    DisplayAssignments(o);
                }
            });
            DisplayAssignments(cached);
            // temp
            //ResultLabel.Text = "loadin";
        }
        private void DisplayAssignments(List<SchoolAssignment> o)
        {
            assignmentsTableView.ClassNames.Clear();
            assignmentsTableView.AssignmentTypes.Clear();
            assignmentsTableView.DueDates.Clear();
            assignmentsTableView.AssignmentDescriptions.Clear();
            try
            {
                foreach (SchoolAssignment schoolAssignment in o)
                {
                    assignmentsTableView.ClassNames.Add(schoolAssignment.DisplayClass());
                    assignmentsTableView.AssignmentTypes.Add(schoolAssignment.DisplayAssignmentType());
                    assignmentsTableView.DueDates.Add(schoolAssignment.DisplayDueDate());
                    assignmentsTableView.AssignmentDescriptions.Add(schoolAssignment.DisplayDescription());
                }
                assignmentsTableView.ReloadData();
            }
            catch (Exception e)
            {

            }
        }
    }
}