

namespace WpfSockets.Models
{
    public class TranferInfomation(int id, string fileName, string tranType, float rate)
    {
        public int Id { get; set; } = id;
        public string FileName { get; set; } = fileName;
        public string TranType { get; set; } = tranType;
        public float Rate { get; set; } = rate;
    }
}
