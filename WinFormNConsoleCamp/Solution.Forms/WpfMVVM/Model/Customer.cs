
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WpfMVVM.Model
{
    public class Customer
    {
        [Key]
        [DisplayName("고객번호")]
        public int Id { get; set; }

        [MaxLength(100)]
        [DisplayName("고객이름")]
        public string? Name { get; set; }


    }
}
