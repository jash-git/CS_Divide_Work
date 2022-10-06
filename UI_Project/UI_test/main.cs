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
            //����{�����Ҷ����㦳���w�W�٪�Type����
            type = assembly.GetType("Dy_Dll.Class1");//namespace.Class
            //���Class1����
            var C1 = Activator.CreateInstance(type);//�غc�禡public Class1() 
            var C2 = Activator.CreateInstance(type, "string");//�غc�禡public Class1(string name) 
            var C3 = Activator.CreateInstance(type, "string", 123);//�غc�禡public Class1(string name,int age) 
            //�����k
            var m1 = type.GetMethod("M1");//��k�WM1�bClass1�����S���L���A������T ��k���p���Τ��s�b�ɪ�^null
            var val1 = m1.Invoke(null, null);//����m1 M1�禡���R�A���A�B�L�ѡAInvoke���޼ƥi��null

            var m2 = type.GetMethod("M2", new Type[] { typeof(string) });//���public static void M2(string name)
            var val2 = m2.Invoke(null, new object[] { "str" });//����m2

            var m3 = type.GetMethod("M2", new Type[] { typeof(string), typeof(int) });//���public static void M2(string name, int age)
            var val3 = m3.Invoke(null, new object[] { "str", 124 });//����m3

            //�D�R�A�禡������ΩI�s
            var m4 = type.GetMethod("M3");
            var val4 = m4.Invoke(C1, null);//�������,�ǰ�
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

            //�D�R�A�禡������ΩI�s(�^�ǹB�⵲�G)
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