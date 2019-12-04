using Core.DataProviders.Repositories;
using Data.Database;
using Data.Database.Models;
using Data.Models.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataProviders.Repositories
{
    public class SaleItemsRepository : ISaleItemsRepository
    {

        public SaleItemsRepository()
        {
        }

        public Task AddSaleItem(SaleItem item)
        {
            return PostItemAsync($"{Universe.URL}api/SaleItems", JsonConvert.SerializeObject(item));
        }

        public Task<List<SaleItem>> GetAllSaleItems()
        {
            Update().GetAwaiter();
            return GetDataFromApi($"{Universe.URL}api/SaleItems");
        }

        public Task<List<SaleItem>> GetItemsByUser(int userId)
        {
            Update().GetAwaiter();
            return GetDataFromApi($"{Universe.URL}api/SaleItems/byuserid?id={userId}");
        }

        public Task<List<SaleItem>> GetNotOwnItems(int actualUserId)
        {
            Update().GetAwaiter();
            return GetDataFromApi($"{Universe.URL}api/SaleItems/bynotuserid?id={actualUserId}");
        }

        public Task RemoveSaleItem(SaleItem item)
        {
            return RemoveAsync($"{Universe.URL}api/SaleItems/{item.id}");
        }

        public Task UpdateItemAuctionPrice(SaleItem item, long newValue, string newWinner)
        {
            Update().GetAwaiter();
            item.AuctionWinner = newWinner;
            item.AuctionPrice = newValue;
            return PutItemAsync($"{Universe.URL}api/SaleItems/{item.id}", JsonConvert.SerializeObject(item));
        }

        private async Task Update()
        {
            //Update DB
            var db = await GetDataFromApi($"{Universe.URL}api/SaleItems");
            var ret = db.Where(item => (DateTime.Now.DayOfYear - item.TimeAdded.DayOfYear) > 3);
            foreach(var item in ret)
            {
                await RemoveAsync($"{Universe.URL}api/SaleItems/{item.id}");
            }

        }

        private async Task<List<SaleItem>> GetDataFromApi(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<SaleItem>>(await reader.ReadToEndAsync()).ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        private async Task<HttpResponseMessage> PostItemAsync(string uri, string jsondata)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    uri,
                    new StringContent(jsondata, Encoding.UTF8, "application/json"));

                return response;
            }
        }

        private async Task<HttpResponseMessage> RemoveAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(
                    uri
                    );

                return response;
            }
        }

        private async Task<HttpResponseMessage> PutItemAsync(string uri, string jsondata)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(
                    uri,
                    new StringContent(jsondata, Encoding.UTF8, "application/json"));
                return response;
            }
        }
    }
}
