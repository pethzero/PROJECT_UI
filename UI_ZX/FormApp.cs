using System.Data;
using System.Text;
using System.Text.Json;
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
        private string? selectedFolderTop = null;

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

        private void ButtonFile2_Click(object sender, EventArgs e)
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

        private void buttonFindDuplicateFolder_Click(object sender, EventArgs e)
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
        }

        private async void buttonProcessFindDuplicateFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFolderPath1))
            {
                MessageBox.Show("กรุณาเลือกโฟลเดอร์ก่อน");
                return;
            }

            string saveFolder = string.IsNullOrEmpty(Properties.Settings.Default.SettingLocationSave)
                                ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                                : Properties.Settings.Default.SettingLocationSave;

            using var cts = new CancellationTokenSource();
            var loading = new LoadingForm();
            loading.Cts = cts;
            loading.SetReameApp("กำลังค้นหาไฟล์ซ้ำ...");
            loading.Show(this);

            var progress = new Progress<int>(p =>
            {
                loading.SetProgress(p);
                loading.SetStatus($"กำลังค้นหาไฟล์ซ้ำ {p}%");
            });

            bool canceled = false;
            string filePath = "";

            try
            {
                filePath = await Task.Run(() =>
                {
                    return Lib.ExportDuplicateToJsonWithProgress(selectedFolderPath1, saveFolder, "MD5", progress, cts.Token);
                }, cts.Token);

                if (cts.IsCancellationRequested)
                {
                    canceled = true;
                    label1.Text = "ยกเลิกการทำงาน";
                }
                else
                {
                    label1.Text = "ค้นหาเสร็จแล้ว!";
                    MessageBox.Show($"Export สำเร็จ!\nไฟล์ถูกบันทึกที่:\n{filePath}");
                }
            }
            catch (OperationCanceledException)
            {
                canceled = true;
                label1.Text = "ยกเลิกการทำงาน";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (canceled)
                {
                    loading.SetStatus("กำลังยกเลิก...");
                    await Task.Delay(500);
                }
                if (!loading.IsDisposed)
                    loading.Close();

                if (canceled)
                {
                    MessageBox.Show("งานถูกยกเลิกโดยผู้ใช้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private async void StartLongTaskWithProgress_Click(object sender, EventArgs e)
        {
            using var cts = new CancellationTokenSource();
            var loading = new LoadingForm();
            loading.Cts = cts;
            loading.SetReameApp("กำลังโหลดข้อมูล");

            //this.Enabled = false;
            loading.Show(this);

            var progress = new Progress<int>(p =>
            {
                loading.SetProgress(p);
                loading.SetStatus($"กำลังทำงาน {p}%");
            });

            bool canceled = false;

            try
            {
                await Task.Run(() => DoHeavyWork(progress, cts.Token), cts.Token);

                // ตรวจสอบสถานะ Cancel หลัง Task จบ
                if (cts.IsCancellationRequested)
                {
                    canceled = true;
                    label1.Text = "ยกเลิกการทำงาน";
                }
                else
                {
                    label1.Text = "โหลดเสร็จแล้ว!";
                    MessageBox.Show("งานเสร็จสมบูรณ์!", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (OperationCanceledException)
            {
                canceled = true;
                label1.Text = "ยกเลิกการทำงาน";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (canceled)
                {
                    loading.SetStatus("กำลังยกเลิก...");
                    await Task.Delay(500);
                }
                if (!loading.IsDisposed)
                    loading.Close();


                if (canceled)
                {
                    MessageBox.Show("งานถูกยกเลิกโดยผู้ใช้ โปรแกรมจะปิดตัวลง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Application.Exit();
                }
            }
        }
        // ตัวอย่างงานหนัก ต้องตรวจสอบ token เป็นระยะ และ report progress
        private void DoHeavyWork(IProgress<int> progress, CancellationToken token)
        {
            for (int i = 1; i <= 100; i++)
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                }
                catch (OperationCanceledException)
                {
                    // ออกจาก loop ทันที
                    break;
                }
                Thread.Sleep(50);
                progress.Report(i);
            }
        }

        private void btnCompareFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFolderPath1) || string.IsNullOrEmpty(selectedFolderPath2))
            {
                MessageBox.Show("กรุณาเลือกโฟลเดอร์ทั้งสองก่อนเปรียบเทียบ");
                return;
            }

            try
            {
                string saveFolder = string.IsNullOrEmpty(Properties.Settings.Default.SettingLocationSave)
                  ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                  : Properties.Settings.Default.SettingLocationSave;

                string algo = comboBox1.SelectedItem?.ToString() ?? "MD5";
                // เรียก compare
                var compareResult = Lib.CompareFolderNew(selectedFolderPath1, selectedFolderPath2, algo);
                // export + copy ไฟล์
                string newFolder = Lib.ExportSubFolderCompareResultToLocationSave(selectedFolderPath2, compareResult, saveFolder);

                MessageBox.Show($"✅ สำเร็จ! สร้างโฟลเดอร์ใหม่ที่: \n{newFolder}", "สำเร็จ");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error");
            }
        }


        private async void btnCompareFolderWithProgress_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFolderPath1) || string.IsNullOrEmpty(selectedFolderPath2))
            {
                MessageBox.Show("กรุณาเลือกโฟลเดอร์ทั้งสองก่อนเปรียบเทียบ");
                return;
            }

            string saveFolder = string.IsNullOrEmpty(Properties.Settings.Default.SettingLocationSave)
                ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                : Properties.Settings.Default.SettingLocationSave;

            string algo = comboBox1.SelectedItem?.ToString() ?? "MD5";

            using var cts = new CancellationTokenSource();
            var loading = new LoadingForm();
            loading.Cts = cts;
            loading.SetReameApp("กำลังเปรียบเทียบโฟลเดอร์...");
            loading.SetStatus($"กำลังเปรียบเทียบ 0%");
            loading.Show(this);

            var progress = new Progress<int>(p =>
            {
                loading.SetProgress(p);
                loading.SetStatus($"กำลังเปรียบเทียบ {p}%");
            });

            bool canceled = false;
            string newFolder = "";

            try
            {
                newFolder = await Task.Run(() =>
                {
                    var compareResult = Lib.CompareFolderNew(selectedFolderPath1, selectedFolderPath2, algo);
                    return Lib.ExportSubFolderCompareResultToLocationSaveWithProgress(
                        selectedFolderPath2, compareResult, saveFolder, progress, cts.Token);
                }, cts.Token);

                if (cts.IsCancellationRequested)
                {
                    canceled = true;
                    label1.Text = "ยกเลิกการทำงาน";
                }
                else
                {
                    label1.Text = "เปรียบเทียบเสร็จแล้ว!";
                    MessageBox.Show($"✅ สำเร็จ! สร้างโฟลเดอร์ใหม่ที่: \n{newFolder}", "สำเร็จ");
                }
            }
            catch (OperationCanceledException)
            {
                canceled = true;
                label1.Text = "ยกเลิกการทำงาน";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error");
            }
            finally
            {
                if (canceled)
                {
                    loading.SetStatus("กำลังยกเลิก...");
                    await Task.Delay(500);
                }
                if (!loading.IsDisposed)
                    loading.Close();

                if (canceled)
                {
                    MessageBox.Show("งานถูกยกเลิกโดยผู้ใช้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSelectTop_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // ได้ path ของโฟลเดอร์
                string selectedFolder = folderBrowserDialog1.SelectedPath;
                // แสดงผล (หรือเก็บไว้ในตัวแปรก็ได้)
                MessageBox.Show("คุณเลือกโฟลเดอร์: " + selectedFolder);
                // เก็บในตัวแปรไว้ใช้งาน
                selectedFolderTop = selectedFolder;
                txtFolderTop.Text = selectedFolder;
            }

        }

        private void btnProcessTop_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ ตรวจว่าผู้ใช้เลือกโฟลเดอร์หรือยัง
                if (string.IsNullOrEmpty(selectedFolderTop) || !Directory.Exists(selectedFolderTop))
                {
                    MessageBox.Show("กรุณาเลือกโฟลเดอร์ก่อนเริ่มประมวลผล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2️⃣ ตำแหน่งที่ใช้เก็บไฟล์ output
                string saveFolder = string.IsNullOrEmpty(Properties.Settings.Default.SettingLocationSave)
                    ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                    : Properties.Settings.Default.SettingLocationSave;

                // 3️⃣ เรียกฟังก์ชันประมวลผล
                var topFolders = Lib.ProcessFindTopFolder(selectedFolderTop, 5);

                // 4️⃣ สร้างไฟล์ .txt export
                string exportPath = Path.Combine(saveFolder, $"top_folders_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

                using (var sw = new StreamWriter(exportPath, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine($"📊 Top 5 Folders in: {selectedFolderTop}");
                    sw.WriteLine($"Generated: {DateTime.Now}");
                    sw.WriteLine(new string('-', 60));

                    int index = 1;
                    foreach (var f in topFolders)
                    {
                        sw.WriteLine($"{index}. {f.Path}");
                        sw.WriteLine($"   Size: {f.SizeReadable}");
                        sw.WriteLine();
                        index++;
                    }
                }

                MessageBox.Show($"✅ บันทึกผลเรียบร้อย\n\nไฟล์: {exportPath}",
                    "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // (Optional) เปิดโฟลเดอร์หลังบันทึก
                if (MessageBox.Show("ต้องการเปิดโฟลเดอร์ที่บันทึกไหม?", "เปิดโฟลเดอร์", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", saveFolder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProcessTopFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedFolderTop) || !Directory.Exists(selectedFolderTop))
                {
                    MessageBox.Show("กรุณาเลือกโฟลเดอร์ก่อนเริ่มค้นหาไฟล์", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string saveFolder = string.IsNullOrEmpty(Properties.Settings.Default.SettingLocationSave)
                    ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                    : Properties.Settings.Default.SettingLocationSave;

                // 🔍 หาไฟล์ที่ใหญ่ที่สุด Top 10
                var topFiles = Lib.ProcessFindTopFiles(selectedFolderTop, 10);

                string exportPath = Path.Combine(saveFolder, $"top_files_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

                using (var sw = new StreamWriter(exportPath, false, Encoding.UTF8))
                {
                    sw.WriteLine($"📄 Top 10 Largest Files in: {selectedFolderTop}");
                    sw.WriteLine($"Generated: {DateTime.Now}");
                    sw.WriteLine(new string('-', 70));

                    int index = 1;
                    foreach (var file in topFiles)
                    {
                        sw.WriteLine($"{index}. {file.Path}");
                        sw.WriteLine($"   Size: {file.SizeReadable}");
                        sw.WriteLine();
                        index++;
                    }
                }

                MessageBox.Show($"✅ บันทึกผลเรียบร้อย\n\nไฟล์: {exportPath}",
                    "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (MessageBox.Show("ต้องการเปิดโฟลเดอร์ที่บันทึกไหม?", "เปิดโฟลเดอร์", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", saveFolder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        ///////////////////
        private async void btnProcessTopFolderWithProgress_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFolderTop) || !Directory.Exists(selectedFolderTop))
            {
                MessageBox.Show("กรุณาเลือกโฟลเดอร์ก่อนเริ่มค้นหา", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string saveFolder = string.IsNullOrEmpty(Properties.Settings.Default.SettingLocationSave)
                ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                : Properties.Settings.Default.SettingLocationSave;

            using var cts = new CancellationTokenSource();
            var loading = new LoadingForm();
            loading.Cts = cts;
            loading.SetReameApp("กำลังประมวลผล Top Folder...");
            loading.SetStatus("กำลังเริ่มต้น...");
            loading.Show(this);

            var progress = new Progress<int>(p =>
            {
                loading.SetProgress(p);
                loading.SetStatus($"กำลังวิเคราะห์ {p}%");
            });

            bool canceled = false;
            string exportPath = "";

            try
            {
                exportPath = await Task.Run(() =>
                {
                    var folders = Lib.ProcessFindTopFolderWithProgress(selectedFolderTop, 10, progress, cts.Token);
                    return Lib.ExportTopFolderResult(folders, selectedFolderTop, saveFolder);
                }, cts.Token);

                if (cts.IsCancellationRequested)
                {
                    canceled = true;
                }
                else
                {
                    MessageBox.Show($"✅ สำเร็จ! บันทึกไฟล์ที่: \n{exportPath}", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label1.Text = "ค้นหาโฟลเดอร์เสร็จแล้ว!";

                    if (MessageBox.Show("ต้องการเปิดโฟลเดอร์ที่บันทึกไหม?", "เปิดโฟลเดอร์", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", saveFolder);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (canceled)
                {
                    loading.SetStatus("กำลังยกเลิก...");
                    await Task.Delay(500);
                    MessageBox.Show("งานถูกยกเลิกโดยผู้ใช้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label1.Text = "ยกเลิกการทำงาน";
                }

                if (!loading.IsDisposed)
                    loading.Close();
            }
        }

        private async void btnProcessTopFileWithProgress_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFolderTop) || !Directory.Exists(selectedFolderTop))
            {
                MessageBox.Show("กรุณาเลือกโฟลเดอร์ก่อนเริ่มค้นหาไฟล์", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string saveFolder = string.IsNullOrEmpty(Properties.Settings.Default.SettingLocationSave)
                ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                : Properties.Settings.Default.SettingLocationSave;

            using var cts = new CancellationTokenSource();
            var loading = new LoadingForm();
            loading.Cts = cts;
            loading.SetReameApp("กำลังประมวลผล Top File...");
            loading.SetStatus("กำลังเริ่มต้น...");
            loading.Show(this);

            var progress = new Progress<int>(p =>
            {
                loading.SetProgress(p);
                loading.SetStatus($"กำลังสแกน {p}%");
            });

            bool canceled = false;
            string exportPath = "";

            try
            {
                exportPath = await Task.Run(() =>
                {
                    var files = Lib.ProcessFindTopFilesWithProgress(selectedFolderTop, 10, progress, cts.Token);
                    return Lib.ExportTopFileResult(files, selectedFolderTop, saveFolder);
                }, cts.Token);

                if (cts.IsCancellationRequested)
                {
                    canceled = true;
                }
                else
                {
                    MessageBox.Show($"✅ สำเร็จ! บันทึกไฟล์ที่: \n{exportPath}", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label1.Text = "ค้นหาไฟล์เสร็จแล้ว!";
                }
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (canceled)
                {
                    loading.SetStatus("กำลังยกเลิก...");
                    await Task.Delay(500);
                    MessageBox.Show("งานถูกยกเลิกโดยผู้ใช้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label1.Text = "ยกเลิกการทำงาน";
                }

                if (!loading.IsDisposed)
                    loading.Close();
            }
        }


    }
}
