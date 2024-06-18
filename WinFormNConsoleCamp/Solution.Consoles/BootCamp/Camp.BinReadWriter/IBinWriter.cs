namespace Camp.BinReadWriter;

public interface IBinWriter
{
    void Write(int id, string path, double grade, int score, bool isAttending);
}
