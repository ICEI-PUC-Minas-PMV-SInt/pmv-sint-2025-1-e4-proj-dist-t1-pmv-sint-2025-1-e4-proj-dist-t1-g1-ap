using System.ComponentModel.DataAnnotations;

namespace api_adota_pet.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? ExternalId { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Required]
        public string? Telefone { get; set; }

        [Required]
        public string? Documento { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
