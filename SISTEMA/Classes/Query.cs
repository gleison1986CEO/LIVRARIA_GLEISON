using Microsoft.Data.SqlClient;
using sistema.Classes;
using System.Data;

    
namespace sistema.Classes
{
    
    public class UPDATE
    {
        
        private readonly Connections connections_ = new Connections();
        private readonly LOCATION location        = new LOCATION();
        private readonly Uuid uuid                = new Uuid();
        private readonly DATEGEN date             = new DATEGEN();


        public int Relevante(String? codigo_laudo, string? relevancia)

            {
            
                Connections connections_  = new Connections();
                
                var connetionString       = connections_.Connection();


                var sql   = "UPDATE Laudos SET relevancia = @relevancia WHERE codigo_laudo = @codigo_laudo";

                 using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@relevancia", SqlDbType.NVarChar).Value = relevancia;
                                    command.Parameters.Add("@codigo_laudo", SqlDbType.NVarChar).Value = codigo_laudo;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }


        //, UPDATE MAPA LAT LONG
        //, VERIFICAR MAPA MAPA LAT LONG
        
        
        public int UPDATEMAPA(string? foto, string? aeronave)

        {

            Connections connections_ = new Connections();

            var connetionString = connections_.Connection();


            var sql = "UPDATE Mapa SET foto = @foto WHERE aeronave = @aeronave";

            using (var connection = new SqlConnection(connetionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@foto", SqlDbType.NVarChar).Value = foto;
                    command.Parameters.Add("@aeronave", SqlDbType.NVarChar).Value = aeronave;
                    connection.Open();
                    int QueryData = command.ExecuteNonQuery();
                    return QueryData;

                }
            }
        }



     
        public int UPDATEMAPALOCATION(string? lat,string? lng, string? aeronave)

        {

            Connections connections_ = new Connections();

            var connetionString = connections_.Connection();


            var sql = "UPDATE Mapa SET latitude = @latitude, longitude = @longitude  WHERE aeronave = @aeronave";

            using (var connection = new SqlConnection(connetionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@latitude", SqlDbType.NVarChar).Value  = lat;
                    command.Parameters.Add("@longitude", SqlDbType.NVarChar).Value = lng;
                    command.Parameters.Add("@aeronave", SqlDbType.NVarChar).Value  = aeronave;
                    connection.Open();
                    int QueryData = command.ExecuteNonQuery();
                    return QueryData;

                }
            }
        }


        public int INSERTMAPA(string? foto, string? aeronave)

