using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMML_Launcher
{
    public class CustomProgressBar : ProgressBar
    {
        public CustomProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = new Rectangle(0, 0, Width, Height);
            double scaleFactor = ((double)Value - Minimum) / ((double)Maximum - Minimum);
            SolidBrush backBrush = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(backBrush, rec);
            rec.Width = (int)(rec.Width * scaleFactor);
            SolidBrush brush = new SolidBrush(ForeColor);
            e.Graphics.FillRectangle(brush, rec);
        }
    }
}
