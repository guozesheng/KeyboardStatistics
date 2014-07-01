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

        private int getKeyNum()
        {
            int max = 0;
            int num = 0;

            for (int i = 0; i < _keyRecord.Key.Length; i++)
            {
                if (max < _keyRecord.Key[i])
                {
                    max = _keyRecord.Key[i];
                    num = i;
                }
            }

            return num;
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
            int maxIndex = getKeyNum();
            int maxhCF = _keyRecord.Key[maxIndex];
            int panHCF = panel1.Height - bottom - top;

            // 没有记录任何按键
            if (maxhCF <= 0)
            {
                return;
            }

            for (int i = 0; i < _keyRecord.Key.Length; i++)
            {
                x = left + w * i * 2;
                h = _keyRecord.Key[i] * panHCF / maxhCF;
                y = panel1.Height - bottom - h;

                g.FillRectangle(new SolidBrush(c), x, y, w, h);
                char key = (char)(i + 65);
                g.DrawString(key.ToString(), sFont, sBrush, x - 5, panel1.Height - bottom);
            }
        }

        private void showForm()
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.notifyIcon1.Visible = false;
            this.Activate();
        }

        private void hideForm()
        {
            this.Visible = false;
            this.ShowInTaskbar = false;
            this.notifyIcon1.Visible = true;
        }

        private void _btnRun_Click(object sender, EventArgs e)
        {
            drawImg();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _boardHook.Hook_Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _boardHook.Hook_Clear();
        }

        private void _tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            showForm();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                hideForm();
            }
        }

        private void _tsmiShow_Click(object sender, EventArgs e)
        {
            showForm();
        }
    }
}
