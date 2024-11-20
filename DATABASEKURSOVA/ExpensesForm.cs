using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DATABASEKURSOVA
{
    public partial class ExpensesForm : Form
    {
        private string connectionString;
        private string currentTable;
        private int? selectedId; // Для редагування ми будемо зберігати ID запису

        public ExpensesForm(string connectionString, string currentTable, Dictionary<string, object> rowData = null)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.currentTable = currentTable;


            dateExpenses.Format = DateTimePickerFormat.Custom;
            dateExpenses.CustomFormat = "yyyy-MM-dd";

            // Якщо rowData не null, то це редагування, і ми заповнюємо поля
            if (rowData != null)
            {
                textBoxExpenses.Text = rowData.ContainsKey("expenses_type") ? rowData["expenses_type"].ToString() : "";
                textBoxAmount.Text = rowData.ContainsKey("amount") ? rowData["amount"].ToString() : "";
                textBoxDescription.Text = rowData.ContainsKey("description") ? rowData["description"].ToString() : "";
                dateExpenses.Text = rowData.ContainsKey("expenses_date") ? rowData["expenses_date"].ToString() : "";

                this.selectedId = (int)rowData["id"];
                btnSave.Text = "Оновити запис"; // Якщо це редагування, змінюємо текст кнопки
                this.Text = "Редагування";
            }
            else
            {
                textBoxExpenses.Clear();
                textBoxAmount.Clear();
                textBoxDescription.Clear();
                btnSave.Text = "Створити запис"; // Текст кнопки для створення нового запису
                this.Text = "Створення";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedId.HasValue)
                {
                    // Якщо selectedId не null, то це оновлення існуючого запису
                    string query = $"UPDATE {currentTable} SET expenses_type = @expenses_type, amount = @amount, description = @description, expenses_date = @expenses_date WHERE id = @id";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@expenses_type", textBoxExpenses.Text);
                            cmd.Parameters.AddWithValue("@amount", textBoxAmount.Text);
                            cmd.Parameters.AddWithValue("@description", textBoxDescription.Text);
                            cmd.Parameters.AddWithValue("@expenses_date", dateExpenses.Text);
                            cmd.Parameters.AddWithValue("@id", selectedId.Value);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Запис оновлено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Якщо selectedId null, то це новий запис
                    string query = $"INSERT INTO {currentTable} (expenses_type, amount, description, expenses_date) VALUES (@expenses_type, @amount, @description, @expenses_date);";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@expenses_type", textBoxExpenses.Text);
                            cmd.Parameters.AddWithValue("@amount", textBoxAmount.Text);
                            cmd.Parameters.AddWithValue("@description", textBoxDescription.Text);
                            cmd.Parameters.AddWithValue("@expenses_date", dateExpenses.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Новий запис створено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
