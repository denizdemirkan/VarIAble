namespace VerIAble.UI
{
    partial class MainForm
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
            dataGridView1 = new DataGridView();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            loadDataToolStripMenuItem = new ToolStripMenuItem();
            calculateViolationsToolStripMenuItem = new ToolStripMenuItem();
            consoleToolStripMenuItem = new ToolStripMenuItem();
            createPDFToolStripMenuItem = new ToolStripMenuItem();
            typesToolStripMenuItem = new ToolStripMenuItem();
            testToolStripMenuItem = new ToolStripMenuItem();
            importSettingsToolStripMenuItem1 = new ToolStripMenuItem();
            exportSettingsToolStripMenuItem = new ToolStripMenuItem();
            statisticsToolStripMenuItem = new ToolStripMenuItem();
            codexBookToolStripMenuItem = new ToolStripMenuItem();
            regexBookToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 28);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(583, 713);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(583, 28);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { loadDataToolStripMenuItem, calculateViolationsToolStripMenuItem, typesToolStripMenuItem, testToolStripMenuItem, codexBookToolStripMenuItem, regexBookToolStripMenuItem, statisticsToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(60, 24);
            toolStripMenuItem1.Text = "Menu";
            // 
            // loadDataToolStripMenuItem
            // 
            loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
            loadDataToolStripMenuItem.Size = new Size(223, 26);
            loadDataToolStripMenuItem.Text = "Load Data";
            loadDataToolStripMenuItem.Click += loadDataToolStripMenuItem_Click;
            // 
            // calculateViolationsToolStripMenuItem
            // 
            calculateViolationsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { consoleToolStripMenuItem, createPDFToolStripMenuItem });
            calculateViolationsToolStripMenuItem.Name = "calculateViolationsToolStripMenuItem";
            calculateViolationsToolStripMenuItem.Size = new Size(223, 26);
            calculateViolationsToolStripMenuItem.Text = "Calculate Violations";
            // 
            // consoleToolStripMenuItem
            // 
            consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            consoleToolStripMenuItem.Size = new Size(224, 26);
            consoleToolStripMenuItem.Text = "See on Console";
            consoleToolStripMenuItem.Click += consoleToolStripMenuItem_Click;
            // 
            // createPDFToolStripMenuItem
            // 
            createPDFToolStripMenuItem.Name = "createPDFToolStripMenuItem";
            createPDFToolStripMenuItem.Size = new Size(224, 26);
            createPDFToolStripMenuItem.Text = "Create PDF";
            // 
            // typesToolStripMenuItem
            // 
            typesToolStripMenuItem.Name = "typesToolStripMenuItem";
            typesToolStripMenuItem.Size = new Size(223, 26);
            typesToolStripMenuItem.Text = "Custom Types";
            typesToolStripMenuItem.Click += typesToolStripMenuItem_Click;
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { importSettingsToolStripMenuItem1, exportSettingsToolStripMenuItem });
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new Size(223, 26);
            testToolStripMenuItem.Text = "Import/Export Tool";
            // 
            // importSettingsToolStripMenuItem1
            // 
            importSettingsToolStripMenuItem1.Name = "importSettingsToolStripMenuItem1";
            importSettingsToolStripMenuItem1.Size = new Size(194, 26);
            importSettingsToolStripMenuItem1.Text = "Import Settings";
            importSettingsToolStripMenuItem1.Click += importSettingsToolStripMenuItem1_Click;
            // 
            // exportSettingsToolStripMenuItem
            // 
            exportSettingsToolStripMenuItem.Name = "exportSettingsToolStripMenuItem";
            exportSettingsToolStripMenuItem.Size = new Size(194, 26);
            exportSettingsToolStripMenuItem.Text = "Export Settings";
            exportSettingsToolStripMenuItem.Click += exportSettingsToolStripMenuItem_Click;
            // 
            // statisticsToolStripMenuItem
            // 
            statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            statisticsToolStripMenuItem.Size = new Size(223, 26);
            statisticsToolStripMenuItem.Text = "Statistics";
            statisticsToolStripMenuItem.Click += statisticsToolStripMenuItem_Click;
            // 
            // codexBookToolStripMenuItem
            // 
            codexBookToolStripMenuItem.Name = "codexBookToolStripMenuItem";
            codexBookToolStripMenuItem.Size = new Size(223, 26);
            codexBookToolStripMenuItem.Text = "Codex Book";
            // 
            // regexBookToolStripMenuItem
            // 
            regexBookToolStripMenuItem.Name = "regexBookToolStripMenuItem";
            regexBookToolStripMenuItem.Size = new Size(223, 26);
            regexBookToolStripMenuItem.Text = "Regex Book";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(583, 741);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "VarIAble";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnLoad;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem loadDataToolStripMenuItem;
        private ToolStripMenuItem calculateViolationsToolStripMenuItem;
        private ToolStripMenuItem testToolStripMenuItem;
        private ToolStripMenuItem typesToolStripMenuItem;
        private ToolStripMenuItem consoleToolStripMenuItem;
        private ToolStripMenuItem importSettingsToolStripMenuItem1;
        private ToolStripMenuItem exportSettingsToolStripMenuItem;
        private ToolStripMenuItem createPDFToolStripMenuItem;
        private ToolStripMenuItem statisticsToolStripMenuItem;
        private ToolStripMenuItem codexBookToolStripMenuItem;
        private ToolStripMenuItem regexBookToolStripMenuItem;
    }
}