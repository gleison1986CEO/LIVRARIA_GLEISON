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
    public class SobreController : Controller
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
        
        

        public SobreController(AppIdentityDbContext context,UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
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
           ViewBag.RETURN = "/Sobre/Index";     

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
        public async Task<IActionResult> Index(string? Categoria, string? Titulo, string? Inicio, string? Fim)
        {

            ViewData["CATEGORIA"]     = new SelectList(_context.Categoria.ToList().Where(c => c.Ativo), "titulo", "titulo");
            // VERIFY TIMER
            // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER  
            
            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE O SISTEMA";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar e cadastrar novas informações ao sistema";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP  


            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();



            // SEARCH
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Sobre orderby gq.codigo select gq.date;
            GradeList.AddRange(GradeQuery.Distinct());
            ViewBag.Clientes__Grade = new SelectList(GradeList);

            var KY = from cr in _context.Sobre select cr;



            if (!String.IsNullOrEmpty(Categoria))
            {

                KY = KY.Where(g => g.categoria.Contains(Categoria));


            }

            if (!String.IsNullOrEmpty(Titulo))
            {

                KY  = KY.Where(g => g.titulo == Titulo);


            }


            if (!String.IsNullOrEmpty(Inicio) && !String.IsNullOrEmpty(Fim))
            {


                DateTime final   = Convert.ToDateTime(Fim);
                DateTime inicial = Convert.ToDateTime(Inicio);
                
                KY = KY.Where(g => g.data >= inicial && g.data <= final);

            }
            
            // UNIO DATA
            var data = KY.Union(KY).Take(Convert.ToInt32(100));

            // VIEWS
            ViewBag.TOTAL =  KY.Count();
            ViewBag.PORCREDITOREVENDA = "";
            ViewBag.REVENDEDORES = "";
            // VIEWS

            if (!String.IsNullOrEmpty(Categoria) ||
                    !String.IsNullOrEmpty(Titulo) ||
                    !String.IsNullOrEmpty(Inicio) ||
                    !String.IsNullOrEmpty(Fim))
            {
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " CATEGORIA", location_.GetLocalIPAddress());
                if (data.IsNullOrEmpty())
                {
                    return RedirectToAction(nameof(SearchVerification));
                }
                return View(data.Take(70).OrderByDescending(g => g.date));

            }
            else
            {
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " SOBRE", location_.GetLocalIPAddress());
                return View(data.Take(10));
            }
        }

        
        
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
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

            if (id == null || _context.Sobre == null)
            {
                return NotFound();
            }

            var dados = await _context.Sobre
                .FirstOrDefaultAsync(m => m.codigo == id);
            datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " SOBRE", location_.GetLocalIPAddress());
            if (dados == null)
            {
                return NotFound();
            }

            return View(dados);
        }
        
        
        
        [Authorize(Roles = "Administrador")]
        public IActionResult Novo()
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE O SISTEMA";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar e cadastrar novas informações ao sistema";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP              
            ViewData["CATEGORIA"] = new SelectList(_context.Categoria.ToList().Where(c => c.Ativo), "titulo", "titulo");
            return View();
        }

        
        
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(Sobre dados)
        {

  

            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();
            dados.data         = Convert.ToDateTime(dados.date);
            dados.hashcode     = HASHCODE;
            
            try
            {

                _context.Add(dados);
                await _context.SaveChangesAsync();
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.SAVE() + " SOBRE", location_.GetLocalIPAddress());

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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE O SISTEMA";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador gestoriar e atualizar novas informações ao sistema";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP  
            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            if (id == null || _context.Sobre == null)
            {
                return NotFound();
            }

            var dados = await _context.Sobre.FindAsync(id);
            if (dados == null)
            {
                return NotFound();
            }
            ViewData["CATEGORIA"]     = new SelectList(_context.Categoria.ToList().Where(c => c.Ativo), "titulo", "titulo");
            return View(dados);
        }
        

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dados(int id, Sobre dados)
        {


            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();
            dados.data         = Convert.ToDateTime(dados.date);
            dados.hashcode     = HASHCODE;
            
            if (id != dados.codigo)
            {
                return NotFound();
            }


                try
                {
 
                        _context.Update(dados);
                        await _context.SaveChangesAsync();
                        datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.UPDATE() + " SOBRE", location_.GetLocalIPAddress());    
              
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SobreExists(dados.codigo))
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
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE SISTEMA";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao usuário deletar dados presentes neste ID:" + id;
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP


            string HASHCODE    = uid_.UID();

            string DATE        = dateTime.DATE();

            if (id == null || _context.Sobre == null)
            {
                return NotFound();
            }

            var dados = await _context.Sobre
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (dados == null)
            {
                return NotFound();
            }

            return View(dados);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            if (_context.Sobre == null)
            {
                return Problem("Entity set 'Context.Sobre'  is null.");
            }
            var dados = await _context.Sobre.FindAsync(id);
            if (dados!= null)
            {
                _context.Sobre.Remove(dados);
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.DELETE() + " SOBRE", location_.GetLocalIPAddress());        
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SobreExists(int id)
        {
          return (_context.Sobre?.Any(e => e.codigo == id)).GetValueOrDefault();
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
