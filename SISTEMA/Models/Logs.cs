using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema.Models
{
    
    public class Logs
    {

        [Key]
        [Column("codigo")]
        public int codigo { get; set; }



        [Column("hostname")]
        public string hostname { get; set; }


        [Column("data")]
        public string data { get; set; }


        public bool Ativo { get; set; }
}



public class LogsInsert
    {
        [Key]
        public bool Ativo { get; set; }
    }}