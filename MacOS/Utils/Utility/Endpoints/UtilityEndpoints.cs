using Microsoft.EntityFrameworkCore;
using Utility.Mapping;

namespace Utility.Endpoints;

public static class UtilityEndpoints
{
    public static RouteGroupBuilder MapColorTableEndpoints(this WebApplication app)
    {
        //--> dotnet add package MinimalApis.Extensions
        var group = app.MapGroup("color-table").WithParameterValidation();

        //! Get /colors
        group.MapGet("/", async (UtilityContext db)
        => await db.ColorTables.Select(x => x.ToColorTableSummaryDto()).AsNoTracking().ToListAsync());
        return group;
    }
}
