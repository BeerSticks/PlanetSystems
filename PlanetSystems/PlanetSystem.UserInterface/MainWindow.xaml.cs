using System;
using System.Windows;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

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
            using (var con = new OleDbConnection())
            {
                var dt = new DataTable();
                var dir = System.IO.Path.GetDirectoryName(excelFilePath);
                var fileExtension = System.IO.Path.GetExtension(excelFilePath);
                var fileName = System.IO.Path.GetFileNameWithoutExtension(excelFilePath);

                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                {
                    con.ConnectionString = string.Format(
                        "Provider=Microsoft.ACE.OLEDB.12.0;" +
                        "Data Source={0};" +
                        "Extended Properties='Excel 12.0;HDR=Yes;'",
                        excelFilePath);

                    con.Open();

                    using (var comm = new OleDbCommand())
                    {
                        comm.CommandText = "SELECT * FROM [Sheet1$]";
                        comm.Connection = con;

                        // Import into Sql Server Database.
                        const string sqlConnString = "Data Source=.\\sqlexpress;" +
                                                     "Initial Catalog=PlanetSystems;" +
                                                     "Integrated Security=true";
                        using (var sqlCon = new SqlConnection(sqlConnString))
                        {
                            sqlCon.Open();
                            var reader = comm.ExecuteReader();

                            using (var bulkCopy = new SqlBulkCopy(sqlConnString))
                            {
                                bulkCopy.DestinationTableName = "dbo.excel_table";
                                while (reader.Read())
                                {
                                    try
                                    {
                                        bulkCopy.WriteToServer(reader);
                                    }
                                    catch (Exception e)
                                    {
                                        System.Windows.Forms.MessageBox.Show(e.Message);
                                    }
                                    finally
                                    {
                                        reader.Close();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
