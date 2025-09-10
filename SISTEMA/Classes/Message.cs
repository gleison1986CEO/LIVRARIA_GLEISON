namespace sistema.Classes
{
    public class Whatsapp
    {
        public String whatsApp()
        {
            ///WHATSAPP
            var whatsString =  $"Olá! , Encontrei seu contato como fonoaudiologo(a) credenciado(a) PAC Online. Tenho interesse e gostaria de mais informações, por favor.";
            return whatsString;
        }

        public String whatsAppApi()
        {
            ///WHATSAPP
            var whatsString =  $"https://api.whatsapp.com/send/?phone=55";
            return whatsString;
        }



    }
}
