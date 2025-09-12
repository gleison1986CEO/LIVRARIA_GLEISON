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
    public class RequisitoController : Controller
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
        
        

        public RequisitoController(AppIdentityDbContext context,UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
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
           ViewBag.RETURN = "/Requisito/Index";     

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
        public async Task<IActionResult> Index(string? Produto, string? Inicio, string? Fim)
        {
            
            ViewData["PRODUTO"]     = new SelectList(_context.Produto.ToList().Where(c => c.Ativo), "titulo", "titulo");
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE REQUISITOS";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador cadastrar novos requisitos e revisar requisitos anexadas aos filmes, canais, séries e eoutros";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP  
            // SEARCH
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Requisito orderby gq.codigo select gq.date;
            GradeList.AddRange(GradeQuery.Distinct());
            ViewBag.Clientes__Grade = new SelectList(GradeList);

            var KY = from cr in _context.Requisito select cr;



            if (!String.IsNullOrEmpty(Produto))
            {

                KY = KY.Where(g => g.nome.Contains(Produto));


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

            if (!String.IsNullOrEmpty(Produto) ||
                    !String.IsNullOrEmpty(Inicio) ||
                    !String.IsNullOrEmpty(Fim))
            {
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " REQUISITO", location_.GetLocalIPAddress());
                if (data.IsNullOrEmpty())
                {
                    return RedirectToAction(nameof(SearchVerification));
                }
                return View(data.Take(70).OrderByDescending(g => g.date));

            }
            else
            {
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " REQUISITO", location_.GetLocalIPAddress());
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
            
            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE REQUISITO";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador cadastrar REQUISITO para o website.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 

            ViewData["PRODUTO"]     = new SelectList(_context.Produto.ToList().Where(c => c.Ativo), "titulo", "titulo");
            ViewData["CONSOLE"]     = new SelectList(_context.Consoles.ToList().Where(c => c.Ativo), "nome", "nome");
            return View();
        }

         [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(Requisito dados)
        {

  

            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();
            dados.data         = Convert.ToDateTime(dados.date);
            dados.hashcode     = HASHCODE;
            
            try
            {

                _context.Add(dados);
                await _context.SaveChangesAsync();
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.SAVE() + " REQUISITO", location_.GetLocalIPAddress());

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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE REQUISITO";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador atualizar REQUISITO para o website.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 


            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            if (id == null || _context.Requisito == null)
            {
                return NotFound();
            }

            var dados = await _context.Requisito.FindAsync(id);
            if (dados == null)
            {
                return NotFound();
            }

            
            ViewData["PRODUTO"]     = new SelectList(_context.Produto.ToList().Where(c => c.Ativo), "titulo", "titulo");
            ViewData["CONSOLE"]     = new SelectList(_context.Consoles.ToList().Where(c => c.Ativo), "nome", "nome");
            return View(dados);
        }
        

         [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dados(int id, Requisito dados)
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
                        datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.UPDATE() + " REQUISITO", location_.GetLocalIPAddress());    
              
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequisitoExists(dados.codigo))
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
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE REQUISITO";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao usuário deletar dados presentes neste ID:" + id;
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP


            string HASHCODE    = uid_.UID();

            string DATE        = dateTime.DATE();

            if (id == null || _context.Requisito == null)
            {
                return NotFound();
            }

            var dados = await _context.Requisito
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

            if (_context.Requisito == null)
            {
                return Problem("Entity set 'Context.Requisito'  is null.");
            }
            var dados = await _context.Requisito.FindAsync(id);
            if (dados!= null)
            {
                _context.Requisito.Remove(dados);
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.DELETE() + " CUPONS ", location_.GetLocalIPAddress());        
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequisitoExists(int id)
        {
          return (_context.Requisito?.Any(e => e.codigo == id)).GetValueOrDefault();
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
