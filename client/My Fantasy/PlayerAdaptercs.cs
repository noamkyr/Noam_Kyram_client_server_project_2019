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
    class PlayerAdapter : BaseAdapter<Standard>
    {
        Android.Content.Context context;

        List<Standard> objects;



        public PlayerAdapter(Android.Content.Context context, System.Collections.Generic.List<Standard> objects)

        {



            this.context = context;



            this.objects = objects;



        }




        public List<Standard> GetList()
        {
            List<Standard> fs = new List<Standard>();

            foreach (var i in this.objects)
            {
                if (i.pos == "F")
                {
                    fs.Add(i);
                }
            }


            return fs;
        }


        public override long GetItemId(int position)
        {
            return position;
        }


        public override int Count
        {
            get { return this.objects.Count; }
        }

        public override Standard this[int position]
        {
            get { return this.objects[position]; }
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Android.Views.LayoutInflater layoutInflater = ((LVplayers)context).LayoutInflater;
            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.oneItemP, parent, false);
            TextView tvTitle = view.FindViewById<TextView>(Resource.Id.tvFull);

            Standard temp = objects[position];

            if (temp != null)
            {
                tvTitle.Text = temp.firstName + " " + temp.lastName;
            }

            return view;



        }
    }
}