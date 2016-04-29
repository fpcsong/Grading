using System;
using System.IO;
using Newtonsoft.Json.Linq;
namespace Grading
{
    class PublicSettings
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
    }
}
