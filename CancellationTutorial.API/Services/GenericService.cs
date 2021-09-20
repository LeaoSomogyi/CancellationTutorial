using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CancellationTutorial.API.Interfaces;
using CancellationTutorial.API.Records;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;

namespace CancellationTutorial.API.Services
{
    public class GenericService : IGenericService
    {
        private readonly IRestClient _restClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericService(IRestClient restClient, IHttpContextAccessor httpContextAccessor)
        {
            _restClient = restClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GenericRecord> Get(CancellationToken cancellationToken = default)
        {
            var request = new RestRequest("/cancellations", Method.GET);
            var response = await _restClient.ExecuteGetAsync<GenericRecord>(request, _httpContextAccessor.HttpContext.RequestAborted);
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