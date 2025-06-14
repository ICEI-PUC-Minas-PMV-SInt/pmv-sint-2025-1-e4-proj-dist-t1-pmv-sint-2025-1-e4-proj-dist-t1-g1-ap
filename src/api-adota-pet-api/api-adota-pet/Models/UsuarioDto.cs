using System.ComponentModel.DataAnnotations;

namespace api_adota_pet.Models
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Senha { get; set; }

        [Required]
        public bool EAdmin { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Required]
        public string? Telefone { get; set; }

        [Required]
        public string? Documento { get; set; }
    }
}
