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
    [Activity(Label = "errorActivity")]
    public class errorActivity : Activity, Android.Views.View.IOnClickListener
    {
        Button btnError;
        TextView tvError;

        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.errorLayout);

            btnError = FindViewById<Button>(Resource.Id.btnError);
            btnError.SetOnClickListener(this);

            tvError = FindViewById<TextView>(Resource.Id.tvError);

            string errorType = Intent.GetStringExtra("errorType");

            tvError.Text = errorType;


        }
    }
}