
using Camp.DiExample;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.TryAddSingleton<ILogWriter, LogWriter>();

using IHost host = builder.Build();

await host.RunAsync();
