using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.Media.Session;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace My_Fantasy
{
    [Service]
    public class MediaPlayerService : IntentService
    {
        public static String ACTION_PLAY = "action_play";
        public static String ACTION_PAUSE = "action_pause";
        public static String ACTION_REWIND = "action_rewind";
        public static String ACTION_FAST_FORWARD = "action_fast_foward";
        public static String ACTION_NEXT = "action_next";
        public static String ACTION_PREVIOUS = "action_previous";
        public static String ACTION_STOP = "action_stop";

        private static MediaPlayer mMediaPlayer;
        private static MediaSessionManager mManager;
        private static MediaSession mSession;
        private static Android.Media.Session.MediaController mController;
        

        private static String CHANNEL_ID = "MediaPlayerService";

        public MediaPlayerService() : base("MediaPlayerService")
        {
            //InitMediaSession();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            InitMediaSession();
            return base.OnStartCommand(intent, flags, startId);
        }

        private void InitMediaSession()
        {
            try
            {
                if (mMediaPlayer == null)
                {
                    mMediaPlayer = new MediaPlayer();
                    mMediaPlayer.SetAudioStreamType(Stream.Music);
                    mMediaPlayer.Prepared += (sender, args) => mMediaPlayer.Start();
                    mMediaPlayer.Completion += (sender, args) => mMediaPlayer.Pause();
                    SetPlayer(Resource.Raw.Halleluka);

                    mMediaPlayer.Looping = true;
                    //שליטה על הכפתורים במופע החדש של נגינת השיר
                    mManager = (MediaSessionManager)GetSystemService(MediaSessionService);
                    mSession = new MediaSession(this, "sample session");
                    mController = mSession.Controller;

                    mSession.SetCallback(new CustomCallback(this));
                }
            }
            catch( Exception ex)
            {
                string tmp = ex.Message;
            }
        }

        private void SetPlayer(int mp3)
        {
            String uri = "android.resource://my_fantasy.my_fantasy/Raw/" + mp3;
            mMediaPlayer.SetDataSource(ApplicationContext, Android.Net.Uri.Parse(uri));
            mMediaPlayer.Prepare();
        }

        class CustomCallback : MediaSession.Callback
        {
            //מחלקת עזר שעוזרת לעדכן את סטטוס השיר
            MediaPlayerService service;
            public CustomCallback(MediaPlayerService service) : base()
            {
                this.service = service;
            }
            //טיפול באירועים שונים
            public override void OnFastForward()
            {
                base.OnFastForward();
            }

            public override void OnPause()
            {
                //base.OnPause();

                if (mMediaPlayer != null && mMediaPlayer.IsPlaying)
                {
                    mMediaPlayer.Pause();
                }


                service.buildNotification(service.generateAction(Android.Resource.Drawable.IcMediaPlay, "Play", ACTION_PLAY));
            }

            public override void OnPlay()
            {
                //base.OnPlay();
                if (mMediaPlayer != null && !mMediaPlayer.IsPlaying)
                {
                    mMediaPlayer.Start();
                }


                service.buildNotification(service.generateAction(Android.Resource.Drawable.IcMediaPause, "Pause", ACTION_PAUSE));
            }

            public override void OnRewind()
            {
                base.OnRewind();
            }

            public override void OnSeekTo(long pos)
            {
                base.OnSeekTo(pos);
            }

            public override void OnSetRating(Rating rating)
            {
                base.OnSetRating(rating);
            }

            public override void OnSkipToNext()
            {
                base.OnSkipToNext();
                service.buildNotification(service.generateAction(Android.Resource.Drawable.IcMediaPause, "Pause", ACTION_PAUSE));
            }

            public override void OnSkipToPrevious()
            {
                base.OnSkipToPrevious();
                service.buildNotification(service.generateAction(Android.Resource.Drawable.IcMediaPause, "Pause", ACTION_PAUSE));
            }

            public override void OnStop()
            {
                base.OnStop();

                NotificationManager notificationManager = NotificationManager.FromContext(this.service);
                notificationManager.Cancel(1);
                Intent intent = new Intent(service.ApplicationContext, typeof(MediaPlayerService));
                service.StopService(intent);
            }
        }


        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                if (intent == null || intent.Action == null)
                    return;

                String action = intent.Action;

                if (action.Equals(ACTION_PLAY))
                {
                    mController.GetTransportControls().Play();
                    //mMediaPlayer.Start();
                }
                else if (action.Equals(ACTION_PAUSE))
                {
                    mController.GetTransportControls().Pause();
                    //mMediaPlayer.Pause();
                }
                else if (action.Equals(ACTION_FAST_FORWARD))
                {
                    mController.GetTransportControls().FastForward();
                }
                else if (action.Equals(ACTION_REWIND))
                {
                    mController.GetTransportControls().Rewind();
                }
                else if (action.Equals(ACTION_PREVIOUS))
                {
                    mController.GetTransportControls().SkipToPrevious();
                }
                else if (action.Equals(ACTION_NEXT))
                {
                    mController.GetTransportControls().SkipToNext();
                }
                else if (action.Equals(ACTION_STOP))
                {
                    mController.GetTransportControls().Stop();
                    mMediaPlayer.Stop();
                }
            }
            catch { }
        }

        private void buildNotification(Notification.Action action)
        {
            try
            {
                Notification.MediaStyle style = new Notification.MediaStyle();

                Intent intent = new Intent(ApplicationContext, typeof(MediaPlayerService));
                intent.SetAction(ACTION_STOP);
                PendingIntent pendingIntent = PendingIntent.GetService(ApplicationContext, 1, intent, 0);

                createChannel();
                Notification.Builder builder = new Notification.Builder(this, CHANNEL_ID)
                    .SetSmallIcon(Resource.Drawable.a1)
                    .SetContentTitle("Media Title")
                    .SetContentText("Media Artist")
                    .SetDeleteIntent(pendingIntent)
                    .SetStyle(style)
                    .SetOnlyAlertOnce(true);

                builder.AddAction(generateAction(Android.Resource.Drawable.IcMediaPrevious, "Previous", ACTION_PREVIOUS));
                builder.AddAction(generateAction(Android.Resource.Drawable.IcMediaRew, "Rewind", ACTION_REWIND));
                builder.AddAction(action);
                builder.AddAction(generateAction(Android.Resource.Drawable.IcMediaFf, "Fast Foward", ACTION_FAST_FORWARD));
                builder.AddAction(generateAction(Android.Resource.Drawable.IcMediaNext, "Next", ACTION_NEXT));

                NotificationManager notificationManager = NotificationManager.FromContext(this);
                notificationManager.Notify(1, builder.Build());
            }
            catch { }
        }

        private Notification.Action generateAction(int icon, String title, String intentAction)
        {
            Intent intent = new Intent(ApplicationContext, typeof(MediaPlayerService));
            intent.SetAction(intentAction);
            PendingIntent pendingIntent = PendingIntent.GetService(ApplicationContext, 1, intent, 0);
            return new Notification.Action.Builder(icon, title, pendingIntent).Build();
        }

        public override bool OnUnbind(Intent intent)
        {
            mSession.Release();
            return base.OnUnbind(intent);
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
    }
}