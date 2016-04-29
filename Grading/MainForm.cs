using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Grading
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (PublicSettings.LoadSettings() == false)
            {
                Settings settings = new Settings();
                settings.ShowDialog(this);
            }
            labelTitle.Text = PublicSettings.title;
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;
            PaintForm();
            
        }
        private void PaintForm()
        {
            int width = panelStudent.Size.Width;
            for (int i = 0; i < PublicSettings.teacherCount; i++)
            {
                Label label = new Label();
                label.Name = "teacherlabel" + (i + 1).ToString();
                label.Text = "教师" + (i + 1).ToString();
                label.Size = new Size(50, 15);
                label.Location = new Point(10 + (i % 5) * (width / 5), 10 + (i / 5) * 30);
                panelTeacher.Controls.Add(label);
                TextBox textBox = new TextBox();
                textBox.Name = "teacherTextbox" + (i + 1).ToString();
                textBox.Size = new Size(50, 15);
                textBox.Location = new Point(10 + label.Location.X + 40, 5 + (i / 5) * 30);
                panelTeacher.Controls.Add(textBox);

            }
            for (int i = 0; i < PublicSettings.stuCount; i++)
            {
                Label label = new Label();
                label.Name = "studentlabel" + (i + 1).ToString();
                label.Text = "学生" + (i + 1).ToString();
                label.Size = new Size(50, 15);
                label.Location = new Point(10 + (i % 5) * (width / 5), 10 + (i / 5) * 30);
                panelStudent.Controls.Add(label);
                TextBox textBox = new TextBox();
                textBox.Name = "studentTextbox" + (i + 1).ToString();
                textBox.Size = new Size(50, 15);
                textBox.Location = new Point(10 + label.Location.X + 40, 5 + (i / 5) * 30);
                panelStudent.Controls.Add(textBox);
            }
        }
        private void ClearData()
        {
            currTeacherName.Text = "";
            foreach(Control obj in panelTeacher.Controls)
            {
                if (obj is TextBox) ((TextBox)obj).Text = "";
            }
            foreach (Control obj in panelStudent.Controls)
            {
                if (obj is TextBox) ((TextBox)obj).Text = "";
            }
            PublicSettings.Init();
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;
            int width = panelStudent.Size.Width;
            int cnt = 0;
           foreach(Control label in panelTeacher.Controls)
            {
                if (label is Label)
                {
                    label.Location = new Point(10 + (cnt % 5) * (width / 5), 10 + (cnt / 5) * 30);
                    cnt++;
                }
            }
            cnt = 0;
            foreach(Control textBox in panelTeacher.Controls)
            {
                if (textBox is TextBox)
                {
                    textBox.Location = new Point(65 + (cnt % 5) * (width / 5), 5 + (cnt / 5) * 30);
                    cnt++;
                }
            }
            cnt = 0;
            foreach (Control label in panelStudent.Controls)
            {
                if (label is Label)
                {
                    label.Location = new Point(10 + (cnt % 5) * (width / 5), 10 + (cnt / 5) * 30);
                    cnt++;
                }
            }
            cnt = 0;
            foreach (Control textBox in panelStudent.Controls)
            {
                if (textBox is TextBox)
                {
                    textBox.Location = new Point(65 + (cnt % 5) * (width / 5), 5 + (cnt / 5) * 30);
                    cnt++;
                }
            }
            this.Refresh();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            ShowDetail showDetail = new ShowDetail();
            double sumTeacher = 0, sumStudent = 0;
            if (currTeacherName.Text.ToString().Length < 1)
            {
                MessageBox.Show("请输入选手姓名");
                return;
            }
            int cnt = 0;
            foreach(Control obj in panelTeacher.Controls)
            {
                if (obj is TextBox)
                {
                    TextBox textBox = (TextBox)obj;
                    double temp = 0;
                    if (double.TryParse(textBox.Text.ToString(),out temp) == false)
                    {
                        MessageBox.Show(textBox.Name + " 输入有误");
                        return;
                    }
                    if (temp > 30)
                    {
                        MessageBox.Show(textBox.Name + " 输入有误");
                        return;
                    }
                    PublicSettings.maxTeacherScore = Math.Max(PublicSettings.maxTeacherScore, temp);
                    PublicSettings.minTeacherScore = Math.Min(PublicSettings.minTeacherScore, temp);
                    sumTeacher += temp;
                    Label label = new Label();
                    label.Name = "teac" + cnt.ToString();
                    label.Font = new Font("隶书", 20, FontStyle.Bold);
                    label.AutoSize = true;
                    label.Location = new Point(10 + (cnt % 5) * (showDetail.panelTeacher.Size.Width / 5), 10 + (cnt / 5) * 30);
                    label.Text = temp.ToString().ToString().Substring(0, Math.Min(6, temp.ToString().Length));
                    showDetail.panelTeacher.Controls.Add(label);
                    cnt++;
                }
            }
            cnt = 0;
            foreach (Control obj in panelStudent.Controls)
            {
                if (obj is TextBox)
                {
                    TextBox textBox = (TextBox)obj;
                    double temp = 0;
                    if (double.TryParse(textBox.Text.ToString(), out temp) == false)
                    {
                        MessageBox.Show(textBox.Name + " 输入有误");
                        return;
                    }
                    if (temp > 30)
                    {
                        MessageBox.Show(textBox.Name + " 输入有误");
                        return;
                    }
                    PublicSettings.maxStudentScore = Math.Max(PublicSettings.maxStudentScore, temp);
                    PublicSettings.minStudentScore = Math.Min(PublicSettings.minStudentScore, temp);
                    sumStudent += temp;
                    Label label = new Label();
                    label.Name = "teac" + cnt.ToString();
                    label.Font = new Font("隶书", 20, FontStyle.Bold);
                    label.AutoSize = true;
                    label.Location = new Point(10 + (cnt % 5) * (showDetail.panelTeacher.Size.Width / 5), 10 + (cnt / 5) * 30);
                    label.Text = temp.ToString().ToString().Substring(0, Math.Min(6, temp.ToString().Length));
                    showDetail.panelStudent.Controls.Add(label);
                    cnt++;
                }
            }
            sumTeacher -= PublicSettings.maxTeacherScore + PublicSettings.minTeacherScore;
            sumTeacher /= PublicSettings.teacherCount - 2;
            sumStudent -= PublicSettings.maxStudentScore + PublicSettings.minStudentScore;
            sumStudent /= PublicSettings.stuCount - 2;
            PublicSettings.argvStud = sumStudent;
            PublicSettings.argvTeac = sumTeacher;
            showDetail.teacMaxScore.Text = PublicSettings.maxTeacherScore.ToString();
            showDetail.teacMinScore.Text = PublicSettings.minTeacherScore.ToString();
            showDetail.studMaxScore.Text = PublicSettings.maxStudentScore.ToString();
            showDetail.studMinScore.Text = PublicSettings.minStudentScore.ToString();
            showDetail.teacArgv.Text = PublicSettings.argvTeac.ToString();
            showDetail.studArgv.Text = PublicSettings.argvStud.ToString();
            double score = sumTeacher * 0.7 + sumStudent * 0.3;
           // MessageBox.Show(score.ToString());
            object[] para = new object[3];
            para[0] = dataGridView1.Rows.Count;
            para[1] = currTeacherName.Text.ToString();
            para[2] = score.ToString().Substring(0,Math.Min(6,score.ToString().Length));
            dataGridView1.Rows.Add(para);
            dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Descending);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
            //ShowInSecDisplay();
            showDetail.label1.Text = currTeacherName.Text;
            showDetail.finalScore.Text = para[2].ToString();
            showDetail.Show();
            ClearData();
        }
    }
}
