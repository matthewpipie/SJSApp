using Foundation;
using CoreGraphics;
using System;
using UIKit;
using System.Collections.Generic;

namespace SJSApp.iOS
{
    public partial class AssignmentsTableView : UITableView
    {
        public AssignmentsTableView (IntPtr handle) : base (handle)
        {
            this.RegisterClassForCellReuse(typeof(UITableViewCell), cellID);
            this.Source = new AssignmentsTableDataSource(this);
        }

        static NSString cellID = new NSString("AssignmentCellID");
        public List<string> ClassNames = new List<string> { };
        public List<string> AssignmentTypes = new List<string> { };
        public List<string> DueDates = new List<string> { };
        public List<string> AssignmentDescriptions = new List<string> { };

        /*public void addEntry(string entry)
        {
                this.entries.Add("d");
                this.ReloadData();
        }*/

        class AssignmentsTableDataSource : UITableViewSource
        {
            AssignmentsTableView controller;

            public AssignmentsTableDataSource(AssignmentsTableView controller)
            {
                this.controller = controller;
            }

            // Returns the number of rows in each section of the table
            public override nint RowsInSection(UITableView tableView, nint section)
            {
                return controller.ClassNames.Count;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(AssignmentsTableView.cellID) as AssignmentTableCell;
                if (cell == null)
                {
                    cell = new AssignmentTableCell();
                }

                int row = indexPath.Row;
                cell.ClassName.Text = controller.ClassNames[row];
                cell.AssignmentType.Text = controller.AssignmentTypes[row];
                cell.DueDate.Text = controller.DueDates[row];
                cell.AssignmentDescription.Text = controller.AssignmentDescriptions[row];
                return cell;
            }
        }
        class AssignmentTableCell : UITableViewCell
        {
            public UILabel ClassName { get; set; }
            public UILabel AssignmentType { get; set; }
            public UILabel DueDate { get; set; }
            public UILabel AssignmentDescription { get; set; }

            public AssignmentTableCell()
            {
                int len1 = 40;
                int len2 = 40;
                int len3 = 50;
                ClassName = new UILabel(new CGRect(0, 0, len1, 50));
                AssignmentType = new UILabel(new CGRect(len1, 0, len2, 50));
                DueDate = new UILabel(new CGRect(len1 + len2, 0, len3, 50));
                AssignmentDescription = new UILabel(new CGRect(len1 + len2 + len3, 0, 999, 50));
                //LeftValue.AdjustsFontSizeToFitWidth = true;
                //LeftValue.Lines = 2;
                //LeftValue.LineBreakMode = UILineBreakMode.WordWrap;

                //LeftValue.Layer.BorderColor = UIColor.LightGray.CGColor;
                //LeftValue.Layer.BorderWidth = 1;
                //RightValue.Layer.BorderColor = UIColor.LightGray.CGColor;
                //RightValue.Layer.BorderWidth = 1;
                AssignmentDescription.Lines = 0;
                AssignmentDescription.LineBreakMode = UILineBreakMode.WordWrap;

                AddSubview(ClassName);
                AddSubview(AssignmentType);
                AddSubview(DueDate);
                AddSubview(AssignmentDescription);
            }
        }
    }
}