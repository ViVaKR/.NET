using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Repos.Models;

public partial class PaymentDetail
{
    [Key]
    public int PaymentDetailId { get; set; }

    [StringLength(100)]
    public string CardOwnerName { get; set; } = null!;

    [StringLength(16)]
    public string CardNumber { get; set; } = null!;

    [StringLength(5)]
    public string ExpirationDate { get; set; } = null!;

    [StringLength(3)]
    public string SecurityCode { get; set; } = null!;
}
