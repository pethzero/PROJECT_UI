namespace UI_ZX
{
    partial class FormSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            tbxLocation = new TextBox();
            label2 = new Label();
            btnLocationSave = new Button();
            label3 = new Label();
            btnOK = new Button();
            btnCancel = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 31);
            label1.Name = "label1";
            label1.Size = new Size(130, 15);
            label1.TabIndex = 0;
            label1.Text = "System Location Folder";
            // 
            // tbxLocation
            // 
            tbxLocation.Location = new Point(38, 91);
            tbxLocation.Name = "tbxLocation";
            tbxLocation.Size = new Size(633, 23);
            tbxLocation.TabIndex = 3;
            tbxLocation.Leave += Modified_Process;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 73);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 2;
            label2.Text = "Output";
            // 
            // btnLocationSave
            // 
            btnLocationSave.Location = new Point(677, 90);
            btnLocationSave.Name = "btnLocationSave";
            btnLocationSave.Size = new Size(75, 23);
            btnLocationSave.TabIndex = 4;
            btnLocationSave.Text = "...";
            btnLocationSave.UseVisualStyleBackColor = true;
            btnLocationSave.Click += btnLocationSave_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 117);
            label3.Name = "label3";
            label3.Size = new Size(335, 15);
            label3.TabIndex = 4;
            label3.Text = "If it's null, it will be calculated from the Base Directory\\Output.";
            // 
            // btnOK
            // 
            btnOK.Location = new Point(573, 406);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btn_ProcessData;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(677, 406);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btn_ProcessData;
            // 
            // FormSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(767, 446);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(label3);
            Controls.Add(btnLocationSave);
            Controls.Add(label2);
            Controls.Add(tbxLocation);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FormSetting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormSetting";
            Load += FormSetting_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbxLocation;
        private Label label2;
        private Button btnLocationSave;
        private Label label3;
        private Button btnOK;
        private Button btnCancel;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}