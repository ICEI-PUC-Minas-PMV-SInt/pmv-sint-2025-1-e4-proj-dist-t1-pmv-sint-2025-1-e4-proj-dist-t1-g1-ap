using System.ComponentModel.DataAnnotations;

namespace api_adota_pet.Models
{
    public class AnuncioDto
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public int IdadeAnimal { get; set; }

        [Required]
        public CategoriaAnimal CategoriaAnimal { get; set; }

        [Required]
        public string RacaAnimal { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string ImagemCapa { get; set; }

        //[Required]
        //public int UsuarioId { get; set; }
    }
}
