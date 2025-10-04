namespace UI_ZX
{
    partial class LoadingForm
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
            progressBar1 = new ProgressBar();
            labelStatus = new Label();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(22, 40);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(520, 28);
            progressBar1.TabIndex = 0;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(22, 89);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(38, 15);
            labelStatus.TabIndex = 1;
            labelStatus.Text = "label1";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(467, 133);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Close";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // LoadingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(574, 193);
            Controls.Add(btnCancel);
            Controls.Add(labelStatus);
            Controls.Add(progressBar1);
            Name = "LoadingForm";
            Text = "LoadingForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBar1;
        private Label labelStatus;
        private Button btnCancel;
    }
}