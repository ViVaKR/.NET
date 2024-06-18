
using CampEvaluate;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.Extensions.Configuration;

//# (1) 프로젝트 생성 #//
//! $ dotnet new console -o CampEvaluate

//# (2) 테스트 클래서 Runner 생성 #//
//! $ dotnet new class -n Runner

//# (3) 스크립트 처리 nuget package 설치 #//
//! $ dotnet add package Microsoft.CodeAnalysis.CSharp.Scripting

//# 코드 작성 후.. //

//# 실행 테스트,  수식을 제 1 Argument 로 넣기, 솔루션 에서 프로젝트 실행 할시 --project 옵션 넣기 //
//! $ dotnet run "System.Math.Sqrt(50 + 350)" --InputPath "~/Temp" --OutputPath "~/Text/Output"

//==> Start Point <==//

//! Example (1)
var value = await CSharpScript.EvaluateAsync(args[0]);
Console.WriteLine();
Console.WriteLine($"Args[0] Expresion: {args[0]} => {value}");
Console.WriteLine();

//! Example (2)
Runner.ExecuteScript(1200, 58);
Console.WriteLine("====");
Console.WriteLine();
Runner.GetScript(40, 30);


IConfiguration config 
= new ConfigurationBuilder().AddCommandLine(args).Build();

// Read confiiguration values
Console.WriteLine($"Input: {config["InputPath"]}");
Console.WriteLine($"OutputPath: {config["OutputPath"]}");


// End //
