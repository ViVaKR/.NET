// Encapsulation

using PatternConsole;

BadBankAccount badAccount = new()
{
    balance = 1000
};


Console.WriteLine($"Bad account balance: {badAccount.balance}");
