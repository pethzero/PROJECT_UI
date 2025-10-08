namespace UI_ZX
{
    partial class FormApp
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            fileToolStripMenuItem1 = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            panel1 = new Panel();
            panel2 = new Panel();
            tabControlAPP = new TabControl();
            tabPage2 = new TabPage();
            panel6 = new Panel();
            label6 = new Label();
            cbx = new ComboBox();
            label5 = new Label();
            btnSelectTop = new Button();
            txtFolderTop = new TextBox();
            btnCompareFolder = new Button();
            button4 = new Button();
            label4 = new Label();
            label3 = new Label();
            txbFolder2 = new TextBox();
            btnFolder2 = new Button();
            btnFolder1 = new Button();
            txbFolder1 = new TextBox();
            btnCompare = new Button();
            comboBox1 = new ComboBox();
            textBox2 = new TextBox();
            label1 = new Label();
            button1 = new Button();
            ButtonFile1 = new Button();
            textBox1 = new TextBox();
            tabPage3 = new TabPage();
            panel3 = new Panel();
            panel4 = new Panel();
            panel7 = new Panel();
            panel5 = new Panel();
            btnLoadBar = new Button();
            tabPage1 = new TabPage();
            label_xml = new Label();
            btnXML = new Button();
            openFileDialog1 = new OpenFileDialog();
            folderBrowserDialog1 = new FolderBrowserDialog();
            label2 = new Label();
            btnProcessTop = new Button();
            contextMenuStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel2.SuspendLayout();
            tabControlAPP.SuspendLayout();
            tabPage2.SuspendLayout();
            panel6.SuspendLayout();
            tabPage3.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, exitToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.OwnerItem = fileToolStripMenuItem1;
            contextMenuStrip1.Size = new Size(94, 48);
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(93, 22);
            fileToolStripMenuItem.Text = "File";
            fileToolStripMenuItem.Click += OpenFile;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(93, 22);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // fileToolStripMenuItem1
            // 
            fileToolStripMenuItem1.DropDown = contextMenuStrip1;
            fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            fileToolStripMenuItem1.Size = new Size(56, 20);
            fileToolStripMenuItem1.Text = "Setting";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RenderMode = ToolStripRenderMode.System;
            menuStrip1.Size = new Size(1071, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(1071, 66);
            panel1.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(tabControlAPP);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 90);
            panel2.Name = "panel2";
            panel2.Size = new Size(1071, 591);
            panel2.TabIndex = 4;
            // 
            // tabControlAPP
            // 
            tabControlAPP.Controls.Add(tabPage2);
            tabControlAPP.Controls.Add(tabPage3);
            tabControlAPP.Controls.Add(tabPage1);
            tabControlAPP.Dock = DockStyle.Fill;
            tabControlAPP.Location = new Point(0, 0);
            tabControlAPP.Name = "tabControlAPP";
            tabControlAPP.SelectedIndex = 0;
            tabControlAPP.Size = new Size(1071, 591);
            tabControlAPP.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.White;
            tabPage2.Controls.Add(panel6);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1063, 563);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "COMPARE FILE";
            // 
            // panel6
            // 
            panel6.BackColor = Color.Gainsboro;
            panel6.Controls.Add(btnProcessTop);
            panel6.Controls.Add(label6);
            panel6.Controls.Add(cbx);
            panel6.Controls.Add(label5);
            panel6.Controls.Add(btnSelectTop);
            panel6.Controls.Add(txtFolderTop);
            panel6.Controls.Add(btnCompareFolder);
            panel6.Controls.Add(button4);
            panel6.Controls.Add(label4);
            panel6.Controls.Add(label3);
            panel6.Controls.Add(txbFolder2);
            panel6.Controls.Add(btnFolder2);
            panel6.Controls.Add(btnFolder1);
            panel6.Controls.Add(txbFolder1);
            panel6.Controls.Add(btnCompare);
            panel6.Controls.Add(comboBox1);
            panel6.Controls.Add(textBox2);
            panel6.Controls.Add(label1);
            panel6.Controls.Add(button1);
            panel6.Controls.Add(ButtonFile1);
            panel6.Controls.Add(textBox1);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(3, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(1057, 557);
            panel6.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(46, 334);
            label6.Name = "label6";
            label6.Size = new Size(56, 21);
            label6.TabIndex = 26;
            label6.Text = "Count";
            // 
            // cbx
            // 
            cbx.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx.FormattingEnabled = true;
            cbx.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            cbx.Location = new Point(117, 336);
            cbx.Name = "cbx";
            cbx.Size = new Size(144, 23);
            cbx.TabIndex = 25;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(46, 299);
            label5.Name = "label5";
            label5.Size = new Size(111, 21);
            label5.TabIndex = 24;
            label5.Text = "SelectTopFile";
            // 
            // btnSelectTop
            // 
            btnSelectTop.Location = new Point(304, 386);
            btnSelectTop.Name = "btnSelectTop";
            btnSelectTop.Size = new Size(86, 23);
            btnSelectTop.TabIndex = 22;
            btnSelectTop.Text = "SelectFolder1";
            btnSelectTop.UseVisualStyleBackColor = true;
            btnSelectTop.Click += btnSelectTop_Click;
            // 
            // txtFolderTop
            // 
            txtFolderTop.Location = new Point(46, 386);
            txtFolderTop.Name = "txtFolderTop";
            txtFolderTop.ReadOnly = true;
            txtFolderTop.Size = new Size(239, 23);
            txtFolderTop.TabIndex = 23;
            // 
            // btnCompareFolder
            // 
            btnCompareFolder.Location = new Point(481, 256);
            btnCompareFolder.Name = "btnCompareFolder";
            btnCompareFolder.Size = new Size(127, 23);
            btnCompareFolder.TabIndex = 21;
            btnCompareFolder.Text = "CopySelectFolder2";
            btnCompareFolder.UseVisualStyleBackColor = true;
            btnCompareFolder.Click += btnCompareFolderWithProgress_Click;
            // 
            // button4
            // 
            button4.Location = new Point(481, 167);
            button4.Name = "button4";
            button4.Size = new Size(127, 23);
            button4.TabIndex = 20;
            button4.Text = "Only Folder";
            button4.UseVisualStyleBackColor = true;
            button4.Click += buttonProcessFindDuplicateFolder_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(481, 80);
            label4.Name = "label4";
            label4.Size = new Size(127, 21);
            label4.TabIndex = 19;
            label4.Text = "CompareFolder";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(46, 80);
            label3.Name = "label3";
            label3.Size = new Size(106, 21);
            label3.TabIndex = 18;
            label3.Text = "CompareFile";
            // 
            // txbFolder2
            // 
            txbFolder2.Location = new Point(481, 208);
            txbFolder2.Name = "txbFolder2";
            txbFolder2.ReadOnly = true;
            txbFolder2.Size = new Size(239, 23);
            txbFolder2.TabIndex = 17;
            // 
            // btnFolder2
            // 
            btnFolder2.Location = new Point(739, 208);
            btnFolder2.Name = "btnFolder2";
            btnFolder2.Size = new Size(86, 23);
            btnFolder2.TabIndex = 16;
            btnFolder2.Text = "SelectFolder2";
            btnFolder2.UseVisualStyleBackColor = true;
            btnFolder2.Click += btnFolder2_Click;
            // 
            // btnFolder1
            // 
            btnFolder1.Location = new Point(739, 120);
            btnFolder1.Name = "btnFolder1";
            btnFolder1.Size = new Size(86, 23);
            btnFolder1.TabIndex = 14;
            btnFolder1.Text = "SelectFolder1";
            btnFolder1.UseVisualStyleBackColor = true;
            btnFolder1.Click += btnFolder1_Click;
            // 
            // txbFolder1
            // 
            txbFolder1.Location = new Point(481, 120);
            txbFolder1.Name = "txbFolder1";
            txbFolder1.ReadOnly = true;
            txbFolder1.Size = new Size(239, 23);
            txbFolder1.TabIndex = 15;
            // 
            // btnCompare
            // 
            btnCompare.Location = new Point(46, 208);
            btnCompare.Name = "btnCompare";
            btnCompare.Size = new Size(70, 23);
            btnCompare.TabIndex = 13;
            btnCompare.Text = "OK";
            btnCompare.UseVisualStyleBackColor = true;
            btnCompare.Click += btnCompare_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "MD5", "SHA1", "SHA256", "SHA384", "SHA512" });
            comboBox1.Location = new Point(104, 15);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(144, 23);
            comboBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(46, 168);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(239, 23);
            textBox2.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(44, 17);
            label1.Name = "label1";
            label1.Size = new Size(54, 21);
            label1.TabIndex = 8;
            label1.Text = "HASH";
            // 
            // button1
            // 
            button1.Location = new Point(304, 168);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 11;
            button1.Text = "SelectFile2";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ButtonFile2_Click;
            // 
            // ButtonFile1
            // 
            ButtonFile1.Location = new Point(304, 119);
            ButtonFile1.Name = "ButtonFile1";
            ButtonFile1.Size = new Size(75, 23);
            ButtonFile1.TabIndex = 9;
            ButtonFile1.Text = "SelectFile1";
            ButtonFile1.UseVisualStyleBackColor = true;
            ButtonFile1.Click += ButtonFile1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(46, 119);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(239, 23);
            textBox1.TabIndex = 10;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.Transparent;
            tabPage3.Controls.Add(panel3);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1063, 563);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "ABC";
            // 
            // panel3
            // 
            panel3.BackColor = Color.Gainsboro;
            panel3.Controls.Add(panel4);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1063, 563);
            panel3.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.BackColor = Color.White;
            panel4.Controls.Add(panel7);
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(1063, 563);
            panel4.TabIndex = 0;
            // 
            // panel7
            // 
            panel7.BackColor = Color.DarkGray;
            panel7.Dock = DockStyle.Right;
            panel7.Location = new Point(475, 0);
            panel7.Name = "panel7";
            panel7.Size = new Size(588, 563);
            panel7.TabIndex = 1;
            // 
            // panel5
            // 
            panel5.AutoSize = true;
            panel5.BackColor = Color.Gainsboro;
            panel5.Controls.Add(btnLoadBar);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(1063, 563);
            panel5.TabIndex = 0;
            // 
            // btnLoadBar
            // 
            btnLoadBar.Location = new Point(23, 43);
            btnLoadBar.Name = "btnLoadBar";
            btnLoadBar.Size = new Size(156, 23);
            btnLoadBar.TabIndex = 0;
            btnLoadBar.Text = "btnLoadBar";
            btnLoadBar.UseVisualStyleBackColor = true;
            btnLoadBar.Click += StartLongTaskWithProgress_Click;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.Gainsboro;
            tabPage1.Controls.Add(label_xml);
            tabPage1.Controls.Add(btnXML);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1063, 563);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "APP 1";
            // 
            // label_xml
            // 
            label_xml.AutoSize = true;
            label_xml.Location = new Point(8, 25);
            label_xml.Name = "label_xml";
            label_xml.Size = new Size(85, 15);
            label_xml.TabIndex = 1;
            label_xml.Text = "XML EXAMPLE";
            // 
            // btnXML
            // 
            btnXML.Location = new Point(8, 43);
            btnXML.Name = "btnXML";
            btnXML.Size = new Size(75, 23);
            btnXML.TabIndex = 0;
            btnXML.Text = "btnXML";
            btnXML.UseVisualStyleBackColor = true;
            btnXML.Click += btnXML_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(293, 111);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 0;
            label2.Text = "label2";
            // 
            // btnProcessTop
            // 
            btnProcessTop.Location = new Point(46, 436);
            btnProcessTop.Name = "btnProcessTop";
            btnProcessTop.Size = new Size(86, 23);
            btnProcessTop.TabIndex = 27;
            btnProcessTop.Text = "ProcessTop";
            btnProcessTop.UseVisualStyleBackColor = true;
            btnProcessTop.Click += btnProcessTop_Click;
            // 
            // FormApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1071, 681);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FormApp";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            contextMenuStrip1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel2.ResumeLayout(false);
            tabControlAPP.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            tabPage3.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem1;
        private Panel panel1;
        private Panel panel2;
        private TabControl tabControlAPP;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label_xml;
        private Button btnXML;
        private OpenFileDialog openFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;
        private TabPage tabPage3;
        private Panel panel3;
        private Label label2;
        private Panel panel4;
        private Panel panel6;
        private Button btnCompare;
        private ComboBox comboBox1;
        private TextBox textBox2;
        private Label label1;
        private Button button1;
        private Button ButtonFile1;
        private TextBox textBox1;
        private Panel panel7;
        private Panel panel5;
        private Label label3;
        private TextBox txbFolder2;
        private Button btnFolder2;
        private Button btnFolder1;
        private TextBox txbFolder1;
        private Button button4;
        private Label label4;
        private Button btnLoadBar;
        private Button btnCompareFolder;
        private Label label6;
        private ComboBox cbx;
        private Label label5;
        private Button btnSelectTop;
        private TextBox txtFolderTop;
        private Button btnProcessTop;
    }
}
