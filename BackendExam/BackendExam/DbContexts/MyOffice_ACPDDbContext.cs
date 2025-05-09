using BackendExam.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BackendExam.DbContexts
{
    public class MyOffice_ACPDDbContext(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public async Task<List<MyOffice_ACPDModel>> GetAsync(string json)
        {
            var result = new List<MyOffice_ACPDModel>();
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_MyOffice_ACPD_Select", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@json", json);

                await con.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new MyOffice_ACPDModel
                        {
                            ACPD_SID = reader["ACPD_SID"].ToString(),
                            ACPD_Cname = reader["ACPD_Cname"].ToString(),
                            ACPD_Ename = reader["ACPD_Ename"].ToString(),
                            ACPD_Sname = reader["ACPD_Sname"].ToString(),
                            ACPD_Email = reader["ACPD_Email"].ToString(),
                            ACPD_Status = reader.GetByte(reader.GetOrdinal("ACPD_Status")),
                            ACPD_Stop = reader.GetBoolean(reader.GetOrdinal("ACPD_Stop")),
                            ACPD_StopMemo = reader["ACPD_StopMemo"].ToString(),
                            ACPD_LoginID = reader["ACPD_LoginID"].ToString(),
                            ACPD_LoginPWD = reader["ACPD_LoginPWD"].ToString(),
                            ACPD_Memo = reader["ACPD_Memo"].ToString(),
                            ACPD_NowDateTime = reader.IsDBNull(reader.GetOrdinal("ACPD_NowDateTime"))?(DateTime?)null:reader.GetDateTime(reader.GetOrdinal("ACPD_NowDateTime")),
                            ACPD_NowID = reader["ACPD_NowID"].ToString(),
                            ACPD_UPDDateTime = reader.IsDBNull(reader.GetOrdinal("ACPD_UPDDateTime"))?(DateTime?)null:reader.GetDateTime(reader.GetOrdinal("ACPD_UPDDateTime")),
                            ACPD_UPDID = reader["ACPD_UPDID"].ToString()
                        });
                    }
                }
            }
            return result;
        }


        public async Task<bool> InsertAsync(string json)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_MyOffice_ACPD_Update", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@json", json);

                await conn.OpenAsync();
                using (var transaction = conn.BeginTransaction())
                {
                    cmd.Transaction = transaction;
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<bool> UpdateAsync(string json)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_MyOffice_ACPD_Insert", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@json", json);

                await conn.OpenAsync();
                using (var transaction = conn.BeginTransaction())
                {
                    cmd.Transaction = transaction;
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<bool> DeleteAsync(string json)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_MyOffice_ACPD_Delete", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@json", json);

                await conn.OpenAsync();
                using (var transaction = conn.BeginTransaction())
                {
                    cmd.Transaction = transaction;
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

    }
    /*
    public class MyOffice_ACPDDbContext_EF : DbContext
    {
        public MyOffice_ACPDDbContext_EF(DbContextOptions<MyOffice_ACPDDbContext> options) : base(options) { }

        public DbSet<MyOffice_ACPDModel> MyOffice_ACPD_DbSet { get; set; }

        public async Task<List<MyOffice_ACPDModel>> GetAsync(string json)
        {
            SqlParameter param = new SqlParameter(@"JsonString", json);
            var result = await MyOffice_ACPD_DbSet.FromSqlRaw("EXEC sp_MyOffice_ACPD_Select @json = @JsonString", param).ToListAsync();
            return result;
        }


        public async Task<bool> InsertAsync(string json)
        {
            using var transaction = await Database.BeginTransactionAsync();
            try
            {
                SqlParameter param = new SqlParameter(@"JsonString", json);

                await Database.ExecuteSqlRawAsync("EXEC sp_MyOffice_ACPD_Insert @json = @JsonString", param);

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(string json)
        {
            using var transaction = await Database.BeginTransactionAsync();
            try
            {
                SqlParameter param = new SqlParameter(@"JsonString", json);

                await Database.ExecuteSqlRawAsync("EXEC sp_MyOffice_ACPD_Update @json = @JsonString", param);

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(string json)
        {
            using var transaction = await Database.BeginTransactionAsync();
            try
            {
                SqlParameter param = new SqlParameter(@"JsonString", json);

                await Database.ExecuteSqlRawAsync("EXEC sp_MyOffice_ACPD_Delete @json = @JsonString", param);

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }

        }
    
    }
    */
}
