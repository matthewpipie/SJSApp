// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SJSApp.iOS
{
    [Register ("AssignmentsViewController")]
    partial class AssignmentsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        SJSApp.iOS.AssignmentsTableView assignmentsTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (assignmentsTableView != null) {
                assignmentsTableView.Dispose ();
                assignmentsTableView = null;
            }
        }
    }
}