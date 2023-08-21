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
        private Button cancelButton;

        public InputDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Değer Girişi";
            Width = 300;
            Height = 150;

            userInputTextBox = new TextBox();
            userInputTextBox.Width = 200;
            userInputTextBox.Location = new System.Drawing.Point(50, 50);
            Controls.Add(userInputTextBox);

            okButton = new Button();
            okButton.Text = "Tamam";
            okButton.Location = new System.Drawing.Point(50, 100);
            okButton.Click += OkButton_Click;
            Controls.Add(okButton);

            cancelButton = new Button();
            cancelButton.Text = "İptal";
            cancelButton.Location = new System.Drawing.Point(150, 100);
            cancelButton.Click += CancelButton_Click;
            Controls.Add(cancelButton);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public string UserInput
        {
            get { return userInputTextBox.Text; }
        }
    }
}
