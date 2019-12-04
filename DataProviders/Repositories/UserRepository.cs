using Core.DataProviders.Repositories;
using Data.Database;
using Data.Database.Models;
using Data.Models.Constants;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataProviders.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
        }

        public Task<BasicUser> AuthenticateUser(string name, string password)
        {
            try
            {
                return GetOneFromApi($"{Universe.URL}api/BasicUsers/authenticate?username={name}&password={password}");
            }
            catch
            {
                return null;
            }
        }

        public Task<List<BasicUser>> GetAllUsers()
        {
            return GetDataFromApi($"{Universe.URL}api/BasicUsers");
        }

        public Task<BasicUser> GetUserById(int id)
        {
            return GetOneFromApi($"{Universe.URL}api/BasicUsers/{id}");
        }

        public Task RegisterUserToDatabase(BasicUser user)
        {
            return Task.Run(async () =>
            {
                var temp = GetOneFromApi($"{Universe.URL}api/BasicUsers/byusername?username={user.UserName}").GetAwaiter().GetResult();
                //Existing user check
                if (temp == null)
                     await PostUserAsync($"{Universe.URL}api/BasicUsers", JsonConvert.SerializeObject(user));
                else
                    throw new System.Exception("User already exists");
            });
        }

        private async Task<List<BasicUser>> GetDataFromApi(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<BasicUser>>(await reader.ReadToEndAsync()).ToList();
                }
            }
            catch
            {
                return null;
            }
        }
        private async Task<BasicUser> GetOneFromApi(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<BasicUser>(await reader.ReadToEndAsync());
                }
            }
            catch
            {
                return null;
            }
        }

        private async Task<HttpResponseMessage> PostUserAsync(string uri, string jsondata)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    uri,
                    new StringContent(jsondata, Encoding.UTF8, "application/json"));

                return response;
            }
        }

    }
}
