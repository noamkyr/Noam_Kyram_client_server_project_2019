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
using My_Fantasy_UsersOBJ;

namespace My_Fantasy
{
    class UserAdapter : BaseAdapter<SingleUser>
    {
        Android.Content.Context context;

        List<SingleUser> objects;



        public UserAdapter(Android.Content.Context context, System.Collections.Generic.List<SingleUser> objects)

        {
            this.context = context;

            this.objects = objects;
            
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override int Count
        {
            get { return this.objects.Count; }
        }

        

        public override SingleUser this[int position]
        {
            get { return this.objects[position]; }
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Android.Views.LayoutInflater layoutInflater = ((LVUsers)context).LayoutInflater;
            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.oneUser, parent, false);
            TextView tvUser = view.FindViewById<TextView>(Resource.Id.tvUsername);

            SingleUser temp = objects[position];

            if (temp != null)
            {
                tvUser.Text = temp.name;
              
            }

            return view;


        }

    }
}