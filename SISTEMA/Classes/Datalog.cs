using Microsoft.Data.SqlClient;
using System.Data;

namespace sistema.Classes
{
    public class DataLogs
    {
        
        private readonly Connections connections_ = new Connections();
        private readonly DATEGEN dateTime         = new DATEGEN();
        private readonly UPDATE update            = new UPDATE();
    
        public int DATALOG(String? hashcode, String? data_hora, String? cargo, String? login, String? executou, String? ip)

            {
                
                string email = update.DatalogSelectUser(login);
                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();
                int Ativo = 1; 

                var sql   = "INSERT INTO datalog (hashcode, data_hora, datahora, cargo, login, executou, ip) VALUES (@hashcode, @data_hora, @datahora, @cargo, @login, @executou, @ip) ";
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@hashcode", SqlDbType.NVarChar).Value  = hashcode;
                                    command.Parameters.Add("@data_hora", SqlDbType.NVarChar).Value = data_hora;
                                    command.Parameters.Add("@datahora", SqlDbType.NVarChar).Value  = Convert.ToString(data_hora);
                                    command.Parameters.Add("@cargo", SqlDbType.NVarChar).Value     = cargo;
                                    command.Parameters.Add("@login", SqlDbType.NVarChar).Value     = "CPF: " + login + ", EMAIL: " + email.Replace(",","") + ", USERNAME: " + email.Replace(",","").Split('@')[0] + " ";
                                    command.Parameters.Add("@executou", SqlDbType.NVarChar).Value  = executou;
                                    command.Parameters.Add("@ip", SqlDbType.NVarChar).Value        = ip;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }





}}

    
