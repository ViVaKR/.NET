

namespace KorPublicAPI
{
    public class MyGridView : DataGridView
    {
        public MyGridView()
        {
            Dock = DockStyle.Fill;
            DoubleBuffered = true;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
