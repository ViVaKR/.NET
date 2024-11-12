
namespace PatternConsole;

public class BankAccount
{
    private decimal balance;

    // public decimal Balance
    // {
    //     get => balance;
    //     set
    //     {
    //         if (value < 0)
    //         {
    //             throw new ArgumentOutOfRangeException(nameof(value), "Balance cannot be negative");
    //         }
    //         balance = value;
    //     }
    // }

    public BankAccount(decimal balance)
    {
        Deposit(balance);
    }

    private static void Deposit(decimal balance)
    {
        if (balance < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(balance), "Balance cannot be negative");
        }
    }
}
