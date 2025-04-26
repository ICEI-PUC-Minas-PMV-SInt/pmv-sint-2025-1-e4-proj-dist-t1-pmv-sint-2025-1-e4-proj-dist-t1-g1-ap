using System.ComponentModel.DataAnnotations;

namespace api_adota_pet.Models
{
    public class AuthenticateDto
    {
        [Required]
        public string Documento { get; set; }
        
        [Required]
        public string Senha { get; set; }

    }
}
