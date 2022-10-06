using DLL_Project;
namespace WinForms_Share
{
    public partial class Share01 : Form
    {
        public Share01()
        {
            InitializeComponent();
        }

        private void Share01_Load(object sender, EventArgs e)
        {
            LogFile.CleanLog();
            LogFile.Write("Share01 start");
        }

        private void Share01_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogFile.Write("Share01 end");
        }
    }
}