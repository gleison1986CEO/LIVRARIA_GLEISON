using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema.Models
{
    public class Produto
    {

        [Key]
        [Column("codigo")]
        public int codigo { get; set; }


        [Column("hashcode")]
        public string? hashcode { get; set; }


        [Column("foto")]
        public string? foto{ get; set; }


        [Column("categoria")]

        public string?  categoria{ get; set; }


        [Column("subcategoria")]

        public string? subcategoria{ get; set; }


        [Column("quantidade")]

        public string? quantidade{ get; set; }

        [Column("estado")]

        public string? estado{ get; set; }

        [Column("titulo")]

        public string? titulo{ get; set; }


        [Column("descricao")]

        public string? descricao{ get; set; }



        [Column("console")]

        public string? console{ get; set; }



        [Column("multilanguage")]

        public string? multilanguage{ get; set; }


        [Column("global")]

        public string? global{ get; set; }


        [Column("valor")]

        public string? valor{ get; set; }


        [Column("desconto")]

        public string? desconto{ get; set; }


        [Column("cupom")]

        public string? cupom{ get; set; }


        [Column("date")]

        public string? date{ get; set; }

        [Column("data")]
        public DateTime? data { get; set; }


        public bool Ativo { get; set; }
}

}
