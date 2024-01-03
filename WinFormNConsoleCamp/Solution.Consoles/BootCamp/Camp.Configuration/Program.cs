
using CampConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateApplicationBuilder(args).Build();
// using IHost host = new HostBuilder().Build();

var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
lifetime.ApplicationStarted.Register(() => Console.WriteLine("Started"));
lifetime.ApplicationStopping.Register(() => Console.WriteLine("Stopping"));
lifetime.ApplicationStopped.Register(() => Console.WriteLine("Stopped"));

// Started
// await host.RunAsync();
await host.StartAsync();

//* Build a configuration object from command line
IConfiguration confArgs = new ConfigurationBuilder().AddCommandLine(args).Build();
//! Read configuration values
Console.WriteLine($"input: {confArgs["input"]}");
Console.WriteLine($"output: {confArgs["output"]}");

//* Build a configuration object from JSON file
IConfiguration confSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


//? (Binder) Bind a configuration sectin to an instance of Settings Class
Console.WriteLine("From Binder");
Settings? settings = confSettings.GetSection("Settings").Get<Settings>();
// Read Simple values
Console.WriteLine($"Server: {settings!.Server}");
Console.WriteLine($"Database: {settings!.Database}");
foreach (var item in settings.Endpoints)
{
    Console.WriteLine($"{item.IPAddress}:{item.Port}");
}

IConfigurationSection section = confSettings.GetSection("Settings");
Console.WriteLine($"Server: {section["Server"]}");
Console.WriteLine($"Database: {section["Database"]}");

//* 
IConfigurationRoot confRoot
    = new ConfigurationBuilder().AddJsonFile("demo.json").AddEnvironmentVariables().Build();


//* NULL Operator
List<int>? numbers = null;
(numbers ??= []).AddRange(Enumerable.Range(1, 4));
Console.WriteLine(string.Join(", ", numbers));
int? a = null;
Console.WriteLine(a ?? 3);
Console.WriteLine(a is null);
numbers.Add(a ??= 0);
Console.WriteLine(string.Join(", ", numbers));
Console.WriteLine(a);

int? b = null;
Display(b, 33);
static void Display<T>(T b, T backup)
{
    Console.WriteLine(b ?? backup);
}

//

var sum = SumNumbers(null, 0);
Console.WriteLine(sum);
static double SumNumbers(List<double[]>? setsOfNumbers, int indexOfSetToSum)
{
    return setsOfNumbers?[indexOfSetToSum]?.Sum() ?? double.NaN;
}

//

int? j = null;
int? k = j ?? -1;
Console.WriteLine(k); // output: -1


// 
Console.WriteLine(IsNullable(typeof(int?))? "nullable" : "non-nullable");
Console.WriteLine(IsNullable(typeof(int)) ? "nullable" : "non-nullable");
static bool IsNullable(Type type) => Nullable.GetUnderlyingType(type) != null;

//* Host
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Configuration.Sources.Clear();
IHostEnvironment env = builder.Environment;
Console.WriteLine($"{env.EnvironmentName}");

//? Ask the service provide for the configuration abstraction
IConfiguration cfg = host.Services.GetRequiredService<IConfiguration>();
var server = cfg.GetValue<string>("Settings:Server");
var ip1 = cfg.GetValue<string>("Settings:Endpoints:0:Port");
Console.WriteLine(server);
Console.WriteLine(ip1);

//==> (Run) `$ dotnet run --input "Hello" --output ", World"`

//? (Shutdown) Listens for Ctrl + C
Console.WriteLine("Shutdown => Press CTRL + C");
await host.WaitForShutdownAsync();

/* 
    --> `appsettings.json`
    
    --> IConfiguration
        ! appsettins.{Environment}.json
        ! Environment variables
        ! Command-line arguments
        ! *Other sources
        
    --> The main types provided by this library are:
        ! Microsoft.Extensions.Host.
        ! Microsoft.Extensions.Hosting.HostApplicationBuilder
        ! Microsoft.Extensions.Hosting.HostBuilder
        ! Microsoft.Extensions.Hosting.IHostedService
        ! Microsoft.Extensions.Hosting.IHostedLifecycleService
    
    --> Additional Documentation
        ! Generic host
    
    --> API documentation
        ! Host
        ! HostApplicationBuilder
        ! HostBuilder
    
    --> Related Packages
        ! Microsoft.Extensions.Configuration
        ! Microsoft.Extensions.DependencyInjection
        ! Microsoft.Extensions.Hosting.Abstractions
        ! Microsoft.Extensions.Logging
        ! Microsoft.Extensions.Options
 */


/* 

! = From Nullable to Non-Nullable
? = From Non-Nullbale To Nullable

* [ ? ]
-> 변수가 널 값을 보유할 수 있음을 컴파일러에게 알려줍니다, 변수를 정의할 때 사용

string? x;
	x 는 참조 유형이므로 기본적으로 널이 불가능함으로, ? 연산자를 적용하여 null 을 허용하도록 만듬
	x = null; // 잘 작동함

string y;
	y 는 참조 유형이므로 기본적으로 널이 불가능함
	y = null; // 널이 안되는 항목에 널을 할 당했기 때문에 경고를 생성함

* [ ! ] : Null-Forgiving-Operator
-> 널일 수 있는 항목에 액세스 해도 안전하다는 것을 컴파일러에게 알림. 즉, 널 안전성에 대해 관심 두지 않는 다는 의도를 표현함, 변수에 접근할 때 사용

e.g. string x; string? y;
(1) x = y;  // (불법) 할당의 왼쪽은 널을 허용하지 않지만, 오른쪽은 널을 허용하므로, 의미상 올바르지 않기 때문에 작동하지 않음
(2) x = y!; // (합법) y 유형 수정자가 적용된 참조 유형이므로 ? 다르게 입증되지 않으면 널이 허용됨, 즉 널 허용여부를 재정의하여 널을 허용하지 않도록 ! 를 y 에 적용, 왼쪽과 오른쪽 모두 널을 허용하지 않는 다는 의미상 맞게 됨, 단. 유형 검사 시스템 수준에서만 컴파일러 검사를 해제하는 것으로 런타임시에 값은 여전히 널일 수 있음으로 Null-Forgiving-Operator 를 사용하지 않도록 노력해야 함. 사용시 컴파일러에서 보장하는 널 안정성 무효화가 되므로 시스테 ㅁ설계 결함의 증상이 될 수 있음, ! 버그 찾기가 매우 어려워짐
그렇다면, 왜 ! 연산자가 존재하는가? : 
	1. 일부 경우에서는 컴파일러가 널 허용값이 실제로 널 허용이 아님을 감지 할 수 없음
	2. 레거시 코드 기반 마이그레이션이 더 쉬워짐
	3. 경우에 따라서는 널이 되어도 상관하지 않을 때
	4. 단위 테스트로 작업할 때 코드가 널 실행될 때 코드의 동작을 확인하고 싶을 수 있을 때
단지 경고를 없애기 위해 코드에 수백개를 쳐 넣지 마세요.

그럼 .. null! 은 무슨 뜻인가?
-> 널 값이 아니라는 것을 컴파일러에게 알려 줌, x = y! 와 같은 케이스, 이때 null 리터럴은 다른 표현식/유형/값/변수 와 동일한 의미.
-> 리터럴 null 유형은 기본적으로 null 을 허용하는 유일한 유형이므로 모든 유형의 널 허용여부는 !null 허용 불가로 재정의 될 수 있음.
 */
