using System;
using System.Threading;
using System.Windows.Forms;

namespace UI_ZX
{
    public partial class LoadingForm : Form
    {
        // ให้ Form หลักกำหนดค่า CancellationTokenSource ให้
        public CancellationTokenSource Cts { get; set; }

        public LoadingForm()
        {
            InitializeComponent();

            // ปรับความสวยงาม/พฤติกรรม
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false;      // ปิดปุ่ม X
            this.StartPosition = FormStartPosition.CenterParent;

            // กดยกเลิก
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            // สั่ง cancel ผ่าน CTS (ถ้ามี)
            btnCancel.Enabled = false;
            labelStatus.Text = "กำลังยกเลิก...";

            Cts?.Cancel();
        }

        // ช่วยให้อัพเดต UI จาก background thread ได้อย่างปลอดภัย
        public void SetProgress(int percent)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() => progressBar1.Value = Math.Max(0, Math.Min(100, percent))));
            }
            else
            {
                progressBar1.Value = Math.Max(0, Math.Min(100, percent));
            }
        }

        public void SetStatus(string text)
        {
            if (labelStatus.InvokeRequired)
                labelStatus.Invoke(new Action(() => labelStatus.Text = text));
            else
                labelStatus.Text = text;
        }
    }
}
