using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculos.Models.Entities
{
    public class Cartao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        public int Id_Cliente { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        [StringLength(20)]
        public string Numero { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeTitular { get; set; }

        [Required]
        public DateTime Validade { get; set; }

        [Required]
        [StringLength(5)]
        public string Cvc { get; set; }

        [StringLength(20)]
        public string Bandeira { get; set; }
    }
}