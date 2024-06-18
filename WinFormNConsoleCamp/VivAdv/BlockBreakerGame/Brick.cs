
using System.Drawing;
using System.Windows.Forms;

namespace BlockBreakerGame
{
    public class Brick :Panel
    {
        public int Id { get; set; }

        public Rectangle Rect { get; set; }

        public int Health { get; set; }
    }
}
