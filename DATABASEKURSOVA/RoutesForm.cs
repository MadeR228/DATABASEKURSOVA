using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DATABASEKURSOVA
{
    public partial class RoutesForm : Form
    {
        private string connectionString;
        private string currentTable;
        private int? selectedId; // Для редагування ми будемо зберігати ID запису

        public RoutesForm(string connectionString, string currentTable, Dictionary<string, object> rowData = null)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.currentTable = currentTable;

            // Якщо rowData не null, то це редагування, і ми заповнюємо поля
            if (rowData != null)
            {
                textBoxRouteNum.Text = rowData.ContainsKey("route_num") ? rowData["route_num"].ToString() : "";
                textBoxStart.Text = rowData.ContainsKey("start_point") ? rowData["start_point"].ToString() : "";
                textBoxEnd.Text = rowData.ContainsKey("end_point") ? rowData["end_point"].ToString() : "";
                textBoxDescription.Text = rowData.ContainsKey("description") ? rowData["description"].ToString() : "";

                this.selectedId = (int)rowData["id"];
                btnSave.Text = "Оновити запис"; // Якщо це редагування, змінюємо текст кнопки
                this.Text = "Редагування";
            }
            else
            {
                textBoxRouteNum.Clear();
                textBoxStart.Clear();
                textBoxEnd.Clear();
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
                    string query = $"UPDATE {currentTable} SET route_num = @route_num, start_point = @start_point, end_point = @end_point, description = @description WHERE id = @id";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@route_num", textBoxRouteNum.Text);
                            cmd.Parameters.AddWithValue("@start_point", textBoxStart.Text);
                            cmd.Parameters.AddWithValue("@end_point", textBoxEnd.Text);
                            cmd.Parameters.AddWithValue("@description", textBoxDescription.Text);
                            cmd.Parameters.AddWithValue("@id", selectedId.Value);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Запис оновлено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Якщо selectedId null, то це новий запис
                    string query = $"INSERT INTO {currentTable} (route_num, start_point, end_point, description) VALUES (@route_num, @start_point, @end_point, @description);";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@route_num", textBoxRouteNum.Text);
                            cmd.Parameters.AddWithValue("@start_point", textBoxStart.Text);
                            cmd.Parameters.AddWithValue("@end_point", textBoxEnd.Text);
                            cmd.Parameters.AddWithValue("@description", textBoxDescription.Text);

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
