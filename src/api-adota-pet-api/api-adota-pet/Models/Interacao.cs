using System.ComponentModel.DataAnnotations;

namespace api_adota_pet.Models
{
    public class Interacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public NomeInteracao NomeInteracao { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public int IdAnuncio { get; set; }
    }

    public enum NomeInteracao {
        Like,
        Report
    }
}
