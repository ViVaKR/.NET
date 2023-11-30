using AutoMapper;
using LearnAPI.Container;
using LearnAPI.Helper;
using LearnAPI.Repos;
using LearnAPI.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LearnDataContext>(x=> x.UseSqlServer(builder.Configuration.GetConnectionString("ViVaKRConnection")));

// 의존성 주입 3가지 방식 예시
// (1) DI e.g.`Transient` <Interface, Implement>
builder.Services.AddTransient<ICustomerService, CustomerService>();

// (2) DI e.g. `Singleton` <Interface, Implement>
// builder.Services.AddSingleton<ICustomerService, CustomerService>();

// (3) DI e.g. `Scope` Example <Interface, Implement>
// builder.Services.AddScoped<ICustomerService, CustomerService>();

var automapper = new MapperConfiguration(x => x.AddProfile(new AutoMapperHandler()));
IMapper mapper = automapper.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares : 순서가 매우 중요함

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
