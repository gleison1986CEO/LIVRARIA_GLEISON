namespace sistema.Classes
{
    public class Connections
    {
        private readonly  DataConn dataConn_ = new DataConn();
        public String Connection()
        {
            var connetionString = dataConn_.dataConnServer();
            return connetionString;
        }

    }
}

