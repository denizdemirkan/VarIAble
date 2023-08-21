using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VerIAble.UI.Classes;

namespace VerIAble.UI
{
    public partial class CustomTypesForm : Form
    {

        private DataGridView dataGridView1;
        private Button btnAddType;
        private Button btnDeleteType;
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
            btnAddType = new Button();
            btnDeleteType = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 47);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1435, 459);
            dataGridView1.TabIndex = 0;
            // 
            // btnAddType
            // 
            btnAddType.Location = new Point(12, 12);
            btnAddType.Name = "btnAddType";
            btnAddType.Size = new Size(106, 29);
            btnAddType.TabIndex = 1;
            btnAddType.Text = "Add Type";
            btnAddType.UseVisualStyleBackColor = true;
            btnAddType.Click += btnAddType_Click;
            // 
            // btnDeleteType
            // 
            btnDeleteType.Location = new Point(124, 12);
            btnDeleteType.Name = "btnDeleteType";
            btnDeleteType.Size = new Size(106, 29);
            btnDeleteType.TabIndex = 2;
            btnDeleteType.Text = "Delete Type";
            btnDeleteType.UseVisualStyleBackColor = true;
            // 
            // CustomTypesForm
            // 
            ClientSize = new Size(1459, 518);
            Controls.Add(btnDeleteType);
            Controls.Add(btnAddType);
            Controls.Add(dataGridView1);
            Name = "CustomTypesForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            customTypes.Add(new CustomType("New Custom Type"));

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = this.customTypes;
        }
    }
}
