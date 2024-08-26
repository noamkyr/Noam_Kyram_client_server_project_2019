using Android.App;
using Android.OS;
using Android.Widget;
using Android.Views;
using System.IO;
using System.Text;
using Android.Content;

namespace My_Fantasy
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : Activity, Android.Views.View.IOnClickListener
    {

        EditText newUserName;
        Button submitNewUser;
        public string str;
        int count = 0;
        public static string usr;
        


        public void OnClick(View v)
        {
            if (v == submitNewUser)
            {

                string str = newUserName.Text;
                string ans = string.Empty;
                if (!str.Equals("") )
                {
                    try
                    {
                        ConnectToSer c = new ConnectToSer();

                        ans = c.CallMyServer("user", str);
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



                    if (ans.Equals("Added"))
                    {
                        try
                        {
                            using (Stream stream = OpenFileOutput("username.xml", FileCreationMode.Private))
                            {

                                if (str != null)
                                {

                                    try
                                    {
                                        
                                        stream.Write(Encoding.UTF8.GetBytes(str), 0, str.Length);

                                        stream.Close();

                                        Toast.MakeText(this, "saved!", ToastLength.Long).Show();
                                        MainActivity.usr = str;
                                        Android.Content.Intent i = new Android.Content.Intent(this, typeof(Challenges));
                                        StartActivityForResult(i, 1);

                                    }

                                    catch (Java.IO.IOException e)
                                    {

                                        e.PrintStackTrace();

                                    }
                                }

                                Android.Content.Intent intent = new Android.Content.Intent(this, typeof(Challenges));

                                StartActivityForResult(intent, 1);//1 means go to test


                            }
                        }
                        catch (Java.IO.FileNotFoundException e)
                        {
                            e.PrintStackTrace();
                        }

                    }

                    if (ans.Equals("InList"))
                    {
                        Toast.MakeText(this, "already exists!", ToastLength.Long).Show();
                        newUserName.Text = "";
                    }

                }

            }

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {



            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Harshama);

            bool b = IsReg();

            if (b)
            {
                               
                Android.Content.Intent intent = new Android.Content.Intent(this, typeof(Challenges));
                StartActivityForResult(intent, 1);
            }
            

            newUserName = FindViewById<EditText>(Resource.Id.etNewUname);
            submitNewUser = FindViewById<Button>(Resource.Id.btnNewUser);

            submitNewUser.SetOnClickListener(this);


        }

        public override void OnBackPressed()
        {
            // disables you come back
            return;

        }

        public bool IsReg()
        {
            
            try

            {

                //using (var i = new StreamReader(OpenFileInput(NumeroCartaoFilename)))

                using (Stream inTo = OpenFileInput("username.xml"))

                    try

                    {

                        byte[] buffer = new byte[4096];

                        int num  = inTo.Read(buffer, 0, buffer.Length);

                        str = System.Text.Encoding.UTF8.GetString(buffer);

                        str = str.Substring(0, num);

                        inTo.Close();

                        if (str != null) {
                            usr = str;
                            return true;
                        }

                        return false;
                    }

                    catch (Java.IO.IOException e)

                    {

                        e.PrintStackTrace();
                        return false;

                    }

                    

            }

            catch (Java.IO.FileNotFoundException e)

            {

                e.PrintStackTrace();
                return false;

            }

            


        }




    }
}