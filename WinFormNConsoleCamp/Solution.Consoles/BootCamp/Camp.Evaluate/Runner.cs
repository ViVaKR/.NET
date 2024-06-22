using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace CampEvaluate;

public class Runner
{
    public static void ExecuteScript(int a, int b)
    {
        try
        {
            var options = ScriptOptions.Default.AddReferences(nameof(CampEvaluate), nameof(Runner));
            string inputSript = GetScript(a, b);
            Execute(inputSript, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    ///! 문의에 대한 답변 파트 
    ///! 3개의 주제별 구문 실행데모
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static string GetScript(int a = 10, int b = 20) =>
    $$""""
        using CampEvaluate;

        //! (Example: 2 - 1) 구문 처리
        System.Console.WriteLine("For Loop Statement");
        for (int i = 0; i < 10; i++)
        {
            System.Console.WriteLine($"How Are You? ^ ({(i + 1):00}) ^ Fine Thanks And You?");
        }

        //! (Example: 2 - 2) 델리게이트 처리
        System.Console.WriteLine("Action Delegate");
        var action = () => System.Console.WriteLine($"{{a}} / {{b}} = {{(a / b):n0}}\n{{a}} * {{b}} = {{(a * b):n0}}");
        action();

        //! (Example: 2 - 3) 메서드 호출
        Runner.CallMethod({{a}}, {{b}});
    """";

    public static async void Execute(string code, ScriptOptions options)
        => await CSharpScript.EvaluateAsync(code, options);

    public static void CallMethod(int a, int b)
        => Console.WriteLine($"{a} % {b} = {(a % b):n0}");
}

