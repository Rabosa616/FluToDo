using FluToDo.Mobile.Interfaces;
using FluToDo.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FluToDo.Mobile.Services
{
    public class ApiService : IApiService
    {
        private HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<IEnumerable<ToDoItem>> GetItems()
        {
            var items = new List<ToDoItem>();
            var uri = new Uri($"{GlobalSettings.FluToDoApiEndPoint}/todo");

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<ToDoItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return items;
        }

        public async Task UpdateItem(ToDoItem item)
        {
            var uri = new Uri($"{GlobalSettings.FluToDoApiEndPoint}/todo/{item.Key}");
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                if (string.IsNullOrEmpty(item.Key))
                {
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    response = await _client.PutAsync(uri, content);
                }
                var responseObject = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    item.Key = JsonConvert.DeserializeObject<string>(responseObject);
                }
                else
                {
                    throw new Exception(responseObject.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

        public async Task DeleteItem(string itemKey)
        {
            var uri = new Uri($"{GlobalSettings.FluToDoApiEndPoint}/todo/{itemKey}");
            try
            {
                var response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    string key = JsonConvert.DeserializeObject<string>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }
    }
}
