using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Models.Entities
{
    public class Cor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nome { get; set; }

        [StringLength(7)]
        public string CodigoHex { get; set; }

        public ICollection<Veiculo> Veiculos { get; set; }
    }
}