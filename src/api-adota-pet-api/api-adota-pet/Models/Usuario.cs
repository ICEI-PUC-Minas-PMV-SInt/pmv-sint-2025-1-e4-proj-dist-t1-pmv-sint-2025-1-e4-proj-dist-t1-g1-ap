﻿using System.ComponentModel.DataAnnotations;

namespace api_adota_pet.Models
{
    public class Usuario//: LinkHATEOS
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string ExternalId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public bool EAdmin { get; set; }

        [Required]
        public StatusUsuario Status { get; set; } 

        public ICollection<Anuncio> Anuncio { get; set; }


    }

    public enum StatusUsuario
    {
        Habilitado,
        Desabilitado,
    }
}
