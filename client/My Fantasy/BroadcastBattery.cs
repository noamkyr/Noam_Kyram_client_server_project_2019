using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace My_Fantasy
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { Intent.ActionBatteryChanged })]
    public class BroadcastBattery : BroadcastReceiver
    {

        int lastShown = 0;

        public BroadcastBattery()
        {

        }


        public override void OnReceive(Context context, Intent intent)
        {
            int battery = intent.GetIntExtra("level", 0);
            //this.status = battery;

            if(battery <= 20)
            {
 
                if ((Math.Abs(lastShown - battery) >= 5) || 
                    (lastShown == 0))
                {
                    lastShown = battery;

                    AlertDialog.Builder builder = new AlertDialog.Builder(context);

                    builder.SetTitle("Your battery is low!");

                    builder.SetMessage("pay attention! You have " + battery.ToString() + "%");

                    builder.SetCancelable(true);

                    AlertDialog dialog = builder.Create();

                    dialog.Show();
                }
            }

        }


    }
}