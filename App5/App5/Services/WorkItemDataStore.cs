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
    public class WorkItemDataStore : IDataStore<Item>
    {
        List<Item> items;

        public WorkItemDataStore()
        {
            items = new List<Item>();
        }

        public static async Task<string> GetAuthorizedUrlAsync(string url)
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
                        url))
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

        public async Task<IEnumerable<WorkItemInstance>> GetWorkItemInstancesAsync()
        {
            string workItemsListURL = "https://dev.azure.com/test-hackathon/test-project-hackathon/_apis/wit/wiql/d34e5ddb-c5c3-416c-8431-24fdfc818ead?api-version=5.0";
            var response = await GetAuthorizedUrlAsync(workItemsListURL);
            if (string.IsNullOrEmpty(response))
                return null;

            var items = JsonConvert.DeserializeObject<ApiItem>(response);
            //
            List<WorkItemInstance> workItems = new List<WorkItemInstance> { };
            foreach (var item in items.WorkItems)
            {
                workItems.Add(JsonConvert.DeserializeObject<WorkItemInstance>(await GetAuthorizedUrlAsync(item.Url.ToString())));
            }
            return workItems;
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
            var items = await GetWorkItemInstancesAsync();

            var api = items.Select(x => new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = x.Fields.SystemTitle?.ToString(),
                Description = x.Fields.SystemDescription?.ToString(),
                State = x.Fields.SystemBoardColumn?.ToString(),
                AssignedTo = x.Fields.SystemAssignedTo?.DisplayName?.ToString(),
                Category = x.Fields.SystemWorkItemType?.ToString(),
                Icon = Item.GetIcon(x.Fields.SystemWorkItemType?.ToString())
            });

            return api.ToArray();
        }
    }
}