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
    public class IpsController : Controller
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

        public IpsController(AppIdentityDbContext context,UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
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
           ViewBag.RETURN = "/Ips/Index";     

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
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER              
            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();
            

            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE INDETIFICAÇÃO DE MAQUINAS/IPS";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar maquinas que serão permitidas logar no sistema";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP  

            // SEARCH
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Ips orderby gq.codigo select gq.nome;
            GradeList.AddRange(GradeQuery.Distinct());
            ViewBag.Clientes__Grade = new SelectList(GradeList);

            var cliente = from cr in _context.Ips select cr;

            if (!String.IsNullOrEmpty(Nome))
            {
                cliente = cliente.Where(g => g.nome == Nome);
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.FILTRO() +" IPS", location_.GetLocalIPAddress());    
            }
            
            
            
            // RESULT
            return View(cliente);
        }



        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {

            
            // VERIFY TIMER
                        // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER              
            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            if (id == null || _context.Ips == null)
            {
                return NotFound();
            }

            var ips = await _context.Ips.FirstOrDefaultAsync(m => m.codigo == id);
            if (ips == null)
            {
                return NotFound();
            }
            string IP   = IpURI.GetCountryByIP(ips.ip);
            string MAPS = IpURI.GetMapsByIP(ips.ip);
            ViewBag.IP   = IP;
            ViewBag.MAPS = MAPS;

            return View(ips);
        }


        [Authorize(Roles = "Administrador")]
        public IActionResult NovoIps()
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE INDETIFICAÇÃO DE MAQUINAS/IPS";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar e cadastrar novas maquinas que serão permitidas logar no sistema";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP      


            return View();
        }
        


        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovoIps(Ips ips)
        {

            
            string HASHCODE     = uid_.UID();
            string DATE         = dateTime.DATE();
            
            try{
                if (ModelState.IsValid)
                {
                    _context.Add(ips);
                    await _context.SaveChangesAsync();
                    datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() +" IP", location_.GetLocalIPAddress());
                    return RedirectToAction(nameof(Index));
                }
                return View(ips);
            }catch (Exception e)
                {
                    return RedirectToAction(nameof(Error));
                }       
            
        }


        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DadosIps(int? id, string? hash)
        {

            // VERIFY TIMER
                        // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER              
            if (id == null || _context.Ips == null)
            {
                return NotFound();
            }
            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE INDETIFICAÇÃO DE MAQUINAS/IPS";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar e atualizar maquinas que serão permitidas logar no sistema";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP  
            var ips = await _context.Ips.FindAsync(id);
            if (ips == null)
            {
                return NotFound();
            }
            return View(ips);
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DadosIps(int id, Ips ips)
        {

            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();
            
            if (id != ips.codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ips);
                    await _context.SaveChangesAsync();
                    datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.UPDATE() +" IP", location_.GetLocalIPAddress());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IpsExists(ips.codigo))
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
           

            return View(ips);
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
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE IPS DE ACESSO";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao usuário deletar dados presentes neste ID:" + id;
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP


            if (id == null || _context.Ips == null)
            {
                return NotFound();
            }

            var ips = await _context.Ips
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (ips == null)
            {
                return NotFound();
            }

            return View(ips);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            if (_context.Ips == null)
            {
                return Problem("Entity set 'Context.Ips'  is null.");
            }
            var ips = await _context.Ips.FindAsync(id);
            if (ips != null)
            {
                _context.Ips.Remove(ips);
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.DELETE() +" IP", location_.GetLocalIPAddress());    
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IpsExists(int id)
        {
          return (_context.Ips?.Any(e => e.codigo == id)).GetValueOrDefault();
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
