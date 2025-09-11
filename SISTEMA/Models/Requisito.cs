using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema.Models
{
    public class Requisito
    {

        [Key]
        [Column("codigo")]
        public int codigo { get; set; }


        [Column("hashcode")]
        public string? hashcode { get; set; }


        [Column("nome")]
        public string? nome{ get; set; }


        [Column("audio")]

        public string?  audio{ get; set; }


        [Column("video")]

        public string?  video{ get; set; }

        [Column("tamanho")]

        public string?  tamanho{ get; set; }


        [Column("console")]

        public string?  console{ get; set; }


        [Column("descricao")]

        public string? descricao{ get; set; }


        [Column("date")]

        public string? date{ get; set; }

        [Column("data")]
        public DateTime? data { get; set; }


        public bool Ativo { get; set; }
}

}
