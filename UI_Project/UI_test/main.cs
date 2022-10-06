using DLL_Project;
using System.Reflection;
using WinForms_Share;
namespace UI_test
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            //---
            Assembly assembly;
            Type type;

            string path = @".\Dy_Dll.dll";
            assembly = Assembly.LoadFrom(path);
            //獲取程式集例項中具有指定名稱的Type物件
            type = assembly.GetType("Dy_Dll.Class1");//namespace.Class
            //獲取Class1物件
            var C1 = Activator.CreateInstance(type);//建構函式public Class1() 
            var C2 = Activator.CreateInstance(type, "string");//建構函式public Class1(string name) 
            var C3 = Activator.CreateInstance(type, "string", 123);//建構函式public Class1(string name,int age) 
            //獲取方法
            var m1 = type.GetMethod("M1");//方法名M1在Class1類中沒有過載，獲取明確 方法為私有或不存在時返回null
            var val1 = m1.Invoke(null, null);//執行m1 M1函式為靜態的，且無參，Invoke中引數可為null

            var m2 = type.GetMethod("M2", new Type[] { typeof(string) });//獲取public static void M2(string name)
            var val2 = m2.Invoke(null, new object[] { "str" });//執行m2

            var m3 = type.GetMethod("M2", new Type[] { typeof(string), typeof(int) });//獲取public static void M2(string name, int age)
            var val3 = m3.Invoke(null, new object[] { "str", 124 });//執行m3

            //非靜態函式的獲取及呼叫
            var m4 = type.GetMethod("M3");
            var val4 = m4.Invoke(C1, null);//執行個體,傳參
            val4 = m4.Invoke(C2, null);
            val4 = m4.Invoke(C3, null);

            var m5 = type.GetMethod("M4", new Type[] { typeof(string) });
            var val5 = m5.Invoke(C1, new object[] { "dtr" });
            val5 = m5.Invoke(C2, new object[] { "dtr" });
            val5 = m5.Invoke(C3, new object[] { "dtr" });

            var m6 = type.GetMethod("M4", new Type[] { typeof(int) });
            var val6 = m6.Invoke(C1, new object[] { 225 });
            val6 = m6.Invoke(C2, new object[] { 225 });
            val6 = m6.Invoke(C3, new object[] { 225 });

            //非靜態函式的獲取及呼叫(回傳運算結果)
            var m7 = type.GetMethod("Add", new Type[] { typeof(double), typeof(double) });
            var val7 = m7.Invoke(C1, new object[] { 456, 124 });

            //---

            LogFile.CleanLog();
            LogFile.Write("main Start");
            Share01 share01 = new Share01();
            share01.ShowDialog();
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogFile.Write("main Close");
        }
    }
}