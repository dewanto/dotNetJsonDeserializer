using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.IO;

namespace JSONDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Initial Console Commands
            Console.WriteLine("Welcome to the BreweryDB");
            Console.Write("What ZipCode would you like to search?: ");
            string zipcode = Console.ReadLine();
            #endregion

            #region HttpWebRequest
            string requestUri = "http://api.brewerydb.com/v2/locations?key=656235b356644cb485f32c575cddb9e6&postalCode=" + zipcode;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            httpWebRequest.Accept = "application/json";

            string text;
            var response = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            #endregion

            #region JSON Deserialization
            RootObject ro = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(text);
            #endregion

            #region Ending Console Commands
            Console.WriteLine("Breweries Found"); 
            
            foreach(Datum datum in ro.data)
            {
                Console.WriteLine( datum.brewery.name );
            }
            Console.ReadLine();
            #endregion

        }
    }

    #region Json Import
    public class Images
    {
        public string icon { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
    }

    public class Brewery
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public string established { get; set; }
        public string isOrganic { get; set; }
        public Images images { get; set; }
        public string status { get; set; }
        public string statusDisplay { get; set; }
        public string createDate { get; set; }
        public string updateDate { get; set; }
    }

    public class Country
    {
        public string isoCode { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string isoThree { get; set; }
        public int numberCode { get; set; }
        public string urlTitle { get; set; }
        public string createDate { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string name { get; set; }
        public string streetAddress { get; set; }
        public string locality { get; set; }
        public string region { get; set; }
        public string postalCode { get; set; }
        public string phone { get; set; }
        public string hoursOfOperation { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string isPrimary { get; set; }
        public string inPlanning { get; set; }
        public string isClosed { get; set; }
        public string openToPublic { get; set; }
        public string locationType { get; set; }
        public string locationTypeDisplay { get; set; }
        public string countryIsoCode { get; set; }
        public string yearOpened { get; set; }
        public string status { get; set; }
        public string statusDisplay { get; set; }
        public string createDate { get; set; }
        public string updateDate { get; set; }
        public string breweryId { get; set; }
        public Brewery brewery { get; set; }
        public Country country { get; set; }
        public string website { get; set; }
    }

    public class RootObject
    {
        public int currentPage { get; set; }
        public int numberOfPages { get; set; }
        public int totalResults { get; set; }
        public List<Datum> data { get; set; }
        public string status { get; set; }
    }
#endregion

}
