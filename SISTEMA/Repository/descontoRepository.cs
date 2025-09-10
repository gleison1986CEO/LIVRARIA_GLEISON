using sistema.Areas.Identity.Data;
using sistema.Models;
using sistema.Procedure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace sistema.Repository
{  
    public class descontoRepository : descontoProcedure
    {

        private readonly AppIdentityDbContext _dbContext;

        public descontoRepository(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// DESCONTO LIST
        public async Task<IEnumerable<Desconto>> desconto_()
        {

            var Resultado_ = await Task.Run(() => _dbContext.Desconto
                              .FromSqlRaw(@"exec sp_desconto").ToListAsync());

            return Resultado_;
        }


    }
}