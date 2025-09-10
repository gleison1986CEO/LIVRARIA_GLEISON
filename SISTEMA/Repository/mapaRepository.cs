using sistema.Areas.Identity.Data;
using sistema.Models;
using sistema.Procedure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace sistema.Repository
{  
    public class mapaRepository : mapaProcedure
    {

        private readonly AppIdentityDbContext _dbContext;

        public mapaRepository(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// MAPA CADASTRO
        public async Task<IEnumerable<Mapa>> mapa(string? hashcode, string? foto, string? aeronave, string? latitude, string? longitude)
        {
            var hashcode_          = new SqlParameter("@hashcode", hashcode);
            var foto_              = new SqlParameter("@foto", foto);
            var aeronave_          = new SqlParameter("@aeronave", aeronave);
            var latitude_          = new SqlParameter("@latitude", latitude);
            var longitude_         = new SqlParameter("@longitude", longitude);
            var Ativo_             = new SqlParameter("@Ativo", 1);

            var Resultado_ = await Task.Run(() => _dbContext.Mapa
                              .FromSqlRaw(@"exec sp_mapa @hashcode, @foto, @aeronave, @latitude, @longitude,  @Ativo", hashcode_, foto_, aeronave_, latitude_, longitude_, Ativo_).ToListAsync());

            return Resultado_;
        }
        /// MAPA CADASTRO
    }
}