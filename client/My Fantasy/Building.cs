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
    [Activity(Label = "Building")]

    public class Building : Activity, Android.Views.View.IOnClickListener
    {
        int selBtn = 0;
        Button btnSubmitCha;

        Button inv1;
        Button inv2;
        Dialog d;
        Button btnSubInv;
        EditText etUser;
        static Spinner spinner;
        Button bpg, bsg, bsf, bpf, bc;
        static string posChosen = "";
        static string valYear = "2019";
        static String[] teambuilt;
        static TextView tvPg, tvSg, tvSf, tvPf, tvC;
        static EditText etTeamName;
        static string usr1 = string.Empty;
        static string usr2 = string.Empty;
        static string challengesChosenChalYear;
        string challengeID;




        public void OnClick(View v)
        {

            if (v == inv1 || v == inv2)
            {   

                //if the user pressed to invite another user

                if(v == inv1)
                {
                    selBtn = 1;
                }
                else
                {
                    selBtn = 2;
                }

                //pick user from list
                Android.Content.Intent intent = new Android.Content.Intent(this, typeof(LVUsers));
                StartActivityForResult(intent, 2);

            }



            if (v == bc || v == bpf || v == bsf || v == bsg || v == bpg)
            {
                if (v == bc)
                {
                    posChosen = "C";

                } else if (v == bpf)
                {
                    posChosen = "PF";
                }
                else if (v == bsf)
                {
                    posChosen = "SF";
                }
                else if (v == bsg)
                {
                    posChosen = "SG";
                }
                else
                {
                    posChosen = "PG";
                }

                Android.Content.Intent intent = new Android.Content.Intent(this, typeof(LVplayers));

                intent.PutExtra("valYear", valYear);
                intent.PutExtra("teambuilt", teambuilt);
                intent.PutExtra("posChosen", posChosen);

                StartActivityForResult(intent, 1);

            }




            if (v == btnSubInv)
            {
                string i = etUser.Text;
                //bool b = findPer(i);

                bool b = true;


            }

            if (v == btnSubmitCha)
            {
                if (isGoodCha())
                {
                    string joinedArr = string.Join("/", teambuilt);
                    if (challengesChosenChalYear == null )
                    {
                        //create Challenge

                        ConnectToSer c = new ConnectToSer();
                        
                        string ans = c.CallMyServer("addChallenge", MainActivity.usr+"/"+joinedArr+"/"+etTeamName.Text+"/"+usr1+"/"+usr2+"/"+valYear);
                        if (ans.Equals("Added"))
                        {

                            
                            Intent intent = new Intent();
                            SetResult(Result.Ok, intent);
                            Finish();
                            //send some vars with intent

                        }

                    }
                    else
                    {
                        //send team to server and edit challenge
                        ConnectToSer c = new ConnectToSer();
                        string ans = c.CallMyServer("addTeam", challengeID+"/"+joinedArr+"/"+ etTeamName.Text + "/"+MainActivity.usr);
                        Intent intent = new Intent();
                        SetResult(Result.Ok, intent);
                        Finish();
                    }

                }
            }

       

        }



        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if ( resultCode != Result.Ok)
            {
                return;
            }

            switch(requestCode)
            {
                case 1:
                    doPlayerPick(data);
                    break;
                case 2:
                    doUserPick(data);
                    break;
            }
        }

        private void doUserPick(Intent data)
        {
            string returnName = data.GetStringExtra("name");
            string returnUserId = data.GetStringExtra("userId");

            if (!string.IsNullOrEmpty(usr1) && usr1.Equals(returnName))
            {
                Toast.MakeText(this, "you have already chose user "+returnName, ToastLength.Long).Show();
                return;
            }

            if (!string.IsNullOrEmpty(usr2) && usr2.Equals(returnName))
            {
                Toast.MakeText(this, "you have already chose user " + returnName, ToastLength.Long).Show();
                return;
            }

            //etUser.Text = returnName;

            if (selBtn == 1)
            {
                usr1 = returnName;
                inv1.Text = usr1;
            }
            else
            {
                usr2 = returnName;
                inv2.Text = usr2;
            }

        }

        private void doPlayerPick(Intent data)
        {
            string returnPlayerId = data.GetStringExtra("playerId");
            string returnPlayerFullName = data.GetStringExtra("playerFullName");
            string returnPosChosen = data.GetStringExtra("posChosen");

            Toast.MakeText(this, "returnPlayerFullName = " + returnPlayerFullName, ToastLength.Long).Show();
            spinner.Enabled = false;



            switch (returnPosChosen)
            {
                case "PG":
                    tvPg.Text = returnPlayerFullName;
                    teambuilt[0] = returnPlayerId;
                    break;
                case "SG":
                    tvSg.Text = returnPlayerFullName;
                    teambuilt[1] = returnPlayerId;
                    break;
                case "SF":
                    tvSf.Text = returnPlayerFullName;
                    teambuilt[2] = returnPlayerId;
                    break;
                case "PF":
                    tvPf.Text = returnPlayerFullName;
                    teambuilt[3] = returnPlayerId;
                    break;
                case "C":
                    tvC.Text = returnPlayerFullName;
                    teambuilt[4] = returnPlayerId;
                    break;
            }

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            usr1 = string.Empty;
            usr2 = string.Empty;



            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.newTeam);

            challengesChosenChalYear = Intent.GetStringExtra("chosenChalYear");
            challengeID = Intent.GetStringExtra("challengeID");
            //challengesIsNewChal = Intent.GetBooleanExtra("isNewChal",false);


            spinner = FindViewById<Spinner>(Resource.Id.spinner);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.years_array, Android.Resource.Layout.SimpleSpinnerItem);

        

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            tvC = FindViewById<TextView>(Resource.Id.tvc);
            tvPg = FindViewById<TextView>(Resource.Id.tvpg);
            tvSg = FindViewById<TextView>(Resource.Id.tvsg);
            tvPf = FindViewById<TextView>(Resource.Id.tvpf);
            tvSf = FindViewById<TextView>(Resource.Id.tvsf);

                        

            bpg = FindViewById<Button>(Resource.Id.btnpg);
            bpg.SetOnClickListener(this);

            bsg = FindViewById<Button>(Resource.Id.btnsg);
            bsg.SetOnClickListener(this);

            bsf = FindViewById<Button>(Resource.Id.btnsf);
            bsf.SetOnClickListener(this);

            bpf = FindViewById<Button>(Resource.Id.btnpf);
            bpf.SetOnClickListener(this);

            bc = FindViewById<Button>(Resource.Id.btncenter);
            bc.SetOnClickListener(this);

            btnSubmitCha = FindViewById<Button>(Resource.Id.btnCreateCha);
            btnSubmitCha.SetOnClickListener(this);

            inv1 = FindViewById<Button>(Resource.Id.btnInvite1);
            inv1.SetOnClickListener(this);

            inv2 = FindViewById<Button>(Resource.Id.btnInvite2);
            inv2.SetOnClickListener(this);
 

            etTeamName = FindViewById<EditText>(Resource.Id.etNewTeamName);


            //if (teambuilt == null)
            //{
            teambuilt = new String[5];
            //}



            if (challengesChosenChalYear != null)
            {

                setSelSpinner(challengesChosenChalYear);

                spinner.Enabled = false;

                inv1.Enabled = false;
                inv2.Enabled = false;


            }




        }


        public void setSelSpinner(string y)
        {
            if (y.Equals("2019"))
            {
                spinner.SetSelection(0);
            }
            else if (y.Equals("2018"))
            {
                spinner.SetSelection(1);
            }
            else if (y.Equals("2017"))
            {
                spinner.SetSelection(2);
            }
            else if (y.Equals("2016"))
            {
                spinner.SetSelection(3);
            }
            else if (y.Equals("2015"))
            {
                spinner.SetSelection(4);
            }
            else if (y.Equals("2014"))
            {
                spinner.SetSelection(5);
            }
            else if (y.Equals("2013"))
            {
                spinner.SetSelection(6);
            }
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            valYear = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, valYear, ToastLength.Long).Show();
       
        }


        public static bool isGoodCha()
        {
            for(int i =0; i<teambuilt.Length; i++)
            {
                if(teambuilt[i] == null || teambuilt[i].Equals(""))
                {
                    return false;
                }
            }
            if (etTeamName.Text == null || etTeamName.Text.Equals(""))
            {
                return false;
            }

            if(challengesChosenChalYear == null)
            {
                if (usr1 == null || usr2 == null)
                {
                    return false;
                }

                if (usr1.Equals(usr2))
                {
                    return false;
                }

            }

            //check isExists user

            
            return true;

        }


    }
}