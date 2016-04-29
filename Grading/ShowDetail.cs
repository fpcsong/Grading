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
            panelTeacher.Height = this.Size.Height / 2 - panelTeacher.Location.X - 50;
            panelStudent.Location = new System.Drawing.Point(panelStudent.Location.X, this.Height / 2 + 30);
            panelStudent.Height = panelTeacher.Height;
            label3.Location = new System.Drawing.Point(panelStudent.Location.X, panelStudent.Location.Y - 35);
            label1.Left = (this.Width - label1.Width) / 2;
            label10.Left = (this.Width / 2 - label10.Size.Width + 10);
            finalScore.Left = (this.Width / 2 + 10);

        }

        private void ShowDetail_Load(object sender, EventArgs e)
        {
            label1.Left = (this.Width - label1.Width) / 2;
            label10.Left = (this.Width / 2 - label10.Size.Width + 10);
            finalScore.Left = (this.Width / 2 + 10);
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
