using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerIAble.UI
{
    public class CsvSeperatorQuestionForm : Form
    {
        private ComboBox optionComboBox;
        private Button okButton;
        private Label label1;
        private Button cancelButton;

        public CsvSeperatorQuestionForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            optionComboBox = new ComboBox();
            okButton = new Button();
            cancelButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // optionComboBox
            // 
            optionComboBox.Items.AddRange(new object[] { ";", "," });
            optionComboBox.Location = new Point(70, 46);
            optionComboBox.Name = "optionComboBox";
            optionComboBox.Size = new Size(172, 28);
            optionComboBox.TabIndex = 0;
            // 
            // okButton
            // 
            okButton.Location = new Point(70, 80);
            okButton.Name = "okButton";
            okButton.Size = new Size(76, 33);
            okButton.TabIndex = 1;
            okButton.Text = "Select";
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(170, 80);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(72, 33);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.Click += CancelButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(73, 23);
            label1.Name = "label1";
            label1.Size = new Size(169, 20);
            label1.TabIndex = 3;
            label1.Text = "Choose a CSV Seperator";
            // 
            // CsvSeperatorQuestionForm
            // 
            ClientSize = new Size(322, 145);
            Controls.Add(label1);
            Controls.Add(optionComboBox);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Name = "CsvSeperatorQuestionForm";
            Text = "Select CSV Seperator";
            ResumeLayout(false);
            PerformLayout();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            SelectedOption = optionComboBox.SelectedItem?.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public string SelectedOption { get; private set; }
    }
}
