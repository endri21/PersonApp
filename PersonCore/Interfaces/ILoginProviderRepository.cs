using PersonCore.Models;
using System.Threading.Tasks;

namespace PersonCore.Interfaces
{
    public interface ILoginProviderRepository
    {
        Task<LoginResponseModel> LogInUserAsync(LogInRequestModel logIn);
    }
}
