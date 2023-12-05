using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoopGame.UControls
{
    internal class UvPoop : Panel
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }

        private Panel _ground;
        public UvPoop(Image image, Panel ground)
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint |  ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
            _ground = ground;
            BackgroundImage = image;
            BackgroundImageLayout = ImageLayout.Stretch;
            BackColor = Color.Transparent;
            Size = new Size(image.Size.Width / 10, image.Size.Height / 10);
        }

        private void UvPoop_Move(object sender, EventArgs e)
        {
            if (!(sender is Panel p)) return;

            if (p.Location.Y == _ground.Height / 2)
                p.Dispose();
        }


    }
}
