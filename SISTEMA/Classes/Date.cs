
namespace sistema.Classes
{
    public class DATEGEN
    {
        public String  DATE()

            {  
                   

                var date = DateTime.Now;

                String DATEIME = date.ToString();


                return DATEIME ;
                                


            }

        public static implicit operator string(DATEGEN v)
        {
            throw new NotImplementedException();
        }
    }
}

    

