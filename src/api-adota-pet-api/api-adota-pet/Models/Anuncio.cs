using System.ComponentModel.DataAnnotations;

namespace api_adota_pet.Models
{
    public class Anuncio //: LinkHATEOS
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? ExternalId { get; set; }

        [Required]
        public string? Titulo { get; set; }

        [Required]
        public int IdadeAnimal { get; set; }

        [Required]
        public CategoriaAnimal CategoriaAnimal { get; set; }

        [Required]
        public string? RacaAnimal { get; set; }

        [Required]
        public string? Descricao { get; set; }

        [Required]
        public string? ImagemCapa { get; set; }

        [Required]
        public DateTime DataPostagem { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

    }

    public enum CategoriaAnimal
    {
        Canino,
        Felino,
        Ave,
        Exotico,
        Outros
    }

    public enum Status
    {
        Publicado,
        Deletado,
    }
}
