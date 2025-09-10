using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema.Models
{
    public class Identificacao
    {

        [Key]
        [Column("codigo")]
        public int codigo { get; set; }



        [Column("email")]
        public string email { get; set; }


        [Column("ip")]
        public string ip{ get; set; }

        [Column("secret")]

        public string? secret{ get; set; }

        [Column("auth2fa")]
        public string? auth2fa{ get; set; }


        [Column("token")]
        public string? token{ get; set; }                


        [Column("cpf")]
        public string? cpf{ get; set; }     


        [Column("data")]
        public DateTime? data { get; set; }

        public bool Ativo { get; set; }
}




}
