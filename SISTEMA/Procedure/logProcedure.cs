using sistema.Models;
using sistema.Areas.Identity.Data;


namespace sistema.Procedure
{
    public interface logProcedure
    {
        public Task<IEnumerable<LogsInsert>> logs(string? cpf, string? data, int? ativo);
    }
}