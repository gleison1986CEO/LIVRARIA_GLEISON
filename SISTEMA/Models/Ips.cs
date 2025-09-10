using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema.Models
{
    public class Ips
    {

        [Key]
        [Column("codigo")]
        public int codigo { get; set; }



        [Column("nome")]
        public string nome { get; set; }



        [Column("ip")]
        public string ip{ get; set; }


        public bool Ativo { get; set; }
}




}
