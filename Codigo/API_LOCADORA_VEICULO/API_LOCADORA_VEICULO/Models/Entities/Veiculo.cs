using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculos.Models.Entities
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Modelo")]
        public int Id_Modelo { get; set; }
        public Modelo Modelo { get; set; }

        [Required]
        [ForeignKey("Cor")]
        public int Id_Cor { get; set; }
        public Cor Cor { get; set; }

        [Required]
        [StringLength(10)]
        public string Placa { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal ValorCompra { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal ValorVenda { get; set; }

        [Required]
        public int AnoFabricacao { get; set; }

        public int AnoModelo { get; set; }

        public int Quilometragem { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public DateTime DataCadastro { get; set; }

        public ICollection<Aluguel> Alugueis { get; set; }
    }
}