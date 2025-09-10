using sistema.Areas.Identity.Data;
using sistema.Models;
using sistema.Procedure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace sistema.Repository
{  
    public class logsRepository : logProcedure
    {

        private readonly AppIdentityDbContext _dbContext;

        public logsRepository(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// LOGS  CADASTRO
        public async Task<IEnumerable<LogsInsert>> logs(string? cpf, string? data, int? ativo)
        {

            var cpf_          = new SqlParameter("@cpf", cpf);
            var data_         = new SqlParameter("@data", data);
            var ativo_        = new SqlParameter("@Ativo", ativo);

            var Resultado_ = await Task.Run(() => _dbContext.LogsInsert .FromSqlRaw(@"exec sp_logs @cpf, @data, @Ativo", cpf_, data_, ativo_).ToListAsync());
            
            return Resultado_;
            
        }
        /// LOGS  CADASTRO

     
    }
}