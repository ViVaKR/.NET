using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Modal
{
    public class CustomerModal
    {
        [StringLength(100)]
        public string CardOwnerName { get; set; } = null!;

        [StringLength(16)]
        public string CardNumber { get; set; } = null!;

        [StringLength(5)]
        public string ExpirationDate { get; set; } = null!;

        [StringLength(3)]
        public string SecurityCode { get; set; } = null!;
    }
}
