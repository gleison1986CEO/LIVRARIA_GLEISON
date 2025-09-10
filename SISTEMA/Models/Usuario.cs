using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema.Models
{
    public class Usuario
    {

        [Key]
        [Column("codigo")]
        public int codigo{ get; set; }


        [Column("hashcode")]
        public string? hashcode { get; set; }


        [Column("foto")]
        public string? foto { get; set; }


        [Column("nome")]
        public string? nome { get; set; }


        [Column("telefone")]
        public string? telefone { get; set; }


        [Column("sobrenome")]
        public string? sobrenome { get; set; }


        [Column("bio")]
        public string? bio { get; set; }


        [Column("website")]
        public string? website { get; set; }


        [Column("username")]
        public string? username { get; set; }


        [Column("email")]
        public string? email { get; set; }


        [Column("senha")]
        public string? senha { get; set; }  


        [Column("perfil")]
        public string? perfil { get; set; }  


        [Column("cidade")]
        public string? cidade { get; set; }  


        [Column("estado")]
        public string? estado { get; set; }  


        [Column("local")]
        public string? local { get; set; }  

        [Column("sexos")]
        public string? sexos { get; set; }  

        public bool Ativo { get; set; }
}


}