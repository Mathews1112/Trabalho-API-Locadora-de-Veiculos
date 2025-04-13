using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculos.Models.Entities
{
    public class Aluguel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        public int Id_Cliente { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        [ForeignKey("Veiculo")]
        public int Id_Veiculo { get; set; }
        public Veiculo Veiculo { get; set; }

        [ForeignKey("FormaPagamento")]
        public int Id_FormaPagamento { get; set; }
        public FormaPagamento FormaPagamento { get; set; }

        public int QuilometragemInicial { get; set; }

        public int? QuilometragemFinal { get; set; }

        public int QuilometragemPermitida { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorDiaria { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal ValorTotal { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorMulta { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        public DateTime DataDevolucaoPrevista { get; set; }

        public DateTime? DataDevolucaoReal { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public string Observacoes { get; set; }
    }
}