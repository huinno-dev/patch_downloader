using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomLabel
{
    public class RoundLabel : Label
    {

        public int cornerRadius = 30; //���� �ʺ�
        public Color borderColor = System.Drawing.Color.FromArgb((int)0xd6, (int)0xd6, (int)0xd6);//�ܰ��� ����
        public int borderWidth = 1;//�ܰ��� �β�
        public Color backColor = Color.White;//��� ����

        public bool isFillLeftTop = false;//������ �簢���� ä���(���� ����X)
        public bool isFillRightTop = false;//�������� �簢���� ä���(���� ����X)
        public bool isFillLeftBtm = false;//���ʾƷ� �簢���� ä���(���� ����X)
        public bool isFillRightBtm = false;//�����ʾƷ� �簢���� ä���(���� ����X)

        //int diminisher = 1;

        public RoundLabel()
        {
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var graphicsPath = _getRoundRectangle(this.ClientRectangle))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                var brush = new SolidBrush(backColor);
                var pen = new Pen(borderColor, borderWidth);
                e.Graphics.FillPath(brush, graphicsPath);
                e.Graphics.DrawPath(pen, graphicsPath);

                TextRenderer.DrawText(e.Graphics, Text, this.Font, this.ClientRectangle, this.ForeColor);
            }
        }

        private GraphicsPath _getRoundRectangle(Rectangle rectangle)
        {
            GraphicsPath path = new GraphicsPath();

            //path.AddArc(rectangle.X, rectangle.Y, cornerRadius, cornerRadius, 180, 90);

            int left = rectangle.X;
            int top = rectangle.Y;
            int right = rectangle.X + rectangle.Width - borderWidth;
            int bottom = rectangle.Y + rectangle.Height - borderWidth;

            if (isFillLeftTop)
            {//�»�
                path.AddLine(left, top + cornerRadius, left, top);
                path.AddLine(left, top, left + cornerRadius, top);
            }
            else
            {
                path.AddArc(rectangle.X, rectangle.Y, cornerRadius, cornerRadius, 180, 90);
            }
            if (isFillRightTop)
            {//���
                path.AddLine(right - cornerRadius, top, right, top);
                path.AddLine(right, top, right, top + cornerRadius);
            }
            else
            {
                path.AddArc(rectangle.X + rectangle.Width - cornerRadius - borderWidth, rectangle.Y, cornerRadius, cornerRadius, 270, 90);
            }
            if (isFillRightBtm)
            {//����
                path.AddLine(right, bottom - cornerRadius, right, bottom);
                path.AddLine(right, bottom, right - cornerRadius, bottom);
            }
            else
            {
                path.AddArc(rectangle.X + rectangle.Width - cornerRadius - borderWidth, rectangle.Y + rectangle.Height - cornerRadius - borderWidth, cornerRadius, cornerRadius, 0, 90);
            }
            if (isFillLeftBtm)
            {//����
                path.AddLine(left + cornerRadius, bottom, left, bottom);
                path.AddLine(left, bottom, left, bottom - cornerRadius);
            }
            else
            {
                path.AddArc(rectangle.X, rectangle.Y + rectangle.Height - cornerRadius - borderWidth, cornerRadius, cornerRadius, 90, 90);
            }

            path.CloseAllFigures();

            return path;
        }
    }
}