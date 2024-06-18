
using System.IO;


namespace WpfSockets.Libs
{
    public class PacketWriter : BinaryWriter
    {
        private readonly MemoryStream ms;
        public PacketWriter()
        {
            ms = new MemoryStream();
        }

        public byte[] GetBytes()
        {
            Close();

            return ms.ToArray();
        }
    }
}
