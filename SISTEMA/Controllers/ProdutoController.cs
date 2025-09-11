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
    public class ProdutoController : Controller
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
        
        

        public ProdutoController(AppIdentityDbContext context,UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
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
           ViewBag.RETURN = "/Produto/Index";     

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
        public async Task<IActionResult> Index(string? Titulo, string? Categoria, string? SubCategoria, string? Console, string? Cupom,string? Inicio, string? Fim)
        {

            ViewData["CATEGORIA"]     = new SelectList(_context.Categoria.ToList().Where(c => c.Ativo), "nome", "nome");
            ViewData["SUBCATEGORIA"]  = new SelectList(_context.SubCategoria.ToList().Where(c => c.Ativo), "nome", "nome");
            ViewData["CONSOLE"]       = new SelectList(_context.Consoles.ToList().Where(c => c.Ativo), "nome", "nome");
            ViewData["CUPOM"]         = new SelectList(_context.Cupom.ToList().Where(c => c.Ativo), "nome", "nome");

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
            ViewBag.EXT = ".png";

            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE PRODUTOS";
            ViewBag.INFOTEXT  = "Administradores. Esta área permite ao super-admin gestoriar e cadastrar novos PRODUTOS ao website que fica na área principal do sistema.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 

            // SEARCH
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Produto orderby gq.codigo select gq.date;
            GradeList.AddRange(GradeQuery.Distinct());
            ViewBag.Clientes__Grade = new SelectList(GradeList);

            var KY = from cr in _context.Produto select cr;



            if (!String.IsNullOrEmpty(Categoria))
            {

                KY = KY.Where(g => g.categoria.Contains(Categoria));


            }

            if (!String.IsNullOrEmpty(SubCategoria))
            {

                KY = KY.Where(g => g.subcategoria.Contains(SubCategoria));


            }

            if (!String.IsNullOrEmpty(Console))
            {

                KY  = KY.Where(g => g.console == Console);


            }

            if (!String.IsNullOrEmpty(Titulo))
            {

                KY  = KY.Where(g => g.titulo == Titulo);


            }
            if (!String.IsNullOrEmpty(Cupom))
            {

                KY  = KY.Where(g => g.cupom == Cupom);


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
                    !String.IsNullOrEmpty(SubCategoria) ||
                    !String.IsNullOrEmpty(Titulo) ||
                    !String.IsNullOrEmpty(Cupom) ||
                    !String.IsNullOrEmpty(Console) ||
                    !String.IsNullOrEmpty(Inicio) ||
                    !String.IsNullOrEmpty(Fim))
            {
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " PRODUTOS", location_.GetLocalIPAddress());
                if (data.IsNullOrEmpty())
                {
                    return RedirectToAction(nameof(SearchVerification));
                }
                return View(data.Take(70).OrderByDescending(g => g.date));

            }
            else
            {
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " PRODUTOS", location_.GetLocalIPAddress());
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

            if (ViewBag.TIMER == "" || ViewBag.TIMER == null)
            {
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER  

            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE PRODUTOS";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador cadastrar novos PRODUTOS ao website, que ficam no topo do website";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 

            ViewData["CATEGORIA"]     = new SelectList(_context.Categoria.ToList().Where(c => c.Ativo), "nome", "nome");
            ViewData["SUBCATEGORIA"]  = new SelectList(_context.SubCategoria.ToList().Where(c => c.Ativo), "nome", "nome");
            ViewData["CONSOLE"]       = new SelectList(_context.Consoles.ToList().Where(c => c.Ativo), "nome", "nome");
            ViewData["CUPOM"]         = new SelectList(_context.Cupom.ToList().Where(c => c.Ativo), "nome", "nome");

            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(IFormFile foto, Produto dados)
        {

         

            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();
            dados.data         = Convert.ToDateTime(dados.date);
            dados.hashcode     = HASHCODE;
            
            try
            {


                string ImageName  = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                string SavePath   = Path.Combine(Directory.GetCurrentDirectory(),IpURI.PRODUTO(),ImageName);

                using (var stream = new FileStream(IpURI.PRODUTO() + HASHCODE + ".png", FileMode.OpenOrCreate))

                {


                    await foto.CopyToAsync(stream);
                    dados.foto     = HASHCODE;
                    dados.hashcode = HASHCODE;

                    _context.Add(dados);
                    await _context.SaveChangesAsync();
                    datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.SAVE() + " PRODUTO", location_.GetLocalIPAddress());

                    return RedirectToAction(nameof(Index));
                }


 

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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE PRODUTO";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador atualizar PRODUTO ao website, que ficam no topo do website";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 
            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            if (id == null || _context.Produto == null)
            {
                return NotFound();
            }

            var dados = await _context.Produto.FindAsync(id);
            if (dados == null)
            {
                return NotFound();
            }
            ViewData["CATEGORIA"]     = new SelectList(_context.Categoria.ToList().Where(c => c.Ativo), "nome", "nome");
            ViewData["SUBCATEGORIA"]  = new SelectList(_context.SubCategoria.ToList().Where(c => c.Ativo), "nome", "nome");
            ViewData["CONSOLE"]       = new SelectList(_context.Consoles.ToList().Where(c => c.Ativo), "nome", "nome");
            ViewData["CUPOM"]         = new SelectList(_context.Cupom.ToList().Where(c => c.Ativo), "nome", "nome");

            return View(dados);
        }
        

         [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dados(IFormFile foto,int id, Produto dados)
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

            if(foto != null){
                    
                    string ImageName  = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                    string SavePath   = Path.Combine(Directory.GetCurrentDirectory(),IpURI.PRODUTO(),ImageName);

                    using (var stream = new FileStream(IpURI.PRODUTO() + HASHCODE + ".png", FileMode.OpenOrCreate))

                        {


                            await foto.CopyToAsync(stream);
                            dados.foto     = HASHCODE;
                            dados.hashcode = HASHCODE;

                            _context.Update(dados);
                            await _context.SaveChangesAsync();
                            datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.UPDATE() + " PRODUTO", location_.GetLocalIPAddress());    
                        }
                    
                }else{
                            dados.foto = dados.foto;
                            _context.Update(dados);
                            await _context.SaveChangesAsync();
                            datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.UPDATE() + " PRODUTO", location_.GetLocalIPAddress());    
                }
               
            }catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(dados.codigo))
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
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE PRODUTOS";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao usuário deletar dados presentes neste ID:" + id;
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP


            string HASHCODE    = uid_.UID();

            string DATE        = dateTime.DATE();

            if (id == null || _context.Produto == null)
            {
                return NotFound();
            }

            var dados = await _context.Produto
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

            if (_context.Produto == null)
            {
                return Problem("Entity set 'Context.Produto'  is null.");
            }
            var dados = await _context.Produto.FindAsync(id);
            if (dados!= null)
            {
                _context.Produto.Remove(dados);
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.DELETE() + " PRODUTO", location_.GetLocalIPAddress());        
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
          return (_context.Produto?.Any(e => e.codigo == id)).GetValueOrDefault();
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
