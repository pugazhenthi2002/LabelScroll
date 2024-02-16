using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Practice
{
    internal class TextBindScrollBar : UserControl
    {

        public int ContentHeight
        {
            get
            {
                return contentHeight;
            }

            set
            {
                contentHeight = value;
                SetThumbHeight();
                this.Invalidate();
                OnPaint(new PaintEventArgs(this.CreateGraphics(), ClientRectangle));
            }
        }

        public void SetThumbRectangle(int y)
        {
            thumbRectangle = new Rectangle(2, thumbRectangle.Y+y, 16, (int)thumbHeight);
            OnPaint(new PaintEventArgs(this.CreateGraphics(), ClientRectangle));
        }
        public void SetThumbHeight()
        {

            float viewableRatio = Height / (float)contentHeight;

            var scrollBarArea = Height - 40; 

            thumbHeight = scrollBarArea * viewableRatio; 

            var scrollTrackSpace = contentHeight - Height; 
            var scrollThumbSpace = Height - thumbHeight;
            scrollJump = (int)Math.Ceiling(scrollTrackSpace / scrollThumbSpace);

            thumbRectangle = new Rectangle(2, 20, 16, (int)thumbHeight);

        }

        protected override void OnLoad(EventArgs e)
        {
            Height = Parent.Height;
            Width = 20;
            thumbRectangle = new Rectangle(2, 20, 16, Height - 40);
            Dock = DockStyle.Right;
        }

        protected override void OnResize(EventArgs e)
        {
            Height = Parent.Height;
            Width = 20;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Point[] upArrow = new Point[] { new Point(2, 18), new Point(18, 18), new Point(10, 2) };
            Point[] downArrow = new Point[] { new Point(2, Height - 18), new Point(18, Height - 18), new Point(10, Height - 2) };
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillPolygon(scrollBrush, upArrow);
            g.FillPolygon(scrollBrush, downArrow);
            g.FillRectangle(scrollBrush, thumbRectangle);
            g.DrawRectangle(new Pen(Color.Black), thumbRectangle);
        }
        private float thumbHeight;
        private int contentHeight;
        public int scrollJump;
        private Brush scrollBrush = new SolidBrush(Color.Blue);
        public Rectangle thumbRectangle;
    }
}
