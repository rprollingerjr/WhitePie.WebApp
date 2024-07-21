using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Buffers.Text;
using WhitePie.Models;
using WhitePie.Models.Settings;
using WhitePie.WebApp.Data.Interfaces;
using WhitePie.WebApp.Services.Interfaces;

namespace WhitePie.Services
{
    public class MomentService : IMomentService
    {
        private readonly IMomentRepository _momentRepository;

        public MomentService(IMomentRepository momentRepository)
        {
            _momentRepository = momentRepository;
        }

        public async Task<IEnumerable<Moment>> GetAllMomentsAsync()
        {
            return await _momentRepository.GetAllMomentsAsync();
        }

        public async Task<byte[]> GetFileBytesAsync(string id)
        {
            return await _momentRepository.GetFileBytesAsync(id);
        }
    }
}
