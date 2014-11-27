using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Client;
using Newtonsoft.Json;

namespace Lisa.Kiwi.WebApi.Access
{
	public class ReportProxy
	{
		public ReportProxy(Uri odataUrl, string token = null, string tokenType = null)
		{
            _container = new AuthenticationContainer(odataUrl, token, tokenType);
            _odataUri = odataUrl;
		}

		// Get an entire entity set.
		public IQueryable<Report> GetReports()
		{
            return _container.Report;             
		}

		//Create a new entity
		public Report AddReport(Report report)
		{
			_container.AddToReport(report);
			_container.SaveChanges();

            return report;
		}


        public async Task<Report> AddManualReport(Report report)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_odataUri + "/Report")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            var mappedReport = new 
            {
                Description = report.Description,
                Created = report.Created,
                Location = report.Location,
                Time = report.Time,
                Guid = report.Guid,
                UserAgent = report.UserAgent,
                Ip = report.Ip,
                Type = report.Type
            };

            var serializedReport = JsonConvert.SerializeObject(mappedReport);

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, new Uri(_odataUri + "/Report"));
            req.Content = new StringContent(serializedReport, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(req);
            
            var requestResponse = await response.Content.ReadAsStringAsync();
            var returnReponse = JsonConvert.DeserializeObject<Report>(requestResponse);

            return returnReponse;
        }

	    public void SaveReport(Report report)
	    {
            _container.ChangeState(report, EntityStates.Modified);
	        _container.SaveChanges();
	    }

        private readonly AuthenticationContainer _container;
        private readonly Uri _odataUri;
	}
}