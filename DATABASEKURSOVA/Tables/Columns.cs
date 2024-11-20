using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DATABASEKURSOVA
{
    public class Columns
    {
        private string connectionString;

        public Columns(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void LoadColumnNames(string currentTable, ComboBox comboColumns)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $"SHOW COLUMNS FROM {currentTable};";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    comboColumns.Items.Clear();
                    comboColumns.Items.Add("Показати все");
                    while (reader.Read())
                    {
                        string columnName = reader.GetString(0);
                        if (columnName.ToLower() != "id")
                        {
                            comboColumns.Items.Add(columnName);
                        }
                    }
                    reader.Close();

                    comboColumns.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadSelectedColumns(string currentTable, string columnName, DataGridView dataGridView1)
        {
            if (string.IsNullOrEmpty(currentTable)) return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = columnName == "Показати все" ? $"SELECT * FROM {currentTable};" : $"SELECT id, {columnName} FROM {currentTable};";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
