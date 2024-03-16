using Microsoft.EntityFrameworkCore;
using Utility;
using Utility.Endpoints;


var origins = "_utilityAllowOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: origins, policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();

    });
});
// Jwt
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var connectionString = builder.Configuration["Utility:ConnectionString"];

builder.Services.AddDbContext<UtilityContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(origins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapColorTableEndpoints();
app.UseEndpoints(end =>
{
    _ = end.MapGet("/echo",
    context => context.Response.WriteAsync("echo"))
    .RequireCors(origins);

});
await app.MigrateDbAsync();

app.Run();

