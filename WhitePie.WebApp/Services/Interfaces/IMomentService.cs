using WhitePie.Models;

namespace WhitePie.WebApp.Services.Interfaces
{
    public interface IMomentService
    {
       Task<IEnumerable<Moment>> GetAllMomentsAsync();
        Task<byte[]> GetFileBytesAsync(string id);
    }
}
