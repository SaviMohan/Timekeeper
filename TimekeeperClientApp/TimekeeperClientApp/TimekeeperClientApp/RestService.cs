using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TimekeeperClientApp
{
    class RestService 
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Data>> RefreshDataAsync()
        {
            System.Diagnostics.Debug.WriteLine("Hi");
            List<Data> Items = new List<Data>();

            var response = await client.GetAsync("http://timekeeperapi.azurewebsites.net/api.php/test3");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();                       
                System.Diagnostics.Debug.WriteLine(content);
                //Items = JsonConvert.DeserializeObject<List<Data>>(content);
            } else
            {
                System.Diagnostics.Debug.WriteLine(response);
                System.Diagnostics.Debug.WriteLine("Bye");
            }   
            return Items;
        }
    }
}
