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
        private Button cancelButton;

        public CsvSeperatorQuestionForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Seçenek Seçimi";
            Width = 300;
            Height = 150;

            optionComboBox = new ComboBox();
            optionComboBox.Items.AddRange(new string[] { ";", "," });
            optionComboBox.Location = new System.Drawing.Point(50, 50);
            Controls.Add(optionComboBox);

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
