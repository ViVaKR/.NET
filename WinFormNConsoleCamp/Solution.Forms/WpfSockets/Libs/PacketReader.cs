
using System.IO;


namespace WpfSockets.Libs
{
    public class PacketReader(byte[] data) : BinaryReader(new MemoryStream(data)) { }


}
