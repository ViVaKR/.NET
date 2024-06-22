namespace Camp.Start;

public class StringInterpolation
{
    public static void Examples()
    {

        //? 복수라인 표현식
        int score = 88;
        string s = $@"점수 = {score}, 학점={score switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
        }}";
        Console.WriteLine(s);

        //! 문자열 리터럴 이중인용부호(double quote)
        //! 복수 라인에 걸쳐 있는 경우 이중인용부호 앞에 @ 사인을 넣어 복수 라인임을 표시하였다.
        //? 3중 인용부호

        string str = """
        {
            "Order": "Latter",
            "Age": 56
        }
        """;

        Console.WriteLine(str);

        //? 4개 인용부호 : 3중 인용 부호 표현하기
        str = """"
            안녕하세요 """ 반갑습니다."""
        """";
        Console.WriteLine(str);

        //? 이중 $ 사인
        int x = 100;
        int y = 200;
        str = $"""Point: {x}, {y} """;
        Console.WriteLine(str);

        //? 이중 $ 사인으로 감싸는 대괄호 표현 하기
        str = $$"""Point: {{{x}}, {{y}}}""";
        Console.WriteLine(str);
    }
}
