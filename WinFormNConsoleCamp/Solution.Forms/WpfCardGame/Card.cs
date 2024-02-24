
namespace WpfCardGame;

    public class Card(Suits suit, Values value)
    {
        public Suits Suit { get; set; } = suit;
        public Values Value { get; set; } = value;
    }

    public enum Suits : long
    {
        하트 = '\u2661',
        다이아몬드 = '\u2662',
        스페이드 = '\u2664',
        클로바 = '\u2667'
    }

    public enum Values
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
