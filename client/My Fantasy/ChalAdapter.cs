using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using My_Fantasy_Mychallenge;

namespace My_Fantasy
{
    class ChalAdapter : BaseAdapter<Mychallenge>
    {
        Android.Content.Context context;

        List<Mychallenge> objects;

        //Android.Views.LayoutInflater layoutInflater;

        public ChalAdapter(Android.Content.Context context, System.Collections.Generic.List<Mychallenge> objects)

        {



            this.context = context;



            this.objects = objects;


            //layoutInflater = ((Challenges)context).LayoutInflater;
            //Android.Views.View view = layoutInflater.Inflate(Resource.Layout.oneItemChallenge, parent, false);

        }



        public void setList(System.Collections.Generic.List<Mychallenge> objects)
        {
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


        public override Mychallenge this[int position]
        {
            get { return this.objects[position]; }
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Android.Views.LayoutInflater layoutInflater = ((Challenges)context).LayoutInflater;
            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.oneItemChallenge, parent, false);
            TextView tvUser = view.FindViewById<TextView>(Resource.Id.tvUser);
            TextView tvYear = view.FindViewById<TextView>(Resource.Id.tvTypeChallenge);
            TextView tvScoreCreator = view.FindViewById<TextView>(Resource.Id.tvScoreCreator);
            TextView tvPer1 = view.FindViewById<TextView>(Resource.Id.tvPer1);
            TextView tvScorePer1 = view.FindViewById<TextView>(Resource.Id.tvScorePer1);
            TextView tvPer2 = view.FindViewById<TextView>(Resource.Id.tvPer2);
            TextView tvScorePer2 = view.FindViewById<TextView>(Resource.Id.tvScorePer2);
            Mychallenge temp = objects[position];

            if (temp != null)
            {
                tvUser.Text = temp.creator.user;

                if (temp.creator.user.Equals(MainActivity.usr))
                {
                    tvUser.SetTextColor(Color.Blue);
                }

                tvYear.Text =  temp.year + "|";
                tvScoreCreator.Text = "|" + temp.creator.points;
                tvPer1.Text = "|" + temp.Team1.user;
                tvScorePer1.Text = "|" + temp.Team1.points;
                tvPer2.Text = "|" + temp.Team2.user;
                tvScorePer2.Text = "|" + temp.Team2.points;
                
         
                
                if(temp.Team1.points!= 0 && temp.Team2.points != 0)
                {
                    int maxPts = Math.Max(Math.Max(temp.Team1.points, temp.Team2.points), temp.creator.points);

                    if(temp.creator.points == maxPts)
                    {
                        tvScoreCreator.SetTextColor(Color.Green);
                        tvUser.SetTextColor(Color.Green);
                    }
                    if (temp.Team1.points == maxPts)
                    {
                        tvPer1.SetTextColor(Color.Green);
                        tvScorePer1.SetTextColor(Color.Green);
                    }
                    if (temp.Team2.points == maxPts)
                    {
                        tvPer2.SetTextColor(Color.Green);
                        tvScorePer2.SetTextColor(Color.Green);
                    }


                }

            }

            return view;


        }

    }
}