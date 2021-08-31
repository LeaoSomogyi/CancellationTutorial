using System.Threading;
using System.Threading.Tasks;
using CancellationTutorial.API.Records;

namespace CancellationTutorial.API.Interfaces
{
    public interface IGenericService
    {
        Task<GenericRecord> Get(CancellationToken cancellationToken = default);
        Task<GenericRecord> GetWithoutRequest(CancellationToken cancellationToken = default);
    }
}