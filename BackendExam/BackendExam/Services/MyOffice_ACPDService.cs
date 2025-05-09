using BackendExam.DbContexts;
using BackendExam.Models;

namespace BackendExam.Services
{
    public class MyOffice_ACPDService
    {
        private readonly MyOffice_ACPDDbContext _dbContext;
        public MyOffice_ACPDService(MyOffice_ACPDDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MyOffice_ACPDModel>> GetAllData(string json)
        {
            var data = await _dbContext.GetAsync(json);
            return data;
        }

        public async Task InsertData(string json)
        {
            await _dbContext.InsertAsync(json);
        }
        public async Task UpdateData(string json)
        {
            await _dbContext.UpdateAsync(json);
        }
        public async Task DeleteData(string json)
        {
            await _dbContext.DeleteAsync(json);
        }
    }
}
