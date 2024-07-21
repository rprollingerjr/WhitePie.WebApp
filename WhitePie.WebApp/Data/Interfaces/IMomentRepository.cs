using WhitePie.Models;

namespace WhitePie.WebApp.Data.Interfaces
{
    public interface IMomentRepository
    {
        Task<IEnumerable<Moment>> GetAllMomentsAsync();
        Task<byte[]> GetFileBytesAsync(string fileId);
    }
}
