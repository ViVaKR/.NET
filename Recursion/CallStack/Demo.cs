using System;

namespace CallStack;

public class Demo
{

  public static int BinarySearch(int[] A, int left, int right, int x)
  {
    if (left > right) return -1;

    int mid = left + (right - left) / 2;

    if (x == A[mid]) return mid;

    if (x < A[mid])
      return BinarySearch(A, left, mid - 1, x);

    return BinarySearch(A, mid + 1, right, x);
  }

  // Sum of Natural Numbers
  public static int SumOfNumbers(int number)
  {
    if (number <= 1) return number;
    return number + (SumOfNumbers(number - 1));
  }

  // Decimal To Binary
  public static string DecimalToBinary(int number, string result)
  {
    if (number == 0) return result;

    // --> 233 / 2 = 116 rem 1;
    // result = 116 + string.Empty
    var rem = number % 2;
    var rs = string.Concat(rem, result);
    return DecimalToBinary(number / 2, rs);
  }

  // Palindrome (회전문)
  // 앞뒤를 변경해도 같은 문장 : kayak
  public static bool IsPalindrome(string input)
  {
    if (input.Length == 0 || input.Length == 1)
      return true;

    if (input[0] == input[^1])
      return IsPalindrome(input[1..^1]);
    return false;
  }


  // Reverse String
  public static string ReverseString(string input)
  {
    // base case
    if (string.IsNullOrWhiteSpace(input))
      return string.Empty;

    // smallest
    return ReverseString(input[1..]) + input[0];
  }
  public string A()
  {
    return "hello " + B();
  }

  public string B()
  {
    return "my " + C();
  }

  public static string C()
  {
    return "friends.";
  }
}
