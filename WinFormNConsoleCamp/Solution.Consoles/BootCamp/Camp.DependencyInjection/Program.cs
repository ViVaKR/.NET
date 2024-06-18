using Camp.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// IServiceCollection
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IMessageWriter, LoggingMessageWriter>();

using IHost host = builder.Build();
host.Run();
