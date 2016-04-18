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
            this.groupBoxCharacterInfo = new System.Windows.Forms.GroupBox();
            this.listViewCapabilities = new System.Windows.Forms.ListView();
            this.columnHeaderCapability = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewCharacterSkills = new System.Windows.Forms.ListView();
            this.columnHeaderSkillName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBoxCharacterImage = new System.Windows.Forms.PictureBox();
            this.listViewCharacterSelector = new System.Windows.Forms.ListView();
            this.groupBoxPlanetaryColonies = new System.Windows.Forms.GroupBox();
            this.objectListViewPins = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnPinPlanet = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnInstallation = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnCycleTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnQuantityPerCycle = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnTimeLeft = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnExpiryTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnContentType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnContentQuantity = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnVolume = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objectListViewColonies = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnPlanet = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnSystem = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPlanetType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnUpgradeLevel = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnInstallations = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.groupBoxCharacterInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCharacterImage)).BeginInit();
            this.groupBoxPlanetaryColonies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewPins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewColonies)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxCharacterInfo
            // 
            this.groupBoxCharacterInfo.AutoSize = true;
            this.groupBoxCharacterInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxCharacterInfo.Controls.Add(this.listViewCapabilities);
            this.groupBoxCharacterInfo.Controls.Add(this.listViewCharacterSkills);
            this.groupBoxCharacterInfo.Controls.Add(this.pictureBoxCharacterImage);
            this.groupBoxCharacterInfo.Controls.Add(this.listViewCharacterSelector);
            this.groupBoxCharacterInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCharacterInfo.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCharacterInfo.Name = "groupBoxCharacterInfo";
            this.groupBoxCharacterInfo.Size = new System.Drawing.Size(923, 187);
            this.groupBoxCharacterInfo.TabIndex = 0;
            this.groupBoxCharacterInfo.TabStop = false;
            this.groupBoxCharacterInfo.Text = "Character Info";
            // 
            // listViewCapabilities
            // 
            this.listViewCapabilities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCapability,
            this.columnHeaderValue});
            this.listViewCapabilities.FullRowSelect = true;
            this.listViewCapabilities.GridLines = true;
            this.listViewCapabilities.Location = new System.Drawing.Point(699, 17);
            this.listViewCapabilities.Name = "listViewCapabilities";
            this.listViewCapabilities.Size = new System.Drawing.Size(224, 150);
            this.listViewCapabilities.TabIndex = 4;
            this.listViewCapabilities.UseCompatibleStateImageBehavior = false;
            this.listViewCapabilities.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderCapability
            // 
            this.columnHeaderCapability.Text = "Capability";
            this.columnHeaderCapability.Width = 172;
            // 
            // columnHeaderValue
            // 
            this.columnHeaderValue.Text = "Value";
            this.columnHeaderValue.Width = 48;
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
            this.listViewCharacterSkills.Size = new System.Drawing.Size(245, 150);
            this.listViewCharacterSkills.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewCharacterSkills.TabIndex = 3;
            this.listViewCharacterSkills.UseCompatibleStateImageBehavior = false;
            this.listViewCharacterSkills.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderSkillName
            // 
            this.columnHeaderSkillName.Text = "Skill Name";
            this.columnHeaderSkillName.Width = 194;
            // 
            // columnHeaderLevel
            // 
            this.columnHeaderLevel.Text = "Level";
            this.columnHeaderLevel.Width = 46;
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
            this.groupBoxPlanetaryColonies.Controls.Add(this.objectListViewPins);
            this.groupBoxPlanetaryColonies.Controls.Add(this.objectListViewColonies);
            this.groupBoxPlanetaryColonies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPlanetaryColonies.Location = new System.Drawing.Point(0, 187);
            this.groupBoxPlanetaryColonies.Name = "groupBoxPlanetaryColonies";
            this.groupBoxPlanetaryColonies.Size = new System.Drawing.Size(923, 474);
            this.groupBoxPlanetaryColonies.TabIndex = 1;
            this.groupBoxPlanetaryColonies.TabStop = false;
            this.groupBoxPlanetaryColonies.Text = "Planetary Colonies";
            // 
            // objectListViewPins
            // 
            this.objectListViewPins.AllColumns.Add(this.olvColumnPinPlanet);
            this.objectListViewPins.AllColumns.Add(this.olvColumnInstallation);
            this.objectListViewPins.AllColumns.Add(this.olvColumnCycleTime);
            this.objectListViewPins.AllColumns.Add(this.olvColumnQuantityPerCycle);
            this.objectListViewPins.AllColumns.Add(this.olvColumnTimeLeft);
            this.objectListViewPins.AllColumns.Add(this.olvColumnExpiryTime);
            this.objectListViewPins.AllColumns.Add(this.olvColumnContentType);
            this.objectListViewPins.AllColumns.Add(this.olvColumnContentQuantity);
            this.objectListViewPins.AllColumns.Add(this.olvColumnVolume);
            this.objectListViewPins.AllowColumnReorder = true;
            this.objectListViewPins.CellEditUseWholeCell = false;
            this.objectListViewPins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnPinPlanet,
            this.olvColumnInstallation,
            this.olvColumnCycleTime,
            this.olvColumnQuantityPerCycle,
            this.olvColumnTimeLeft,
            this.olvColumnExpiryTime,
            this.olvColumnContentType,
            this.olvColumnContentQuantity,
            this.olvColumnVolume});
            this.objectListViewPins.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListViewPins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewPins.FullRowSelect = true;
            this.objectListViewPins.GridLines = true;
            this.objectListViewPins.HighlightBackgroundColor = System.Drawing.Color.Empty;
            this.objectListViewPins.HighlightForegroundColor = System.Drawing.Color.Empty;
            this.objectListViewPins.Location = new System.Drawing.Point(3, 154);
            this.objectListViewPins.MultiSelect = false;
            this.objectListViewPins.Name = "objectListViewPins";
            this.objectListViewPins.ShowGroups = false;
            this.objectListViewPins.Size = new System.Drawing.Size(917, 317);
            this.objectListViewPins.TabIndex = 2;
            this.objectListViewPins.UseCompatibleStateImageBehavior = false;
            this.objectListViewPins.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnPinPlanet
            // 
            this.olvColumnPinPlanet.AspectName = "Planet";
            this.olvColumnPinPlanet.Text = "Planet";
            this.olvColumnPinPlanet.Width = 72;
            // 
            // olvColumnInstallation
            // 
            this.olvColumnInstallation.AspectName = "Type";
            this.olvColumnInstallation.AspectToStringFormat = "";
            this.olvColumnInstallation.Text = "Installation";
            this.olvColumnInstallation.Width = 190;
            // 
            // olvColumnCycleTime
            // 
            this.olvColumnCycleTime.AspectName = "CycleTime";
            this.olvColumnCycleTime.Text = "CycleTime";
            this.olvColumnCycleTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnCycleTime.Width = 68;
            // 
            // olvColumnQuantityPerCycle
            // 
            this.olvColumnQuantityPerCycle.AspectName = "QuantityPerCycle";
            this.olvColumnQuantityPerCycle.Text = "Quantity/Cycle";
            this.olvColumnQuantityPerCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnQuantityPerCycle.Width = 98;
            // 
            // olvColumnTimeLeft
            // 
            this.olvColumnTimeLeft.AspectName = "TimeLeft";
            this.olvColumnTimeLeft.AspectToStringFormat = "{0:dd\\.hh\\:mm\\:ss}";
            this.olvColumnTimeLeft.Text = "TTC";
            this.olvColumnTimeLeft.Width = 81;
            // 
            // olvColumnExpiryTime
            // 
            this.olvColumnExpiryTime.AspectName = "Expiration";
            this.olvColumnExpiryTime.Text = "Expiration";
            this.olvColumnExpiryTime.Width = 87;
            // 
            // olvColumnContentType
            // 
            this.olvColumnContentType.AspectName = "Commodity";
            this.olvColumnContentType.Text = "Commodity";
            this.olvColumnContentType.Width = 123;
            // 
            // olvColumnContentQuantity
            // 
            this.olvColumnContentQuantity.AspectName = "Quantity";
            this.olvColumnContentQuantity.Text = "Quantity";
            this.olvColumnContentQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnContentQuantity.Width = 66;
            // 
            // olvColumnVolume
            // 
            this.olvColumnVolume.AspectName = "Volume";
            this.olvColumnVolume.AspectToStringFormat = "{0:n}";
            this.olvColumnVolume.Text = "Volume";
            this.olvColumnVolume.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnVolume.Width = 66;
            // 
            // objectListViewColonies
            // 
            this.objectListViewColonies.AllColumns.Add(this.olvColumnPlanet);
            this.objectListViewColonies.AllColumns.Add(this.olvColumnSystem);
            this.objectListViewColonies.AllColumns.Add(this.olvColumnPlanetType);
            this.objectListViewColonies.AllColumns.Add(this.olvColumnUpgradeLevel);
            this.objectListViewColonies.AllColumns.Add(this.olvColumnInstallations);
            this.objectListViewColonies.CellEditUseWholeCell = false;
            this.objectListViewColonies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnPlanet,
            this.olvColumnSystem,
            this.olvColumnPlanetType,
            this.olvColumnUpgradeLevel,
            this.olvColumnInstallations});
            this.objectListViewColonies.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListViewColonies.Dock = System.Windows.Forms.DockStyle.Top;
            this.objectListViewColonies.FullRowSelect = true;
            this.objectListViewColonies.GridLines = true;
            this.objectListViewColonies.HighlightBackgroundColor = System.Drawing.Color.Empty;
            this.objectListViewColonies.HighlightForegroundColor = System.Drawing.Color.Empty;
            this.objectListViewColonies.Location = new System.Drawing.Point(3, 17);
            this.objectListViewColonies.Name = "objectListViewColonies";
            this.objectListViewColonies.ShowGroups = false;
            this.objectListViewColonies.Size = new System.Drawing.Size(917, 137);
            this.objectListViewColonies.TabIndex = 1;
            this.objectListViewColonies.UseCompatibleStateImageBehavior = false;
            this.objectListViewColonies.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnPlanet
            // 
            this.olvColumnPlanet.AspectName = "Planet";
            this.olvColumnPlanet.Text = "Planet";
            this.olvColumnPlanet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnPlanet.Width = 72;
            // 
            // olvColumnSystem
            // 
            this.olvColumnSystem.AspectName = "System";
            this.olvColumnSystem.Text = "System";
            this.olvColumnSystem.Width = 70;
            // 
            // olvColumnPlanetType
            // 
            this.olvColumnPlanetType.AspectName = "PlanetType";
            this.olvColumnPlanetType.Text = "Planet Type";
            this.olvColumnPlanetType.Width = 82;
            // 
            // olvColumnUpgradeLevel
            // 
            this.olvColumnUpgradeLevel.AspectName = "UpgradeLevel";
            this.olvColumnUpgradeLevel.Text = "Upgrade Level";
            this.olvColumnUpgradeLevel.Width = 83;
            // 
            // olvColumnInstallations
            // 
            this.olvColumnInstallations.AspectName = "InstallationCount";
            this.olvColumnInstallations.Text = "Installations";
            this.olvColumnInstallations.Width = 73;
            // 
            // FrmPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(923, 661);
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
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewPins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewColonies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCharacterInfo;
        private System.Windows.Forms.PictureBox pictureBoxCharacterImage;
        private System.Windows.Forms.ListView listViewCharacterSelector;
        private System.Windows.Forms.GroupBox groupBoxPlanetaryColonies;
        private System.Windows.Forms.ListView listViewCharacterSkills;
        private System.Windows.Forms.ColumnHeader columnHeaderSkillName;
        private System.Windows.Forms.ColumnHeader columnHeaderLevel;
        private System.Windows.Forms.ListView listViewCapabilities;
        private System.Windows.Forms.ColumnHeader columnHeaderCapability;
        private System.Windows.Forms.ColumnHeader columnHeaderValue;
        private BrightIdeasSoftware.ObjectListView objectListViewPins;
        private BrightIdeasSoftware.OLVColumn olvColumnInstallation;
        private BrightIdeasSoftware.OLVColumn olvColumnCycleTime;
        private BrightIdeasSoftware.OLVColumn olvColumnQuantityPerCycle;
        private BrightIdeasSoftware.OLVColumn olvColumnExpiryTime;
        private BrightIdeasSoftware.OLVColumn olvColumnContentType;
        private BrightIdeasSoftware.OLVColumn olvColumnContentQuantity;
        private BrightIdeasSoftware.ObjectListView objectListViewColonies;
        private BrightIdeasSoftware.OLVColumn olvColumnPlanet;
        private BrightIdeasSoftware.OLVColumn olvColumnSystem;
        private BrightIdeasSoftware.OLVColumn olvColumnPlanetType;
        private BrightIdeasSoftware.OLVColumn olvColumnUpgradeLevel;
        private BrightIdeasSoftware.OLVColumn olvColumnInstallations;
        private BrightIdeasSoftware.OLVColumn olvColumnVolume;
        private BrightIdeasSoftware.OLVColumn olvColumnTimeLeft;
        private BrightIdeasSoftware.OLVColumn olvColumnPinPlanet;
    }
}

