using Microsoft.AspNetCore.Mvc;
using sistema.Models;
using sistema.Classes;
using sistema.Areas.Identity.Data;
using sistema.Procedure;

namespace sistema.Controllers
{



    [Route("api")]
    [ApiController]
    public class apiController : ControllerBase
    {


        // PROCEDURE
        private readonly  dispositivoProcedure DispositivoProcedure;
        private readonly  logProcedure LogsProcedure;
        private readonly  mapaProcedure MapaProcedure;
        private readonly  descontoProcedure DescontoProcedure;
        // PROCEDURES


        private readonly                           AppIdentityDbContext _context;
        private readonly  Uuid uid_              = new Uuid();
        private readonly  Whatsapp whats_        = new Whatsapp();
        private readonly  Error error_           = new Error();
        readonly DATEGEN  dateTime                = new DATEGEN();

        public apiController(AppIdentityDbContext context, dispositivoProcedure DispositivoProcedure,  logProcedure LogsProcedure, mapaProcedure MapaProcedure,descontoProcedure DescontoProcedure)
        {
            _context = context;
            this.DispositivoProcedure = DispositivoProcedure;
            this.LogsProcedure        = LogsProcedure;
            this.MapaProcedure        = MapaProcedure;
            this.DescontoProcedure    = DescontoProcedure;
        }
    
        
         //DISPOSITIVOS
        [HttpGet("6d427aa66067b9111f8d5028fa1cf3058addd4cda072793e7f0458e7d023ae8a")] /////// SHA256
        public async Task<IEnumerable<Dispositivo>> dispositivo(string? nome, string? chave, string? id, string? url, string? description)
        {
            try
            {
                string? hashcode = uid_.UID();
                string? date     = dateTime.DATE();
                
                //// IGUAL A HOSTNAME OU PROVIDER
                
                var response = await DispositivoProcedure.dispositivo(hashcode, nome, chave, id, url, description, date);

                if (response == null)
                {
                    return null;
                }
                
                return response;
            }
            catch
            {
                throw;
            }
        }
           


        
         //MAPA
        [HttpGet("d2f671e0a6015903bd0f68e2e2092cc901cf89e0ebf2966e9febe73e1faae135")] /////// SHA256
        public async Task<IEnumerable<Mapa>> mapa(string? foto, string? aeronave, string? latitude, string? longitude)
        {
            try
            {
                string? hashcode = uid_.UID();
                var response = await MapaProcedure.mapa(hashcode,foto, aeronave, latitude, longitude);

                if (response == null)
                {
                    return null;
                }
                
                return response;
            }
            catch
            {
                throw;
            }
        }           
         //LOGS
        [HttpGet("98f38f12db221a8cf8ca7aadfdcd759b01d52eb4ebb3eedbb2d97e92805c6960")] /////// SHA256
        public async Task<IEnumerable<LogsInsert>> logs(string?  hostname)
        {
            try
            {
               
                DateTime data     = DateTime.Today; 
                var date_         = data.ToString();
                int ativo_        = 1;
                var response = await LogsProcedure.logs(hostname, date_ , ativo_);
                if (response == null)
                {
                    return null;
                }
                
                return response;
            }
            catch
            {
                throw;
            }
        }

     


  
        //DESCONTO
        [HttpGet("5ff14344f9b101348baf592e1ffd4b76d41b0a80d950fa6476d2758e6d36787d")] /////// SHA256
        public async Task<IEnumerable<Desconto>> desconto_()
        {
            try
            {
                var response = await DescontoProcedure.desconto_();

                if (response == null)
                {
                    return null;
                }

                return response;
            }
            catch
            {
                throw;
            }
        }

                 


    }

}