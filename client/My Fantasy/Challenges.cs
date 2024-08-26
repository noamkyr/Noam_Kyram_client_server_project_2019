using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using Newtonsoft.Json;
using My_Fantasy_Mychallenge;
using System.Timers;
//using Java.Lang;

namespace My_Fantasy
{
    [Activity(Label = "Challenges")]
    public class Challenges : Activity, Android.Views.View.IOnClickListener, ListView.IOnItemClickListener
    {

        Button newCha;


        Button myRefresh;
        BroadcastBattery broadCastBattery;
        static ListView requests;
        //Android.Views.LayoutInflater locallayoutInflater;
        int count = 0;
        MediaPlayer p;
        static List<Mychallenge> l;
        static ChalAdapter chalAdapter;
        public static bool isNewChal;

        public static string chosenChalYear = "";

        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";

        Handler mHandler;

        public Challenges()
        {

        }

        public void OnClick(View v)
        {

            if (v == newCha)
            {
                aTimer.Stop();
                Intent intent = new Intent(this, typeof(Building));
                isNewChal = true;
                StartActivityForResult(intent, 3);


                //StartActivity(intent);
            }

            if (v == myRefresh)
            {

                updateTable();
            }



        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            RunOnUiThread(() =>
            {
                myRefresh.CallOnClick();
            });


            aTimer.Start();
        }

        private void updateTable()
        {
            try
            {


                ConnectToSer c = new ConnectToSer();
                string ans = c.CallMyServer("challenges", MainActivity.usr);

                List<Mychallenge> deserializedProduct = JsonConvert.DeserializeObject<List<Mychallenge>>(ans);

                l.Clear();
                l.AddRange(deserializedProduct);

                chalAdapter.setList(l);
                requests.Adapter = chalAdapter;
            }
            catch
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);

                builder.SetTitle("Error connceting to server");

                builder.SetMessage("Try again later");

                builder.SetCancelable(true);

                //builder.SetPositiveButton("OK", OkAction);

                //builder.SetNegativeButton("cancel", CancelAction);

                AlertDialog dialog = builder.Create();

                dialog.Show();


            }
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.challengesNew);

            newCha = FindViewById<Button>(Resource.Id.btnNewChallenge);
            myRefresh = FindViewById<Button>(Resource.Id.btnRefresh);
            myRefresh.SetOnClickListener(this);
            myRefresh.Visibility = ViewStates.Invisible;
            //myCha = FindViewById<Button>(Resource.Id.btnYourCahllenge);
            requests = FindViewById<ListView>(Resource.Id.lvChallenges);

            //myCha.SetOnClickListener(this);
            newCha.SetOnClickListener(this);

            //music service
            try
            {
                Intent intent = new Intent(ApplicationContext, typeof(MediaPlayerService));
                intent.SetAction(MediaPlayerService.ACTION_PLAY);
                StartService(intent);
                createChannel();
            }
            catch
            { }

            ConnectToSer c = new ConnectToSer();
            string ans = "";
            try
            {
                ans = c.CallMyServer("challenges", MainActivity.usr);

                List<Mychallenge> deserializedProduct = JsonConvert.DeserializeObject<List<Mychallenge>>(ans);

                requests = FindViewById<ListView>(Resource.Id.lvChallenges);

                //LVplayers.check = false;

                l = new List<Mychallenge>();
                l.AddRange(deserializedProduct);

                chalAdapter = new ChalAdapter(this, l);
                requests.Adapter = chalAdapter;

                requests.OnItemClickListener = this;
                //get is playing by intent 
                //enabled false buttons and lvChallenges if is playing in challense
                //you can only see the status of the current challenge and the other players

                SetTimer();
            }
            catch
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);

                builder.SetTitle("Error connceting to server");

                builder.SetMessage("Try again later");

                builder.SetCancelable(true);

                //builder.SetPositiveButton("OK", OkAction);

                //builder.SetNegativeButton("cancel", CancelAction);

                AlertDialog dialog = builder.Create();

                dialog.Show();
            }



            broadCastBattery = new BroadcastBattery();
            //broadCastBattery = new BroadcastBattery(this, new Android.Content.Intent(this, typeof(Challenges)));


        }

        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(broadCastBattery, new IntentFilter(Intent.ActionBatteryChanged));
        }

        protected override void OnPause()
        {
            UnregisterReceiver(broadCastBattery);
            base.OnPause();
        }

        public override void OnBackPressed()
        {
            // disables you come back to register window
            return;


        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {



            //throw new System.NotImplementedException();

            Intent intent = new Intent(this, typeof(Building));

            Mychallenge temp = Challenges.l[position];
            chosenChalYear = temp.year;
            isNewChal = false;

            if (temp.creator.user.Equals(MainActivity.usr))
            {
                Toast.MakeText(this, "cannot pick yourself", ToastLength.Long).Show();
                return;

            }

            if(temp.Team1.points != 0 && temp.Team1.user.Equals(MainActivity.usr))
            {
                Toast.MakeText(this, "you already did the challenge", ToastLength.Long).Show();
                return;
            }


            if (temp.Team2.points != 0 && temp.Team2.user.Equals(MainActivity.usr))
            {
                Toast.MakeText(this, "you already did the challenge", ToastLength.Long).Show();
                return;
            }

            //intent.PutExtra("pos", position);

            //intent.PutExtra("isNewChal", isNewChal);
            intent.PutExtra("chosenChalYear", chosenChalYear);
            intent.PutExtra("challengeID", temp.challengeID);
            StartActivityForResult(intent, 1);


            //StartActivity(intent);

        }




        private void createChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var name = Resources.GetString(Resource.String.channel_name);
            var description = GetString(Resource.String.channel_description);
            var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }


        private static System.Timers.Timer aTimer;

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(3000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;



        }

        //void MyElapsedMethod(object sender, ElapsedEventArgs e, Android.Content.Context myThis)
        //{

        //    RunOnUiThread(() =>
        //    {
        //        myRefresh.CallOnClick();
        //    });

        //    //Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
        //                   //   e.SignalTime);


        //    aTimer.Start();

        //}

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                myRefresh.CallOnClick();
            });

            //Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
            //                  e.SignalTime);

            aTimer.Start();
        }


    }
}