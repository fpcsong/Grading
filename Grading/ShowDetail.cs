using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Grading
{
    public partial class ShowDetail : Form
    {
        public ShowDetail()
        {
            InitializeComponent();
        }

        private void ShowDetail_Resize(object sender, EventArgs e)
        {
            panelTeacher.Height = this.Size.Height / 2 - panelTeacher.Location.X - 70;
            panelStudent.Location = new System.Drawing.Point(panelStudent.Location.X, panelTeacher.Height + panelTeacher.Location.Y + 50);
            panelStudent.Height = panelTeacher.Height;
            label3.Location = new System.Drawing.Point(panelStudent.Location.X, panelStudent.Location.Y - 35);
            label1.Left = (this.Width - label1.Width) / 2;
            label10.Left = (this.Width / 2 - label10.Size.Width + 10);
            finalScore.Left = (this.Width / 2 + 10);
            //labelTitle.Left = (this.Width - labelTitle.Width) / 2;
            int width = panelStudent.Size.Width;
            int cnt = 0;
            foreach (Control label in panelTeacher.Controls)
            {
                if (label is Label)
                {
                    label.Location = new Point(10 + (cnt % 5) * (width / 5), 10 + (cnt / 5) * 50);
                    cnt++;
                }
            }
            cnt = 0;
            foreach (Control textBox in panelTeacher.Controls)
            {
                if (textBox is TextBox)
                {
                    textBox.Location = new Point(65 + (cnt % 5) * (width / 5), 5 + (cnt / 5) * 50);
                    cnt++;
                }
            }
            cnt = 0;
            foreach (Control label in panelStudent.Controls)
            {
                if (label is Label)
                {
                    label.Location = new Point(10 + (cnt % 5) * (width / 5), 10 + (cnt / 5) * 50);
                    cnt++;
                }
            }
            cnt = 0;
            foreach (Control textBox in panelStudent.Controls)
            {
                if (textBox is TextBox)
                {
                    textBox.Location = new Point(65 + (cnt % 5) * (width / 5), 5 + (cnt / 5) * 50);
                    cnt++;
                }
            }
            int h = panelTeacher.Location.Y + 30;
            int h2 = panelStudent.Location.Y + 30;
            int x1 = label4.Location.X;
            int x2 = teacMaxScore.Location.X;
            label4.Location = new Point(x1, h);
            teacMaxScore.Location = new Point(x2, h);
            label6.Location = new Point(x1, h + 50);
            teacMinScore.Location = new Point(x2, h + 50);
            label8.Location = new Point(x1, h + 100);
            teacArgv.Location = new Point(x2, h + 100);

            label5.Location = new Point(x1, h2);
            studMaxScore.Location = new Point(x2, h2);
            label7.Location = new Point(x1, h2 + 50);
            studMinScore.Location = new Point(x2, h2 + 50);
            label9.Location = new Point(x1, h2 + 100);
            studArgv.Location = new Point(x2, h2 + 100);
            this.Refresh();

        }

        private void ShowDetail_Load(object sender, EventArgs e)
        {
            panelTeacher.Height = this.Size.Height / 2 - panelTeacher.Location.X - 70;
            panelStudent.Location = new System.Drawing.Point(panelStudent.Location.X, panelTeacher.Height + panelTeacher.Location.Y + 50);
            panelStudent.Height = panelTeacher.Height;
            label3.Location = new System.Drawing.Point(panelStudent.Location.X, panelStudent.Location.Y - 35);
            label1.Left = (this.Width - label1.Width) / 2;
            label10.Left = (this.Width / 2 - label10.Size.Width + 10);
            finalScore.Left = (this.Width / 2 + 10);
            //labelTitle.Left = (this.Width - labelTitle.Width) / 2;
            int width = panelStudent.Size.Width;
            int cnt = 0;
            foreach (Control label in panelTeacher.Controls)
            {
                if (label is Label)
                {
                    label.Location = new Point(10 + (cnt % 5) * (width / 5), 10 + (cnt / 5) * 50);
                    cnt++;
                }
            }
            cnt = 0;
            foreach (Control textBox in panelTeacher.Controls)
            {
                if (textBox is TextBox)
                {
                    textBox.Location = new Point(65 + (cnt % 5) * (width / 5), 5 + (cnt / 5) * 50);
                    cnt++;
                }
            }
            cnt = 0;
            foreach (Control label in panelStudent.Controls)
            {
                if (label is Label)
                {
                    label.Location = new Point(10 + (cnt % 5) * (width / 5), 10 + (cnt / 5) * 50);
                    cnt++;
                }
            }
            cnt = 0;
            foreach (Control textBox in panelStudent.Controls)
            {
                if (textBox is TextBox)
                {
                    textBox.Location = new Point(65 + (cnt % 5) * (width / 5), 5 + (cnt / 5) * 50);
                    cnt++;
                }
            }
            int h = panelTeacher.Location.Y + 30;
            int h2 = panelStudent.Location.Y + 30;
            int x1 = label4.Location.X;
            int x2 = teacMaxScore.Location.X;
            label4.Location = new Point(x1, h);
            teacMaxScore.Location = new Point(x2, h);
            label6.Location = new Point(x1, h + 50);
            teacMinScore.Location = new Point(x2, h + 50);
            label8.Location = new Point(x1, h + 100);
            teacArgv.Location = new Point(x2, h + 100);

            label5.Location = new Point(x1, h2);
            studMaxScore.Location = new Point(x2, h2);
            label7.Location = new Point(x1, h2 + 50);
            studMinScore.Location = new Point(x2, h2 + 50);
            label9.Location = new Point(x1, h2 + 100);
            studArgv.Location = new Point(x2, h2 + 100);
            this.Refresh();
        }

        private void ShowDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            Bitmap bit = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bit);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.CopyFromScreen(this.Left, this.Top, 0, 0, new Size(this.Width, this.Height));
            string path = Application.StartupPath + "\\image\\";
            if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
             bit.Save(path + label1.Text.ToString() + ".png");
        }
    }
}
