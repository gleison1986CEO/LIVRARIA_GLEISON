using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema.Models
{
    public class Datalog
    {

        [Key]
        [Column("codigo")]
        public int codigo { get; set; }



        [Column("hashcode")]
        public string hashcode { get; set; }



        [Column("data_hora")]
        public DateTime? data_hora { get; set; }

        [Column("datahora")]
        public string? datahora { get; set; }

        [Column("cargo")]
        public string cargo { get; set; }


        [Column("login")]
        public string login { get; set; }


        [Column("ip")]
        public string ip { get; set; }

        [Column("executou")]
        public string executou { get; set; }

}
}
