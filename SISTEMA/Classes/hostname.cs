using Microsoft.Data.SqlClient;
using System.Data;

namespace sistema.Classes
{
    public class Hostnames
    {
    
        public int HOSTNAME( String? hostname)

            {
            
                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();
                int Ativo = 0; 

                var sql   = "INSERT INTO hostnamelogin (hostname, Ativo) VALUES (@hostname, @Ativo) ";
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@hostname", SqlDbType.NVarChar).Value  = hostname;
                                    command.Parameters.Add("@Ativo", SqlDbType.NVarChar).Value  = Ativo;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }


      public int HOSTNAMESELECT(String? hostname)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();

                    var sql   = "SELECT COUNT(hostname) FROM hostnamelogin WHERE hostname = '" + hostname + "'";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var result = (int)cmd.ExecuteScalar();
                        return result;
                    }
                                


            }



            public string HOSTNAMESELECTNAMES(String? hostname)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from hostnamelogin WHERE hostname = '" + hostname + "'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();

                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                            if (sqlDataReader.HasRows)
                            {
                                while (sqlDataReader.Read())
                                {
                                    string data = sqlDataReader["codigo"].ToString();
                                    Co.Append(data + ",");
                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }




      public int HOSTNAMEDELETE(int? codigo)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "DELETE FROM hostnamelogin WHERE codigo = '" + codigo + "'";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return  cmd.ExecuteNonQuery();
                    }
                                


            }







}}

    
