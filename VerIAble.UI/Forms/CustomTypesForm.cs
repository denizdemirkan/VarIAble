using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VerIAble.UI.Classes;

namespace VerIAble.UI.Forms
{
    public partial class CustomTypesForm : Form
    {

        private DataGridView dataGridView1;
        private Button btnAddType;
        private Button btnDeleteType;
        private Button btnSaveChanges;
        List<CustomType> customTypes;
        public MainForm mainForm;

        public CustomTypesForm(List<CustomType> customTypes, MainForm mainForm)
        {
            InitializeComponent();
            this.customTypes = customTypes;
            dataGridView1.DataSource = this.customTypes;
            this.mainForm = mainForm;
        }

        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            btnAddType = new Button();
            btnDeleteType = new Button();
            btnSaveChanges = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Bottom;
            dataGridView1.Location = new Point(0, 59);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1459, 459);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
            // 
            // btnAddType
            // 
            btnAddType.Location = new Point(12, 12);
            btnAddType.Name = "btnAddType";
            btnAddType.Size = new Size(106, 41);
            btnAddType.TabIndex = 1;
            btnAddType.Text = "Add Type";
            btnAddType.UseVisualStyleBackColor = true;
            btnAddType.Click += btnAddType_Click;
            // 
            // btnDeleteType
            // 
            btnDeleteType.Location = new Point(124, 12);
            btnDeleteType.Name = "btnDeleteType";
            btnDeleteType.Size = new Size(106, 41);
            btnDeleteType.TabIndex = 2;
            btnDeleteType.Text = "Delete Type";
            btnDeleteType.UseVisualStyleBackColor = true;
            // 
            // btnSaveChanges
            // 
            btnSaveChanges.Location = new Point(236, 12);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.Size = new Size(113, 41);
            btnSaveChanges.TabIndex = 3;
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.UseVisualStyleBackColor = true;
            btnSaveChanges.Click += btnSaveChanges_Click;
            // 
            // CustomTypesForm
            // 
            ClientSize = new Size(1459, 518);
            Controls.Add(btnSaveChanges);
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
            dataGridView1.DataSource = customTypes;
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            mainForm.saveChanges();
            MessageBox.Show("Changes Saved!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //MessageBox.Show(dataGridView1.Columns[e.ColumnIndex].Name);
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                if (dataGridView1.Columns[e.ColumnIndex].Name == "Codex")
                {
                    Console.WriteLine(dataGridView1.Columns[e.ColumnIndex].Name);

                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Text Files|*.txt";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFilePath = openFileDialog.FileName;

                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = selectedFilePath;
                    }
                }
            }
        }
    }
}
