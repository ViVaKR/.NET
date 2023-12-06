
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BlockBreakerGame
{
    public class UPanel : Panel
    {
        public PictureBox Racket { get; set; }
        public UPanel()
        {
            // Paint += handler;
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(25, 0, 255, 0);
            Racket = new PictureBox
            {
                Width = 300,
                Height = 10,
                BackColor = Color.DarkOrange,
                BorderStyle = BorderStyle.None,
                
            };
            Controls.Add(Racket);

        }


    }
}
