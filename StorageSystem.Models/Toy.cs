using StorageSystem.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace StorageSystem.Models
{
    public class Toy: Entity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string Color { get; set; }
        [Required]
        [MaxLength(100)]
        public string Material { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
