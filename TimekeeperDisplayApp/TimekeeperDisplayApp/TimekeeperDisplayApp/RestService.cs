﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TimekeeperDisplayApp
{
    public class RestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Data>> RefreshDataAsync()
        {
            List<Data> items = new List<Data>();
            var response = await client.GetAsync("http://timekeeperapi.azurewebsites.net/api.php/test3");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                items = extractData(content);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(response);
            }
            return items;
        }

        private List<Data> extractData(string content)
        {
            int startPos;
            int length;
            string subString;
            Data myData;
            List<Data> dataList = new List<Data>();
            content = content.Remove(0, content.IndexOf("records"));

            while (true)
            {
                if (content.IndexOf('{') != -1)
                {
                    startPos = content.IndexOf('{');
                    length = content.IndexOf('}') - startPos + 1;
                    subString = content.Substring(startPos, length);
                    subString = subString.Replace("\\", "");
                    myData = JsonConvert.DeserializeObject<Data>(subString);
                    dataList.Add(myData);
                    content = content.Remove(startPos, length);
                }
                else
                {
                    break;
                }
            }
            return dataList;
        }
    }
}