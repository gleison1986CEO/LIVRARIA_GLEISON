using System.Diagnostics;

namespace sistema.Classes
{

    
    public class LoginAsync
    {


        private readonly DATEGEN date  = new DATEGEN();
        private readonly UPDATE update = new  UPDATE();
        
        public String Logout(String? cpf)
        {   
            // SE AO PASSAR PAGINA OU EXECUAR FUNCAO  AD ATA SAIDA ESTIVER ACIMA
           
            var dateini          = Convert.ToDateTime(update.IdentificacaoCpfQuery(cpf).Replace(",",""));
            DateTime userTime30  = dateini.AddMinutes(590);
            //DateTime userTime  = dateini - new TimeSpan(0,30,0);
            var timeNow          = Convert.ToDateTime(date.DATE());

            if(timeNow >= userTime30){

                // EXECUTA LOGOUT
                            
                var Logout =  "LOGOUT";
                
                return Logout;

            }
            if(timeNow < userTime30){

                // MOSTRA TEMPO
                var dataFinal =  Convert.ToString(userTime30);
                
                return dataFinal;
            }
             var dataInicial =  Convert.ToString(dateini);
             return dataInicial;

            
        }


        

    }
}
