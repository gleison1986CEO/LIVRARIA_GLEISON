using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema.Models
{
    public class Maps
    {

        [Key]
        [Column("place_id")]
        public int place_id { get; set; }

        [Column("lat")]
        public string lat { get; set; }

        [Column("lon")]
        public string lon { get; set; }

        [Column("display_name")]
        public string display_name{ get; set; }
        

}



    public class GoogleMaps
    {

        [Key]
        [Column("results")]
        public int results { get; set; }

        [Column("lat")]
        public string lat { get; set; }

        [Column("lon")]
        public string lon { get; set; }

        [Column("geometry")]
        public string geometry{ get; set; }
        

}


}
