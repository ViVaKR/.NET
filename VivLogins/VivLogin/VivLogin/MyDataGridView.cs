
using System.Drawing;
using System.Windows.Forms;

namespace VivLogin
{
    public class MyDataGridView  : DataGridView
    {

        public MyDataGridView()
        {
            DoubleBuffered = true;

            Dock = DockStyle.Fill;
            Font = new Font(Font.FontFamily, 14f, FontStyle.Regular);
            ForeColor = Color.Black;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
