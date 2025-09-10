using sistema.Models;
using sistema.Areas.Identity.Data;


namespace sistema.Procedure
{
    public interface dispositivoProcedure
    {
        public Task<IEnumerable<Dispositivo>> dispositivo(string? hashcode, string? nome, string? chave, string? id, string? url, string? description, string? date);
    }
}