using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema.Models
{
    public class Categoria
    {


        [Key]
        [Column("codigo")]
        public int codigo { get; set; }

        [Column("hashcode")]
        public string? hashcode { get; set; }


        [Column("nome")]
        public string? nome{ get; set; }


        [Column("titulo")]

        public string?  titulo{ get; set; }


        [Column("descricao")]

        public string? descricao{ get; set; }


        [Column("date")]

        public string? date{ get; set; }

        [Column("data")]
        public DateTime? data { get; set; }


        public bool Ativo { get; set; }
}

}
