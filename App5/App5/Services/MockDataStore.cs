using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using App5.Models;
using App5.Models.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//using Newtonsoft.Json.Linq;

namespace App5.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };


            foreach (var item in mockItems)
            {
                items.Add(item);
            }
            //  string response = await GetProjectsAsync();
        }

        public static async Task<string> GetWorkItemsListAsync()
        {
            try
            {
                var personalaccesstoken = "luj4bubjlgmxoq2ap36s3gzy3xht35ilazjcsljywkfdd5xpszvq";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", personalaccesstoken))));

                    using (HttpResponseMessage response = await client.GetAsync(
                        "https://dev.azure.com/test-hackathon/test-project-hackathon/_apis/wit/wiql/d34e5ddb-c5c3-416c-8431-24fdfc818ead?api-version=5.0"))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<ApiItem> GetWorkItemsAsync()
        {
            var response = await GetWorkItemsListAsync();
            if (string.IsNullOrEmpty(response))
                return null;

            var item = JsonConvert.DeserializeObject<ApiItem>(response);

            return item;
        }


        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            var item = await GetWorkItemsAsync();

            var api = item.WorkItems.Select(x => new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = x.Id.ToString(),
                Description = x.Url.ToString()
            });

            return api.ToArray();
        }
    }
}