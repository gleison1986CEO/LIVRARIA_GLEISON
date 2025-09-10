using System.ComponentModel.DataAnnotations;

namespace sistema.Models
{
    public class TwoFactor
    {
        [Required]
        public string TwoFactorCode { get; set; }
    }
}
