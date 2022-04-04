using System.Collections.Generic;
using System.Threading.Tasks;

namespace LionTrust.Feature.EXM.Services.Interfaces
{
    public interface IEmailService
    {
        Task<List<Models.Bounce>> GetHardBounces();

        Task<bool> DeleteHardBounce(string email);

        Task<List<Models.Bounce>> GetSoftBounces();

        Task<bool> DeleteSoftBounce(string email);
    }
}