using System;
using System.Windows;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Excel;
using PlanetSystem.Data;
using System.Linq;

namespace PlanetSystem.UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xls, *xlsx)|*.xls;*.xlsx|" +
                                    "JSON files (*.json)|*.json|" +
                                    "XML files (*.xml)|*.xml|" +
                                    "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                ImportDataFromExcel(filePath);
            }
        }

        private void ImportDataFromExcel(string excelFilePath)
        {
            FileStream stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read);
            String fileExtension = Path.GetExtension(excelFilePath);
            IExcelDataReader excelReader = null;

            if (fileExtension == ".xlsx")
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else if (fileExtension == ".xls")
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }

            DataSet result = excelReader.AsDataSet();
            DataTable sheet1 = result.Tables[0];

            dataGrid.ItemsSource = sheet1.AsDataView();

            excelReader.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("No row selected!");
                return;
            }

            if (textBox.Text == null)
            {
                System.Windows.MessageBox.Show("Planetary system name not specified!");
                return;
            }

            DataRowView row = (DataRowView)dataGrid.SelectedItem;

            using (var ctx = new SqlServerContext())
            {
                var solarSystem = ctx.PlanetarySystems.FirstOrDefault(ps => ps.Name == textBox.Text);

                if (solarSystem == null)
                {
                    System.Windows.MessageBox.Show(string.Format("Planetary system with name '{0}' not found!", textBox.Text));
                    textBox.Text = null;
                    return;
                }

                var starToUpdate = ctx.Stars.FirstOrDefault(s => s.StarId == solarSystem.PlanetarySystemId);

                starToUpdate.Name = row[0].ToString();
                starToUpdate.Mass = Convert.ToDouble(row[1]);
                starToUpdate.Radius = Convert.ToDouble(row[2]);

                ctx.SaveChanges();
            }

            System.Windows.MessageBox.Show("Update finished!");
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
