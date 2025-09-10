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
    public class IdentificacaoController : Controller
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
  

        public IdentificacaoController(AppIdentityDbContext context,UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
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
           ViewBag.RETURN = "/Identificacao/Index";     

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
            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();


            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE INDETIFICAÇÃO DO USUARIO";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar usuários logados ao sistema, gerar novos tokens de acesso ou resetar usuario do sistema.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP   
            // SEARCH
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Identificacao orderby gq.codigo select gq.email;
            GradeList.AddRange(GradeQuery.Distinct());
            ViewBag.Clientes__Grade = new SelectList(GradeList);

            var cliente = from cr in _context.Identificacao select cr;

            if (!String.IsNullOrEmpty(Nome))
            {
                cliente = cliente.Where(g => g.email == Nome);
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " IDENTIFICACAO", location_.GetLocalIPAddress());
            }

            // RESULT
            return View(cliente);
        }


        [Authorize(Roles = "Administrador")]
        public IActionResult NovaIdentificacao()
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE INDETIFICAÇÃO DO USUARIO";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar  e cadastrar novas identificações de usuários logados ao sistema, gerar novos tokens de acesso ou resetar usuario do sistema.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP                          
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> NovaIdentificacao(Identificacao identificacao)
        {
            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(identificacao);
                    await _context.SaveChangesAsync();
                    datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.SAVE() + " IDENTIFICACAO", location_.GetLocalIPAddress());
                    return RedirectToAction(nameof(Index));
                }
                return View(identificacao);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error));
            }

        }
        

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DadosIdentificacao(int? id, string? hash)
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
            if (id == null || _context.Identificacao == null)
            {
                return NotFound();
            }

            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE INDETIFICAÇÃO DO USUARIO";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar e atualizar identificações de usuários logados ao sistema, gerar novos tokens de acesso ou resetar usuario do sistema.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP  
            var identificacao = await _context.Identificacao.FindAsync(id);

            string IP = IpURI.GetCountryByIP(identificacao!.ip);
            string MAPS = IpURI.GetMapsByIP(identificacao.ip);

            ViewBag.IP = IP;
            ViewBag.MAPS = MAPS;

            if (identificacao == null)
            {
                return NotFound();
            }
            return View(identificacao);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DadosIdentificacao(int id, Identificacao identificacao)
        {

            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();

            if (id != identificacao.codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(identificacao);
                    await _context.SaveChangesAsync();
                    datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.UPDATE() + " IDENTIFICACAO", location_.GetLocalIPAddress());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdentificacaoExists(identificacao.codigo))
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


            return View(identificacao);
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
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE IDENTIFICAÇÃO DE LOGIN";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao usuário deletar dados presentes neste ID:" + id;
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP


            if (id == null || _context.Identificacao == null)
            {
                return NotFound();
            }

            var dentificacao = await _context.Identificacao
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (dentificacao == null)
            {
                return NotFound();
            }

            return View(dentificacao);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();

            if (_context.Identificacao == null)
            {
                return Problem("Entity set 'Context.Identificacao'  is null.");
            }
            var identificacao = await _context.Identificacao.FindAsync(id);
            if (identificacao != null)
            {
                _context.Identificacao.Remove(identificacao);
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.DELETE() + " IDENTIFICACAO", location_.GetLocalIPAddress());
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador")]
        private bool IdentificacaoExists(int id)
        {
            return (_context.Identificacao?.Any(e => e.codigo == id)).GetValueOrDefault();
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
