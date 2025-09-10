﻿using sistema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sistema.Classes;
using sistema.Areas.Identity.Data;

namespace sistema.Controllers
{   



    public class AdminController : Controller
    {

        private readonly AppIdentityDbContext _context;
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<AppUser> signInManager;
        private IPasswordHasher<AppUser> passwordHasher;
        private readonly  DataLogs datalogs_     = new DataLogs();
        readonly DATEGEN dateTime                = new DATEGEN();
        private readonly  Uuid uid_              = new Uuid();
        private readonly IpConn IPs              = new IpConn();
        private readonly LoginAsync LoginAsync_  = new LoginAsync();
        private readonly UPDATE Query_           = new UPDATE();
        private readonly  Strings Text           = new Strings();

        public AdminController(AppIdentityDbContext context,UserManager<AppUser> userMgr,RoleManager<IdentityRole> roleMgr, SignInManager<AppUser> signinMgr, IPasswordHasher<AppUser> passwordHash)
        {

            _context       = context;
            userManager    = userMgr;
            signInManager  = signinMgr;  
            roleManager    = roleMgr;
            passwordHasher = passwordHash;

        }

        [Authorize]
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
           ViewBag.RETURN = "/Admin/Index";     

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



        public IActionResult ErrorWeb()
        {
            
           return View();
        }
        [Authorize]
        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            
            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();
            // VERIFY TIMER
            // VERIFY TIMER
            TIMER();
            // VERIFY TIMER 
            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE USUARIO/ADMINISTRADORES";
            ViewBag.INFOTEXT  = "Administradores. Esta área permite ao super-admin cadastrar novos usuários a plataforma!";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 
            if(ViewBag.TIMER == "" || ViewBag.TIMER ==null){
                return RedirectToAction(nameof(SessionrTimer));
            }
            // VERIFY TIMER 
            return View(userManager.Users);
        }

