namespace EveHQSharp.SDEConverter
{
    partial class SDEConverterWindow
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCheckingTools = new System.Windows.Forms.Label();
            this.lblDB = new System.Windows.Forms.Label();
            this.btnCheckMarketGroup = new System.Windows.Forms.Button();
            this.btnGenerateCache = new System.Windows.Forms.Button();
            this.btnCheckDB = new System.Windows.Forms.Button();
            this.lblHelp = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCheckingTools);
            this.groupBox1.Controls.Add(this.lblDB);
            this.groupBox1.Controls.Add(this.btnCheckMarketGroup);
            this.groupBox1.Controls.Add(this.btnGenerateCache);
            this.groupBox1.Controls.Add(this.btnCheckDB);
            this.groupBox1.Controls.Add(this.lblHelp);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 208);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EveHQ Cache Generation";
            // 
            // lblCheckingTools
            // 
            this.lblCheckingTools.AutoSize = true;
            this.lblCheckingTools.Location = new System.Drawing.Point(12, 153);
            this.lblCheckingTools.Name = "lblCheckingTools";
            this.lblCheckingTools.Size = new System.Drawing.Size(77, 13);
            this.lblCheckingTools.TabIndex = 9;
            this.lblCheckingTools.Text = "Checking tools";
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(12, 71);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(53, 13);
            this.lblDB.TabIndex = 8;
            this.lblDB.Text = "Database";
            // 
            // btnCheckMarketGroup
            // 
            this.btnCheckMarketGroup.Location = new System.Drawing.Point(8, 169);
            this.btnCheckMarketGroup.Name = "btnCheckMarketGroup";
            this.btnCheckMarketGroup.Size = new System.Drawing.Size(305, 25);
            this.btnCheckMarketGroup.TabIndex = 3;
            this.btnCheckMarketGroup.Text = "Check Market Groups";
            this.btnCheckMarketGroup.UseVisualStyleBackColor = true;
            // 
            // btnGenerateCache
            // 
            this.btnGenerateCache.Location = new System.Drawing.Point(8, 116);
            this.btnGenerateCache.Name = "btnGenerateCache";
            this.btnGenerateCache.Size = new System.Drawing.Size(305, 25);
            this.btnGenerateCache.TabIndex = 2;
            this.btnGenerateCache.Text = "Generate All Cache Files";
            this.btnGenerateCache.UseVisualStyleBackColor = true;
            this.btnGenerateCache.Click += new System.EventHandler(this.btnGenerateCache_Click);
            // 
            // btnCheckDB
            // 
            this.btnCheckDB.Location = new System.Drawing.Point(8, 87);
            this.btnCheckDB.Name = "btnCheckDB";
            this.btnCheckDB.Size = new System.Drawing.Size(305, 25);
            this.btnCheckDB.TabIndex = 1;
            this.btnCheckDB.Text = "Check SQL Database";
            this.btnCheckDB.UseVisualStyleBackColor = true;
            this.btnCheckDB.Click += new System.EventHandler(this.btnCheckDB_Click);
            // 
            // lblHelp
            // 
            this.lblHelp.Location = new System.Drawing.Point(12, 25);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(296, 35);
            this.lblHelp.TabIndex = 0;
            this.lblHelp.Text = "Please make sure the certificates, typeID,  and iconID YAML files are in the reso" +
    "urces folder.";
            // 
            // SDEConverterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 232);
            this.Controls.Add(this.groupBox1);
            this.Name = "SDEConverterWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SDE Converter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Button btnCheckDB;
        private System.Windows.Forms.Button btnCheckMarketGroup;
        private System.Windows.Forms.Button btnGenerateCache;
        internal System.Windows.Forms.Label lblCheckingTools;
        internal System.Windows.Forms.Label lblDB;
    }
}