        {

            Connections connections_ = new Connections();

            var connetionString = connections_.Connection();


            int Ativo = 1;
            var sql = "INSERT INTO Mapa (hashcode,foto, aeronave, latitude, longitude, Ativo) VALUES (@hashcode, @foto, @aeronave, @latitude, @longitude, @Ativo) ";

            using (var connection = new SqlConnection(connetionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {

                    command.Parameters.Add("@hashcode", SqlDbType.NVarChar).Value = uuid.TOKEN().ToString();
                    command.Parameters.Add("@latitude", SqlDbType.NVarChar).Value = "0";
                    command.Parameters.Add("@longitude", SqlDbType.NVarChar).Value = "0";
                    command.Parameters.Add("@foto", SqlDbType.NVarChar).Value = foto;
                    command.Parameters.Add("@aeronave", SqlDbType.NVarChar).Value = aeronave;
                    command.Parameters.Add("@ativo", SqlDbType.NVarChar).Value = Ativo;
                    connection.Open();
                    int QueryData = command.ExecuteNonQuery();
                    return QueryData;

                }
            }
        }


            public int Localizacao(string? email, string? cpf)

            {
                
    
                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();

                int Ativo = 0; 
                var sql   = "INSERT INTO Identificacao (email,ip, secret, auth2fa, token, cpf, data, Ativo) VALUES (@email, @ip, @secret, @auth2fa, @token, @cpf, @data, @Ativo) ";
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value       = email;
                                    command.Parameters.Add("@ip", SqlDbType.NVarChar).Value          = location.GetLocalIPAddress().ToString();
                                    command.Parameters.Add("@secret", SqlDbType.NVarChar).Value      = uuid.SECRET().ToString();
                                    command.Parameters.Add("@auth2fa", SqlDbType.NVarChar).Value     = uuid.AUTH2FA().ToString();
                                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value       = uuid.TOKEN().ToString();
                                    command.Parameters.Add("@cpf", SqlDbType.NVarChar).Value         = cpf;
                                    command.Parameters.Add("@data", SqlDbType.NVarChar).Value        = date.DATE();
                                    command.Parameters.Add("@Ativo", SqlDbType.NVarChar).Value       = Ativo;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }

            




            public int ConfirmationAccount(string? email)

            {
    
                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();

                int Ativo = 0; 
                var sql   = "INSERT INTO Identificacao (email,ip, secret, auth2fa, token, cpf, data, Ativo) VALUES (@email, @ip, @secret, @auth2fa, @token, @cpf, @data, @Ativo) ";
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value       = email;
                                    command.Parameters.Add("@ip", SqlDbType.NVarChar).Value          = location.GetLocalIPAddress().ToString();
                                    command.Parameters.Add("@secret", SqlDbType.NVarChar).Value      = uuid.SECRET().ToString();
                                    command.Parameters.Add("@auth2fa", SqlDbType.NVarChar).Value     = uuid.AUTH2FA().ToString();
                                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value       = uuid.TOKEN().ToString();
                                    command.Parameters.Add("@cpf", SqlDbType.NVarChar).Value         = "null";
                                    command.Parameters.Add("@data", SqlDbType.NVarChar).Value        = date.DATE();
                                    command.Parameters.Add("@Ativo", SqlDbType.NVarChar).Value       = Ativo;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }

            

 
            public int LocalizacaoUpdateLogin(string? email, string? cpf)

            {
            

                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();


                var sql   = "UPDATE Identificacao SET secret = @secret, auth2fa = @auth2fa, token = @token, cpf = @cpf, data = @data, Ativo = @Ativo  WHERE email = @email";
                 
                int Ativo = 1; 
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value       = email;
                                    command.Parameters.Add("@ip", SqlDbType.NVarChar).Value          = location.GetLocalIPAddress().ToString();
                                    command.Parameters.Add("@secret", SqlDbType.NVarChar).Value      = uuid.SECRET().ToString();
                                    command.Parameters.Add("@auth2fa", SqlDbType.NVarChar).Value     = uuid.AUTH2FA().ToString();
                                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value       = uuid.TOKEN().ToString();
                                    command.Parameters.Add("@cpf", SqlDbType.NVarChar).Value         = cpf;
                                    command.Parameters.Add("@data", SqlDbType.NVarChar).Value        = date.DATE();
                                    command.Parameters.Add("@Ativo", SqlDbType.NVarChar).Value       = Ativo;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }



          

 
            public int LocalizacaoUpdateActiveLogin(string? email)

            {
            

                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();


                var sql   = "UPDATE Identificacao SET secret = @secret, auth2fa = @auth2fa, token = @token,  data = @data, Ativo = @Ativo  WHERE email = @email";
                 
                int Ativo = 1; 
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value       = email;
                                    command.Parameters.Add("@ip", SqlDbType.NVarChar).Value          = location.GetLocalIPAddress().ToString();
                                    command.Parameters.Add("@secret", SqlDbType.NVarChar).Value      = uuid.SECRET().ToString();
                                    command.Parameters.Add("@auth2fa", SqlDbType.NVarChar).Value     = uuid.AUTH2FA().ToString();
                                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value       = uuid.TOKEN().ToString();
                                    command.Parameters.Add("@data", SqlDbType.NVarChar).Value        = date.DATE();
                                    command.Parameters.Add("@Ativo", SqlDbType.NVarChar).Value       = Ativo;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }



       

            public int RENEWTOKENACESS(string? email)

            {
            

                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();


                var sql   = "UPDATE Identificacao SET secret = @secret, auth2fa = @auth2fa, token = @token, data = @data  WHERE email = @email";
                 
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value       = email;
                                    command.Parameters.Add("@ip", SqlDbType.NVarChar).Value          = location.GetLocalIPAddress().ToString();
                                    command.Parameters.Add("@secret", SqlDbType.NVarChar).Value      = uuid.SECRET().ToString();
                                    command.Parameters.Add("@auth2fa", SqlDbType.NVarChar).Value     = uuid.AUTH2FA().ToString();
                                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value       = uuid.TOKEN().ToString();
                                    command.Parameters.Add("@data", SqlDbType.NVarChar).Value        = date.DATE();
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }









 
            public int UpdateTokenConfirmation(string? email)

            {
            

                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();


                var sql   = "UPDATE Identificacao SET secret = @secret, auth2fa = @auth2fa, token = @token, data = @data, Ativo = @Ativo  WHERE email = @email";
                 
                int Ativo = 0; 
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                                    command.Parameters.Add("@ip", SqlDbType.NVarChar).Value    = location.GetLocalIPAddress().ToString();
                                    command.Parameters.Add("@secret", SqlDbType.NVarChar).Value      = uuid.SECRET().ToString();
                                    command.Parameters.Add("@auth2fa", SqlDbType.NVarChar).Value     = uuid.AUTH2FA().ToString();
                                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value       = uuid.TOKEN().ToString();
                                    command.Parameters.Add("@data", SqlDbType.NVarChar).Value        = date.DATE();
                                    command.Parameters.Add("@Ativo", SqlDbType.NVarChar).Value = Ativo;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }





            public int LocalizacaoUpdateLogout(string? email, string? cpf)

            {

            
                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();


                var sql   = "UPDATE Identificacao SET secret = @secret, auth2fa = @auth2fa, token = @token,  cpf = @cpf, data = @data, Ativo = @Ativo WHERE email = @email";
                 
                int Ativo = 0; 
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value       = email;
                                    command.Parameters.Add("@ip", SqlDbType.NVarChar).Value          = location.GetLocalIPAddress().ToString();
                                    command.Parameters.Add("@secret", SqlDbType.NVarChar).Value      = uuid.SECRET().ToString();
                                    command.Parameters.Add("@auth2fa", SqlDbType.NVarChar).Value     = uuid.AUTH2FA().ToString();
                                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value       = uuid.TOKEN().ToString();
                                    command.Parameters.Add("@cpf", SqlDbType.NVarChar).Value         = cpf;
                                    command.Parameters.Add("@data", SqlDbType.NVarChar).Value        = date.DATE();
                                    command.Parameters.Add("@Ativo", SqlDbType.NVarChar).Value       = Ativo;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }





            public int ConfirmacaoConta(string? email, string? cpf)

            {


                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();


                var sql   = "UPDATE Identificacao SET secret = @secret, auth2fa = @auth2fa, token = @token, cpf = @cpf, data = @data, Ativo = @Ativo WHERE email = @email";
                 
                int Ativo = 1; 
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value       = email;
                                    command.Parameters.Add("@ip", SqlDbType.NVarChar).Value          = location.GetLocalIPAddress().ToString();
                                    command.Parameters.Add("@secret", SqlDbType.NVarChar).Value      = uuid.SECRET().ToString();
                                    command.Parameters.Add("@auth2fa", SqlDbType.NVarChar).Value     = uuid.AUTH2FA().ToString();
                                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value       = uuid.TOKEN().ToString();
                                    command.Parameters.Add("@data", SqlDbType.NVarChar).Value        = date.DATE();
                                    command.Parameters.Add("@cpf", SqlDbType.NVarChar).Value         = cpf;
                                    command.Parameters.Add("@Ativo", SqlDbType.NVarChar).Value       = Ativo;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }



            public int LocalizacaoQuery(string? email)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();

                    var sql   = "SELECT COUNT(email) FROM Identificacao WHERE email = '" + email + "'";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var result = (int)cmd.ExecuteScalar();
                        return result;
                    }
                                


            }

