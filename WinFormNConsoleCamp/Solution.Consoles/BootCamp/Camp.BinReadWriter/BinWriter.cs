using System.Runtime.InteropServices;
using System.Text;

namespace Camp.BinReadWriter;

public class BinWriter() : IBinWriter
{
    public void Write(int id, string path, double grade, int score, bool isAttending)
    {
        using var stream = File.Open("AppSettings.dat", FileMode.OpenOrCreate);
        using var writer = new BinaryWriter(stream, Encoding.UTF8, false);
        writer.Write(id);
        writer.Write(path);
        writer.Write(grade);
        writer.Write(score);
        writer.Write(isAttending);
    }
}
