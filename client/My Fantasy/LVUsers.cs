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

using My_Fantasy_UsersOBJ;


namespace My_Fantasy
{
    [Activity(Label = "LVUsers")]
    public class LVUsers : Activity, ListView.IOnItemClickListener
    {
        ListView lv;
        List<SingleUser> allUsers;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.listUsers);
            // Create your application here

            lv = FindViewById<ListView>(Resource.Id.lv);
            ConnectToSer c = new ConnectToSer();
            string ans = c.CallMyServer("getUsers", "");
            //List<My_Fantasy_UsersOBJ.Rootobject> deserializedProduct = JsonConvert.DeserializeObject<My_Fantasy_UsersOBJ.Rootobject>(ans);

            allUsers = JsonConvert.DeserializeObject<List<SingleUser>>(ans);

            removeMe();

            lv = FindViewById<ListView>(Resource.Id.lvUsers);

            //allUsers = new System.Collections.Generic.List<User>();

            //allUsers.AddRange(deserializedProduct.users);

            UserAdapter p = new UserAdapter(this, allUsers);

            lv.Adapter = p;

            lv.OnItemClickListener = this;


        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {

            SingleUser temp = allUsers[position];
            Intent intent = new Intent();
            intent.PutExtra("name", temp.name);
            intent.PutExtra("userId", temp.userId);

            SetResult(Result.Ok, intent);

            Finish();

        }

        public void removeMe()
        {
            //remove the user. He cannot pick himself
            List<SingleUser> newL = new List<SingleUser>();
            foreach (SingleUser item in allUsers)
            {
                if (!item.name.Equals(MainActivity.usr))
                {
                    newL.Add(item);
                }
            }
            allUsers = newL;
        }

    }
}