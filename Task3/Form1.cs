using System;
using System.Windows.Forms;
using System.IO;
namespace BelieveOrNotBelieve
{
    public partial class Form1 : Form
    {
        const int mbyte = 8388608;

        // База данных с вопросами
        TrueFalse database;
        public Form1()
        {
            InitializeComponent();
        }

        // Обработчик пункта меню New
        private void miNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(sfd.FileName);
                database.Add("Введите вопрос", true);
                database.Save();
                nudNumber.Minimum = 1;
                nudNumber.Maximum = 1;
                nudNumber.Value = 1;
            };
        }

        // Обработчик пункта меню Open
        private void miOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var openedFile = new FileInfo(ofd.FileName);
                if (openedFile.Length > mbyte)
                {
                    MessageBox.Show("Слишком большой файл");
                    return;
                }
                
                var fileExtension = Path.GetExtension(ofd.FileName);
                if(Path.GetExtension(ofd.FileName) != ".dat")
                {
                    MessageBox.Show("Файл должен иметь расширение .dat");
                    return;
                }

                try
                {
                    database = new TrueFalse(ofd.FileName);
                    database.Load();
                    nudNumber.Minimum = 1;
                    nudNumber.Maximum = database.Count;
                    nudNumber.Value = 1;
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Нельзя открыть пустую базу!\n\n {exc.Message}", "Ошибка открытия файла");
                }
            }
        }

        // Обработчик пункта меню Save
        private void miSave_Click(object sender, EventArgs e)
        {
            if (database != null) database.Save();
            else MessageBox.Show("База данных не создана");
        }

        // Обработчик пункта меню Exit
        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Обработчик кнопки Добавить
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (database == null)
            {
                MessageBox.Show("Создайте новую базу данных", "Сообщение");
                return;
            }

            database.Add(tboxQuestion.Text, cboxTrue.Checked);
            nudNumber.Maximum = database.Count;
            nudNumber.Value = database.Count;
            tboxQuestion.Text = "Введите вопрос";
        }

        // Обработчик кнопки Удалить
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (nudNumber.Maximum == 1 || nudNumber.Maximum == 0 || database == null) return;
            database.Remove((int)nudNumber.Value);
            nudNumber.Maximum--;
            if (nudNumber.Value > 1) nudNumber.Value = nudNumber.Value;
        }

        // Обработчик кнопки Сохранить (вопрос)
        private void btnSaveQuest_Click(object sender, EventArgs e)
        {
            if(database == null)
            {
                MessageBox.Show("Не создана база данных", "Ошибка сохранения");
                return;
            }

            database[(int)nudNumber.Value - 1].text = tboxQuestion.Text;
            database[(int)nudNumber.Value - 1].trueFalse = cboxTrue.Checked;
        }

        // Обработчик события изменения значения numericUpDown
        private void nudNumber_ValueChanged(object sender, EventArgs e)
        {
            if (database == null || nudNumber.Value > database.Count || nudNumber.Value < 1) return;
            
            tboxQuestion.Text = database[(int)nudNumber.Value - 1].text;
            cboxTrue.Checked = database[(int)nudNumber.Value - 1].trueFalse;
        }

        private void aboutProgramm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Название: Верю - Не верю Database\n\nАвтор:       zEphyr\n\n" +
                "Версия      0.0001\n\nGPLv3 (GNU General Public License Version 3)\n\n" +
                "Дата лицензии: 14.03.2020", "О программе");
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(database.Count.ToString());
                database.SaveAs(sfd.FileName);
            };
        }
    }
}

