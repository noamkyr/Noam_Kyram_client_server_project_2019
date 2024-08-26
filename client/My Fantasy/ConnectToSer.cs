using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;



namespace My_Fantasy
{
    class ConnectToSer
    {

        public static string myurl = "http://192.168.3.106:5000/";


        public string CallMyServer(string function, string param)
        {

            string xurl = "";
            if (string.IsNullOrEmpty(param))
            {
                xurl = myurl + function;
            }
            else
            {
                xurl = myurl + function + "/" + param;
            }
            //Toast.MakeText(this, "b4 call url = " + xurl, ToastLength.Long).Show();

            var request = (HttpWebRequest)WebRequest.Create(xurl);

            request.Method = "GET";
            //request.UserAgent = RequestConstants.UserAgentValue;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;


            var content = string.Empty;
            request.Timeout = 100000;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();

                    }
                }
            }



            return content;
        }


    }
}