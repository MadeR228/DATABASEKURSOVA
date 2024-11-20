namespace DATABASEKURSOVA
{
    partial class ReportsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxDriverID = new System.Windows.Forms.TextBox();
            this.dateReport = new System.Windows.Forms.DateTimePicker();
            this.textBoxPassenger = new System.Windows.Forms.TextBox();
            this.textBoxAccident = new System.Windows.Forms.TextBox();
            this.textBoxDelay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxDelay);
            this.panel1.Controls.Add(this.textBoxAccident);
            this.panel1.Controls.Add(this.textBoxPassenger);
            this.panel1.Controls.Add(this.dateReport);
            this.panel1.Controls.Add(this.textBoxDriverID);
            this.panel1.Location = new System.Drawing.Point(-3, -15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 259);
            this.panel1.TabIndex = 0;
            // 
            // textBoxDriverID
            // 
            this.textBoxDriverID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDriverID.Location = new System.Drawing.Point(15, 116);
            this.textBoxDriverID.Name = "textBoxDriverID";
            this.textBoxDriverID.Size = new System.Drawing.Size(148, 25);
            this.textBoxDriverID.TabIndex = 0;
            // 
            // dateReport
            // 
            this.dateReport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateReport.Location = new System.Drawing.Point(15, 167);
            this.dateReport.MinDate = new System.DateTime(2019, 1, 1, 0, 0, 0, 0);
            this.dateReport.Name = "dateReport";
            this.dateReport.Size = new System.Drawing.Size(148, 25);
            this.dateReport.TabIndex = 4;
            // 
            // textBoxPassenger
            // 
            this.textBoxPassenger.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPassenger.Location = new System.Drawing.Point(15, 54);
            this.textBoxPassenger.Name = "textBoxPassenger";
            this.textBoxPassenger.Size = new System.Drawing.Size(148, 25);
            this.textBoxPassenger.TabIndex = 5;
            // 
            // textBoxAccident
            // 
            this.textBoxAccident.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAccident.Location = new System.Drawing.Point(189, 116);
            this.textBoxAccident.Name = "textBoxAccident";
            this.textBoxAccident.Size = new System.Drawing.Size(148, 25);
            this.textBoxAccident.TabIndex = 6;
            // 
            // textBoxDelay
            // 
            this.textBoxDelay.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDelay.Location = new System.Drawing.Point(189, 54);
            this.textBoxDelay.Name = "textBoxDelay";
            this.textBoxDelay.Size = new System.Drawing.Size(148, 25);
            this.textBoxDelay.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Дата";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Кількість пасажирів";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Номер водія";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(186, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Час затримки";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(186, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Кількість казусів";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(189, 167);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(148, 25);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "button1";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 213);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ReportsForm";
            this.Text = "ReportsForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateReport;
        private System.Windows.Forms.TextBox textBoxDriverID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDelay;
        private System.Windows.Forms.TextBox textBoxAccident;
        private System.Windows.Forms.TextBox textBoxPassenger;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
    }
}