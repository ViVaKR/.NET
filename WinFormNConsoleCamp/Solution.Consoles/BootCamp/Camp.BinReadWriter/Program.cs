
using Camp.BinReadWriter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IBinWriter, BinWriter>();
builder.Services.AddSingleton<IBinReader, BinReader>();
using IHost host = builder.Build();

host.Run();


Console.WriteLine("Complete");
