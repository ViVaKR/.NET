
using Camp.Example;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IMessageWriter, ConsoleMessageWriter>();
builder.Services.TryAddSingleton<IMessageWriter, LoggingMessageWriter>();

using IHost host = builder.Build();
await host.RunAsync();


// 서비스 수명
// 1. AddTransient : 서비스 컨테이너에서 요청할 때마다 만들어짐, 간단한 상태 서비스
// 2. AddScoped : 웹 애플리케이션의 경우 범위가 지정된 수명은 클라이언트 요청(연결) 마다 서비스가 생성됨, 요청이 끝날 때 삭제됨, 
// 2-2. AddDbContext : Entity Framework Core 범위가 지정된 수명으로 등록됨
// 3. AddSingleton : 처음 요청되는 경우, 컨테이너에서 자동으로 삭제함
// 
