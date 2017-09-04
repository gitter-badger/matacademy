using System.Threading.Tasks;

namespace MatOrderingService2.Services.Storage
{
    public interface ICodeGenerationService
    {
        Task<string> GetCodeForOrder(int id);
    }
}
