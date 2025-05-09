using BackendExam.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BackendExam.DbContexts
{
    public class MyOffice_ACPDDbContext : DbContext
    {
        public MyOffice_ACPDDbContext(DbContextOptions<MyOffice_ACPDDbContext> options) : base(options) { }

        public DbSet<MyOffice_ACPDModel> MyOffice_ACPD_DbSet { get; set; }

        public async Task<List<MyOffice_ACPDModel>> GetAsync(string json)
        {
            SqlParameter param = new SqlParameter(@"JsonString", json);
            var result = await MyOffice_ACPD_DbSet.FromSqlRaw("EXEC sp_MyOffice_ACPD_Select @json = @JsonString", param).ToListAsync();
            return result;
        }
    }
}
