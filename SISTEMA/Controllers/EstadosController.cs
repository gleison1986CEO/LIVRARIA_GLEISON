using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema.Models;
using sistema.Classes;
using sistema.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace sistema.Controllers
{

    [Authorize]
    public class EstadosController : Controller
    {
        private readonly AppIdentityDbContext _context;

        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
                
        private readonly  Uuid uid_              = new Uuid();
        private readonly  Whatsapp whats_        = new Whatsapp();
        private readonly  Error error_           = new Error();
        private readonly  DataLogs datalogs_     = new DataLogs();
        readonly DATEGEN dateTime                = new DATEGEN();
        private readonly IpConn IpURI            = new IpConn();
        private readonly UPDATE update_          = new UPDATE();
        private readonly LOCATION location_      = new LOCATION();
        private readonly LoginAsync LoginAsync_  = new LoginAsync();
        private readonly  MAPSGENERATE maps_     = new MAPSGENERATE();
        private readonly  Strings Text           = new Strings();


        public EstadosController(AppIdentityDbContext context,UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
        {
            _context = context;
            userManager   = userMgr;
            signInManager = signinMgr;            
        }
        
  
   [Authorize(Roles = "Administrador, Revenda, Parceiro, Cliente")]
        public IActionResult SearchVerification()
        {
            // VERIFY TIMER
                        // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER    


           ViewBag.ORIGEM = "Dados inconsistentes.";      
           ViewBag.RETURN = "/Estados/Index";     

           return View();
        }        

         [Authorize(Roles = "Administrador, Revenda, Parceiro, Cliente")]
        public IActionResult SessionrTimer()
        {
            
           return View();
        }
               
         [Authorize(Roles = "Administrador, Revenda, Parceiro, Cliente")]
        public IActionResult Verificacao()
        {
            // VERIFY TIMER
                        // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER              
           return View();
        }

         [Authorize(Roles = "Administrador, Revenda, Parceiro, Cliente")]
        public IActionResult Error()
        {
            // VERIFY TIMER
                        // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER  
            
           return View();
        }
        
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index(string? Nome)
        {

            // VERIFY TIMER
            // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 

            if (ViewBag.TIMER == "" || ViewBag.TIMER == null)
            {
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER  
            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE ESTADO/REGIÃO";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador cadastrar novas regiões ao sistema, para serem utilizadas em pagamentos e localização de aparelhos.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 
            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();

            ViewBag.HASHCODE = uid_.UID();
            ///////////CONTAGEM DE ESTADOS
            ViewBag.graf1 = _context.Estado.Count();
            //////////CONTAGEM DE ESTADOS ATIVOS
            ViewBag.graf2 = _context.Estado.Count(C => C.Ativo);


            // SEARCH
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Estado orderby gq.codigo select gq.nome;
            GradeList.AddRange(GradeQuery.Distinct());
            ViewBag.Clientes__Grade = new SelectList(GradeList);

            var cliente = from cr in _context.Estado select cr;

            if (!String.IsNullOrEmpty(Nome))
            {
                cliente = cliente.Where(g => g.nome == Nome);
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " ESTADO", location_.GetLocalIPAddress());
            }

            // RESULT
            return View(cliente);
        }  
        
        [Authorize(Roles = "Administrador")]
        public IActionResult NovoEstado()
        {

            // VERIFY TIMER
            // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 

            if (ViewBag.TIMER == "" || ViewBag.TIMER == null)
            {
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER  
            
            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE ESTADO/REGIÃO";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador cadastrar novas regiões ao sistema, para serem utilizadas em pagamentos e localização de aparelhos.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> NovoEstado(Estado estado)
        {
            List<Maps> items = maps_.MAPS(estado.nome);

            string HASHCODE = uid_.UID();

            string DATE = dateTime.DATE();
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var item in items)
                    {
                        int ID = item.place_id;
                        var lats = item.lat;
                        var longs = item.lon;
                        var name = item.display_name;
                        estado.latitude = lats;
                        estado.longitude = longs;
                        _context.Add(estado);
                        await _context.SaveChangesAsync();
                        datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.SAVE() + " ESTADO: " + estado.nome + "" + estado.latitude + estado.longitude, location_.GetLocalIPAddress());
                        break;
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(estado);

            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error));
            }
        }

        [Authorize(Roles = "Administrador")]
        
        public async Task<IActionResult> DadosEstado(int? id, string? hash)
        {

            // VERIFY TIMER
            // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 

            if (ViewBag.TIMER == "" || ViewBag.TIMER == null)
            {
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER  
            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE ESTADO/REGIÃO";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador atualizar regiões ao sistema, para serem utilizadas em pagamentos e localização de aparelhos.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 
            if (id == null || _context.Estado == null)
            {
                return NotFound();
            }

            var estado = await _context.Estado.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }
            return View(estado);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DadosEstado(int id, Estado estado)
        {

            List<Maps> items = maps_.MAPS(estado.nome);

            string HASHCODE = uid_.UID();

            string DATE = dateTime.DATE();
            if (id != estado.codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var item in items)
                    {
                        int ID = item.place_id;
                        var lats = item.lat;
                        var longs = item.lon;
                        var name = item.display_name;
                        estado.latitude = lats;
                        estado.longitude = longs;
                        _context.Update(estado);
                        await _context.SaveChangesAsync();
                        datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.UPDATE() + " ESTADO: " + estado.nome + "" + estado.latitude + estado.longitude, location_.GetLocalIPAddress());
                        break;
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoExists(estado.codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estado);
        }
        
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {

            // VERIFY TIMER
                        // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER 

            // INFORMAÇÕES POPUP
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE ESTADOS";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao usuário deletar dados presentes neste ID:" + id;
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP


            if (id == null || _context.Estado == null)
            {
                return NotFound();
            }

            var estado = await _context.Estado
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            if (_context.Estado == null)
            {
                return Problem("Entity set 'Context.Estado'  is null.");
            }
            var estado = await _context.Estado.FindAsync(id);
            if (estado != null)
            {
                _context.Estado.Remove(estado);
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.DELETE() + " ESTADO", location_.GetLocalIPAddress());    
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoExists(int id)
        {
          return (_context.Estado?.Any(e => e.codigo == id)).GetValueOrDefault();
        }




        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> TIMER() {
        
        
            // ´PASSAR ESSE METODO APRA TODOS
            // ´PASSAR ESSE METODO APRA TODOS
            // ´PASSAR ESSE METODO APRA TODOS

            String STATELOGIN = LoginAsync_.Logout(@User.Identity.Name);
            
            if(STATELOGIN != "LOGOUT"){
                
                ViewBag.TIMER = STATELOGIN;

            }
            return NotFound();
            // ´PASSAR ESSE METODO APRA TODOS
            // ´PASSAR ESSE METODO APRA TODOS
            // ´PASSAR ESSE METODO APRA TODOS
        }



    }
}
