using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CustomComboBox
{
    public class BorderCombobox : ComboBox
    {
        private const int WM_PAINT = 0xF;
        private int buttonWidth = SystemInformation.HorizontalScrollBarArrowWidth;
        Color borderColor = Color.Blue;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);
        private const Int32 CB_SETITEMHEIGHT = 0x153;

        public void SetComboBoxHeight(IntPtr comboBoxHandle, Int32 comboBoxDesiredHeight)
        {
            SendMessage(comboBoxHandle, CB_SETITEMHEIGHT, -1, comboBoxDesiredHeight);
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT && DropDownStyle != ComboBoxStyle.Simple)
            {
                using (var g = Graphics.FromHwnd(Handle))
                {
                    using (var p = new Pen(BorderColor))
                    {
                        g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);

                        var d = FlatStyle == FlatStyle.Popup ? 1 : 0;
                        g.DrawLine(p, Width - buttonWidth - d,
                            0, Width - buttonWidth - d, Height);
                    }
                }
            }
        }
    }
}