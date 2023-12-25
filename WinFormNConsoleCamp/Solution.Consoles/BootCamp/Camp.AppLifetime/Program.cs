
using Camp.AppLifetime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<ExampleHostedService>();

using IHost host = builder.Build();

await host.RunAsync();
