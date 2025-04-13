using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Models.Entities
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }

        [Required]
        [StringLength(20)]
        public string CNH { get; set; }

        public bool Ativo { get; set; }

        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<Cartao> Cartoes { get; set; }
        public ICollection<Aluguel> Alugueis { get; set; }
    }
}