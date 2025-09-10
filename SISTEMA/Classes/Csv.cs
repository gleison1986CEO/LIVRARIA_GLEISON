
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;

namespace sistema.Classes
{
    public class CSVGEN
    {
        private readonly Uuid uuid = new Uuid();
        private readonly IpConn IP = new IpConn();
        public String  DATALOG(dynamic? data)

            {  

                    /// CSV DATA
                    
                    var output      = new StringBuilder();
                    String PAthName = uuid.CSV();
                    String Headers  = "codigo, hashcode, datahora, cargo, login, ip, executou";
                    foreach (var row in data)
                    {

                       output.AppendLine(
                            row.codigo  + ", " + 
                            row.hashcode + ", " +
                            row.datahora + ", " +
                            row.cargo + ", " +
                            row.login + ", " +
                            row.ip + ", " +
                            row.executou + ", " 
                         
                    
                       );
                    }
                    var location = "/files/csv/" + PAthName +".csv";    
                    var filepath = IP.CSV() + PAthName +".csv"; 
                   
                    using (StreamWriter writer = new StreamWriter(new FileStream(filepath,
                    FileMode.Create, FileAccess.Write)))
                    {
                        writer.WriteLine(Headers);
                        
                        writer.WriteLine(output.ToString());
                    }

                    /// CSV DATA


                return location;
                                


            }
        public String  DETALHES(dynamic? data)

            {  

                    /// CSV DATA

                    var output      = new StringBuilder();
                    String PAthName = uuid.CSV();
                    String Headers  = "matricula, fabricante, modelo, serie, proprietario, operador, info, foto, missao,aeronave,numero,data,origem,destino,acionamento,decolagem,pouso,corte,diurno,noturno,vfr,ifr_real,ifr_capota,natureza,pousos,ciclos,distancia,piloto,co_piloto,piloto_reserva,quantidade,medida,carga,unidade,pob,socio,total_noturno_diurno,total_vfr,horas_voo,nome,email,agente,tipo,classificacao,servico,moeda,taxa,valor,fornecedor,nota,pagamento,local,baixa";
                    foreach (var row in data)
                    {
     
                                output.AppendLine(
                                    row.matricula + ","
                                    + row.fabricante + ","
                                    + row.modelo + ","
                                    + row.serie + ","
                                    + row.proprietario + ","
                                    + row.operador + ","
                                    + row.info + ","
                                    + row.foto + ","
                                    + row.missao + ","
                                    + row.aeronave + ","
                                    + row.numero + ","
                                    + row.data + ","
                                    + row.origem + ","
                                    + row.destino + ","
                                    + row.acionamento + ","
                                    + row.decolagem + ","
                                    + row.pouso + ","
                                    + row.corte + ","
                                    + row.diurno + ","
                                    + row.noturno + ","
                                    + row.vfr + ","
                                    + row.ifr_real + ","
                                    + row.ifr_capota + ","
                                    + row.natureza + ","
                                    + row.pousos + ","
                                    + row.ciclos + ","
                                    + row.distancia + ","
                                    + row.piloto + ","
                                    + row.co_piloto + ","
                                    + row.piloto_reserva + ","
                                    + row.quantidade + ","
                                    + row.medida + ","
                                    + row.carga + ","
                                    + row.unidade + ","
                                    + row.pob + ","
                                    + row.pagante + ","
                                    + row.total_noturno_diurno + ","
                                    + row.total_vfr + ","
                                    + row.horas_voo + ","
                                    + row.nome + ","
                                    + row.email + ","
                                    + row.agente + ","
                                    + row.tipo + ","
                                    + row.classificacao + ","
                                    + row.servico + ","
                                    + row.moeda + ","
                                    + row.taxa + ","
                                    + row.valor + ","
                                    + row.fornecedor + ","
                                    + row.nota + ","
                                    + row.pagamento + ","
                                    + row.local + ","
                                    + row.baixa + ","
                         
                    
                       );
                    }
                    

                    var location = "/files/csv/" + PAthName +".csv";   //MUDAR PARA URL SERVIDOR 
                    var filepath = IP.CSV() + PAthName +".csv"; 
                   
                    using (StreamWriter writer = new StreamWriter(new FileStream(filepath,FileMode.Create, FileAccess.Write)))
                    {
                        writer.WriteLine(Headers);
                        
                        writer.WriteLine(output.ToString());
                    }

                    /// CSV DATA


                return location;
                                


            }



            

    }
}

    

