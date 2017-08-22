using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SJSApp.Droid
{
    public class MainFragment : Fragment
    {
        private View view;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //view = inflater.Inflate(Resource.Layout.ScheduleFragment, container, false);

            //Button getAssignments = view.FindViewById<Button>(Resource.Id.assign);

            //getAssignments.Click += delegate {
                //button.Text = string.Format ("{0} clicks!", count++);
                //string username = FindViewById<EditText>(Resource.Id.username).Text;
                //string password = FindViewById<EditText>(Resource.Id.password).Text;
                //new SJSManager().Run(pass, FindViewById<TextView>(Resource.Id.response));
                //new SJSManager().Run2(username, password, (string resp) => { FindViewById<TextView>(Resource.Id.response).Text = resp; });
                //SJSManager.Instance.SetCredentials(username, password, true, () =>
                //{

                //});
                //GetAndDisplayAssignments();


            //};
            /*view.FindViewById<Button>(Resource.Id.storeTrash).Click += delegate
            {
                SJSManager.Instance.SetCredentials("", "", false, () => { });
            };
            view.FindViewById<Button>(Resource.Id.storeTrash2).Click += delegate
            {
                SJSManager.Instance.SetCredentials("", "", true, () => { });
            };
            view.FindViewById<Button>(Resource.Id.deleteToken).Click += delegate
            {
                SJSManager.Instance.DeleteToken();
            };
            view.FindViewById<Button>(Resource.Id.inval).Click += delegate
            {
                SJSManager.Instance.InvalidateToken();
            };*/

            GetAndDisplayAssignments();

            return view;
        }

        public void OnFragmentResult()
        {
            GetAndDisplayAssignments();
        }

        public class ScheduledClass
        {
            public string ClassName { get; set; }
            public string StartTime { get; set; }
            public string EndTime   { get; set; }
        }

        private void GetAndDisplayAssignments()
        {
            /*Object cached = SJSManager.Instance.GetSchedule(DateTime.Today, (Object o) =>
            {
                if (o == null)
                {
                    LoginFragment loginFragment = new LoginFragment();
                    loginFragment.Show(ChildFragmentManager, "login");

                }
                else
                {
                    view.FindViewById<TextView>(Resource.Id.response).Text = "";
                    var items = JArray.Parse(JsonConvert.SerializeObject(o));
                    List<ScheduledClass> classes = new List<ScheduledClass>();
                    for (int i = 0; i < items.Count; i++)
                    {
                        JToken obj = items[i];
                        string className = obj.Value<string>("CourseTitle");
                        string startTime = obj.Value<string>("StartTime");
                        string endTime = obj.Value<string>("EndTime");
                        
                        ScheduledClass scheduledClass = new ScheduledClass();
                        scheduledClass.ClassName = className;
                        scheduledClass.StartTime = startTime.Split(' ')[1];
                        scheduledClass.EndTime = endTime.Split(' ')[1];
                        classes.Add(scheduledClass);

                        Console.WriteLine(className + startTime + endTime);

                    }

                    foreach (ScheduledClass scheduledClass in classes)
                    {
                        view.FindViewById<TextView>(Resource.Id.response).Text +=
                        scheduledClass.ClassName + "&" + scheduledClass.StartTime + "&" + scheduledClass.EndTime + "\n"; 
                    }

                }
            });
            view.FindViewById<TextView>(Resource.Id.response).Text = JsonConvert.SerializeObject(cached);
            // temp
            view.FindViewById<TextView>(Resource.Id.response).Text = "loadin";*/
        }
    }
}