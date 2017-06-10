using System;
using System.Windows;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Excel;
using PlanetSystem.Data;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;

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
            openFileDialog.Filter = "XML files (*.xml)|*.xml|" +
                                    "Excel files (*.xls, *xlsx)|*.xls;*.xlsx|" +
                                    "JSON files (*.json)|*.json|" +
                                    "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;

                String fileExtension = Path.GetExtension(filePath);
                switch (fileExtension)
                {
                    case ".xlsx":
                    case ".xls":
                        ImportDataFromExcel(filePath);
                        break;
                    case ".json":
                        ImportDataFromJson(filePath);
                        break;
                    case ".xml":
                        ImportDataFromXml(filePath);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ImportDataFromXml(string xmlFilePath)
        {
            //List<Star> stars = new List<Star>();
            //XmlSerializer serializer = new XmlSerializer(typeof(List<Star>), new XmlRootAttribute("Stars"));

            //using (FileStream stream = File.Open(xmlFilePath, FileMode.Open, FileAccess.Read))
            //{
            //    stars = serializer.Deserialize(stream) as List<Star>;
            //}

            //dataGrid.ItemsSource = stars;

            DataSet ds = new DataSet();

            using (FileStream stream = File.Open(xmlFilePath, FileMode.Open))
            {
                try
                {
                    ds.ReadXml(stream);
                    dataGrid.ItemsSource = ds.Tables[0].AsDataView();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }

        }

        private void ImportDataFromJson(string filePath)
        {
            throw new NotImplementedException();
        }

        private void ImportDataFromExcel(string excelFilePath)
        {
            using (FileStream stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read))
            {
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

                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();
                DataTable sheet1 = result.Tables[0];

                dataGrid.ItemsSource = sheet1.AsDataView();

                excelReader.Close();
            }
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
