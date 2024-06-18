
namespace DelegateBasic
{
    public class Playground
    {
        delegate void LogDel(string text, DateTime dateTime);

        public void Run()
        {
            Log log = new();

            LogDel multiLogDel;

            multiLogDel = new LogDel(log.LogTextToScreen) + new LogDel(log.LogTextToFile);

            Console.Write("Please Enter your name: ");

            LogText(multiLogDel, Console.ReadLine() ?? "Viv");
        }

        private void LogText(LogDel logDel, string text)
        {
            logDel(text, DateTime.Now);
        }
    }
}

// Short Cut//
// Duplicat Line : Shift + Enter
// Multi Select Word : ALT + Shift + ;
// Refactor.ExtractMethod : Ctrl + R, Ctrl + M
// Edit.ToggleAllOutlining : Ctrl + M, Ctrl + L
// Edit.ToggleOutliningExpansion : Ctrl + M, Ctrl + M
// File.Rename : F2
// ALT + Shift + 방향키
// Collapse to definitions : Ctrl+M, Ctrl+O
// Format Document : Ctrl+K, Ctrl+D
// Go To declaration : Ctrl+F12
// Go To definition : F12
// Go to find combo : Ctrl+D
// Insert snippet : Ctrl+K, Ctrl+X
// Line cut : Ctrl+L
// Line down extend column : Shift+Alt+Down Arrow
// List Member : Ctrl + J
// Select current word : Ctrl + W
// Edit.SurroundWith : Ctrl + K, Ctrl + S
// Block Commnet : Alt + Shift + A


