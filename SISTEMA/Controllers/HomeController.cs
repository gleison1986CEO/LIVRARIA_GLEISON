﻿using sistema.Models;
using sistema.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using sistema.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace sistema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly  DataLogs datalogs_     = new DataLogs();
        readonly DATEGEN dateTime                = new DATEGEN();
        private readonly  Uuid uid_              = new Uuid();
        private readonly IpConn IPs              = new IpConn();
        private readonly LoginAsync LoginAsync_  = new LoginAsync();
        private readonly  Strings Text           = new Strings();
        private readonly  FILES ARQUIVOS         = new FILES();
        private readonly UPDATE update_          = new UPDATE();
        
        private readonly AppIdentityDbContext _context;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IPasswordHasher<AppUser> passwordHasher;
        public HomeController(AppIdentityDbContext context,UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
        {
            _context       = context;
            userManager    = userMgr;
            signInManager  = signinMgr;  
        }
        

        
        [Authorize(Roles = "Administrador,Membro")]
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

        public IActionResult SessionrTimer()
        {

            return View();
        }              
        public IActionResult Grafico()
        {
            
              
            ViewBag.INFOTITLE = "INFORMAÇÕES";
            ViewBag.INFOTEXT  = "Informações gerais";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";            

          try{

            // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER 
            // VERIFY TIMER   
  




            string HASHCODE                  = uid_.UID();

            string DATE                      = dateTime.DATE();

            ViewBag.USUARIO                  = _context.Usuario.Count();

            ViewBag.USUARIOATIVO             = _context.Usuario.Where(c => c.Ativo == true).Count();


            ViewBag.PROD_VENDA               = _context.Produto.Where(c => c.Ativo == true).Count();

            ViewBag.PRO_ESTOQUE              = _context.Produto.Where(c => c.Ativo == false).Count();

            ViewBag.PROD_TOTAL               = _context.Produto.Sum(g => Convert.ToInt32(g.valor));

            ViewBag.VENDA                    = _context.Produto.Where(c => c.Ativo == true).Sum(g => Convert.ToInt32(g.valor));

            ViewBag.ESTOQUE                  = _context.Produto.Where(c => c.Ativo == false).Sum(g => Convert.ToInt32(g.valor));

            
            ViewBag.IDENTIFICACAO            = _context.Identificacao.Count();
  
            ViewBag.IDENTIFICACAOTRUE        = _context.Identificacao.Count(c => c.Ativo ==true);

            ViewBag.IDENTIFICACAOFALSE       = _context.Identificacao.Count(c => c.Ativo ==false);

            _context.Database.SetCommandTimeout(60);

            datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.FILTRO() +"ANALISE GRÁFICO PAINEL ADMINISTRATIVO", IPs.Ip());
        
            return View();


            }   catch
            {
                return RedirectToAction(nameof(Verificacao));
            }  

            return View();
        }              


        public IActionResult Documentacao()
        {
            // VERIFY TIMER
                        // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER  

            ViewBag.INFOTITLE = "INFORMAÇÕES";
            ViewBag.INFOTEXT  = "Informações gerais";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";

            return View();
        }              

        public IActionResult Index()
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
            ViewBag.INFOTITLE = "INFORMAÇÕES";
            ViewBag.INFOTEXT  = "Informações gerais";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
             
            // INFORMAÇÕES POPUP 
            var BANNER = from cr in _context.Banner select cr;
            var SOBRE = from cr in _context.Sobre select cr;

            ViewBag.BANNER = BANNER.Where(g => g.Ativo == true);
            ViewBag.SOBRE = SOBRE.Where(g => g.Ativo == true);
            ViewBag.EXT = ".png";
            
            return View();
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