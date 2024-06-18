using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoopGame.UControls
{
    internal class UButton : Button
    {
        public UButton(string text, object tag)
        {
            Name = $"Button_{tag}";
            Text = text.Trim();
            Height = 150;
            Dock = DockStyle.Top;
            Font = new Font(Font.FontFamily, 32F, FontStyle.Regular);
            ForeColor = Color.Teal;
            TextAlign = ContentAlignment.MiddleCenter;
            Margin = new Padding(5);
        }
    }
}
