using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CancellationTutorial.API.Interfaces;
using CancellationTutorial.API.Records;
using Newtonsoft.Json;
using RestSharp;

namespace CancellationTutorial.API.Services
{
    public class GenericService : IGenericService
    {
        private readonly IRestClient _restClient;

        public GenericService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<GenericRecord> Get(CancellationToken cancellationToken = default)
        {
            var request = new RestRequest("/cancellations", Method.GET);
            var response = await _restClient.ExecuteGetAsync<GenericRecord>(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GenericRecord>(response.Content);
            }

            return null;
        }

        public async Task<GenericRecord> GetWithoutRequest(CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();

                for (int i = 0; i < int.MaxValue; i++)
                {
                    //simulando uma operação que demore
                    await Task.CompletedTask;
                }

                break;
            }

            return new GenericRecord("Feito na munheca.");
        }
    }
}