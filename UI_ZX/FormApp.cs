namespace UI_ZX
{
    public partial class FormApp : Form
    {
        public FormApp()
        {
            InitializeComponent();
        }


        private void OpenFile(object sender, EventArgs e)
        {
            // �Դ namespace UI_ZX
            FormSetting formSetting = new FormSetting();
            formSetting.Show();
        }
    }
}
