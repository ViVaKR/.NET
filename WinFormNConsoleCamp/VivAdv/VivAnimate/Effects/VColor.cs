
namespace VivAnimate.Effects
{
    [Flags]
    public enum VColor
    {
        None = 0,
        Black = 1,
        Red = Black << 1,
        Green = Black << 2,
        Blue = Black << 3,

        All = int.MaxValue
    }

    [Flags]
    public enum MultiHue : short
    {
        None = 0,
        Black = 1,
        Red = 2,
        Green = 4,
        Blue = 8,
    }
}

// 변수 선언
// VColor colr = VColor.Black | VColor.Green | VColor.Blue

// 사용 예시
// if((color & VColor.Green) != 0) { }
// if(color & VColor.Green) == VColor.Green) { }

// 특정 플래그를 변수에 추가
// color |= VColor.Red;

// 특정 플래그를 변수에서 삭제하기
// color &= ~VColor.Red;


// 모든 플래그 초기화 : color = VColor.None.
// 모든 플래그 설정 : color = VColor.All


