using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;
using TravelPortal.Models;
using TravelPortal.Repository.IRepository;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace TravelPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _cityRepository;
        private readonly string accessKey;

        public LocationController(ILocationService cityRepository)
        {
            _cityRepository = cityRepository;

            //Read config from appsettings.json
            var config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json").Build();


            var section = config.GetSection(nameof(LocationConfiguration));
            var LocationConfig = section.Get<LocationConfiguration>();
            accessKey = LocationConfig.key; // get the access key for ipstack.com
        }

        /// <summary>
        /// Get city from the IP address of current request
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            string cityRepositoryJson = "";

            //The ipstack API detects the IP address from which the current API request is coming from
            //and returns its location (Refer to ipstack.com) 

            string path = "http://api.ipstack.com/check";

            //get the parameters
            string parameters = $"?access_key={accessKey}";

            cityRepositoryJson = await _cityRepository.GetAsync(path, parameters);

            if (!String.IsNullOrEmpty(cityRepositoryJson))
            {

                JObject jObject = JObject.Parse(cityRepositoryJson);

                if (jObject.ContainsKey("city"))// check if json contains the details of city
                {
                    Location locationDetails = new Location
                    {
                        city = _cityRepository.GetCity(cityRepositoryJson) // Get city from json
                    };

                    //Convert object to json
                    cityRepositoryJson = JValue.Parse(JsonConvert.SerializeObject(locationDetails)).ToString(Formatting.Indented);
                }

                //return result 
                return Ok(cityRepositoryJson);
            }
            else
            {
                return NoContent();
            }
        }

      
    }
}
