using System.Text;

namespace Dy_Dll
{
    public class LogFile
    {
        public static void CleanLog(int intDay = 30)
        {
            DateTime now = DateTime.Now; // 今天
            String StrLogPath = m_StrSysPath + "Log";

            if (!Directory.Exists(StrLogPath))
            {
                Directory.CreateDirectory(StrLogPath);
            }

            DirectoryInfo di = new DirectoryInfo(StrLogPath); // 取得 X:\ 資料夾資訊
            // 從 di 裡找出所有zip檔，且列舉出所有 (今天 - 屬性日期) 超過 N 天的
            //建立日期:p.CreationTime
            //修改日期:p.LastWriteTime
            //存取日期:p.LastAccessTime
            // 用 foreach把上行列舉到的檔案全跑一遍，z 就是每次被列舉到符合條件的zip
            foreach (var z in di.GetFiles("*.*").Where(p => (now - p.LastWriteTime).TotalDays > intDay))
            {
                z.Delete();  // 很好理解，把 z 刪除！
            }
        }

        //public static string m_StrSysPath = System.Windows.Forms.Application.StartupPath;
        public static string m_StrSysPath = AppDomain.CurrentDomain.BaseDirectory;
        public static void Write(String StrData, bool blnAutoTime = true)
        {
            String StrLogPath = m_StrSysPath + "Log";
            String StrFileName = StrLogPath + String.Format("\\{0}.log", DateTime.Now.ToString("yyyyMMdd"));

            if (!Directory.Exists(StrLogPath))
            {
                Directory.CreateDirectory(StrLogPath);
            }

            FileStream fs = new FileStream(StrFileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            if (blnAutoTime == true)
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff => ") + StrData);// 寫入文字
            }
            else
            {
                sw.WriteLine(StrData);// 寫入文字
            }

            sw.Close();// 關閉串流
        }
    }

    public class Class1
    {
        public String StrOutPut = "";
        private string Name;
        private int Age;
        public Class1() { Name = "name"; Age = 0; StrOutPut = Name + "," + Age;}
        public Class1(string name) { Name = name; Age = 0; StrOutPut = Name + "," + Age;}
        public Class1(string name, int age) { Name = name; Age = age; StrOutPut = Name + "," + Age;}
        public static void M1()
        {
            LogFile.Write("靜態無參函式");
        }
        public static void M2(string name)
        {
            LogFile.Write($"靜態帶參函式String{name}");
        }
        public static void M2(string name, int age)
        {
            LogFile.Write($"靜態帶參函式int{name + age}");
        }
        public void M3()
        {
            LogFile.Write($"非靜態無參函式name={Name},age={Age}");
        }
        public void M4(string name)
        {
            LogFile.Write($"非靜態帶參函式string{name}");
        }
        public void M4(int age)
        {
            LogFile.Write($"非靜態帶參函式int{age}");
        }
        public void M5(string name) { LogFile.Write(name); }

        public double Add(double v1,double v2)
        {
            double dblResult = v1+v2;
            return dblResult;
        }
    }
}