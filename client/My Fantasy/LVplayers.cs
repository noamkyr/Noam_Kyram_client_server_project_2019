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
using Newtonsoft.Json;

namespace My_Fantasy
{
    [Activity(Label = "LVplayers")]
    public class LVplayers : Activity, ListView.IOnItemClickListener
    {
        ListView lv;
        static List<Standard> Allpls;
        static List<Standard> AllplsByPos;
        static bool check = false;
        static string chosenYearInNew;

        string myAnswer = "";

        static string posChosen = "";
        string valYear = "2019";
        String[] teambuilt;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            posChosen = Intent.GetStringExtra("posChosen");
            valYear = Intent.GetStringExtra("valYear");
            teambuilt = Intent.GetStringArrayExtra("teambuilt");



            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.listPlayers);
            lv = FindViewById<ListView>(Resource.Id.lv);
            ConnectToSer c = new ConnectToSer();
            string ans = c.CallMyServer("players", valYear);
            RootobjectPlayers deserializedProduct = JsonConvert.DeserializeObject<RootobjectPlayers>(ans);

            lv = FindViewById<ListView>(Resource.Id.lv);

            Allpls = new System.Collections.Generic.List<Standard>();


            foreach (Standard item in deserializedProduct.league.standard)
            {
                if ( item.teams.Length != 0)
                {
                    Allpls.Add(item);
                }
                
            }


            //Allpls.AddRange(deserializedProduct.league.standard);
            AllplsByPos = new System.Collections.Generic.List<Standard>();
            AllplsByPos.AddRange(getByPos(Allpls));

            PlayerAdapter p = new PlayerAdapter(this, AllplsByPos);

            lv.Adapter = p;

            
            lv.OnItemClickListener = this;

        }

        public static List<Standard> getByPos(List<Standard> l)
        {
            List<Standard> newByPos = new List<Standard>();
            char pos = posChosen[posChosen.Length-1];

            foreach (Standard i in l)
            {
                if (i.pos.Contains(pos))
                {
                    newByPos.Add(i);
                }
            }
            return newByPos;
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            Standard temp = LVplayers.AllplsByPos[position];
            
            if (!teambuilt.Contains(temp.personId))
            {
                if (posChosen.Equals("PG"))
                {
                    teambuilt[0] = temp.personId;

                }else if (posChosen.Equals("SG"))
                {
                    teambuilt[1] = temp.personId;
                }
                else if (posChosen.Equals("SF"))
                {
                    teambuilt[2] = temp.personId;
                }
                else if (posChosen.Equals("PF"))
                {
                    teambuilt[3] = temp.personId;
                    
                }
                else
                {
                    teambuilt[4] = temp.personId;
                }

                check = true;
                chosenYearInNew = valYear;
                //Android.Content.Intent intent = new Android.Content.Intent(this, typeof(Building));

                //StartActivityForResult(intent, 1);
                Intent intent = new Intent();
                intent.PutExtra("playerId", temp.personId);
                intent.PutExtra("playerFullName", temp.firstName + " " + temp.lastName);
                intent.PutExtra("posChosen", posChosen);
                
                SetResult(Result.Ok, intent);

                Finish();
            }
            else
            {
                Toast.MakeText(this, "You have already chosen that player!", ToastLength.Long).Show();
            }
            

        }
        
    }
}