using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DATABASEKURSOVA
{
    public class TablesSettings
    {
        private string connectionString;

        public TablesSettings(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Завантаження таблиць із бази даних
        public void LoadTables(ComboBox comboTables)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SHOW TABLES;";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    comboTables.Items.Clear();
                    while (reader.Read())
                    {
                        comboTables.Items.Add(reader.GetString(0));
                    }
                    reader.Close();

                    if (comboTables.Items.Count > 0)
                        comboTables.SelectedIndex = 0; // Вибрати першу таблицю
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Імпорт бази даних не хвилюйтесь)", "Імпорт", MessageBoxButtons.OK);
                LoadTables(comboTables);
            }
        }

        // Завантаження даних із поточної таблиці з пошуком та обраною колонкою
        public void LoadTableData(string currentTable, DataGridView dataGridView, string searchQuery = "", string columnName = "")
        {
            if (string.IsNullOrEmpty(currentTable)) return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;
                    if (string.IsNullOrEmpty(columnName) || columnName == "Показать все")
                    {
                        query = string.IsNullOrEmpty(searchQuery)
                            ? $"SELECT * FROM {currentTable};"
                            : $"SELECT * FROM {currentTable} WHERE CONCAT_WS(' ', {GetColumnNames(currentTable)}) LIKE '%{searchQuery}%';";
                    }
                    else
                    {
                        query = string.IsNullOrEmpty(searchQuery)
                            ? $"SELECT id, {columnName} FROM {currentTable};"
                            : $"SELECT id, {columnName} FROM {currentTable} WHERE {columnName} LIKE '%{searchQuery}%';";
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Отримання назв усіх колонок таблиці
        public string GetColumnNames(string currentTable)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $"SHOW COLUMNS FROM {currentTable};";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    string columnNames = "";
                    while (reader.Read())
                    {
                        columnNames += $"{reader.GetString(0)}, ";
                    }
                    reader.Close();

                    return columnNames.TrimEnd(',', ' ');
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "*";
            }
        }

    }
}
