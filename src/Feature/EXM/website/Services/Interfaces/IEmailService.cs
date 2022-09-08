using System.Collections.Generic;
using System.Threading.Tasks;

namespace LionTrust.Feature.EXM.Services.Interfaces
{
    public interface IEmailService
    {
        Task<List<Models.Bounce>> GetBounces();

        Task<bool> DeleteBounce(string email);

        Task<List<Models.Bounce>> GetBlocks();

        Task<bool> DeleteBlock(string email);
    }
}