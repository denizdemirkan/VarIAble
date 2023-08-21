using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerIAble.UI.Classes;

namespace VerIAble.UI
{
    public partial class CustomTypesForm : Form
    {

        private DataGridView dataGridView1;
        List<CustomType> customTypes;
        public CustomTypesForm(List<CustomType> customTypes)
        {
            InitializeComponent();
            this.customTypes = customTypes;
            dataGridView1.DataSource = this.customTypes;
        }

        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1435, 494);
            dataGridView1.TabIndex = 0;
            // 
            // CustomTypesForm
            // 
            ClientSize = new Size(1459, 518);
            Controls.Add(dataGridView1);
            Name = "CustomTypesForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }
    }
}
