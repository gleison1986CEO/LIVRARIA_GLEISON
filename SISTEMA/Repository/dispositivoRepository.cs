using sistema.Areas.Identity.Data;
using sistema.Models;
using sistema.Procedure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace sistema.Repository
{  
    public class dispositivoRepository : dispositivoProcedure
    {

        private readonly AppIdentityDbContext _dbContext;

        public dispositivoRepository(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// DISPOSITIVO  CADASTRO
        public async Task<IEnumerable<Dispositivo>> dispositivo(string? hashcode, string? nome, string? chave, string? id, string? url, string? description, string? date)
        {
            var hashcode_    = new SqlParameter("@hashcode", hashcode);
            var nome_        = new SqlParameter("@nome", nome); // IGUAL A HOSTNAME OU PROVIDER
            var chave_       = new SqlParameter("@chave", chave);
            var id_          = new SqlParameter("@id", id);
            var url_         = new SqlParameter("@url", url);
            var description_ = new SqlParameter("@description", description);
            var date_        = new SqlParameter("@date", date);
            var data_        = new SqlParameter("@data", Convert.ToDateTime(date));
            var Ativo_       = new SqlParameter("@Ativo", 1);

            var Resultado_ = await Task.Run(() => _dbContext.Dispositivo
                              .FromSqlRaw(@"exec sp_dispositivo @hashcode, @nome, @chave, @id, @url, @description, @date, @data, @Ativo", hashcode_, nome_, chave_, id_, url_, description_, date_, data_, Ativo_).ToListAsync());

            return Resultado_;
        }
        /// DISPOSITIVO  CADASTRO
    }
}