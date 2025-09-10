using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema.Models;
using sistema.Classes;
using sistema.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace sistema.Controllers
{

    [Authorize]
    public class LogsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        private readonly Uuid uid_ = new Uuid();
        private readonly Whatsapp whats_ = new Whatsapp();
        private readonly Error error_ = new Error();
        private readonly DataLogs datalogs_ = new DataLogs();
        readonly DATEGEN dateTime = new DATEGEN();
        private readonly IpConn IpURI = new IpConn();
        private readonly UPDATE update_ = new UPDATE();
        private readonly LOCATION location_ = new LOCATION();
        private readonly LoginAsync LoginAsync_ = new LoginAsync();
        private readonly MAPSGENERATE maps_ = new MAPSGENERATE();
        private readonly Strings Text = new Strings();


        public LogsController(AppIdentityDbContext context, UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
        {
            _context = context;
            userManager = userMgr;
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
           ViewBag.RETURN = "/Hostname/Index";     

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
        public async Task<IActionResult> Index(String Search)
        {
            ViewBag.HASHCODE = uid_.UID();
            ViewData["HOSTNAME"] = new SelectList(_context.Usuario.ToList().Where(g =>g.Ativo == true), "email", "email");
            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE LOGS DO SISTEMA";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador ver e revisar o que esta acontecendo no sistema e suas ações pelos usuários do aplicativo";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP  
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Logs orderby gq.codigo select gq.hostname;
            GradeList.AddRange(GradeQuery.Distinct());

            var log = from cr in _context.Logs select cr;

            if (!String.IsNullOrEmpty(Search))
            {
                log = log.Where(g => g.hostname == Search);
            }
            ViewBag.TOTAL = log.Count();
            return View(log);
        }

       

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Logs == null)
            {
                return RedirectToAction(nameof(Error));
            }

            var log = await _context.Logs
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (log == null)
            {
                return RedirectToAction(nameof(Error));
            }

            return View(log);
        }





        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Logs == null)
            {
                return Problem("Entity set 'Context.Logs'  is null.");
            }
            var log = await _context.Logs.FindAsync(id);
            if (log != null)
            {
                _context.Logs.Remove(log);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        [Authorize(Roles = "Administrador")]
        private bool LogsExists(int id)
        {
            return (_context.Logs?.Any(e => e.codigo == id)).GetValueOrDefault();
        }
        


        [Authorize(Roles = "Administrador, Revenda, Parceiro, Cliente")]
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