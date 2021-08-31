using System;
using System.Threading;
using System.Threading.Tasks;
using CancellationTutorial.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CancellationTutorial.API.Controllers
{
    [ApiController]
    [Route("cancellations")]
    public class CancellationController : ControllerBase
    {
        private readonly IGenericService _service;

        public CancellationController(IGenericService service) : base()
        {
            _service = service;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            var result = await _service.Get();
            Console.WriteLine(result);
            return Ok(result);
        }

        /// <summary>
        /// O próprio dotnet injeta um CancellationToken (tanto na API como no BackgroundService)
        /// </summary>
        /// <param name="cancellationToken"></param>
        [HttpGet]
        [Route("with-token")]
        public async Task<object> GetWithToken(CancellationToken cancellationToken)
        {
            var result = await _service.Get(cancellationToken);
            Console.WriteLine(result);
            return Ok(result);
        }

        /// <summary>
        /// O próprio dotnet injeta um CancellationToken (tanto na API como no BackgroundService)
        /// </summary>
        /// <param name="cancellationToken"></param>
        [HttpGet]
        [Route("without-request")]
        public async Task<object> GetWithoutRequest(CancellationToken cancellationToken)
        {
            var result = await _service.GetWithoutRequest(cancellationToken);
            Console.WriteLine(result);
            return Ok(result);
        }
    }
}