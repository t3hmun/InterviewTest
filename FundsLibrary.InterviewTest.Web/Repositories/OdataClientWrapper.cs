using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using Newtonsoft.Json.Linq;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface IOdataClientWrapper
    {
        Task<IEnumerable<Fund>> GetAndReadFromContentGetAsync(Guid managerGuid);
    }

    /// <summary>
    /// Ideally this should use a nice OData library.
    /// However it is not worth the time for a single example.
    /// </summary>
    public class OdataClientWrapper : IOdataClientWrapper
    {
        private readonly string _serviceAppUrl;
        private readonly string _authToken;

        public OdataClientWrapper(string serviceAppUrl, string authToken)
        {
            _serviceAppUrl = serviceAppUrl;
            _authToken = authToken;
        }

        public async Task<IEnumerable<Fund>> GetAndReadFromContentGetAsync(Guid managerGuid)
        {
            using (var client = new HttpClient())
            {
                _SetupClient(client);
                // A unfortunately inefficient OData search.
                // The any operator required to do a faster does not seem to be supported on fundslibrary.
                // The select operator fails due to some missing 'Documents' entries.
                // Search is the only way left to find the manager's guid in the team list.
                var requestUri = $"?$search={managerGuid}";

                var response = await client.GetAsync(requestUri);

                response.EnsureSuccessStatusCode();
                //TODO: Handle non success HTTP codes more gracefully.

                var rawJson = await response.Content.ReadAsStringAsync();
                var values = JObject.Parse(rawJson)["value"].Children().ToList();
                var funds = new List<Fund>();
                foreach (var value in values)
                {
                    var staticdata = value["StaticData"];
                    var identification = staticdata["Identification"];
                    var isin = (string) identification["IsinCode"];
                    var fullname = (string) identification["FullName"];
                    funds.Add(new Fund()
                    {
                        IsinCode = isin,
                        FullName = fullname
                    });
                }
                return funds;
            }
        }

        private void _SetupClient(HttpClient client)
        {
            client.BaseAddress = new Uri(_serviceAppUrl);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
        }
    }
}