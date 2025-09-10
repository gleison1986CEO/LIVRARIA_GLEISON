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
    public class MapaController : Controller
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
        private readonly IpConn IP               = new IpConn();
        private readonly UPDATE QUERY            = new UPDATE(); 
        private readonly IpConn IPs              = new IpConn();
        private readonly  Strings Text           = new Strings();
        

        public MapaController(AppIdentityDbContext context,UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
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
           ViewBag.RETURN = "/Mapa/Index";     

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

            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE LOCAIS/REGIÕES E MAPA";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar locais no mapa cadastrados pelo dispositivo";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP   


            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();
            
            ViewBag.FOTO           = IP.AERONAVE(); 
            ViewBag.EXT            = ".png"; 
            ViewBag.USERNAME       = @User.Identity.Name;

            // SEARCH
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Mapa orderby gq.codigo select gq.aeronave;
            GradeList.AddRange(GradeQuery.Distinct());
            ViewBag.Clientes__Grade = new SelectList(GradeList);

            var cliente = from cr in _context.Mapa select cr;

            if (!String.IsNullOrEmpty(Nome))
            {
                cliente = cliente.Where(g => g.aeronave.Contains(Nome));
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.FILTRO() + " MAPA", location_.GetLocalIPAddress());    
            }
            
            
            
            // RESULT
            return View(cliente);
        }




        
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> Mapa(string? Nome)
        {

            // VERIFY TIMER
                        // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER              
            string HASHCODE        = uid_.UID();
            string DATE            = dateTime.DATE();
            
            ViewBag.FOTO           = IP.AERONAVE(); 
            ViewBag.EXT            = ".png"; 
            ViewBag.USERNAME       = @User.Identity.Name;
            // RESULT

            // SEARCH
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Mapa orderby gq.codigo select gq.aeronave;
            GradeList.AddRange(GradeQuery.Distinct());
            ViewBag.Clientes__Grade = new SelectList(GradeList);

            var cliente = from cr in _context.Mapa select cr;

            if (!String.IsNullOrEmpty(Nome))
            {
                cliente = cliente.Where(g => g.aeronave.Contains(Nome));
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.FILTRO() +" MAPA", location_.GetLocalIPAddress());    
            }
            


            return View(cliente);
            
         
            
        }



        [Authorize(Roles = "Administrador")]
        public IActionResult NovoMapa()
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE LOCAIS/REGIÕES E MAPA";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar e cadastrar locais no mapa cadastrados pelo dispositivo";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP               
            ViewData["DISPOSITIVO"]   = new SelectList(_context.Dispositivo.ToList().Where(c => c.Ativo), "chave", "chave");
            return View();
        }
        
        
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovoMapa(Mapa data)
        {

            
            string HASHCODE       = uid_.UID();
            string DATE           = dateTime.DATE();
            
            try{
                    data.foto     = data.foto;
                    data.hashcode = HASHCODE;
                    _context.Add(data);
                    await _context.SaveChangesAsync();
                    datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() +" MAPA", location_.GetLocalIPAddress());
                    return RedirectToAction(nameof(Index));
                
            }catch (Exception e)
                {
                    return RedirectToAction(nameof(Error));
                }       
            
        }

        [Authorize(Roles = "Administrador")]
        
        public async Task<IActionResult> DadosMapa(int? id, string? hash)
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
            if (id == null || _context.Mapa == null)
            {
                return NotFound();
            }
            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE LOCAIS/REGIÕES E MAPA";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar e atualizar locais no mapa cadastrados pelo dispositivo";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP   
            var VERIFY = await _context.Mapa.FindAsync(id);
            if (VERIFY == null)
            {
                return NotFound();
            }
            ViewData["DISPOSITIVO"] = new SelectList(_context.Dispositivo.ToList().Where(c => c.Ativo), "chave", "chave");
            return View(VERIFY);
        }

        
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DadosMapa(int id, Mapa data)
        {

            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();
            
            if (id != data.codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    data.foto = data.foto;
                    _context.Update(data);                
                    await _context.SaveChangesAsync();
                    datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.UPDATE() +" MAPA", location_.GetLocalIPAddress());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MapaExists(data.codigo))
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
           

            return View(data);
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
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE LOCALIZAÇÕES";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao usuário deletar dados presentes neste ID:" + id;
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP


            if (id == null || _context.Mapa == null)
            {
                return NotFound();
            }

            var VERIFY = await _context.Mapa
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (VERIFY == null)
            {
                return NotFound();
            }

            return View(VERIFY);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            if (_context.Mapa == null)
            {
                return Problem("Entity set 'Context.Mapa'  is null.");
            }
            var VERIFY = await _context.Mapa.FindAsync(id);
            if (VERIFY != null)
            {
                _context.Mapa.Remove(VERIFY);
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.DELETE() +" MAPA", location_.GetLocalIPAddress());    
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MapaExists(int id)
        {
          return (_context.Mapa?.Any(e => e.codigo == id)).GetValueOrDefault();
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
