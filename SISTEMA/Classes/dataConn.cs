namespace sistema.Classes
{
    public class DataConn
    {
        public String dataConnServer()
        {
            ///REAL SERVER
            var connetionString = "Data Source=1.tcp.sa.ngrok.io,20889;Initial Catalog=darkgames_2025 ;User Id=sa;password=0908Gle@; Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=False;"; 
            return connetionString;
        }

         public String dataConnStringlocal()
        {
            ///LOCAL SERVER
            var connetionString = "Server=1.tcp.sa.ngrok.io,20889;Initial Catalog=darkgames_2025;User Id=sa;password=0908Gle@; Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=False;"; 
            return connetionString;
        }

    }
}