        [Authorize]
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
            // INFORMAÇÕES POPUP

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE USUARIO/ADMINISTRADORES";
            ViewBag.INFOTEXT  = "Administradores. Esta área permite ao super-admin criar novos usuários ao sistema!";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 
            return View();
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Administrador")]                
        public async Task<IActionResult> Create(User user)
        {
            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();
            
            try{
           
                AppUser appUser = new AppUser{UserName = user.Name, Email  = user.Email};

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded){
                    
                    int USERS = Query_.SELECTUSERS(user.Email);
                    if (USERS == 0)
                    {

                        Query_.APPUSERS(user.Email, user.Password, user.Name);
                        string? ID      = Query_.APPUSERID(user.Email);
                          
                        if (ID != null || ID != ""){

                            
                            if (user.Type == "PARCEIRO")
                            {   

                                
                                IdentityRole role = await roleManager.FindByIdAsync("20cd773b-3b13-4f3-a2cd-af35e8f1354c"); //PARCEIRO
                                result = await userManager.AddToRoleAsync(appUser, role.Name);
                                if (result.Succeeded){
                                
                                    int Parceiro    = Query_.CADASTRARPARCEIRO(user.Email, user.Password, user.Name);

                                    if (Parceiro == 1)
                                    {
                                        datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.SAVE() + " NOVO USUÁRIO", IPs.Ip());
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    { 
                                        return RedirectToAction(nameof(Error)); 
                                    }

                                }else{
                                    return RedirectToAction(nameof(Error)); 
                                }

                            }

                            if (user.Type == "REVENDEDOR")
                            {

                                IdentityRole role = await roleManager.FindByIdAsync("40cd773b-3b13-4f3-a2cd-af35e8f1354c"); //PARCEIRO
                                result = await userManager.AddToRoleAsync(appUser, role.Name);
                                
                                if (result.Succeeded){
                                    int Revendedor  = Query_.CADASTRARREVENDEDOR(user.Email, user.Password, user.Name);
                                    int Credito     = Query_.CREDITOREVENDAADD(user.Email);
                                    
                                    if (Revendedor == 1 && Credito == 1)
                                    {

                                        datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.SAVE() + " NOVO USUÁRIO", IPs.Ip());
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        return RedirectToAction(nameof(Error)); 
                                    }
                                }else{
                                    return RedirectToAction(nameof(Error)); 
                                }    
                            } 


  
                        }        
                    }
                }


            }catch (Exception e){
                return RedirectToAction(nameof(Error));       
            }
            return View(user);
           
        }
             




        // AREA PUBLICA PARA CADASTRO
        public IActionResult RevendaCreate()
        {

            return View();
        }

        [HttpPost]   
        // AREA PUBLICA PARA CADASTRO  
        public async Task<IActionResult> RevendaCreate(User user)
        {
            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            try{
           
                AppUser appUser = new AppUser{UserName = user.Name, Email  = user.Email};

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded){

                    int USERS = Query_.SELECTUSERS(user.Email);
                    if (USERS == 0)
                    {


                        Query_.APPUSERS(user.Email, user.Password, user.Name);
                        string? ID      = Query_.APPUSERID(user.Email);

    
                        if (user.Type == "REVENDEDOR")
                        {
                            

                            if (ID != null || ID != ""){ 
                                
                                IdentityRole role = await roleManager.FindByIdAsync("40cd773b-3b13-4f3-a2cd-af35e8f1354c"); // REVENDA
                                result = await userManager.AddToRoleAsync(appUser, role.Name);

                                if (result.Succeeded){
                                    
                                    int Revendedor  = Query_.CADASTRARREVENDEDOR(user.Email, user.Password, user.Name);
                                    int Credito     = Query_.CREDITOREVENDAADD(user.Email);
                                                    
                                    if (Revendedor == 1 && Credito == 1)
                                    {

                                        datalogs_.DATALOG(HASHCODE, DATE, "ADMINISTRADOR", @User.Identity.Name, Text.SAVE() + " NOVO USUÁRIO", IPs.Ip());
                                        return Redirect("/Account/Login");
                                    }
                                    else
                                    {
                                        return RedirectToAction(nameof(ErrorWeb)); 
                                    }
                                }else{
                                    return RedirectToAction(nameof(ErrorWeb)); 
                                }

                            }else{
                                return RedirectToAction(nameof(ErrorWeb));   
                            }   
           
                        }else{
                            return RedirectToAction(nameof(ErrorWeb));   
                        }

                    }else{
                        return RedirectToAction(nameof(ErrorWeb));   
                    }
                }else{
                    return RedirectToAction(nameof(ErrorWeb));   
                }

            }catch (Exception e){
                return RedirectToAction(nameof(ErrorWeb));       
            }
            return Redirect("/Account/Login");
        }
            
        





        [Authorize]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(string id)
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE USUARIO/ADMINISTRADORES";
            ViewBag.INFOTEXT  = "Administradores. Esta área permite ao super-admin atualizar usuários ja cadastrados a plataforma!";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 
            ViewBag.Id = id;
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Administrador")]        
        public async Task<IActionResult> Update(string id, string email, string password)
        {

            
            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();
            
            
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {

                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email não pode ser vazio");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Senha não pode ser vazia");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded){
                        
                        int USERS = Query_.SELECTUSERS(user.Email);

                        if(USERS == 1){
                            Query_.APPUSERSUPDATE(email, password);
                        }    

                        datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name, Text.UPDATE() + " USUÁRIO: " + email, IPs.Ip());
                        return RedirectToAction("Index");
                    }else{
                        datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name, Text.UPDATE() + " ERROR USUÁRIO: " + email, IPs.Ip());
                        Errors(result);
                }}
            }
            else
                ModelState.AddModelError("", "Usuários não existe");
            return View(user);
        }


        [Authorize]
        [Authorize(Roles = "Administrador")]        
        private void Errors(IdentityResult result)
        {

            
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Administrador")]                
        public async Task<IActionResult> Delete(string id)
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

            ViewBag.INFOTITLE = "INFORMAÇÕES SOBRE USUARIO/ADMINISTRADORES";
            ViewBag.INFOTEXT  = "Administradores. Esta área permite ao super-admin deletar usuários que ja não precisam mais estar no sistema!";
            ViewBag.INFOBUTTON = "INFORMAÇÕES";
                
            // INFORMAÇÕES POPUP 


            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded){
                    
                    int USERS = Query_.SELECTUSERS(user.Email);
                    if(USERS == 1){
                        
                        Query_.APPUSERSDELETE(user.Email);
                        Query_.DELETEREVENDA(user.Email);
                        Query_.DELETEPARCEIRO(user.Email);
                        Query_.DELETEPLANO(user.Email);
                        
                    }
                    datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name, Text.DELETE() + " USUÁRIO: " + id, IPs.Ip());
                    return RedirectToAction("Index");
                }else{
                    datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name, Text.DELETE() + "ERROR USUÁRIO: " + id, IPs.Ip());
                    Errors(result);
                }    
            }
            else
                ModelState.AddModelError("", "Usuário não existe");
            return View("Index", userManager.Users);
        }

        
        [Authorize]
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