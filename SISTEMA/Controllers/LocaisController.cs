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
    public class LocaisController : Controller
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
        private readonly Strings Text            = new Strings();
        
        

        public LocaisController(AppIdentityDbContext context,UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
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
           ViewBag.RETURN = "/Locais/Index";     

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
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar locais permitidos pelo usuario";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP  
            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            
            ///////////CONTAGEM DE CIDADES
            ViewBag.graf1 = _context.Local.Count();
            //////////CONTAGEM DE CIDADES ATIVOS
            ViewBag.graf2 = _context.Local.Count(C => C.Ativo);


            // SEARCH
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Local orderby gq.codigo select gq.nome;
            GradeList.AddRange(GradeQuery.Distinct());
            ViewBag.Clientes__Grade = new SelectList(GradeList);

            var cliente = from cr in _context.Local select cr;

            if (!String.IsNullOrEmpty(Nome))
            {
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.FILTRO() + " ENDREÇOS", location_.GetLocalIPAddress());
                cliente = cliente.Where(g => g.nome.Contains(Nome) || g.sigla.Contains(Nome));
            }

            // RESULT
            return View(cliente);
        }

        
        [Authorize(Roles = "Administrador")]
        public IActionResult NovoLocal()
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
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar e cadastrar novos locais permitidos pelo usuario";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP              
            ViewData["DISPOSITIVO"]   = new SelectList(_context.Dispositivo.ToList().Where(c => c.Ativo), "nome", "nome");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> NovoLocal(Local local)
        {



            var items = maps_.GOOGLEMAPS(local.nome);
            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();

            try
            {

                local.nome = items["results"][0]["address_components"][0]["long_name"];
                local.latitude = items["results"][0]["geometry"]["location"]["lat"];
                local.longitude = items["results"][0]["geometry"]["location"]["lng"];

                int LOCATION = update_.UPDATEMAPALOCATION(local.latitude, local.longitude, local.sigla);


                if (LOCATION == 1)
                {
                    _context.Add(local);
                    await _context.SaveChangesAsync();
                    datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.SAVE() + " ENDEREÇOS " + local.nome + "" + local.latitude + local.longitude, location_.GetLocalIPAddress());

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }


            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error));
            }
        }
        

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DadosLocal(int? id, string? hash)
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
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar e atualizar locais permitidos pelo usuario";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP  
            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            if (id == null || _context.Local == null)
            {
                return NotFound();
            }

            var local = await _context.Local.FindAsync(id);
            if (local == null)
            {
                return NotFound();
            }
            ViewData["DISPOSITIVO"]   = new SelectList(_context.Dispositivo.ToList().Where(c => c.Ativo), "nome", "nome");
            return View(local);
        }
        

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DadosLocal(int id, Local local)
        {
            
            var items = maps_.GOOGLEMAPS(local.nome);

            string HASHCODE    = uid_.UID();

            string DATE        = dateTime.DATE();
            
            if (id != local.codigo)
            {
                return NotFound();
            }

                try
                {
                        local.nome       = items["results"][0]["address_components"][0]["long_name"];
                        local.latitude   = items["results"][0]["geometry"]["location"]["lat"];
                        local.longitude  = items["results"][0]["geometry"]["location"]["lng"];

                        int LOCATION = update_.UPDATEMAPALOCATION(local.latitude, local.longitude, local.sigla);


                        if (LOCATION == 1)
                        {
                            _context.Update(local);
                            await _context.SaveChangesAsync();
                            datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.UPDATE() + " ENDEREÇOS" + local.nome + "" + local.latitude + local.longitude, location_.GetLocalIPAddress());

                        }
                        else
                        { 
                             return RedirectToAction(nameof(Error));
                        }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalExists(local.codigo))
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
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE LOCAIS DO MAPA";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao usuário deletar dados presentes neste ID:" + id;
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP


            string HASHCODE    = uid_.UID();

            string DATE        = dateTime.DATE();

            if (id == null || _context.Local == null)
            {
                return NotFound();
            }

            var local = await _context.Local
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (local == null)
            {
                return NotFound();
            }

            return View(local);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            if (_context.Local == null)
            {
                return Problem("Entity set 'Context.Local'  is null.");
            }
            var local = await _context.Local.FindAsync(id);
            if (local != null)
            {
                _context.Local.Remove(local);
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.DELETE() + " ENDEREÇOS" + local.nome +"", location_.GetLocalIPAddress());        
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalExists(int id)
        {
          return (_context.Local?.Any(e => e.codigo == id)).GetValueOrDefault();
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
