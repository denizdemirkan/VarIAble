using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerIAble.UI
{
    public class InputDialog : Form
    {
        private TextBox userInputTextBox;
        private Button okButton;
        private Label label1;
        private Button cancelButton;

        public string FileName { get; set; }

        public InputDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            userInputTextBox = new TextBox();
            okButton = new Button();
            cancelButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // userInputTextBox
            // 
            userInputTextBox.Location = new Point(42, 46);
            userInputTextBox.Name = "userInputTextBox";
            userInputTextBox.Size = new Size(200, 27);
            userInputTextBox.TabIndex = 0;
            // 
            // okButton
            // 
            okButton.Location = new Point(42, 96);
            okButton.Name = "okButton";
            okButton.Size = new Size(94, 31);
            okButton.TabIndex = 1;
            okButton.Text = "Export";
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(142, 96);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(100, 31);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.Click += CancelButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(84, 19);
            label1.Name = "label1";
            label1.Size = new Size(111, 20);
            label1.TabIndex = 3;
            label1.Text = "Save File Name";
            // 
            // InputDialog
            // 
            ClientSize = new Size(289, 143);
            Controls.Add(label1);
            Controls.Add(userInputTextBox);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Name = "InputDialog";
            Text = "Export File Name";
            ResumeLayout(false);
            PerformLayout();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            FileName = userInputTextBox.Text;
            if (FileName != null || FileName != "")
            {
                DialogResult = DialogResult.OK;
                MessageBox.Show("Settings Exported!", "Export Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                MessageBox.Show("Not a Valid Name!", "Export Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            MessageBox.Show("Export Canceled", "Export Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        public string UserInput
        {
            get { return userInputTextBox.Text; }
        }
    }
}
