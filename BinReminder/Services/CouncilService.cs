using BinReminder.Interfaces;
using BinReminder.Models.BinModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Services
{
    public class CouncilService : ICouncilService
    {
        private static readonly HttpClient client = new();

        public async Task<string> GetAuthKey()
        {
            HttpResponseMessage response = await client.GetAsync(AppConfig.AUTH_URL);
            var auth = response.Headers.GetValues("Authorization")?.FirstOrDefault();
            if (!string.IsNullOrEmpty(auth))
                return auth;

            return null;
        }

        private async Task<IEnumerable<Bin>> GetAllBins(string authKey, string requestBody)
        {
            HttpRequestMessage request = new(HttpMethod.Post, AppConfig.BIN_URL);

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", authKey);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var responseContentJson = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<BinCalendarReturnBody>(responseContentJson)?.Data?.Bins;
        }

        public async Task<IEnumerable<Bin>> GetThisWeeksBins(string authKey, string requestBody)
        {
            IEnumerable<Bin> allBins = await GetAllBins(authKey, requestBody);
            var thisWeeksBins = allBins.Where(b => b.Date < DateTime.Today.AddDays(7)).ToList();

            // if there are bins being collected on two dates this week, remove the ones furthest away
            // We only want to return the bins for a single collection day
            if (thisWeeksBins.DistinctBy(b => b.Date).Any())
                thisWeeksBins.RemoveAll(b => b.Date > thisWeeksBins.Min(b => b.Date));

            return thisWeeksBins;
        }

        public async Task<string> GetPropertyUprn(string authKey, string postCode, string houseNumber)
        {
            HttpRequestMessage request = new(HttpMethod.Post, AppConfig.OBJECT_ID_URL);
            string requestBody = JsonConvert.SerializeObject(new ObjectIdRequestBody(postCode));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", authKey);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var responseContentJson = await response.Content.ReadAsStringAsync();

            List<HouseObject> housesInPostcode = JsonConvert.DeserializeObject<ObjectIdReturnBody>(responseContentJson)?.HousesInPostcode?.ToList();
            var selectedHouseId = housesInPostcode.Where(h => h.Label.Split(" ").First() == houseNumber).First()?.ObjectId;

            HttpRequestMessage uprnRequest = new(HttpMethod.Post, AppConfig.UPRN_BASE_URL + selectedHouseId);
            uprnRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            uprnRequest.Headers.Add("Authorization", authKey);

            var uprnResponse = await client.SendAsync(uprnRequest);
            var uprnResponseContentJson = await uprnResponse.Content.ReadAsStringAsync();

            string uprn = JsonConvert.DeserializeObject<UprnReturnBody>(uprnResponseContentJson)?.Data?.propertyUprn;

            return uprn;
        }
    }
}
