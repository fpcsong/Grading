using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;

namespace Grading
{
    class PublicMethods
    {
        public static int stuCount = 12;
        public static int teacherCount = 17;
        public static bool ifShowInTheSecdisplay = false;
        public static string title = "第十届青年教师讲课竞赛";
        public static double maxTeacherScore = 0;
        public static double maxStudentScore = 0;
        public static double minTeacherScore = 31;
        public static double minStudentScore = 31;
        public static double argvTeac = 0;
        public static double argvStud = 0;
        public static bool LoadSettings()
        {
            string tempjson;
            try
            {
                FileStream fs = new FileStream("settings.json", FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(fs);
                tempjson = sr.ReadToEnd();
                sr.Close();
                fs.Close();
            }
            catch (Exception)
            {
                return false;
            }
            if (tempjson.Length == 0) return false;

            try
            {
                JObject json = JObject.Parse(tempjson);
                stuCount = int.Parse(json["stuCount"].ToString());
                teacherCount = int.Parse(json["teacherCount"].ToString());
                //ifShowInTheSecdisplay = json["ifShowInTheSecdisplay"].ToString() == "true";
                title = json["title"].ToString();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void StoreSettings()
        {
            FileStream fs = new FileStream("settings.json", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            JObject json = new JObject(
                new JProperty("title", title),
                new JProperty("stuCount", stuCount),
                new JProperty("teacherCount", teacherCount)
                //new JProperty("ifShowInTheSecdisplay",ifShowInTheSecdisplay)
                );
            sw.Write(json.ToString());
            sw.Close();
            fs.Close();
        }
        public static void Init()
        {
            maxTeacherScore = 0;
            maxStudentScore = 0;
            minTeacherScore = 31;
            minStudentScore = 31;
            argvTeac = 0;
            argvStud = 0;
        }
        public static void ExportToExcel(string fileName, DataGridView myDGV)
        {
            string saveFileName = fileName;

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1 

            //写入标题
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = myDGV.Columns[i].HeaderText;
            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                    //fileSaved = true;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("导出文件时出错,文件可能正被打开！" + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();
            //MessageBox.Show(fileName + "保存成功", "提示", MessageBoxButtons.OK);
        }
        public static DataSet ExcelToDataSet(string excelName)
        {

            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source="
                  + excelName.Replace(@"\\", "\\")
                  + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            OleDbConnection objConn = new OleDbConnection(strConn);
            DataSet ds = new DataSet();
            try
            {
                objConn.Open();
                // 取得Excel工作簿中所有工作表
                System.Data.DataTable schemaTable = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                OleDbDataAdapter sqlada = new OleDbDataAdapter();

                foreach (DataRow dr in schemaTable.Rows)
                {
                    string strSql = "Select * From [" + dr[2].ToString().Trim() + "]";
                    OleDbCommand objCmd = new OleDbCommand(strSql, objConn);
                    sqlada.SelectCommand = objCmd;
                    sqlada.Fill(ds, dr[2].ToString().Trim());
                    //MessageBox.Show(ds.Tables[0].Rows[0][1].ToString());
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message.ToString());
                //Method.WriteError(ex);
            }
            finally
            {
                objConn.Close();
            }

            return ds;
        }

    }
}
