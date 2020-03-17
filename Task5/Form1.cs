using System;
using System.IO;
using System.Windows.Forms;

namespace Task5
{
    public partial class Form1 : Form
    {
        Csv_convert_Xml converter = new Csv_convert_Xml();

        public Form1()
        {
            InitializeComponent();
        }

        private void openCSV_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                converter.readCsv(ofd.FileName);
            }
            MessageBox.Show($"Файл успешно открыт", "zEphyr");
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            converter.convertCsv();
        }
    }
}
