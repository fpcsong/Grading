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
            PublicMethods.title = textBoxTitle.Text.ToString();
            if (int.TryParse(textBoxStudentNum.Text.ToString(),out PublicMethods.stuCount) == false)
            {
                MessageBox.Show("学生人数不正确");
                return;
            }
            if (int.TryParse(textBoxTeacherNum.Text.ToString(),out PublicMethods.teacherCount) == false)
            {
                MessageBox.Show("老师人数不正确");
                return;
            }
            //PublicSettings.ifShowInTheSecdisplay = checkBox.Checked;
            PublicMethods.StoreSettings();
            Thread.Sleep(200);
            this.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            textBoxTitle.Text = PublicMethods.title;
            textBoxTeacherNum.Text = PublicMethods.teacherCount.ToString();
            textBoxStudentNum.Text = PublicMethods.stuCount.ToString();
            //checkBox.Checked = PublicSettings.ifShowInTheSecdisplay;
        }
    }
}
