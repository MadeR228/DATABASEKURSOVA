using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MySql.Data.MySqlClient;

namespace DATABASEKURSOVA
{
    public partial class MaintenanceForm : Form
    {
        private string connectionString;
        private string currentTable;
        private int? selectedId; // Для редагування ми будемо зберігати ID запису

        public MaintenanceForm(string connectionString, string currentTable, Dictionary<string, object> rowData = null)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.currentTable = currentTable;


            dateService.Format = DateTimePickerFormat.Custom;
            dateService.CustomFormat = "yyyy-MM-dd";

            // Якщо rowData не null, то це редагування, і ми заповнюємо поля
            if (rowData != null)
            {
                textBoxDescription.Text = rowData.ContainsKey("description") ? rowData["description"].ToString() : "";
                textBoxCost.Text = rowData.ContainsKey("cost") ? rowData["cost"].ToString() : "";
                textBoxTrolleyID.Text = rowData.ContainsKey("trolleybusID") ? rowData["trolleybusID"].ToString() : "";
                dateService.Text = rowData.ContainsKey("service_date") ? rowData["service_date"].ToString() : "";

                this.selectedId = (int)rowData["id"];
                btnSave.Text = "Оновити запис"; // Якщо це редагування, змінюємо текст кнопки
                this.Text = "Редагування";
            }
            else
            {
                textBoxDescription.Clear();
                textBoxCost.Clear();
                textBoxTrolleyID.Clear();
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
                    string query = $"UPDATE {currentTable} SET description = @description, cost = @cost, trolleybusID = @trolleybusID, service_date = @service_date WHERE id = @id";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@description", textBoxDescription.Text);
                            cmd.Parameters.AddWithValue("@cost", textBoxCost.Text);
                            cmd.Parameters.AddWithValue("@trolleybusID", textBoxTrolleyID.Text);
                            cmd.Parameters.AddWithValue("@service_date", dateService.Text);
                            cmd.Parameters.AddWithValue("@id", selectedId.Value);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Запис оновлено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Якщо selectedId null, то це новий запис
                    string query = $"INSERT INTO {currentTable} (description, cost, trolleybusID, service_date) VALUES (@description, @cost, @trolleybusID, @service_date);";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@description", textBoxDescription.Text);
                            cmd.Parameters.AddWithValue("@cost", textBoxCost.Text);
                            cmd.Parameters.AddWithValue("@trolleybusID", textBoxTrolleyID.Text);
                            cmd.Parameters.AddWithValue("@service_date", dateService.Text);

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
