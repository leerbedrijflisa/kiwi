using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Lisa.Kiwi.WebApi.Access
{
    public class Report
    {
        public string Description { get; set; }
    }

    public class Connector
    {
        public static HttpClient Client;


        public async Task<Report[]> GetAllReports()
        {
                using (Client = new HttpClient())
                {
                    Client.BaseAddress = new Uri("http://localhost:20151/");
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // HTTP GET Reports
                    HttpResponseMessage reports = await Client.GetAsync("odata/Report");
                    if (reports.IsSuccessStatusCode)
                    {
                        Report[] result = await reports.Content.ReadAsAsync<Report[]>();
                        return result;
                    }
                    return null;
                }
            }
        
    }
}
