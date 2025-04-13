using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculos.Models.Entities
{
    public class Modelo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Fabricante")]
        public int Id_Fabricante { get; set; }
        public Fabricante Fabricante { get; set; }

        [Required]
        [StringLength(50)]
        public string Descricao { get; set; }

        [StringLength(30)]
        public string Tipo { get; set; }

        public ICollection<Veiculo> Veiculos { get; set; }
    }
}