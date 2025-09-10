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
    public class CidadesController : Controller
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
        
        

        public CidadesController(AppIdentityDbContext context,UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
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
           ViewBag.RETURN = "/Cidades/Index";     

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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE CIDADES";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador listar as cidades para que possa ser utilizado em pagamentos e localizações de usuários.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 
            
            ///////////CONTAGEM DE CIDADES
            ViewBag.graf1 = _context.Cidade.Count();
            //////////CONTAGEM DE CIDADES ATIVOS
            ViewBag.graf2 = _context.Cidade.Count(C => C.Ativo);


            // SEARCH
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Cidade orderby gq.codigo select gq.nome;
            GradeList.AddRange(GradeQuery.Distinct());
            ViewBag.Clientes__Grade = new SelectList(GradeList);

            var cliente = from cr in _context.Cidade select cr;

            if (!String.IsNullOrEmpty(Nome))
            {
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.FILTRO() + " CIDADE", location_.GetLocalIPAddress());
                cliente = cliente.Where(g => g.nome == Nome);
            }

            // RESULT
            return View(cliente);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult NovaCidade()
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE CIDADES";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador cadastrar cidades para que possa ser utilizado em pagamentos e localizações de usuários.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> NovaCidade(Cidade cidade)
        {



            List<Maps> items = maps_.MAPS(cidade.nome);


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
                        cidade.latitude = lats;
                        cidade.longitude = longs;
                        _context.Add(cidade);
                        await _context.SaveChangesAsync();
                        datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.SAVE() + " CIDADE " + cidade.nome + "" + cidade.latitude + cidade.longitude, location_.GetLocalIPAddress());
                        break;
                    }
                    return RedirectToAction(nameof(Index));

                }
                return View(cidade);

            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error));
            }
        }
        
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DadosCidade(int? id, string? hash)
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CADASTRO DE CIDADES";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador atualizar cidades para que possa ser utilizado em pagamentos e localizações de usuários.";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 
            
            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();

            if (id == null || _context.Cidade == null)
            {
                return NotFound();
            }

            var cidade = await _context.Cidade.FindAsync(id);
            if (cidade == null)
            {
                return NotFound();
            }
            return View(cidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DadosCidade(int id, Cidade cidade)
        {

            List<Maps> items = maps_.MAPS(cidade.nome);

            string HASHCODE = uid_.UID();

            string DATE = dateTime.DATE();

            if (id != cidade.codigo)
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
                        cidade.latitude = lats;
                        cidade.longitude = longs;
                        _context.Update(cidade);
                        await _context.SaveChangesAsync();
                        datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.UPDATE() + " CIDADE " + cidade.nome + "" + cidade.latitude + cidade.longitude, location_.GetLocalIPAddress());
                        break;
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CidadeExists(cidade.codigo))
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
            return View(cidade);
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
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE CIDADES";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao usuário deletar dados presentes neste ID:" + id;
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP


            string HASHCODE = uid_.UID();

            string DATE = dateTime.DATE();

            if (id == null || _context.Cidade == null)
            {
                return NotFound();
            }

            var cidade = await _context.Cidade
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            if (_context.Cidade == null)
            {
                return Problem("Entity set 'Context.Cidade'  is null.");
            }
            var cidade = await _context.Cidade.FindAsync(id);
            if (cidade != null)
            {
                _context.Cidade.Remove(cidade);
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.DELETE() + " CIDADE" + cidade.nome +"", location_.GetLocalIPAddress());        
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CidadeExists(int id)
        {
          return (_context.Cidade?.Any(e => e.codigo == id)).GetValueOrDefault();
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
