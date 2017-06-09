using System;
using System.Windows;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Excel;
using PlanetSystem.Models.Bodies;
using PlanetSystem.Models.Utilities;
using PlanetSystem.Data;
using System.Collections.Generic;
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

        private static void ImportDataFromExcel(string excelFilePath)
        {
            //using (var con = new OleDbConnection())
            //{
            //    var dt = new DataTable();
            //    var dir = System.IO.Path.GetDirectoryName(excelFilePath);
            //    var fileExtension = System.IO.Path.GetExtension(excelFilePath);
            //    var fileName = System.IO.Path.GetFileNameWithoutExtension(excelFilePath);

            //    if (fileExtension == ".xlsx" || fileExtension == ".xls")
            //    {
            //        con.ConnectionString = string.Format(
            //            "Provider=Microsoft.ACE.OLEDB.12.0;" +
            //            "Data Source={0};" +
            //            "Extended Properties='Excel 12.0;HDR=Yes;'",
            //            excelFilePath);

            //        con.Open();

            //        using (var comm = new OleDbCommand())
            //        {
            //            comm.CommandText = "SELECT * FROM [Sheet1$]";
            //            comm.Connection = con;

            //            // Import into Sql Server Database.
            //            const string sqlConnString = "Data Source=.\\sqlexpress;" +
            //                                         "Initial Catalog=PlanetSystems;" +
            //                                         "Integrated Security=true";
            //            using (var sqlCon = new SqlConnection(sqlConnString))
            //            {
            //                sqlCon.Open();
            //                var reader = comm.ExecuteReader();

            //                using (var bulkCopy = new SqlBulkCopy(sqlConnString))
            //                {
            //                    bulkCopy.DestinationTableName = "dbo.excel_table";
            //                    while (reader.Read())
            //                    {
            //                        try
            //                        {
            //                            bulkCopy.WriteToServer(reader);
            //                        }
            //                        catch (Exception e)
            //                        {
            //                            System.Windows.Forms.MessageBox.Show(e.Message);
            //                        }
            //                        finally
            //                        {
            //                            reader.Close();
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

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

            //DataSet result = excelReader.AsDataSet();
            PlanetarySystem planetarySystem = null;
            //var starsToAdd = new List<Star>();

            while (excelReader.Read())
            {
                var id = excelReader.GetInt32(0);
                var name = excelReader.GetString(1);
                var mass = excelReader.GetFloat(2);
                var veloX = excelReader.GetFloat(3);
                var veloY = excelReader.GetFloat(4);
                var veloZ = excelReader.GetFloat(5);
                var veloLen = excelReader.GetFloat(6);
                var veloTheta = excelReader.GetFloat(7);
                var veloPhi = excelReader.GetFloat(8);
                var centerX = excelReader.GetFloat(9);
                var centerY = excelReader.GetFloat(10);
                var centerZ = excelReader.GetFloat(11);
                var radius = excelReader.GetFloat(12);

                var newStar = new Star(
                                        new Models.Utilities.Point(centerX, centerY, centerY),
                                        mass,
                                        radius,
                                        new Models.Utilities.Vector(new Models.Utilities.Point(veloX, veloY, veloZ)),
                                        name);


                using (var ctx = new SqlServerContext())
                {
                    planetarySystem = ctx.PlanetarySystems
                                            .Where(x => x.Name == "Solar system")
                                            .FirstOrDefault();

                    var starToUpdate = ctx.Stars
                        .Where(x => x.StarId == planetarySystem.PlanetarySystemId)
                        .FirstOrDefault();

                    starToUpdate = newStar;

                    //planetarySystem.SetStar(starToUpdate);

                    ctx.SaveChanges();
                }

                //starsToAdd.Add(newStar);
            }

            //using (var ctx = new SqlServerContext())
            //{
            //    var stars = ctx.Set<Star>();
            //    stars.AddRange(starsToAdd);
            //    ctx.SaveChanges();
            //}

            excelReader.Close();

            if (planetarySystem != null)
            {
                Database.SavePlanetarySystem(planetarySystem);
            }

            System.Windows.MessageBox.Show("Import finished!");
        }
    }
}
