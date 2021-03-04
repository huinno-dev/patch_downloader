using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomProgressBar
{
    public partial class RoundProgressBar : PictureBox
    {
        double pbUnit;
        int pbWIDTH, pbHEIGHT, pbComplete;

        Bitmap bmp;
        Graphics g;

        public RoundProgressBar()
        {
            DoubleBuffered = true;

            //% [min = 0 max = 100]
            pbComplete = 0;
        }

        public int Value { get; set; }

        [Category("Appearance")]
        public Color ProgressBarColor { get; set; }

        [Category("Appearance")]
        public Color ProgressBackColor { get; set; }

        [Category("Appearance")]
        public Font ProgressFont { get; set; }

        [Category("Appearance")]
        public Color ProgressFontColor { get; set; }


        private GraphicsPath GetRoundRectagle(Rectangle bounds)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = 30; // bounds.Height;
            if (bounds.Height <= 0) radius = 20;
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius,
                        radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }
        private void RecreateRegion()
        {
            var bounds = new Rectangle(this.ClientRectangle.Location, this.ClientRectangle.Size);
            bounds.Inflate(-1, -1);
            this.Region = new Region(GetRoundRectagle(bounds));
            this.Invalidate();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.RecreateRegion();
        }

        public void DrawProgress(int val)
        {
            if (val == 0)
            {
                pbWIDTH = this.Width;
                pbHEIGHT = this.Height;

                pbUnit = pbWIDTH / 100.0;

                bmp = new Bitmap(pbWIDTH, pbHEIGHT);
            }

            ProgressFont = new Font(Font.FontFamily, (int)(this.Height * 0.3), FontStyle.Bold);

            pbComplete = val;

            //graphics
            g = Graphics.FromImage(bmp);

            //clear graphics
            g.Clear(ProgressBackColor);

            //draw progressbar
            g.FillRectangle(Brushes.LimeGreen, new Rectangle(0, 0, (int)(pbComplete * pbUnit), pbHEIGHT));

            //draw string
            g.DrawString(pbComplete + "%", new Font(ProgressFont.FontFamily, (int)(pbHEIGHT * 0.3), ProgressFont.Style), new SolidBrush(ProgressFontColor), new PointF(pbWIDTH / 25, pbHEIGHT / 3));

            //load bitmap in picturebox picboxPB
            this.Image = bmp;

            Refresh();

            //pbComplete++;
            if (pbComplete > 100)
            {
                //dispose
                g.Dispose();
                //this.t.Stop();
            }

            //Console.WriteLine(String.Format("{0} {1}", pbComplete, (int)(pbComplete * pbUnit)));
        }
    }
}