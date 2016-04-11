namespace EveHQ.PlanetaryInteraction
{
    partial class FrmPI
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
            this.components = new System.ComponentModel.Container();
            this.groupBoxCharacterInfo = new System.Windows.Forms.GroupBox();
            this.listViewCharacterSkills = new System.Windows.Forms.ListView();
            this.columnHeaderSkillName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBoxCharacterImage = new System.Windows.Forms.PictureBox();
            this.listViewCharacterSelector = new System.Windows.Forms.ListView();
            this.groupBoxPlanetaryColonies = new System.Windows.Forms.GroupBox();
            this.dataGridViewPlanetaryColonies = new System.Windows.Forms.DataGridView();
            this.ColumnSolarSystemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSolarSystemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPlanetID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPlanetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPlanetTypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPlanetTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOwnerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLastUpdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUpgradeLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNumberOfPins = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planetaryColoniesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBoxCharacterInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCharacterImage)).BeginInit();
            this.groupBoxPlanetaryColonies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlanetaryColonies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.planetaryColoniesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxCharacterInfo
            // 
            this.groupBoxCharacterInfo.AutoSize = true;
            this.groupBoxCharacterInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxCharacterInfo.Controls.Add(this.listViewCharacterSkills);
            this.groupBoxCharacterInfo.Controls.Add(this.pictureBoxCharacterImage);
            this.groupBoxCharacterInfo.Controls.Add(this.listViewCharacterSelector);
            this.groupBoxCharacterInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCharacterInfo.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCharacterInfo.Name = "groupBoxCharacterInfo";
            this.groupBoxCharacterInfo.Size = new System.Drawing.Size(784, 187);
            this.groupBoxCharacterInfo.TabIndex = 0;
            this.groupBoxCharacterInfo.TabStop = false;
            this.groupBoxCharacterInfo.Text = "Character Info";
            // 
            // listViewCharacterSkills
            // 
            this.listViewCharacterSkills.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSkillName,
            this.columnHeaderLevel});
            this.listViewCharacterSkills.FullRowSelect = true;
            this.listViewCharacterSkills.GridLines = true;
            this.listViewCharacterSkills.Location = new System.Drawing.Point(447, 17);
            this.listViewCharacterSkills.Name = "listViewCharacterSkills";
            this.listViewCharacterSkills.Size = new System.Drawing.Size(334, 150);
            this.listViewCharacterSkills.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewCharacterSkills.TabIndex = 2;
            this.listViewCharacterSkills.UseCompatibleStateImageBehavior = false;
            this.listViewCharacterSkills.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderSkillName
            // 
            this.columnHeaderSkillName.Text = "Skill Name";
            this.columnHeaderSkillName.Width = 263;
            // 
            // columnHeaderLevel
            // 
            this.columnHeaderLevel.Text = "Level";
            // 
            // pictureBoxCharacterImage
            // 
            this.pictureBoxCharacterImage.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxCharacterImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxCharacterImage.Location = new System.Drawing.Point(291, 17);
            this.pictureBoxCharacterImage.Name = "pictureBoxCharacterImage";
            this.pictureBoxCharacterImage.Size = new System.Drawing.Size(150, 150);
            this.pictureBoxCharacterImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCharacterImage.TabIndex = 1;
            this.pictureBoxCharacterImage.TabStop = false;
            // 
            // listViewCharacterSelector
            // 
            this.listViewCharacterSelector.FullRowSelect = true;
            this.listViewCharacterSelector.GridLines = true;
            this.listViewCharacterSelector.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewCharacterSelector.HideSelection = false;
            this.listViewCharacterSelector.Location = new System.Drawing.Point(3, 17);
            this.listViewCharacterSelector.MultiSelect = false;
            this.listViewCharacterSelector.Name = "listViewCharacterSelector";
            this.listViewCharacterSelector.Size = new System.Drawing.Size(282, 150);
            this.listViewCharacterSelector.TabIndex = 0;
            this.listViewCharacterSelector.UseCompatibleStateImageBehavior = false;
            this.listViewCharacterSelector.View = System.Windows.Forms.View.List;
            this.listViewCharacterSelector.SelectedIndexChanged += new System.EventHandler(this.listViewCharacterSelector_SelectedIndexChanged);
            // 
            // groupBoxPlanetaryColonies
            // 
            this.groupBoxPlanetaryColonies.AutoSize = true;
            this.groupBoxPlanetaryColonies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxPlanetaryColonies.Controls.Add(this.dataGridViewPlanetaryColonies);
            this.groupBoxPlanetaryColonies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPlanetaryColonies.Location = new System.Drawing.Point(0, 187);
            this.groupBoxPlanetaryColonies.Name = "groupBoxPlanetaryColonies";
            this.groupBoxPlanetaryColonies.Size = new System.Drawing.Size(784, 474);
            this.groupBoxPlanetaryColonies.TabIndex = 1;
            this.groupBoxPlanetaryColonies.TabStop = false;
            this.groupBoxPlanetaryColonies.Text = "Planetary Colonies";
            // 
            // dataGridViewPlanetaryColonies
            // 
            this.dataGridViewPlanetaryColonies.AllowUserToAddRows = false;
            this.dataGridViewPlanetaryColonies.AllowUserToDeleteRows = false;
            this.dataGridViewPlanetaryColonies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlanetaryColonies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSolarSystemID,
            this.ColumnSolarSystemName,
            this.ColumnPlanetID,
            this.ColumnPlanetName,
            this.ColumnPlanetTypeID,
            this.ColumnPlanetTypeName,
            this.ColumnOwnerID,
            this.ColumnOwnerName,
            this.ColumnLastUpdate,
            this.ColumnUpgradeLevel,
            this.ColumnNumberOfPins});
            this.dataGridViewPlanetaryColonies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPlanetaryColonies.Location = new System.Drawing.Point(3, 17);
            this.dataGridViewPlanetaryColonies.Name = "dataGridViewPlanetaryColonies";
            this.dataGridViewPlanetaryColonies.Size = new System.Drawing.Size(778, 454);
            this.dataGridViewPlanetaryColonies.TabIndex = 0;
            // 
            // ColumnSolarSystemID
            // 
            this.ColumnSolarSystemID.HeaderText = "SolarSystemID";
            this.ColumnSolarSystemID.Name = "ColumnSolarSystemID";
            // 
            // ColumnSolarSystemName
            // 
            this.ColumnSolarSystemName.HeaderText = "SolarSystemName";
            this.ColumnSolarSystemName.Name = "ColumnSolarSystemName";
            // 
            // ColumnPlanetID
            // 
            this.ColumnPlanetID.HeaderText = "PlanetID";
            this.ColumnPlanetID.Name = "ColumnPlanetID";
            // 
            // ColumnPlanetName
            // 
            this.ColumnPlanetName.HeaderText = "PlanetName";
            this.ColumnPlanetName.Name = "ColumnPlanetName";
            // 
            // ColumnPlanetTypeID
            // 
            this.ColumnPlanetTypeID.HeaderText = "PlanetTypeID";
            this.ColumnPlanetTypeID.Name = "ColumnPlanetTypeID";
            // 
            // ColumnPlanetTypeName
            // 
            this.ColumnPlanetTypeName.HeaderText = "PlanetTypeName";
            this.ColumnPlanetTypeName.Name = "ColumnPlanetTypeName";
            // 
            // ColumnOwnerID
            // 
            this.ColumnOwnerID.HeaderText = "OwnerID";
            this.ColumnOwnerID.Name = "ColumnOwnerID";
            // 
            // ColumnOwnerName
            // 
            this.ColumnOwnerName.HeaderText = "OwnerName";
            this.ColumnOwnerName.Name = "ColumnOwnerName";
            // 
            // ColumnLastUpdate
            // 
            this.ColumnLastUpdate.HeaderText = "LastUpdate";
            this.ColumnLastUpdate.Name = "ColumnLastUpdate";
            // 
            // ColumnUpgradeLevel
            // 
            this.ColumnUpgradeLevel.HeaderText = "UpgradeLevel";
            this.ColumnUpgradeLevel.Name = "ColumnUpgradeLevel";
            // 
            // ColumnNumberOfPins
            // 
            this.ColumnNumberOfPins.HeaderText = "NumberOfPins";
            this.ColumnNumberOfPins.Name = "ColumnNumberOfPins";
            // 
            // planetaryColoniesBindingSource
            // 
            this.planetaryColoniesBindingSource.DataSource = typeof(EveHQ.NewEveApi.Entities.PlanetaryColonies);
            // 
            // FrmPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.groupBoxPlanetaryColonies);
            this.Controls.Add(this.groupBoxCharacterInfo);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmPI";
            this.Text = "Planetary Colonies";
            this.Load += new System.EventHandler(this.FrmPI_Load);
            this.groupBoxCharacterInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCharacterImage)).EndInit();
            this.groupBoxPlanetaryColonies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlanetaryColonies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.planetaryColoniesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCharacterInfo;
        private System.Windows.Forms.PictureBox pictureBoxCharacterImage;
        private System.Windows.Forms.ListView listViewCharacterSelector;
        private System.Windows.Forms.GroupBox groupBoxPlanetaryColonies;
        private System.Windows.Forms.DataGridView dataGridViewPlanetaryColonies;
        private System.Windows.Forms.ListView listViewCharacterSkills;
        private System.Windows.Forms.ColumnHeader columnHeaderSkillName;
        private System.Windows.Forms.ColumnHeader columnHeaderLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSolarSystemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSolarSystemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPlanetID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPlanetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPlanetTypeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPlanetTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOwnerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOwnerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUpgradeLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumberOfPins;
        private System.Windows.Forms.BindingSource planetaryColoniesBindingSource;
    }
}

