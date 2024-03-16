using Utility.Dtos;

namespace Utility.Mapping;

public static class UtilityMapping
{
    public static ColorTableSummaryDto ToColorTableSummaryDto(this ColorTable color)
    {
        return new ColorTableSummaryDto(
            color.Id,
            color.Name,
            color.HexCode,
            color.DecimalCode);
    }
}
