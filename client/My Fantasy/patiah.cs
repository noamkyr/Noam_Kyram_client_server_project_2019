using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace My_Fantasy
{
    [Activity(Label = "MyFantasy", MainLauncher = true)]
    public class patiah : Activity
    {

        private static System.Timers.Timer aTimer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.patiah);
            // Create your application here

            aTimer = new System.Timers.Timer(5000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;

            aTimer.Start();


        }


        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
          

            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);

            Android.Content.Intent intent = new Android.Content.Intent(this, typeof(MainActivity));
            StartActivity(intent);
           
        }

    }
}