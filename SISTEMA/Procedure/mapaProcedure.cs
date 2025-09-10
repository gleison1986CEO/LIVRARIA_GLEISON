using sistema.Models;
using sistema.Areas.Identity.Data;


namespace sistema.Procedure
{
    public interface mapaProcedure
    {
        public Task<IEnumerable<Mapa>> mapa(string? hashcode, string? foto, string? aeronave, string? latitude, string? longitude);
    }
}