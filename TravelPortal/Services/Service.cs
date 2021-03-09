using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TravelPortal.Repository.IRepository;

namespace TravelPortal.Repository
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;

        public Service(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<string> GetAsync(string url, string parameters)
        {
            //Use HttpClientFactory to create the http client
            var httpClient = _clientFactory.CreateClient();

            string finalUrl = url + parameters;
            httpClient.BaseAddress = new Uri(finalUrl);

            //Makes a GET request to external API
            HttpResponseMessage response = await httpClient.GetAsync(finalUrl);

            if (response.IsSuccessStatusCode)
            {
                var dataObjects = await response.Content.ReadAsStringAsync();
                return dataObjects;
            }
            return null;
        }

    }
}
