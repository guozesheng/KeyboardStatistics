using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeyboardStatistics
{
    public partial class Form1 : Form
    {
        private KeyBoardHook _boardHook;
        private KeyRecord _keyRecord;

        public Form1()
        {
            InitializeComponent();

            _boardHook = new KeyBoardHook();
            _keyRecord = KeyRecord.Get();
        }

        private void drawImg()
        {
            int top = 10;
            int bottom = 30;
            int left = 10;
            int x;
            int y;
            int w = 9;
            int h;
            Color c = Color.Red;
            Font sFont = new Font("Arial", 13);
            SolidBrush sBrush = new SolidBrush(Color.Black);

            Graphics g = panel1.CreateGraphics();

            for (int i = 0; i < _keyRecord.Key.Length; i++)
            {
                x = left + w * i * 2;
                y = top;
                h = panel1.Height - bottom - top;

                g.FillRectangle(new SolidBrush(c), x, y, w, h);
                char key = (char)(i + 65);
                g.DrawString(key.ToString(), sFont, sBrush, x - 5, panel1.Height - bottom);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _boardHook.Hook_Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _boardHook.Hook_Clear();
        }

        private void _btnRun_Click(object sender, EventArgs e)
        {
            drawImg();
        }
    }
}
