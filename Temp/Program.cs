
int year = 3000;

string errorMessage = "잘못된 입력입니다.";

for (int i = 1; i <= 12; i++)
{
    // 정상 테스트 ( 1월 ~ 12월 )
    var check = MonthDays(i, year) == -1 ? errorMessage : MonthDays(i, year).ToString();
    string message = $"{year} 년도 {i,5}월 날짜수 {check}";
    Console.WriteLine(message);

    // 비정상 테스트 ( 1 + 123월 ~ 1 + 123월)
    check = MonthDays(i + 123, year) == -1 ? errorMessage : MonthDays(i + 123, year).ToString();
    message = $"{year} 년도 {i + 123,5}월 날짜수 {check}";
    Console.WriteLine(message);
}

static int MonthDays(int month, int year)
    => !Enumerable.Range(1, 12).Any(x => x == month) ? -1 : DateTime.DaysInMonth(year, month);
