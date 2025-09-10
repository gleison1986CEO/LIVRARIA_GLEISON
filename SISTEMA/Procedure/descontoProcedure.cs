using sistema.Models;
using sistema.Areas.Identity.Data;


namespace sistema.Procedure
{
    public interface descontoProcedure
    {
        public Task<IEnumerable<Desconto>> desconto_();
    }
}