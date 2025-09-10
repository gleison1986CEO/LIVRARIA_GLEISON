using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistema.Models;
using sistema.Classes;
using sistema.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace sistema.Controllers
{

    [Authorize]
    
    public class DispositivosController : Controller
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

        public DispositivosController(AppIdentityDbContext context, UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
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
           ViewBag.RETURN = "/Dispositivo/Index";     

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

        [Authorize(Roles = "Administrador, Revenda, Parceiro")]
        public async Task<IActionResult> Index(string? Nome, string? Id, string? Chave, string? Inicio, string? Fim)
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE DISPOSITIVOS";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador atualizar dispositivos/aparelhos, que estão utilizando a plataforma.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 

            ViewBag.HASHCODE = uid_.UID();

            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();

            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Dispositivo orderby gq.codigo select gq.nome;
            GradeList.AddRange(GradeQuery.Distinct());

            var KY = from cr in _context.Dispositivo select cr;




            if (!String.IsNullOrEmpty(Nome))
            {

                KY = KY.Where(g => g.nome == Nome);


            }

            if (!String.IsNullOrEmpty(Id))
            {

                KY = KY.Where(g => g.id == Id);


            }


            if (!String.IsNullOrEmpty(Chave))
            {

                KY = KY.Where(g => g.chave == Chave);


            }


            if (!String.IsNullOrEmpty(Inicio) && !String.IsNullOrEmpty(Fim))
            {


                DateTime final = Convert.ToDateTime(Fim);
                DateTime inicial = Convert.ToDateTime(Inicio);

                KY = KY.Where(g => g.data >= inicial && g.data <= final);

            }

            // UNIO DATA
            var data = KY.Union(KY).Take(Convert.ToInt32(100));



            // VIEWS
            ViewBag.TOTAL = KY.Count();
            // VIEWS

            if (!String.IsNullOrEmpty(Nome) ||
                    !String.IsNullOrEmpty(Chave) ||
                    !String.IsNullOrEmpty(Id) ||
                    !String.IsNullOrEmpty(Inicio) ||
                    !String.IsNullOrEmpty(Fim))
            {
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " DISPOSITIVO", location_.GetLocalIPAddress());
                if (data.IsNullOrEmpty())
                {
                    return RedirectToAction(nameof(SearchVerification));
                }
                return View(data.Take(70).OrderByDescending(g => g.date));

            }
            else
            {
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " DISPOSITIVO", location_.GetLocalIPAddress());
                return View(data.Take(10));
            }
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Novo()
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE DISPOSITIVOS";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador cadastrar dispositivos/aparelhos, que estão utilizando a plataforma.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 


            ViewData["HOSTNAME"]     = new SelectList(_context.Usuario.ToList().Where(c => c.Ativo), "email", "email");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Novo(Dispositivo dispositivo)
        {
            
            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();
            dispositivo.data         = Convert.ToDateTime(dispositivo.date);
            dispositivo.hashcode     = HASHCODE;

            try
            {
                //CRIA DISPOSITIVO NO MAPA
                update_.INSERTMAPA(HASHCODE,dispositivo.chave);

                _context.Add(dispositivo);
                await _context.SaveChangesAsync();
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.SAVE() + " DISPOSITIVO", location_.GetLocalIPAddress());
                return RedirectToAction(nameof(Index));


            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error));
            }
        }
        


        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Dados(int? id, string? hash)
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE DISPOSITIVOS";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador atualizar dispositivos/aparelhos, que estão utilizando a plataforma.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 

            ViewData["HOSTNAME"] = new SelectList(_context.Usuario.ToList().Where(c => c.Ativo), "email", "email");
            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();

            if (id == null || _context.Dispositivo == null)
            {
                return RedirectToAction(nameof(Error));
            }

            var dispositivo = await _context.Dispositivo.FindAsync(id);
            if (dispositivo == null)
            {
                return RedirectToAction(nameof(Error));
            }
            datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.UPDATE() + " DISPOSITIVO", location_.GetLocalIPAddress());
            return View(dispositivo);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Dados(int id, Dispositivo dispositivo)
        {


            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();
            dispositivo.data = Convert.ToDateTime(dispositivo.date);
            dispositivo.hashcode = HASHCODE;

            if (id != dispositivo.codigo)
            {
                return RedirectToAction(nameof(Error));
            }


            try
            {
                //CRIA DISPOSITIVO NO MAPA
                update_.INSERTMAPA(HASHCODE, dispositivo.chave);
                _context.Update(dispositivo);
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.UPDATE() + " DISPOSITIVO", location_.GetLocalIPAddress());
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispositivoExists(dispositivo.codigo))
                {
                    return RedirectToAction(nameof(Error));
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
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE DISPOSITIVOS";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao usuário deletar dados presentes neste ID:" + id;
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP

            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();


            if (id == null || _context.Dispositivo == null)
            {
                return RedirectToAction(nameof(Error));
            }

            var dispositivo = await _context.Dispositivo
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (dispositivo == null)
            {
                return RedirectToAction(nameof(Error));
            }

            datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.DELETE() + " DISPOSITIVO", location_.GetLocalIPAddress());
            return View(dispositivo);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();


            if (_context.Dispositivo == null)
            {
                return Problem("Entity set 'Context.Dispositivo'  is null.");
            }
            var dispositivo = await _context.Dispositivo.FindAsync(id);
            if (dispositivo != null)
            {
                _context.Dispositivo.Remove(dispositivo);
            }

            datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.DELETE() + " DISPOSITIVO", location_.GetLocalIPAddress());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DispositivoExists(int id)
        {
            return (_context.Dispositivo?.Any(e => e.codigo == id)).GetValueOrDefault();
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
