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

    public class RootobjectPlayers
    {
        public _Internal _internal { get; set; }
        public League league { get; set; }
    }

    public class _Internal
    {
        public string pubDateTime { get; set; }
        public string xslt { get; set; }
        public string eventName { get; set; }
    }

    public class League
    {
        public Standard[] standard { get; set; }
        public Africa[] africa { get; set; }
        public Sacramento[] sacramento { get; set; }
        public Vega[] vegas { get; set; }
        public Utah[] utah { get; set; }
    }

    public class Standard
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string personId { get; set; }
        public string teamId { get; set; }
        public string jersey { get; set; }
        public bool isActive { get; set; }
        public string pos { get; set; }
        public string heightFeet { get; set; }
        public string heightInches { get; set; }
        public string heightMeters { get; set; }
        public string weightPounds { get; set; }
        public string weightKilograms { get; set; }
        public string dateOfBirthUTC { get; set; }
        public Team[] teams { get; set; }
        public Draft draft { get; set; }
        public string nbaDebutYear { get; set; }
        public string yearsPro { get; set; }
        public string collegeName { get; set; }
        public string lastAffiliation { get; set; }
        public string country { get; set; }
    }

    public class Draft
    {
        public string teamId { get; set; }
        public string pickNum { get; set; }
        public string roundNum { get; set; }
        public string seasonYear { get; set; }
    }

    public class Team
    {
        public string teamId { get; set; }
        public string seasonStart { get; set; }
        public string seasonEnd { get; set; }
    }

    public class Africa
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string personId { get; set; }
        public string teamId { get; set; }
        public string jersey { get; set; }
        public bool isActive { get; set; }
        public string pos { get; set; }
        public string heightFeet { get; set; }
        public string heightInches { get; set; }
        public string heightMeters { get; set; }
        public string weightPounds { get; set; }
        public string weightKilograms { get; set; }
        public string dateOfBirthUTC { get; set; }
        public Team1[] teams { get; set; }
        public Draft1 draft { get; set; }
        public string nbaDebutYear { get; set; }
        public string yearsPro { get; set; }
        public string collegeName { get; set; }
        public string lastAffiliation { get; set; }
        public string country { get; set; }
    }

    public class Draft1
    {
        public string teamId { get; set; }
        public string pickNum { get; set; }
        public string roundNum { get; set; }
        public string seasonYear { get; set; }
    }

    public class Team1
    {
        public string teamId { get; set; }
        public string seasonStart { get; set; }
        public string seasonEnd { get; set; }
    }

    public class Sacramento
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string personId { get; set; }
        public string teamId { get; set; }
        public string jersey { get; set; }
        public bool isActive { get; set; }
        public string pos { get; set; }
        public string heightFeet { get; set; }
        public string heightInches { get; set; }
        public string heightMeters { get; set; }
        public string weightPounds { get; set; }
        public string weightKilograms { get; set; }
        public string dateOfBirthUTC { get; set; }
        public Team2[] teams { get; set; }
        public Draft2 draft { get; set; }
        public string nbaDebutYear { get; set; }
        public string yearsPro { get; set; }
        public string collegeName { get; set; }
        public string lastAffiliation { get; set; }
        public string country { get; set; }
    }

    public class Draft2
    {
        public string teamId { get; set; }
        public string pickNum { get; set; }
        public string roundNum { get; set; }
        public string seasonYear { get; set; }
    }

    public class Team2
    {
        public string teamId { get; set; }
        public string seasonStart { get; set; }
        public string seasonEnd { get; set; }
    }

    public class Vega
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string personId { get; set; }
        public string teamId { get; set; }
        public string jersey { get; set; }
        public bool isActive { get; set; }
        public string pos { get; set; }
        public string heightFeet { get; set; }
        public string heightInches { get; set; }
        public string heightMeters { get; set; }
        public string weightPounds { get; set; }
        public string weightKilograms { get; set; }
        public string dateOfBirthUTC { get; set; }
        public Team3[] teams { get; set; }
        public Draft3 draft { get; set; }
        public string nbaDebutYear { get; set; }
        public string yearsPro { get; set; }
        public string collegeName { get; set; }
        public string lastAffiliation { get; set; }
        public string country { get; set; }
    }

    public class Draft3
    {
        public string teamId { get; set; }
        public string pickNum { get; set; }
        public string roundNum { get; set; }
        public string seasonYear { get; set; }
    }

    public class Team3
    {
        public string teamId { get; set; }
        public string seasonStart { get; set; }
        public string seasonEnd { get; set; }
    }

    public class Utah
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string personId { get; set; }
        public string teamId { get; set; }
        public string jersey { get; set; }
        public bool isActive { get; set; }
        public string pos { get; set; }
        public string heightFeet { get; set; }
        public string heightInches { get; set; }
        public string heightMeters { get; set; }
        public string weightPounds { get; set; }
        public string weightKilograms { get; set; }
        public string dateOfBirthUTC { get; set; }
        public object[] teams { get; set; }
        public Draft4 draft { get; set; }
        public string nbaDebutYear { get; set; }
        public string yearsPro { get; set; }
        public string collegeName { get; set; }
        public string lastAffiliation { get; set; }
        public string country { get; set; }
    }

    public class Draft4
    {
        public string teamId { get; set; }
        public string pickNum { get; set; }
        public string roundNum { get; set; }
        public string seasonYear { get; set; }
    }

}