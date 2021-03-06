﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Threading.Tasks;
using App5.Models;
using App5.Models.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Diagnostics;

//using Newtonsoft.Json.Linq;

namespace App5.Services
{
    public class WorkerDataStore : IDataStore<Worker>
    {
        List<Worker> workers;

        public WorkerDataStore()
        {
            workers = new List<Worker>();
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

      


        public async Task<bool> AddItemAsync(Worker item)
        {
            workers.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Worker item)
        {
            var oldItem = workers.Where((Worker arg) => arg.Id == item.Id).FirstOrDefault();
            workers.Remove(oldItem);
            workers.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = workers.Where((Worker arg) => arg.Id == id).FirstOrDefault();
            workers.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Worker> GetItemAsync(string id)
        {
            return await Task.FromResult(workers.FirstOrDefault(s => s.Id == id));
        }

     
        public async Task<IEnumerable<Worker>> GetItemsAsync(bool forceRefresh = false)
        {
            string workersListURL = "https://dev.azure.com/test-hackathon/_apis/projects/bfb9c42e-3fe3-4776-9dda-616524dde178/teams/22e14cca-4ddf-47aa-83d8-36c1d94992bf/members?api-version=5.0";
            var response = await GetAuthorizedUrlAsync(workersListURL);
            var items = JsonConvert.DeserializeObject<Workers>(response);
            var api = items.Value.Select(x => new Worker()
            {
                Id = x.Identity.Id.ToString(),
                Name = x.Identity.DisplayName.ToString(),
                Email = x.Identity.UniqueName.ToString(),
                IdentityJson = x.Identity,
            });
            return api.ToArray();   
        }

        public async Task <bool> SetWorkerAsync(Worker worker, string item_id)
        {
            return await Task.FromResult(true);
            string requestText = "[" +
                "{" +
                "\"op\": \"replace\", " +
                "\"path\": \"/fields/System.AssignedTo\"," +
                "\"value\": " +
                JsonConvert.SerializeObject(worker.IdentityJson) +
                "}";

            string url = string.Format("https://dev.azure.com/test-hackathon/test-project-hackathon/_apis/wit/workitems/{0}?api-version=5.0",
                item_id);
                        
            var personalaccesstoken = "luj4bubjlgmxoq2ap36s3gzy3xht35ilazjcsljywkfdd5xpszvq";

            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json-patch+json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        System.Text.ASCIIEncoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "", personalaccesstoken))));

                var method = new HttpMethod("PATCH");
                HttpContent httpContent = new StringContent(requestText, Encoding.UTF8, "application/json-patch+json");
                var request = new HttpRequestMessage(method, url)
                {
                    Content = httpContent
                };

                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return true;
                }
                catch (TaskCanceledException e)
                {
                    Debug.WriteLine("ERROR: " + e.ToString());
                    return false;
                }
            }
                      


        }

    }
}