using System;
using System.Windows.Forms;
using System.Threading;

namespace Grading
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PublicSettings.title = textBoxTitle.Text.ToString();
            if (int.TryParse(textBoxStudentNum.Text.ToString(),out PublicSettings.stuCount) == false)
            {
                MessageBox.Show("学生人数不正确");
                return;
            }
            if (int.TryParse(textBoxTeacherNum.Text.ToString(),out PublicSettings.teacherCount) == false)
            {
                MessageBox.Show("老师人数不正确");
                return;
            }
            //PublicSettings.ifShowInTheSecdisplay = checkBox.Checked;
            PublicSettings.StoreSettings();
            Thread.Sleep(200);
            this.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            textBoxTitle.Text = PublicSettings.title;
            textBoxTeacherNum.Text = PublicSettings.teacherCount.ToString();
            textBoxStudentNum.Text = PublicSettings.stuCount.ToString();
            //checkBox.Checked = PublicSettings.ifShowInTheSecdisplay;
        }
    }
}
