using System.Text;

namespace Camp.BinReadWriter;

public class BinReader : IBinReader
{
    private int id;
    private string? path;
    private double grade;
    private int score;
    private bool isAttending;

    public void Read()
    {
        using var stream = File.Open("AppSettings.dat", FileMode.Open);
        using var fs = new BinaryReader(stream, Encoding.UTF8, false);

        id = fs.ReadInt32();
        path = fs.ReadString();
        grade = fs.ReadDouble();
        score = fs.ReadInt32();
        isAttending = fs.ReadBoolean();
        
        Console.WriteLine($"\n\nID: {id}, Path: {path}, Grade: {grade}, Score: {score}, IsAttending: {isAttending}");
        Console.WriteLine(Environment.NewLine);
    }
}
