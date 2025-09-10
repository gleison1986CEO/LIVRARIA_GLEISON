using sistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using sistema.Classes;

namespace sistema.Controllers
{
    
    [Authorize]
     [Authorize(Roles = "Administrador, Revenda, Parceiro, Cliente")]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        
        readonly DATEGEN dateTime                = new DATEGEN();
        private readonly  Uuid uid_              = new Uuid();
        private readonly UPDATE update_          = new UPDATE();
        private readonly IpConn IpURI            = new IpConn();
        private readonly Email email_            = new Email();
        private readonly LOCATION Location       = new LOCATION();
        private readonly  Strings Text           = new Strings();
        private readonly  DataLogs datalogs_     = new DataLogs();
    

        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
        {
            userManager   = userMgr;
            signInManager = signinMgr;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {

          
            string HASHCODE    = uid_.UID();
            string DATE        = dateTime.DATE();

            AppUser appUser       =  await userManager.FindByEmailAsync(login.Email);
            string NEWAUTHTOKEN   =  update_.IdentificacaoAhthQuery(login.Email);
            int LOGINVERIFYLIVRE  =  update_.LocalizacaoQueryAtivo(login.Email);
            int LOGINVERIFYLOGADO =  update_.LocalizacaoQueryInaAtivo(login.Email);
            int USEREXIST         =  update_.USEREXISTE(login.Email);
  

            if(USEREXIST == 0){ // USER NOT CONTAIN

                int NEWUSERIDENTY  =  update_.ConfirmationAccount(login.Email);
                int NEWTOKENACESS  =  update_.RENEWTOKENACESS(login.Email);
                string GETAUTHTOKEN=  update_.IdentificacaoAhthQuery(login.Email);
                

                if(NEWUSERIDENTY  == 1 && NEWTOKENACESS ==1){

                    
                    ViewBag.ErrorLogin = "Usuário: " + login.Email + "\nOlá! acabamos de confirmar sua conta, Seja Bem Vindo!\nEnviamos um email de confirmação, obrigado!" ;
                    email_. SendConfirmation(login.Email, Convert.ToString(GETAUTHTOKEN.Replace(",","").Replace(" ","")));
                    datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() +" AUTENTICAÇÃO", Location.GetLocalIPAddress());    

                }else{

                    ViewBag.ErrorLogin = "Usuário: " + login.Email + "\nTivemos um problema ao ativar seu usuário\nPorfavor Contate o surporte!" ;
                    datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() +" ERROR AUTENTICAÇÃO", Location.GetLocalIPAddress()); 
                    email_. SendError(login.Email);
                }

                
                
            }



            if(login.auth2fa == "" || login.auth2fa == null){

                login.auth2fa = "Digite o email e senha do usuário";

            }

            if(appUser == null){
                

                ViewBag.ErrorLogin = "Este email:" + login.Email + " não existe!." ;

            }



            
            if(
            Convert.ToString(login.auth2fa.Replace(",","").Replace(" ","")) != Convert.ToString(NEWAUTHTOKEN.Replace(",","").Replace(" ","")) || 
            Convert.ToString(login.auth2fa.Replace(",","").Replace(" ","")) == null){  // TOKEN NULL OR != SEND NEW TOKEN
                
                update_.RENEWTOKENACESS(login.Email);
                string NEWAUTHTOKENS  =  update_.IdentificacaoAhthQuery(login.Email);

                if(LOGINVERIFYLIVRE == 1){
                        
                        ViewBag.ErrorLogin = "Um novo 'Token Autenticador' foi enviado para o seu Email. \n\nPor Favor, copie o código enviado para o email, cole no campo necessário para entrar na plataforma!" ;
                        email_. Send(login.Email, Convert.ToString(NEWAUTHTOKENS.Replace(",","").Replace(" ","")));
                        datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() + Convert.ToString(NEWAUTHTOKENS.Replace(",","").Replace(" ","")), Location.GetLocalIPAddress()); 
                        

                }

