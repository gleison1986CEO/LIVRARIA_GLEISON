namespace sistema.Classes
{
    public class Uuid
    {

      public string UID()
        {   
            var UUIDs  = string.Format("{0}_{1:N}", "KY_PLAYER_TV_STREAMING", Guid.NewGuid());
            Console.WriteLine(UUIDs);
            return UUIDs;
        }


        public string TOKEN()
        {   
            var UUIDs  = string.Format("{0}_{1:N}", "HASH_KY_PLAYER_TV_STREAMING", Guid.NewGuid());
            Console.WriteLine(UUIDs);
            return UUIDs;
        }

        
        public string CSV()
        {   
            var UUIDs  = string.Format("{0}_{1:N}", "CSV_TOKEN_KY_PLAYER_TV_STREAMING", Guid.NewGuid());
            Console.WriteLine(UUIDs);
            return UUIDs;
        }


        public string AUTH2FA()
        {   
            var UUIDs  = string.Format("{0}_{1:N}", "AUTH_KY_PLAYER_TV_STREAMING", Guid.NewGuid());
            Console.WriteLine(UUIDs);
            return UUIDs;
        }        


        public string SECRET()
        {   
            var UUIDs  = string.Format("{0}_{1:N}", "SECRET_KY_PLAYER_TV_STREAMING", Guid.NewGuid());
            Console.WriteLine(UUIDs);
            return UUIDs;
        }        
    }
}
