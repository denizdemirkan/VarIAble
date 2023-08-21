using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerIAble.UI.Classes;

namespace VerIAble.UI
{
    public partial class ExportImportForm : Form
    {
        List<CustomType> customTypes;
        public ExportImportForm(List<CustomType> customTypes)
        {
            InitializeComponent();
            this.customTypes = customTypes;
        }

        private Button button1;
        private Button button2;
        private CheckedListBox checkedListBox1;
        private Button btnSave;
        private Button btnLoad;
        private Label label1;

        private void InitializeComponent()
        {
            btnSave = new Button();
            btnLoad = new Button();
            checkedListBox1 = new CheckedListBox();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 12);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(112, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(94, 29);
            btnLoad.TabIndex = 1;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Items.AddRange(new object[] { "Seperator:  ,", "Seperator:  ;" });
            checkedListBox1.Location = new Point(12, 107);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(133, 48);
            checkedListBox1.TabIndex = 2;
            // 
            // ExportImportForm
            // 
            ClientSize = new Size(1316, 636);
            Controls.Add(checkedListBox1);
            Controls.Add(btnLoad);
            Controls.Add(btnSave);
            Name = "ExportImportForm";
            ResumeLayout(false);
        }
    }
}
