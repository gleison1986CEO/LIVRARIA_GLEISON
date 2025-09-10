using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema.Models
{
    public class Cidade
    {

        [Key]
        [Column("codigo")]
        public int codigo { get; set; }



        [Column("nome")]
        public string nome { get; set; }



        [Column("lat")]
        public string latitude { get; set; }



        [Column("long")]
        public string longitude { get; set; }



        public bool Ativo { get; set; }
}




}
