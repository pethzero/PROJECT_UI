using System.Data;
using System.Text;
using System.Xml.Linq;

namespace UI_ZX
{
    public partial class FormApp : Form
    {
        public FormApp()
        {
            InitializeComponent();

            LocationSave = string.IsNullOrEmpty(Properties.Settings.Default.SettingLocationSave)
                           ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                           : Properties.Settings.Default.SettingLocationSave;
        }

        private string? selectedFilePath1 = null;
        private string? selectedFilePath2 = null;
        private string? selectedFolderPath1 = null;
        private string? selectedFolderPath2 = null;

        string LocationSave;

        private void OpenFile(object sender, EventArgs e)
        {
            // เปิด namespace UI_ZX
            FormSetting formSetting = new FormSetting();
            formSetting.Show();
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Column1");
            table.Columns.Add("Column2");
            table.Rows.Add("Value1", "Value2");

            XDocument xDoc = new XDocument(
                new XElement("Table",
                    from row in table.AsEnumerable()
                    select new XElement("Row",
                        new XElement("Column1", row["Column1"]),
                        new XElement("Column2", row["Column2"])
                    )
                )
            );

            // สร้าง path เต็มที่ประกอบด้วย LocationSave และชื่อไฟล์
            string xmlFilePath = Path.Combine(LocationSave, "table.xml");
            xDoc.Save(xmlFilePath);

            MessageBox.Show($"XML saved to {LocationSave}");
        }

        private void ButtonFile1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // ได้ path ของไฟล์
                string filePath = openFileDialog1.FileName;

                // แสดง path ใน TextBox หรือ MessageBox
                MessageBox.Show("คุณเลือกไฟล์: " + filePath);

                // เก็บ path ไว้ใช้กับฟังก์ชัน Hash
                selectedFilePath1 = filePath;
                textBox1.Text = filePath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // ได้ path ของไฟล์
                string filePath = openFileDialog1.FileName;

                // แสดง path ใน TextBox หรือ MessageBox
                MessageBox.Show("คุณเลือกไฟล์: " + filePath);

                // เก็บ path ไว้ใช้กับฟังก์ชัน Hash
                selectedFilePath2 = filePath;
                textBox2.Text = filePath;
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าไฟล์ถูกเลือกหรือยัง
            if (string.IsNullOrEmpty(selectedFilePath1) || string.IsNullOrEmpty(selectedFilePath2))
            {
                MessageBox.Show("กรุณาเลือกไฟล์ทั้ง 2 ก่อนทำการเปรียบเทียบ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // ออกจากฟังก์ชัน ถ้ายังไม่ได้เลือกไฟล์
            }

            try
            {
                // ใช้ ComboBox เลือก Algorithm
                string algo = comboBox1.SelectedItem?.ToString() ?? "MD5";

                // เปรียบเทียบไฟล์
                bool same = Lib.CompareFiles(selectedFilePath1, selectedFilePath2, algo);

                if (same)
                    MessageBox.Show("ไฟล์เหมือนกัน (แม้ชื่อจะต่างกัน)", "ผลลัพธ์");
                else
                    MessageBox.Show("ไฟล์แตกต่างกัน", "ผลลัพธ์");
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFolder1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // ได้ path ของโฟลเดอร์
                string selectedFolder = folderBrowserDialog1.SelectedPath;

                // แสดงผล (หรือเก็บไว้ในตัวแปรก็ได้)
                MessageBox.Show("คุณเลือกโฟลเดอร์: " + selectedFolder);

                // เก็บในตัวแปรไว้ใช้งาน
                selectedFolderPath1 = selectedFolder;
                txbFolder1.Text = selectedFolder;
            }
        }

        private void btnFolder2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // ได้ path ของโฟลเดอร์
                string selectedFolder = folderBrowserDialog1.SelectedPath;

                // แสดงผล (หรือเก็บไว้ในตัวแปรก็ได้)
                MessageBox.Show("คุณเลือกโฟลเดอร์: " + selectedFolder);

                // เก็บในตัวแปรไว้ใช้งาน
                selectedFolderPath2 = selectedFolder;
                txbFolder2.Text = selectedFolder;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFolderPath1))
            {
                MessageBox.Show("กรุณาเลือกโฟลเดอร์ก่อน");
                return;
            }

            string saveFolder = string.IsNullOrEmpty(Properties.Settings.Default.SettingLocationSave)
                              ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                              : Properties.Settings.Default.SettingLocationSave;

            string filePath = Lib.ExportDuplicateToJson(selectedFolderPath1, saveFolder, "MD5");

            MessageBox.Show($"Export สำเร็จ!\nไฟล์ถูกบันทึกที่:\n{filePath}");
            //  if (string.IsNullOrEmpty(selectedFolderPath1))
            //  {
            //      MessageBox.Show("กรุณาเลือกโฟลเดอร์ก่อน");
            //      return;
            //  }

            //  var duplicates = Lib.FindDuplicateFiles(selectedFolderPath1, comboBox1.SelectedItem?.ToString() ?? "MD5");

            //  if (duplicates.Count == 0)
            //  {
            //      MessageBox.Show("ไม่พบไฟล์ที่ซ้ำกัน");
            //  }
            //  else
            //  {
            //      StringBuilder sb = new StringBuilder();
            //      foreach (var kv in duplicates)
            //      {
            //          sb.AppendLine($"Hash: {kv.Key}");
            //          foreach (var file in kv.Value)
            //              sb.AppendLine("   " + Path.GetFileName(file));
            //      }

            //      MessageBox.Show(sb.ToString(), "ไฟล์ซ้ำที่พบ");
            //  }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            using var cts = new CancellationTokenSource();
            var loading = new LoadingForm();
            loading.Cts = cts;

            this.Enabled = false;
            loading.Show(this);

            var progress = new Progress<int>(p =>
            {
                loading.SetProgress(p);
                loading.SetStatus($"กำลังทำงาน {p}%");
            });

            try
            {
                await Task.Run(() => DoHeavyWork(progress, cts.Token), cts.Token);

                // ✅ มาถึงตรงนี้ได้ แสดงว่างานเสร็จสมบูรณ์ (ไม่ถูก cancel)
                label1.Text = "โหลดเสร็จแล้ว!";
            }
            catch (OperationCanceledException)
            {
                // ✅ ถ้างานถูก cancel → มาที่นี่
                label1.Text = "ยกเลิกการทำงาน";
            }
            catch (Exception ex)
            {
                // กรณี error อื่น ๆ
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (!loading.IsDisposed)
                    loading.Close();

                this.Enabled = true;
            }
        }

        // ตัวอย่างงานหนัก ต้องตรวจสอบ token เป็นระยะ และ report progress
        private void DoHeavyWork(IProgress<int> progress, CancellationToken token)
        {
            for (int i = 1; i <= 100; i++)
            {
                // ตรวจสอบ cancellation เป็นระยะ
                token.ThrowIfCancellationRequested();

                // ทำงานทีละนิด (จำลอง)
                Thread.Sleep(50); // แทนงานจริงที่ทำ IO / process

                // รายงาน progress (จะกลับมาเรียกบน UI thread ผ่าน IProgress)
                progress.Report(i);
            }
        }
    }
}