           public int LocalizacaoQueryAtivo(string? email)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();

                    var sql   = "SELECT COUNT(email) FROM Identificacao WHERE Ativo = 0 and email = '" + email + "'";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var result = (int)cmd.ExecuteScalar();
                        return result;
                    }
                                


            }



            public int USEREXISTE(string? email)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();

                    var sql   = "SELECT COUNT(email) FROM Identificacao WHERE email = '" + email + "'";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var result = (int)cmd.ExecuteScalar();
                        return result;
                    }
                                


            }

            public int LocalizacaoQueryInaAtivo(string? email)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();

                    var sql   = "SELECT COUNT(email) FROM Identificacao WHERE Ativo = 1 and email = '" + email + "'";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var result = (int)cmd.ExecuteScalar();
                        return result;
                    }
                                


            }



            public string IpsQuery()
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from Ips"; 
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
                                    string data = sqlDataReader["ip"].ToString();
                                    Co.Append(data + ",");

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }





            public string GETDATAUSER(string? Nome)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from AspNetUsers where UserName = '" + Nome + "'";
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
                                    string data = sqlDataReader["Email"].ToString();
                                    Co.Append(data);

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }


        public string SOCIO(string? Nome)
        {

            Connections connections_ = new Connections();
            var connetionString = connections_.Connection();
            var sql = "select * from Socio where aeronave = '" + Nome + "'";
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(sql, conn))
                {

                    int OUT = 0;
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                    if (sqlDataReader.HasRows)
                    {

                        while (sqlDataReader.Read())
                        {

                            string data = sqlDataReader["cota"].ToString();
                            OUT += Convert.ToInt32(data);

                        }


                    }


                    return Convert.ToString(OUT);


                }



            }
        }






            public string SOCIOUSERS(string? Nome)
            {
                  
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from Socio where nome = '" +  Nome + "'"; 
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
                                    string data = sqlDataReader["cota"].ToString();
                                    Co.Append(data);

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }



        public string AERONAVE(string? Nome)
        {



            Connections connections_ = new Connections();
            var connetionString = connections_.Connection();
            var sql = "SELECT  * from Aeronave INNER JOIN Socio ON (Aeronave.matricula = Socio.aeronave) INNER JOIN Despesa ON (Aeronave.matricula = Despesa.agente) INNER JOIN Voo ON (Aeronave.matricula = Voo.aeronave) where matricula = '" + Nome + "'";
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
                            string matricula = sqlDataReader["matricula"].ToString();
                            string fabricante = sqlDataReader["fabricante"].ToString();
                            string modelo = sqlDataReader["modelo"].ToString();
                            string serie = sqlDataReader["serie"].ToString();
                            string proprietario = sqlDataReader["proprietario"].ToString();
                            string operador = sqlDataReader["operador"].ToString();
                            string info = sqlDataReader["info"].ToString();
                            string foto = sqlDataReader["foto"].ToString();
                            string missao = sqlDataReader["missao"].ToString();
                            string aeronave = sqlDataReader["aeronave"].ToString();
                            string numero = sqlDataReader["numero"].ToString();
                            string data = sqlDataReader["data"].ToString();
                            string origem = sqlDataReader["origem"].ToString();
                            string destino = sqlDataReader["destino"].ToString();
                            string acionamento = sqlDataReader["acionamento"].ToString();
                            string decolagem = sqlDataReader["decolagem"].ToString();
                            string pouso = sqlDataReader["pouso"].ToString();
                            string corte = sqlDataReader["corte"].ToString();
                            string diurno = sqlDataReader["diurno"].ToString();
                            string noturno = sqlDataReader["noturno"].ToString();
                            string vfr = sqlDataReader["vfr"].ToString();
                            string ifr_real = sqlDataReader["ifr_real"].ToString();
                            string ifr_capota = sqlDataReader["ifr_capota"].ToString();
                            string natureza = sqlDataReader["natureza"].ToString();
                            string pousos = sqlDataReader["pousos"].ToString();
                            string ciclos = sqlDataReader["ciclos"].ToString();
                            string distancia = sqlDataReader["distancia"].ToString();
                            string piloto = sqlDataReader["piloto"].ToString();
                            string co_piloto = sqlDataReader["co_piloto"].ToString();
                            string piloto_reserva = sqlDataReader["piloto_reserva"].ToString();
                            string quantidade = sqlDataReader["quantidade"].ToString();
                            string medida = sqlDataReader["medida"].ToString();
                            string carga = sqlDataReader["carga"].ToString();
                            string unidade = sqlDataReader["unidade"].ToString();
                            string pob = sqlDataReader["pob"].ToString();
                            string pagante = sqlDataReader["pagante"].ToString();
                            string total_noturno_diurno = sqlDataReader["total_noturno_diurno"].ToString();
                            string total_vfr = sqlDataReader["total_vfr"].ToString();
                            string horas_voo = sqlDataReader["horas_voo"].ToString();
                            string nome = sqlDataReader["nome"].ToString();
                            string email = sqlDataReader["email"].ToString();
                            string agente = sqlDataReader["agente"].ToString();
                            string tipo = sqlDataReader["tipo"].ToString();
                            string classificacao = sqlDataReader["classificacao"].ToString();
                            string servico = sqlDataReader["servico"].ToString();
                            string moeda = sqlDataReader["moeda"].ToString();
                            string taxa = sqlDataReader["taxa"].ToString();
                            string valor = sqlDataReader["valor"].ToString();
                            string fornecedor = sqlDataReader["fornecedor"].ToString();
                            string nota = sqlDataReader["nota"].ToString();
                            string pagamento = sqlDataReader["pagamento"].ToString();
                            string local = sqlDataReader["local"].ToString();
                            string baixa = sqlDataReader["baixa"].ToString();
                            string cotas = sqlDataReader["cota"].ToString();


                            Co.Append(
                            matricula + ","
                            + fabricante + ","
                            + modelo + ","
                            + serie + ","
                            + proprietario + ","
                            + operador + ","
                            + info + ","
                            + foto + ","
                            + missao + ","
                            + aeronave + ","
                            + numero + ","
                            + data + ","
                            + origem + ","
                            + destino + ","
                            + acionamento + ","
                            + decolagem + ","
                            + pouso + ","
                            + corte + ","
                            + diurno + ","
                            + noturno + ","
                            + vfr + ","
                            + ifr_real + ","
                            + ifr_capota + ","
                            + natureza + ","
                            + pousos + ","
                            + ciclos + ","
                            + distancia + ","
                            + piloto + ","
                            + co_piloto + ","
                            + piloto_reserva + ","
                            + quantidade + ","
                            + medida + ","
                            + carga + ","
                            + unidade + ","
                            + pob + ","
                            + pagante + ","
                            + total_noturno_diurno + ","
                            + total_vfr + ","
                            + horas_voo + ","
                            + nome + ","
                            + email + ","
                            + agente + ","
                            + tipo + ","
                            + classificacao + ","
                            + servico + ","
                            + moeda + ","
                            + taxa + ","
                            + valor + ","
                            + fornecedor + ","
                            + nota + ","
                            + pagamento + ","
                            + local + ","
                            + baixa + ","
                            + cotas + ","


                            );



                        }
                    }

                    return Convert.ToString(Co);
                }



            }
        }



            public string WORDLIST(string? mensagem)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from Wordlist"; 
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
                                    string data = sqlDataReader["palavra"].ToString();
                                    Co.Append(data + ",");

                                }
                            }

                                string[] words    = Convert.ToString(Co).Split(",");
                                foreach (var word in words)
                                {
                                    string telegramas = mensagem;
                                    string wordlist   = Convert.ToString(word).Replace(" ","").Replace(",","");
                                    bool contain      = telegramas.Contains(wordlist);
            
                                        if(contain == true){
                                            
                                          return "Y";

                                        }else{

                                          return "N"; 

                                        }
                                                    


                                    
                                }
                                return Convert.ToString(Co);
                           
                        }
                        

                     
                    }
            }









            public int ROLETELEGRAMA(string? username, string? role_username)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();

                    var sql   = "SELECT COUNT(username) FROM Role_telegrama WHERE username = '" + username + "' and role_username = '" + role_username + "' and Ativo = 1";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var result = (int)cmd.ExecuteScalar();
                        return result;
                    }
                                


            }






            public int ROLEGRUPO(string? username, string? role_grupo)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();

                    var sql   = "SELECT COUNT(username) FROM Role_grupo WHERE username = '" + username + "' and grupo = '" + role_grupo + "' and Ativo = 1";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var result = (int)cmd.ExecuteScalar();
                        return result;
                    }
                                


            }











            public int ROLEEVENTO(string? username, string? role_evento)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();

                    var sql   = "SELECT COUNT(username) FROM Role_evento WHERE username = '" + username + "' and evento = '" + role_evento + "' and Ativo = 1";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var result = (int)cmd.ExecuteScalar();
                        return result;
                    }
                                


            }





            public int ROLEPERFIL(string? username, string? role_username)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();

                    var sql   = "SELECT COUNT(username) FROM Role_perfil WHERE username = '" + username + "' and role_username = '" + role_username + "' and Ativo = 1";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var result = (int)cmd.ExecuteScalar();
                        return result;
                    }
                                


            }




            public string DatalogSelectUser(String? cpf)
            {
                    
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from AspNetUsers WHERE UserName = '" + cpf + "'";
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
                                    string data = sqlDataReader["Email"].ToString();
                                    Co.Append(data + ",");

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }



            public string IdentificacaoSecretQuery(String? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from Identificacao WHERE email = '" + email + "'";
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
                                    string data = sqlDataReader["secret"].ToString();
                                    Co.Append(data + ",");

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }



            public string IdentificacaoDeleteQuery(String? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "delete from identificacao WHERE email = '" + email + "'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();

                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                            
                            return "Ok";
                        }
                        

                     
                    }
            }

            public string IdentificacaoCpfQuery(String? cpf)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from Identificacao WHERE cpf = '" + cpf + "'";
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
                                    string data = sqlDataReader["data"].ToString();
                                    Co.Append(data + ",");

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }            

            public string IdentificacaoTokenQuery(String? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from Identificacao WHERE email = '" + email + "'";
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
                                    string data = sqlDataReader["token"].ToString();
                                    Co.Append(data + ",");

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }



            public string IdentificacaoAhthQuery(String? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from Identificacao WHERE email = '" + email + "'";
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
                                    string data = sqlDataReader["auth2fa"].ToString();
                                    
                                    Co.Append(data + ",");

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }

 
            public int SELECTUSERS(string? email)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();

                    var sql   = "SELECT COUNT(email) from usuario WHERE email = '" + email + "'";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var result = (int)cmd.ExecuteScalar();
                        return result;
                    }
                                


            }


 
            public int SELECTUSERSBYUSERNAME(string? username)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();

                    var sql   = "SELECT COUNT(email) from usuario WHERE username = '" + username + "'";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var result = (int)cmd.ExecuteScalar();
                        return result;
                    }
                                


            }




 
            public int SELECTMAPA(string? username)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();

                    var sql   = "SELECT COUNT(username) from mapa WHERE username = '" + username + "'";
                    
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    using (SqlCommand cmd     = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var result = (int)cmd.ExecuteScalar();
                        return result;
                    }
                                


            }




            public int APPUSERS(string? email,string? senha, string? username)

            {
            
                {
            
                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();

                int Ativo = 1; 
                var sql   = "INSERT INTO usuario (hashcode,foto,nome,sobrenome,bio,telefone, website,username,email,senha,perfil,cidade,estado,local,sexos, Ativo) VALUES (@hashcode,@foto,@nome,@sobrenome,@bio, @telefone,@website,@username,@email,@senha,@perfil,@cidade,@estado,@local,@sexos,@Ativo) ";
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@hashcode", SqlDbType.NVarChar).Value    = uuid.TOKEN().ToString();
                                    command.Parameters.Add("@foto", SqlDbType.NVarChar).Value        = username;
                                    command.Parameters.Add("@nome", SqlDbType.NVarChar).Value        = "0";
                                    command.Parameters.Add("@sobrenome", SqlDbType.NVarChar).Value   = "0";
                                    command.Parameters.Add("@bio", SqlDbType.NVarChar).Value         = "0";
                                    command.Parameters.Add("@telefone", SqlDbType.NVarChar).Value    = "0";
                                    command.Parameters.Add("@website", SqlDbType.NVarChar).Value     = "0";
                                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value    = username;
                                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value       = email;
                                    command.Parameters.Add("@senha", SqlDbType.NVarChar).Value       = senha;
                                    command.Parameters.Add("@perfil", SqlDbType.NVarChar).Value      = "0";
                                    command.Parameters.Add("@cidade", SqlDbType.NVarChar).Value      = "0";
                                    command.Parameters.Add("@estado", SqlDbType.NVarChar).Value      = "0";
                                    command.Parameters.Add("@local", SqlDbType.NVarChar).Value       = "0";
                                    command.Parameters.Add("@sexos", SqlDbType.NVarChar).Value       = "0";
                                    command.Parameters.Add("@Ativo", SqlDbType.NVarChar).Value       = Ativo;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }

            }


            public string APPUSERID(string? email)
            {
                    // REGRA SÓ EXISTE 1 ATIVO O RESTANTE FICA INATIVO E DELETADO AO PEGAR VALOR TOTAL E SOMAR AO ATIVO PRINCIPAL
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from AspNetUsers WHERE Email = '" + email + "'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                            string data = "";
                            if (sqlDataReader.HasRows)
                            {
                                while (sqlDataReader.Read())
                                {
                                    data = sqlDataReader["Id"].ToString();

                                }
                            }
                            return data;
                        }
                        

                     
                    }
            }




            public int APPUSERSROLES(string? userid_, string? userrole_)

            {
            
                {
                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();
                var sql   = "INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES (@UserId, @RoleId)";

                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                   
                                    command.Parameters.Add("@UserId", SqlDbType.NVarChar).Value       =  userid_;
                                    command.Parameters.Add("@RoleId", SqlDbType.NVarChar).Value       =  userrole_;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();                                 
                                    return QueryData;
                                    
                                } }
            }

            }


           public int CADASTRARPARCEIRO(string? email,string? senha, string? username)
  

            {
            
                {
            
                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();

                int Ativo = 0; 
                var sql   = "INSERT INTO parceiro (hashcode,nome,email,cpf,telefone, cidade, estado,limite, limite_hostname, limite_acesso, date, data, Ativo) VALUES (@hashcode,@nome,@email,@cpf,@telefone,@cidade,@estado, @limite, @limite_hostname, @limite_acesso, @date, @data, @Ativo) ";
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@hashcode", SqlDbType.NVarChar).Value    = uuid.TOKEN().ToString();
                                    command.Parameters.Add("@nome", SqlDbType.NVarChar).Value        = username;
                                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value       = email;
                                    command.Parameters.Add("@cpf", SqlDbType.NVarChar).Value         = "0";
                                    command.Parameters.Add("@telefone", SqlDbType.NVarChar).Value    = "0";
                                    command.Parameters.Add("@cidade", SqlDbType.NVarChar).Value      = "0";
                                    command.Parameters.Add("@estado", SqlDbType.NVarChar).Value      = "0";
                                    command.Parameters.Add("@limite", SqlDbType.NVarChar).Value      = "0";
                                    command.Parameters.Add("@limite_hostname", SqlDbType.NVarChar).Value      = "0";
                                    command.Parameters.Add("@limite_acesso", SqlDbType.NVarChar).Value        = "0";
                                    command.Parameters.Add("@date", SqlDbType.NVarChar).Value        = Convert.ToString(date.DATE());
                                    command.Parameters.Add("@data", SqlDbType.NVarChar).Value        = date.DATE();
                                    command.Parameters.Add("@Ativo", SqlDbType.NVarChar).Value       = Ativo;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }

            }



           public int CADASTRARREVENDEDOR(string? email,string? senha, string? username)
  

            {
            
                {
            
                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();

                int Ativo = 0; 
                var sql   = "INSERT INTO revenda (hashcode,nome,email,cpf,telefone, cidade, estado, limite, limite_hostname, limite_acesso, date, data, Ativo) VALUES (@hashcode,@nome,@email,@cpf,@telefone,@cidade,@estado, @limite, @limite_hostname, @limite_acesso, @date, @data, @Ativo) ";
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.Add("@hashcode", SqlDbType.NVarChar).Value    = uuid.TOKEN().ToString();
                                    command.Parameters.Add("@nome", SqlDbType.NVarChar).Value        = username;
                                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value       = email;
                                    command.Parameters.Add("@cpf", SqlDbType.NVarChar).Value         = "0";
                                    command.Parameters.Add("@telefone", SqlDbType.NVarChar).Value    = "0";
                                    command.Parameters.Add("@cidade", SqlDbType.NVarChar).Value      = "0";
                                    command.Parameters.Add("@estado", SqlDbType.NVarChar).Value      = "0";
                                    command.Parameters.Add("@limite", SqlDbType.NVarChar).Value      = "00";
                                    command.Parameters.Add("@limite_hostname", SqlDbType.NVarChar).Value    = "00";
                                    command.Parameters.Add("@limite_acesso", SqlDbType.NVarChar).Value      = "00";
                                    command.Parameters.Add("@date", SqlDbType.NVarChar).Value        = Convert.ToString(date.DATE());
                                    command.Parameters.Add("@data", SqlDbType.NVarChar).Value        = date.DATE();
                                    command.Parameters.Add("@Ativo", SqlDbType.NVarChar).Value       = Ativo;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }

            }


        public int CREDITOREVENDA(string? email, string? credito)

        {

            {

                Connections connections_ = new Connections();
                var connetionString = connections_.Connection();
                var sql = "UPDATE credito SET credito = @credito WHERE email = @email and Ativo = 1";
                using (var connection = new SqlConnection(connetionString))
                {
                    using (var command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.Add("@email", SqlDbType.NVarChar).Value   = email;
                        command.Parameters.Add("@credito", SqlDbType.NVarChar).Value = credito;
                        connection.Open();
                        int QueryData = command.ExecuteNonQuery();
                        return QueryData;

                    }
                }
            }

        }



        public int CREDITOREVENDAADD(string? email)

        {

            {

                Connections connections_ = new Connections();
                var connetionString = connections_.Connection();
                var sql = "INSERT INTO credito (hashcode,email,credito,descricao, date, data, Ativo) VALUES (@hashcode,@email,@credito,@descricao,@date, @data, @Ativo)";
                int Ativo = 1; 
                using (var connection = new SqlConnection(connetionString))
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@hashcode", SqlDbType.NVarChar).Value    = uuid.TOKEN().ToString();
                        command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                        command.Parameters.Add("@credito", SqlDbType.NVarChar).Value = "0";
                        command.Parameters.Add("@descricao", SqlDbType.NVarChar).Value = "0";
                        command.Parameters.Add("@date", SqlDbType.NVarChar).Value = Convert.ToString(date.DATE());
                        command.Parameters.Add("@data", SqlDbType.NVarChar).Value = date.DATE();
                        command.Parameters.Add("@Ativo", SqlDbType.NVarChar).Value = Ativo;
                        connection.Open();
                        int QueryData = command.ExecuteNonQuery();
                        return QueryData;

                    }
                }
            }

        }
            public string TOTALCREDITOREVENDA(string? Nome)
            {
                    // REGRA SÓ EXISTE 1 ATIVO O RESTANTE FICA INATIVO E DELETADO AO PEGAR VALOR TOTAL E SOMAR AO ATIVO PRINCIPAL
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from credito where Ativo = 1 AND email = '" + Nome + "'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                            string data = "";
                            if (sqlDataReader.HasRows)
                            {
                                while (sqlDataReader.Read())
                                {
                                    data = sqlDataReader["credito"].ToString();

                                }
                            }
                            return data;
                        }
                        

                     
                    }
            }


            public string LIMITEUSERHOSTNAME(string? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from parceiro where email = '" + email + "'";
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
                                    string data = sqlDataReader["limite_hostname"].ToString();
                                    Co.Append(data);

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }



            public string LIMITEUSERAPPACESS(string? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from parceiro where email = '" + email + "'";
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
                                    string data = sqlDataReader["limite_acesso"].ToString();
                                    Co.Append(data);

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }            







            public string LIMITEUSERHOSTNAMEREVENDA(string? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from revenda where email = '" + email + "'";
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
                                    string data = sqlDataReader["limite_hostname"].ToString();
                                    Co.Append(data);

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }



            public string LIMITEUSERAPPACESSREVENDA(string? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from revenda where email = '" + email + "'";
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
                                    string data = sqlDataReader["limite_acesso"].ToString();
                                    Co.Append(data);

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }          


            public string LIMITEUSER(string? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from parceiro where email = '" + email + "'";
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
                                    string data = sqlDataReader["limite"].ToString();
                                    Co.Append(data);

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }




            public string LIMITEUSERREVENDA(string? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from revenda where email = '" + email + "'";
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
                                    string data = sqlDataReader["limite"].ToString();
                                    Co.Append(data);

                                }
                            }
                            return Convert.ToString(Co);
                        }
                        

                     
                    }
            }


            public string DESCONTO()
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from desconto";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            if (sqlDataReader.HasRows)
                            {
                                while (sqlDataReader.Read())
                                {

                                    string? data = sqlDataReader["valor"].ToString();
                                    dynamic? Ativo = sqlDataReader["Ativo"];

                                    if(Ativo == true){
                                       return Convert.ToString(data);
                                    }
                                    
                                }
                            }
                            
                            return "0";
                            
                        }

                    }
            }



            public string CREDITODELETE(int? id)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "delete from credito WHERE id = '" + id + "'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();

                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                            
                            return "Ok";
                        }
                        

                     
                    }
            }






            public int APPUSERSUPDATE(string? email,string? senha)

            {
            
                {
            
                Connections connections_  = new Connections();
                var connetionString       = connections_.Connection();
                var sql   = "UPDATE usuario SET email = @email, senha = @senha WHERE email = @email";
                using(var connection = new SqlConnection(connetionString))
                            {
                                using(var command = new SqlCommand(sql, connection))
                                {
                                   
                                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value       = email;
                                    command.Parameters.Add("@senha", SqlDbType.NVarChar).Value       = senha;
                                    connection.Open();
                                    int QueryData = command.ExecuteNonQuery();
                                    return QueryData;
                                    
                                } }
            }

            }


            // SECTION DELETE USER
            // SECTION DELETE USER


            public string DELETEREVENDA(string? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "delete from revenda WHERE email = '" + email + "'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();

                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                            
                            return "Ok";
                        }
                        

                     
                    }
            }


            public string DELETEPARCEIRO(string? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "delete from parceiro WHERE email = '" + email + "'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();

                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                            
                            return "Ok";
                        }
                        

                     
                    }
            }




            public string APPUSERSDELETE(string? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "delete from usuario WHERE email = '" + email + "'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();

                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                            
                            return "Ok";
                        }
                        

                     
                    }
            }




            public string DELETEPLANO(string? email)
            {
                    

                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "delete from plano WHERE revenda = '" + email + "'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();

                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                            
                            return "Ok";
                        }
                        

                     
                    }
            }
            // SECTION DELETE USER
            // SECTION DELETE USER


            // SELECT HOSTNAME BY DNS
            // SELECT HOSTNAME BY DNS

            public string GETHOSTNAMEBYDNS(string? dns)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from dnsuri where dns = '" + dns + "'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                            string data = "";
                            if (sqlDataReader.HasRows)
                            {
                                while (sqlDataReader.Read())
                                {
                                    data = sqlDataReader["hostname"].ToString();

                                }
                            }
                            return data;
                        }
                        

                     
                    }
            }






            public string GETPLANOBYHOST(string? hostname)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from plano where hostname = '" + hostname + "'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                            string data = "";
                            if (sqlDataReader.HasRows)
                            {
                                while (sqlDataReader.Read())
                                {
                                    data = sqlDataReader["hostname"].ToString();

                                }
                            }
                            return data;
                        }
                        

                     
                    }
            }


            public string GETDNSBYHOSTNAME(string? hostname)
            {
                    
                    Connections connections_  = new Connections();
                    var connetionString       = connections_.Connection();
                    var sql   = "select * from dnsuri where hostname = '" + hostname + "'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {

                            SqlDataReader sqlDataReader = command.ExecuteReader();
                            System.Text.StringBuilder Co = new System.Text.StringBuilder("");
                            string data = "";
                            if (sqlDataReader.HasRows)
                            {
                                while (sqlDataReader.Read())
                                {
                                    data = sqlDataReader["dns"].ToString();

                                }
                            }
                            return data;
                        }
                        

                     
                    }
            }


            // SELECT HOSTNAME BY DNS
            // SELECT HOSTNAME BY DNS

}}

