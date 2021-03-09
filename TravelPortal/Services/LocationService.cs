using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TravelPortal.Repository.IRepository;

namespace TravelPortal.Repository.IRepository
{
    public class LocationService : Service<string>, ILocationService
    {
        private readonly IHttpClientFactory _clientFactory;

        public LocationService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory; //Initialise the client factory
        }

        public string GetCity(string json)
        {
            string result = "";

            //Parse the json and extract the string
            JObject jObject = JObject.Parse(json);
            JToken jUser = jObject["city"];

            result = jUser.ToString();

            return result;
        }
    }
}
