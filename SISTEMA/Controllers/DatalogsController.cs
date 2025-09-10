using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema.Models;
using sistema.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using sistema.Classes;
using Microsoft.AspNetCore.Identity;

namespace sistema.Controllers
{
    [Authorize]
    public class DatalogsController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IPasswordHasher<AppUser> passwordHasher;
        private readonly  DataLogs datalogs_     = new DataLogs();
        readonly DATEGEN dateTime                = new DATEGEN();
        private readonly  Uuid uid_              = new Uuid();
        private readonly LoginAsync LoginAsync_  = new LoginAsync();
        private readonly CSVGEN  CSVGENERATION   = new CSVGEN();
        public DatalogsController(AppIdentityDbContext context,UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
        {
            _context       = context;
            userManager    = userMgr;
            signInManager  = signinMgr;  


        }
        
        [Authorize(Roles = "Administrador")]
        public IActionResult SessionrTimer()
        {

            return View();
        }
        
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index(string? Login, string? Ip, string? DataInicio, string? DataFim, string? Executou)
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE DATALOGS DO SISTEMA";
            ViewBag.INFOTEXT  = "Administradores. Permite ao administrador ver e revisar o que esta acontecendo no sistema e suas ações pelos usuários";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPU
            ViewBag.GERAL = _context.Datalog.Count();
            // SEARCH
            var GradeList = new List<string>();
            var GradeQuery = from gq in _context.Datalog orderby gq.codigo select gq.login;
            GradeList.AddRange(GradeQuery.Distinct());
            ViewBag.Clientes__Grade = new SelectList(GradeList);

            var cliente = from cr in _context.Datalog select cr;

            if (

                !String.IsNullOrEmpty(Login) ||
                !String.IsNullOrEmpty(Ip) ||
                !String.IsNullOrEmpty(Executou) ||
                !String.IsNullOrEmpty(DataInicio) ||
                !String.IsNullOrEmpty(DataFim)



            )
            {

                try
                {


                    if (Login != null)
                    {
                        cliente = cliente.Where(g => g.login == Login);
                    }

                    if (Ip != null)
                    {
                        cliente = cliente.Where(g => g.ip == Ip);
                    }

                    if (Executou != null)
                    {
                        cliente = cliente.Where(g => g.executou.Contains(Executou));
                    }

                    if (DataInicio != null)
                    {


                        var date = Convert.ToDateTime(DataInicio);
                        cliente = cliente.Where(g => g.data_hora >= date);
                    }

                    if (DataFim != null)
                    {

                        var date = Convert.ToDateTime(DataFim);
                        cliente = cliente.Where(g => g.data_hora <= date);
                    }



                    // UNIO DATA
                    var data = cliente.Union(cliente).OrderByDescending(g => g.data_hora);
                    // UNION DATA


                    /// CSV DATA

                    String CSV = CSVGENERATION.DATALOG(data);

                    /// CSV DATA

                    if (data != null)
                    {

                        ViewBag.CSVFILE = CSV;
                        ViewBag.Count = data.Count();
                        return View(data.OrderByDescending(g => g.codigo));

                    }
                    else
                    {

                        ViewBag.Count = data.Take(5).Count();
                        return View(data.Take(5));
                    }

                }
                catch
                {
                    return View(cliente.Take(0));

                }
            }

            else
            {
                return View(cliente.Take(10));
            }
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create(Datalog datalog)
        {

            string HASHCODE = uid_.UID();
            string DATE = dateTime.DATE();



            if (ModelState.IsValid)
            {
                datalog.datahora = Convert.ToString(DATE);
                datalog.data_hora = Convert.ToDateTime(DATE);
                _context.Add(datalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(datalog);
        }


        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
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

            if (id == null || _context.Datalog == null)
            {
                return NotFound();
            }

            var datalog = await _context.Datalog.FindAsync(id);
            if (datalog == null)
            {
                return NotFound();
            }
            return View(datalog);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, Datalog datalog)
        {


            if (id != datalog.codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatalogExists(datalog.codigo))
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
            return View(datalog);
        }
        
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
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


            if (id == null || _context.Datalog == null)
            {
                return NotFound();
            }

            var datalog = await _context.Datalog
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (datalog == null)
            {
                return NotFound();
            }

            return View(datalog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Datalog == null)
            {
                return Problem("Entity set 'Context.Datalog'  is null.");
            }
            var datalog = await _context.Datalog.FindAsync(id);
            if (datalog != null)
            {
                _context.Datalog.Remove(datalog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatalogExists(int id)
        {
          return (_context.Datalog?.Any(e => e.codigo == id)).GetValueOrDefault();
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
