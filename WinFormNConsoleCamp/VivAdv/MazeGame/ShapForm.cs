using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace MazeGame
{
    public partial class ShapForm : Form
    {

        public ShapForm()
        {
            InitializeComponent();

            FormClosed += (s, e) =>
            {
                if (Application.OpenForms.OfType<Form>().Any(x => x.Name == nameof(MainForm)))
                {
                    var m = Application.OpenForms.OfType<Form>().Single(x => x.Name == nameof(MainForm));
                    m.Close();
                }
            };
            Paint += ShapForm_Paint;

            Width = 2000;
            Height = 1600;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void ShapForm_Paint(object? sender, PaintEventArgs e)
        {
            // Translate transformation matrix.
            e.Graphics.TranslateTransform(150, 0);

            // Save translated graphics state.
            GraphicsState transState = e.Graphics.Save();

            // Reset transformation matrix to identity and fill rectangle.
            e.Graphics.ResetTransform();
            e.Graphics.FillRectangle(new SolidBrush(Color.Red), 0, 0, 100, 100);

            // Restore graphics state to translated state and fill second

            // rectangle.
            e.Graphics.Restore(transState);
            e.Graphics.FillRectangle(new SolidBrush(Color.Blue), 0, 0, 100, 100);
        }
    }
}
