using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Models.Entities
{
    public class FormaPagamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public ICollection<Aluguel> Alugueis { get; set; }
    }
}