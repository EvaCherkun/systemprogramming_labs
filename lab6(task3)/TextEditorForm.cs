using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace lab6_task_3
{
    public class TextEditorForm : Form
    {
        private readonly string _filePath;
        private readonly TextBox _textBox;

        public TextEditorForm(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));

            Text = Path.GetFileName(filePath);
            Size = new Size(560, 420);
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = true;

            _textBox = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                Dock = DockStyle.Fill,
                AcceptsReturn = true,
                AcceptsTab = true,
                WordWrap = false
            };

            var panel = new Panel { Dock = DockStyle.Bottom, Height = 36 };
            var btnSave = new Button { Text = "Зберегти", Location = new Point(8, 6), Size = new Size(75, 23) };
            var btnClose = new Button { Text = "Закрити", Anchor = AnchorStyles.Top | AnchorStyles.Right, Size = new Size(75, 23) };
            btnClose.Location = new Point(panel.Width - btnClose.Width - 8, 6);
            panel.Resize += (s, e) => { btnClose.Left = panel.ClientSize.Width - btnClose.Width - 8; };

            btnSave.Click += (s, e) => Save();
            btnClose.Click += (s, e) => Close();

            panel.Controls.Add(btnSave);
            panel.Controls.Add(btnClose);

            Controls.Add(panel);
            Controls.Add(_textBox);

            Load += TextEditorForm_Load;
        }

        private void TextEditorForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader reader = File.OpenText(_filePath))
                    _textBox.Text = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Помилка читання", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Save()
        {
            try
            {
                using (var w = new StreamWriter(_filePath, false, new UTF8Encoding(false)))
                    w.Write(_textBox.Text);
                MessageBox.Show(this, "Збережено", "Файл", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Помилка запису", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
