using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using sistema.Models;
using sistema.Areas.Identity.Data;
using sistema.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace sistema.Controllers
{   
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IPasswordHasher<AppUser> passwordHasher;
        private readonly  Uuid uid_              = new Uuid();
        private readonly  Whatsapp whats_        = new Whatsapp();
        private readonly  Error error_           = new Error();
        private readonly  DataLogs datalogs_     = new DataLogs();
        readonly DATEGEN dateTime                = new DATEGEN();
        private readonly IpConn IP               = new IpConn();
        private readonly LoginAsync LoginAsync_  = new LoginAsync();
        private readonly MAPSGENERATE GOOGLE     = new MAPSGENERATE();  
        private readonly UPDATE QUERY            = new UPDATE(); 
        private readonly IpConn IPs              = new IpConn();
        private readonly  Strings Text           = new Strings();
        private readonly UPDATE update_          = new UPDATE();
        private readonly LOCATION location_      = new LOCATION();

        public UsuarioController(AppIdentityDbContext context, UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
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
           ViewBag.RETURN = "/Usuario/Index";     

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
        public async Task<IActionResult> Index(string? Email)
        {
          
            // REGRAS DO USUARIO
            if (this.User.IsInRole("Revenda") || this.User.IsInRole("Parceiro") )

            {
                ViewData["PERFIL"] = update_.GETDATAUSER(@User.Identity.Name);


            }
            

            ViewData["USUARIOS"] = new SelectList(_context.Usuario.ToList().Where(c => c.Ativo), "email", "email");
            


            // REGRAS DO USUARIO
            // VERIFY TIMER
            // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER

            // INFORMAÇÕES POPUP
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE USUÁRIOS";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao administrador gestoriar e cadastrar novos usuários";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP

            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            ViewBag.graf1 = _context.Usuario.Count();
            ViewBag.graf2 = _context.Usuario.Count(C => C.Ativo);
            
            ViewBag.FOTO = IP.FOTURL(); 
            ViewBag.EXT  = ".png"; 

            // SEARCH
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Usuario orderby gq.hashcode select gq.username;
            GradeList.AddRange(GradeQuery.Distinct());
            ViewBag.Clientes__Grade = new SelectList(GradeList);

            var KY = from cr in _context.Usuario select cr;
            
            if (!String.IsNullOrEmpty( Email))
            {

                KY = KY.Where(g => g.email == Email);


            }
            
            // UNIO DATA
            var data = KY.Union(KY).Take(Convert.ToInt32(100));

            // VIEWS
  
        

            if (!String.IsNullOrEmpty(Email) )
            {
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " CREDITO", location_.GetLocalIPAddress());
                if (data.IsNullOrEmpty())
                {
                    return RedirectToAction(nameof(SearchVerification));
                }
                return View(data.Take(70).OrderByDescending(g => g.codigo));

            }
            else
            {
                datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.FILTRO() + " CREDITO", location_.GetLocalIPAddress());
                    
                    if (this.User.IsInRole("Administrador"))
                    {
                        return View(data.Take(20));
                    }else{
                        return View(data.Take(0));
                    }
            }


        }


        [Authorize(Roles = "Administrador")]
        public IActionResult NovoUsuario()
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
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE USUÁRIOS";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao administrador gestoriar e cadastrar novos usuários";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP
            ViewData["estado"]     = new SelectList(_context.Estado.ToList().Where(c => c.Ativo), "nome", "nome");
            ViewData["cidade"]     = new SelectList(_context.Cidade.ToList().Where(c => c.Ativo), "nome", "nome");
            
           return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> NovoUsuario(IFormFile foto, Usuario user)
        {
            
            try{



                string HASHCODE            = uid_.UID();
                string DATE                = dateTime.DATE();
                string ImageName  = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                string SavePath   = Path.Combine(Directory.GetCurrentDirectory(),IP.FOTURL(),ImageName);
                using(var stream  = new FileStream(IP.FOTURL() + HASHCODE +"@"+ user.username + ".png", FileMode.OpenOrCreate))
                
                {
                    await foto.CopyToAsync(stream);
                    user.foto       = HASHCODE +"@"+ user.username;
                    user.hashcode   = HASHCODE;
                    _context.Add(user);

                    // ATUALIZA MAPA
                    datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() + " USUARIO", IP.Ip());
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }


                }catch (Exception e)
                {
                    return RedirectToAction(nameof(Error));
                }
        }


        [Authorize(Roles = "Administrador, Revenda, Parceiro")]
        public async Task<IActionResult> EditarUsuario(int? id)
        {


            ViewData["REVENDA"] = new SelectList(_context.Usuario.ToList().Where(c => c.Ativo), "email", "email");
            


            // REGRAS DO USUARIO


            // VERIFY TIMER
            // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER

           // INFORMAÇÕES POPUP
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE USUÁRIOS";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao administrador gestoriar e atualizar usuários";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP
            ViewBag.FOTO = IP.FOTURL(); 
            ViewBag.EXT  = ".png"; 

            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var user = await _context.Usuario.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
    
      

            ViewData["estado"]     = new SelectList(_context.Estado.ToList().Where(c => c.Ativo), "nome", "nome");
            ViewData["cidade"]     = new SelectList(_context.Cidade.ToList().Where(c => c.Ativo), "nome", "nome");
            return View(user);
        }



        [Authorize(Roles = "Administrador, Revenda, Parceiro")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuario(IFormFile? foto, int? id, Usuario user)
        {

            if (id != user.codigo)
            {
                
                return NotFound();
                
            }
        

            if (ModelState.IsValid)
            {
                try{
                        if(foto != null){

                            
                            string HASHCODE            = uid_.UID();
                            string DATE                = dateTime.DATE();
                            string ImageName  = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                            string SavePath   = Path.Combine(Directory.GetCurrentDirectory(),IP.FOTURL(),ImageName);
                            using(var stream  = new FileStream(IP.FOTURL() + HASHCODE +"@"+ user.username + ".png", FileMode.OpenOrCreate))
                        
                            {
                                await foto.CopyToAsync(stream);
                                user.foto       = HASHCODE +"@"+ user.username;
                                user.hashcode   = HASHCODE;
                                _context.Update(user);
                            

                                await _context.SaveChangesAsync();
                                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.UPDATE() + " USUARIO COM FOTO", IP.Ip());
                            }
                        }else{

                                string HASHCODE            = uid_.UID();
                                string DATE                = dateTime.DATE();
                                user.foto = user.foto;
                                _context.Update(user);
                                await _context.SaveChangesAsync();
                                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.UPDATE() + " USUARIO", IP.Ip());
                        }
                    
                }catch (DbUpdateConcurrencyException){


                        if (!UsuarioExists(user.hashcode))
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
            return View(user);
        }




         [Authorize(Roles = "Administrador, Revenda, Parceiro")]

        public async Task<IActionResult> EditarImagem(int? id)
        {




            // VERIFY TIMER
            // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER


            ViewBag.FOTO = IP.FOTURL(); 
            ViewBag.EXT  = ".png"; 

            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var user = await _context.Usuario.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
 
            return View(user);
        }



         [Authorize(Roles = "Administrador, Revenda, Parceiro")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarImagem(IFormFile? foto, int? id, Usuario user)
        {

            if (id != user.codigo)
            {
                
                return NotFound();
                
            }
        

            if (ModelState.IsValid)
            {
                try{
                        if(foto != null){

                            
                            string HASHCODE            = uid_.UID();
                            string DATE                = dateTime.DATE();
                            string ImageName  = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                            string SavePath   = Path.Combine(Directory.GetCurrentDirectory(),IP.FOTURL(),ImageName);
                            using(var stream  = new FileStream(IP.FOTURL() + HASHCODE +"@"+ user.username + ".png", FileMode.OpenOrCreate))
                        
                            {
                                await foto.CopyToAsync(stream);
                                user.foto       = HASHCODE +"@"+ user.username;
                                user.hashcode   = HASHCODE;
                                _context.Update(user);
                                
                                  


                                await _context.SaveChangesAsync();
                                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.UPDATE() + " USUARIO COM FOTO", IP.Ip());
                            }
                        }else{

                                string HASHCODE            = uid_.UID();
                                string DATE                = dateTime.DATE();
                                user.foto = user.foto;
                                _context.Update(user);
                                await _context.SaveChangesAsync();
                                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.UPDATE() + " USUARIO", IP.Ip());
                        }
                    
                }catch (DbUpdateConcurrencyException){


                        if (!UsuarioExists(user.hashcode))
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
            return View(user);
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

            

            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var user = await _context.Usuario.FirstOrDefaultAsync(m => m.codigo == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }




        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
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
            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE USUÁRIOS";
            ViewBag.INFOTEXT  = "Delete. Esta área permite ao usuário deletar dados presentes neste ID:" + id;
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
            // INFORMAÇÕES POPUP


            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            if (_context.Usuario == null)
            {
                return Problem("Entity set 'Context.Usuario'  is null.");
            }
            var user = await _context.Usuario.FindAsync(id);
            if (user != null)
            {
                _context.Usuario.Remove(user);
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.DELETE() + " USUARIO", IP.Ip());
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(string id)
        {
          return (_context.Usuario?.Any(e => e.hashcode == id)).GetValueOrDefault();
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
