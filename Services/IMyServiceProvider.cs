using hw5.Model;
using Microsoft.EntityFrameworkCore;

namespace hw5.Services
{
    public interface IMyServiceProvider
    {
        public Task<bool> Edit(User user, Service service);
        public Task<string> GetServiceInfo(int userId);
        public Task<bool> Register(User user, Service service);
    }
}
