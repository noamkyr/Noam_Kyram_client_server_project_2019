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

    public class Rootobject
    {
        public Mychallenge[] myChallenges { get; set; }
    }

    public class Mychallenge
    {
        public Team1 Team1 { get; set; }
        public Team1 Team2 { get; set; }
        public Team1 creator { get; set; }
        public int status { get; set; }
        public string teamId { get; set; }
        public string year { get; set; }
        public string challengeID { get; set; }
    }

    public class Team1
    {
        public string name { get; set; }
        public object ownerId { get; set; }
        public string[] players { get; set; }
        public int points { get; set; }
        public string teamId { get; set; }
        public string user { get; set; }
        public string year { get; set; }
    }

    public class Team2
    {
        public string name { get; set; }
        public object ownerId { get; set; }
        public string[] players { get; set; }
        public int points { get; set; }
        public string teamId { get; set; }
        public string user { get; set; }
        public string year { get; set; }
    }

    public class Creator
    {
        public string name { get; set; }
        public object ownerId { get; set; }
        public string[] players { get; set; }
        public int points { get; set; }
        public string teamId { get; set; }
        public string user { get; set; }
        public string year { get; set; }
    }

}