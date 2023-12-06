// -2^31 through 2^31 -1
// 1 1 1 1 (-1)
// 1 0 0 0 (-8)

// 0 0 0 1 (1)
// 0 1 1 1 (7)

int min = int.MinValue;
Console.WriteLine(Convert.ToString(min, toBase: 2));

int max = int.MaxValue;
Console.WriteLine(Convert.ToString(max, toBase: 2));

int bmin = byte.MinValue;
Console.WriteLine(Convert.ToString(bmin, toBase: 2));

int a = 0b_1101;
Console.WriteLine(Convert.ToString(a, toBase: 2));


// XOR //
// 0 과 xor 연산 : 항상 원래의 값이 나옴
// 1 과 xor 연산 : 항상 반대의 값 not 연산과 동일
// 같은 값으로 xor  연산 : 모두 0으로 바뀜
int xor = 0b_0101;
int o = 0b_0000;
int l = 0b_1111;
int rs0 = xor ^ xor;
int rs1 = xor ^ o;
int rs2 = xor ^ l;
Console.WriteLine("\nxor 연산");
Console.WriteLine($"{Convert.ToString(rs0, 2).PadLeft(4, '0')}"); // 0으로 셋팅하기
Console.WriteLine($"({Convert.ToString(xor, 2).PadLeft(4, '0')}) => {Convert.ToString(rs1, 2).PadLeft(4, '0')} - {Convert.ToString(rs2, 2).PadLeft(4, '0')}"); //.Write(bit);

// AND //
// 0과 and 연산 : 항상 0으로 셋팅
// 1과 and 연산 : 항상 원본 값이 나옴
// 자기 자신 과 연산 : 항상 자기 자신이 나옴
int and = 0b_1010;
rs0 = and & 0b_0000;
Console.WriteLine("\n& 연산");
Console.WriteLine($"{Convert.ToString(rs0, 2).PadLeft(4, '0')}"); // 모두 0으로 셋팅

// OR //
// 0과 연산 : 항상 원래의 값이 나옴
// 1과 연산 : 모두 1로 셋팅됨
// 자기 자신과 연산 : 자기 자신이 나옴


// Shift //
rs0 = 0b_1111 >>> 2;
Console.WriteLine(rs0.ToString());
Console.WriteLine(Convert.ToString(rs0, 2).PadLeft(0, '0'));

Console.WriteLine(180 / Math.PI);
Console.WriteLine(Math.PI / 180);

// Logical Right Shift (>>>) // 
sbyte nShift = -0b_1000_0000;
sbyte result = (sbyte)(nShift >>> 1);

// 0b_1100_0000
Console.WriteLine($"{nShift} {result:b}");

// Arithmetic Right Shift (>>) //
result = (sbyte)(nShift >> 2);
Console.WriteLine($"{nShift} {result:b} {result}");

#region Get Bit

/* Get Bit */
Console.WriteLine(GetBit(9, 3)); // 1001
Console.WriteLine(GetBit(5, 3));
static bool GetBit(int num, int i)
{
    //   x x x x
    // & 1 0 0 0
    //   x 0 0 0
    // 1000 ( 1 << 3 )
    return (num & (1 << i)) != 0;
}
#endregion

#region SetBit

/* Set Bit */
Console.WriteLine(Convert.ToString(SetBit(5, 3), toBase: 2)); // 3번째 비트를 1로 세팅하기

static int SetBit(int num, int i)
{
    return (num | (1 << i));
}
#endregion

#region ClearBit

/* Clear Bit */
Console.WriteLine($"{Convert.ToString(ClearBit(13, 3), toBase: 2).PadLeft(4, '0')}");
static int ClearBit(int num, int i)
{
    // 1000
    // 0111 = ~1000
    return (num & (~(1 << i))); // ~1000 -> 0111
}
#endregion

#region ClearLeftBits
Console.WriteLine($"{Convert.ToString(234866, toBase: 2).PadLeft(32, '0')}");
Console.WriteLine($"{Convert.ToString(ClearLeftBits(234866, 7), toBase: 2).PadLeft(32, '0')}");

static int ClearLeftBits(int num, int i)
{
    // 인덱스 앞의 비트를 모두 0으로 만들기
    // 1000 = 1 << 3
    // 0111 = 1000 -1

    // Not 을 사용하지 않은 이유
    // ~001000 = 110111 이 됨으로 왼쪽도 변경됨
    // 그러나 1을 빼주면 
    // 001000 - 1 = 000111 이 됨으로 적절한 마스크가 생성됨

    return (num & ((1 << i) - 1));
}


#endregion

#region ClearRightBits

// 인덱스를 포함한 오른쪽 비트들을 클리어 하기
Console.WriteLine($"{Convert.ToString(12345, toBase: 2).PadLeft(32, '0')}");
Console.WriteLine(Convert.ToString(-1 << 3, toBase: 2).PadLeft(32, '0'));
int test = -32;
Console.WriteLine(Convert.ToString(test, toBase: 2).PadLeft(32, '0'));
Console.WriteLine($"{Convert.ToString(ClearRightBits(12345, 3), toBase: 2).PadLeft(32, '0')}");
static int ClearRightBits(int num, int i)
{
    // 모든 비트를 1로 세팅하는 것은 -1 임

    return (num & ((-1 << (i + 1))));
}
#endregion

#region Update Bit
int num = 123456;
Console.WriteLine(Convert.ToString(num, toBase: 2).PadLeft(32, '0'));
Console.WriteLine($"{Convert.ToString(UpdateBit(num, 3, true), toBase: 2).PadLeft(32, '0')}");
static int UpdateBit(int num, int i, bool val)
{
    // 해당 비트만 0으로 세팅하고
    // 나머지는 1로 세팅 한 후
    // & 연산을 하면 해당 비트는 0이 되고 나머는 원래의 값을 가짐
    // 그 결과를 가지고 해당 비트에 값을 넣고 나머지 비트를 0으로 한후
    // | 연산을 하면 해당 비트만 변경됨
    return (num & ~(1 << i)) | ((val ? 1 : 0) << i);
}
#endregion


#region 부동소수점

// 소숫점 2진수 변환 //
double number = 0.5625;
ConvertToBinary(number);
static void ConvertToBinary(double number)
{
    int intNumber = (int)Math.Truncate(number);
    double decimalNumber = number - intNumber;

    string intStr = intNumber == 0 ? "0" : ConvertIntToHex(intNumber);
    Console.Write($"\n({number}) => ");
    Console.WriteLine($"{intStr}.{ConvertDoubleToHex(decimalNumber, 10)}");
}

static string ConvertIntToHex(int n)
{
    if (n == 0) return string.Empty;
    int mod = n % 2;
    return $"{ConvertIntToHex(n / 2)}{mod}";
}

static string ConvertDoubleToHex(double n, int count)
{
    double mul = n * 2;
    if (mul == 1.0) return "1";
    if (count == 0) return string.Empty;
    double t = mul >= 1 ? 1 : 0;
    double g = mul - t;
    return $"{t}{ConvertDoubleToHex(g, count - 1)}";
}

#endregion