                if(LOGINVERIFYLOGADO == 1){

                        ViewBag.ErrorLogin = "Este usuário ja está logado na plataforma.\nPorfavor deslogue o usuário para continuar" ;
                        email_. SendNotificationAtivo(login.Email, Convert.ToString(NEWAUTHTOKEN.Replace(",","").Replace(" ","")));
                        datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() + " USUÁRIO LOGADO", Location.GetLocalIPAddress()); 
                   

                }

            }else if(LOGINVERIFYLOGADO == 1){
                        
                        ViewBag.ErrorLogin = "Este usuário ja está logado na plataforma.\nPorfavor deslogue o usuário para continuar" ;
                        email_. SendNotificationAtivo(login.Email, Convert.ToString(NEWAUTHTOKEN.Replace(",","").Replace(" ","")));
                        datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() + " USUÁRIO LOGADO", Location.GetLocalIPAddress()); 
                  

            }else if(LOGINVERIFYLIVRE == 1){
                        if (ModelState.IsValid)
                        {



                                if (appUser != null)  
                                {


                                    
                                    await signInManager.SignOutAsync();
                                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, login.Remember, false);

                                    /// VERIFY USER AND IS EXIST
                                
                                    if(result.Succeeded)
                                    {
                                        int VERIFY = update_.LocalizacaoQuery(login.Email); 
                                        
                                        if(VERIFY == 1){
                                            
                                            /// VERIFY USER AND IS EXIST AND INACTIVE LOGIN
                                            int LOGINVERIFY = update_.LocalizacaoQueryAtivo(login.Email);


                                            if(LOGINVERIFY == 1){

                                                int LOGINACTIVE = update_.LocalizacaoUpdateActiveLogin(login.Email);

                                                if(LOGINACTIVE == 1){

                                                    update_.ConfirmacaoConta(login.Email, @User.Identity.Name);
                                                    email_. SendConfirmationLogin(login.Email);
                                                    
                                                    return Redirect("/Home/Grafico");

                                                    

                                                }else if(LOGINACTIVE == 1){

                                                    update_.ConfirmacaoConta(login.Email, @User.Identity.Name);
                                                    email_. SendConfirmationLogin(login.Email);

                                                    return Redirect("/Home/Grafico");
                                                

                                                }
                                                

                                                


                                            }else if(LOGINVERIFY == 0){
                                                /// IF ALREDY LOGIN REDRECT TO ANOTHER PAGES HOME OUT
                                                ViewBag.ErrorLogin = "Este usuário ja está logado na plataforma.\nPorfavor deslogue o usuário para continuar" ;
                                                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() + " USUÁRIO LOGADO", Location.GetLocalIPAddress()); 
                                            }
                                            

                                        }else{
                                            
                                            /// IF USER NOT EXIST ON DATABASE IDENTIFICATION MAKE INSERT
                                            update_.Localizacao(login.Email, @User.Identity.Name);
                                            /// VERIFY USER AND IS EXIST AND INACTIVE LOGIN
                                            int LOGINVERIFY = update_.LocalizacaoQueryAtivo(login.Email);

                                            
                                            if(LOGINVERIFY == 1){
                                                int LOGINACTIVE = update_.LocalizacaoUpdateActiveLogin(login.Email);


                                                if(LOGINACTIVE == 1){
                                                    
                                                    update_.ConfirmacaoConta(login.Email, @User.Identity.Name);
                                                    email_. SendConfirmationLogin(login.Email);
                                                    
                                                    return Redirect("/Home/Grafico");

                                                    

                                                }else if(LOGINACTIVE == 1){

                                                    update_.ConfirmacaoConta(login.Email, @User.Identity.Name);
                                                    email_. SendConfirmationLogin(login.Email);

                                                    return Redirect("/Home/Grafico");
                                                

                                                }
                                                

                                                


                                            }else if(LOGINVERIFY == 0){
                                                /// IF ALREDY LOGIN REDRECT TO ANOTHER PAGES HOME OUT
                                                ViewBag.ErrorLogin = "Este usuário ja está logado na plataforma.\nPorfavor deslogue o usuário para continuar" ;
                                                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() + " USUÁRIO LOGADO", Location.GetLocalIPAddress()); 
                                            }

                                        }
            

                                    }else{

                                        ViewBag.ErrorLogin = "* Sua senha está incorreta!\nPorfavor tente novamente!." ;
                                        datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() + " ERRO TENTATIVA DE SENHA", Location.GetLocalIPAddress()); 
                                        
                                    }


                                    if (result.RequiresTwoFactor)
                                    {
                                        return RedirectToAction("LoginTwoStep", new { appUser.Email, login.ReturnUrl });
                                    }

                                    bool emailStatus = await userManager.IsEmailConfirmedAsync(appUser);
                                    if (emailStatus == false)
                                    {
                                        ModelState.AddModelError(nameof(login.Email), "Email não autorizado, porfavor autorize primeiro");
                                    }

                                    if (result.IsLockedOut)
                                        ModelState.AddModelError("", "Sua conta foi bloqueada porfavor tente novamente mais tarde...");
                                }else{
                                    ViewBag.ErrorLogin = "O seu IP na esta na lista de acessos!\nPorfavor Contate o suporte para cadastrar seu IP!";
                                }
                            
                            ModelState.AddModelError(nameof(login.Email), "Email ou senha Inválidos");
                        }else{
                            ViewBag.ErrorLogin = "* Erro ao tentar logar, porfavor confira seus dados e tente novamente!" ;
                            datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() + " ERRO TENTATIVA DE SENHA", Location.GetLocalIPAddress()); 
                        }
            
            }return View();            
        }
        
        public async Task<IActionResult> LogoutInstance()
        {
                /// REDIRECT USER TO LOGOUT
                await signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
                string HASHCODE    = uid_.UID();
                string DATE        = dateTime.DATE();

                AppUser appUser = await userManager.FindByNameAsync(@User.Identity.Name);
                update_.LocalizacaoUpdateLogout(appUser.Email, @User.Identity.Name);
                
                /// REDIRECT USER TO LOGOUT
                await signInManager.SignOutAsync();
                datalogs_.DATALOG(HASHCODE,DATE,"ADMINISTRADOR",@User.Identity.Name,Text.SAVE() + " SAINDO DA PLATAFORMA", Location.GetLocalIPAddress()); 
                return RedirectToAction("Index", "Home");
        }

        

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            if (result.Succeeded)
                return View(userInfo);
            else
            {
                AppUser user = new AppUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };

                IdentityResult identResult = await userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        return View(userInfo);
                    }
                }
                return AccessDenied();
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(string email, string returnUrl)
        {
            var user = await userManager.FindByEmailAsync(email);

            var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailTwoFactorCode(user.Email, token);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(TwoFactor twoFactor, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(twoFactor.TwoFactorCode);
            }

            var result = await signInManager.TwoFactorSignInAsync("Email", twoFactor.TwoFactorCode, false, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError("", "Login ou Senha inválidos, porfavor tente novamente mais tarde!!");
                return View();
            }
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            if (!ModelState.IsValid)
                return View(email);

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

            if (emailResponse)
                return RedirectToAction("ForgotPasswordConfirmation");
            else
            {
                // log email failed 
            }
            return View(email);
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var user = await userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                RedirectToAction("ResetPasswordConfirmation");

            var resetPassResult = await userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View();
            }

            return RedirectToAction("ResetPasswordConfirmation");
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
