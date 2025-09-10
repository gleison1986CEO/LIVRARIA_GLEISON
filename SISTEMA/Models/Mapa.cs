using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema.Models
{
    public class Mapa

    {

        [Key]
        [Column("codigo")]
        public int codigo { get; set; }



        [Column("hashcode")]
        public string? hashcode { get; set; }


        [Column("foto")]
        public string? foto{ get; set; }


        [Column("aeronave")]
        public string? aeronave{ get; set; }


        [Column("latitude")]
        public string? latitude{ get; set; }


        [Column("longitude")]
        public string? longitude{ get; set; }        


        public bool Ativo { get; set; }
}




}
