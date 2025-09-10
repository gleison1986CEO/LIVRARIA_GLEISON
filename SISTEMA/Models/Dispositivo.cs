using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema.Models
{
    public class Dispositivo
    {

        [Key]
        [Column("codigo")]
        public int codigo { get; set; }


        [Column("hashcode")]
        public string? hashcode { get; set; }

        [Column("nome")]
        public string? nome { get; set; }

        [Column("chave")]
        public string? chave { get; set; }

        [Column("id")]
        public string? id { get; set; }

        [Column("url")]
        public string? url { get; set; }

        [Column("description")]
        public string? description { get; set; }

        [Column("date")]
        public string? date{ get; set; }

        [Column("data")]
        public DateTime? data { get; set; }

        public bool Ativo { get; set; }
}


    public class DispositivoInsert
    {

        [Key]
        public bool Ativo { get; set; }
    }



}
