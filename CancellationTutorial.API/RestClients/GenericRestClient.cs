using CancellationTutorial.API.Settings;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CancellationTutorial.API.RestClients
{
    public class GenericRestClient : RestClient
    {
        public GenericRestClient(IOptions<AppSettings> options) : base(options.Value.ExternalUrl) { }
    }
}