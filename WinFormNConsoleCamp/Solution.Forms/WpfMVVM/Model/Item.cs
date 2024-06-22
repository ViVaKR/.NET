
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WpfMVVM.Model
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? SerialNumber { get; set; }

        [Range(0, 100)]
        [DisplayName("수량")]
        public int Quantity { get; set; }
    }
}
