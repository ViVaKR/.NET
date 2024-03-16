using System.ComponentModel.DataAnnotations;

namespace Utility.Dtos;

public record class ColorTableSummaryDto(

    int Id,

    [Required]
    [StringLength(50)]
    string Name,

    [StringLength(20)]
    string HexCode,

    [StringLength(20)]
    string DecimalCode);
