using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DATABASEKURSOVA.Properties;
using MySql.Data.MySqlClient;

namespace DATABASEKURSOVA
{
    public partial class MainForm : Form
    {
        private string connectionString;    // Рядок підключення
        private string currentTable;        // Обрана таблиця

        private string server = "localhost"; // Сервер MySQL
        private string user = "root";        // Користувач
        private string password = "1234";   // Пароль
        private string database = "trolleydepot"; // Назва бази даних
        private string batFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "importbase.bat");

        private TablesSettings tables;
        private Columns columns;
        private FormFactory formFactory;    // Фабрика форм
        private DatabaseAudit databaseAudit;

        public MainForm()
        {
            InitializeComponent();
            connectionString = $"Server={server};Database={database};User ID={user};Password={password};";

            databaseAudit = new DatabaseAudit(connectionString);
            tables = new TablesSettings(connectionString);
            formFactory = new FormFactory(connectionString);
            columns = new Columns(connectionString);

            Environment.SetEnvironmentVariable("DB_PASSWORD", password);


            try
            {
                // Перевірка бази даних і запуск файлу .bat, якщо її не існує
                databaseAudit.CheckAndRunBatFile(connectionString, database, batFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            tables.LoadTables(comboTables);

        }

       
        // Подія TextChanged для пошуку
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = textBoxSearch.Text;
            if (comboColumns.SelectedItem != null)
            {
                string selectedColumn = comboColumns.SelectedItem.ToString();
                if (selectedColumn == "Показати все")
                {
                    tables.LoadTableData(currentTable, dataGridView1, searchQuery);
                }
                else
                {
                    tables.LoadTableData(currentTable, dataGridView1, searchQuery, selectedColumn);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentTable))
            {
                MessageBox.Show("Виберіть таблицю, щоб додати новий запис.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Form form = formFactory.GetForm(currentTable);
                form.ShowDialog();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (comboColumns.SelectedItem != null)
            {
                string selectedColumn = comboColumns.SelectedItem.ToString();
                if (selectedColumn == "Показати все")
                {
                    columns.LoadSelectedColumns(currentTable, "Показати все", dataGridView1);
                }
                else
                {
                    columns.LoadSelectedColumns(currentTable, selectedColumn, dataGridView1);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Виберіть запис для редагування.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("Виберіть тільки один запис для редагування.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            Dictionary<string, object> rowData = new Dictionary<string, object>();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                rowData[column.Name] = selectedRow.Cells[column.Name].Value;
            }

            try
            {
                Form form = formFactory.GetForm(currentTable, rowData);
                form.ShowDialog();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (comboColumns.SelectedItem != null)
            {
                string selectedColumn = comboColumns.SelectedItem.ToString();
                if (selectedColumn == "Показати все")
                {
                    columns.LoadSelectedColumns(currentTable, "Показати все", dataGridView1);
                }
                else
                {
                    columns.LoadSelectedColumns(currentTable, selectedColumn, dataGridView1);
                }
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Виберіть запис для видалення.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Ви впевнені, що хочете видалити вибрані записи?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int id = Convert.ToInt32(row.Cells["id"].Value);
                        string deleteQuery = $"DELETE FROM {currentTable} WHERE id = @id";
                        using (MySqlCommand cmd = new MySqlCommand(deleteQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Записи видалено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (comboColumns.SelectedItem != null)
            {
                string selectedColumn = comboColumns.SelectedItem.ToString();
                if (selectedColumn == "Показати все")
                {
                    columns.LoadSelectedColumns(currentTable, "Показати все", dataGridView1);
                }
                else
                {
                    columns.LoadSelectedColumns(currentTable, selectedColumn, dataGridView1);
                }
            }
        }

        private void comboColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboColumns.SelectedItem == null) return;

            string selectedColumn = comboColumns.SelectedItem.ToString();
            columns.LoadSelectedColumns(currentTable, selectedColumn, dataGridView1);
        }

        // Завантаження даних із таблиці
        private void comboTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboTables.SelectedItem == null) return;

            currentTable = comboTables.SelectedItem.ToString();
            columns.LoadColumnNames(currentTable, comboColumns);
            columns.LoadSelectedColumns(currentTable, "Показати все", dataGridView1);
        }
    }
}

