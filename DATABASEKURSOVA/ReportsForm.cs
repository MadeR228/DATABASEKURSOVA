using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DATABASEKURSOVA
{
    public partial class ReportsForm : Form
    {
        private string connectionString;
        private string currentTable;
        private int? selectedId; // Для редагування ми будемо зберігати ID запису

        public ReportsForm(string connectionString, string currentTable, Dictionary<string, object> rowData = null)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.currentTable = currentTable;

            dateReport.Format = DateTimePickerFormat.Custom;
            dateReport.CustomFormat = "yyyy-MM-dd";

            // Якщо rowData не null, то це редагування, і ми заповнюємо поля
            if (rowData != null)
            {
                textBoxPassenger.Text = rowData.ContainsKey("passengers_count") ? rowData["passengers_count"].ToString() : "";
                textBoxDelay.Text = rowData.ContainsKey("delay") ? rowData["delay"].ToString() : "";
                textBoxDriverID.Text = rowData.ContainsKey("driverID") ? rowData["driverID"].ToString() : "";
                textBoxAccident.Text = rowData.ContainsKey("accidents_count") ? rowData["accidents_count"].ToString() : "";
                dateReport.Text = rowData.ContainsKey("report_date") ? rowData["report_date"].ToString() : "";

                this.selectedId = (int)rowData["id"];
                btnSave.Text = "Оновити запис"; // Якщо це редагування, змінюємо текст кнопки
                this.Text = "Редагування";
            }
            else
            {
                textBoxAccident.Clear();
                textBoxDelay.Clear();
                textBoxDriverID.Clear();
                textBoxPassenger.Clear();
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
                    string query = $"UPDATE {currentTable} SET report_date = @report_date, passengers_count = @passengers_count, delay = @delay, driverID = @driverID, accidents_count = @accidents_count WHERE id = @id";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@report_date", dateReport.Text);
                            cmd.Parameters.AddWithValue("@passengers_count", textBoxPassenger.Text);
                            cmd.Parameters.AddWithValue("@delay", textBoxDelay.Text);
                            cmd.Parameters.AddWithValue("@driverID", textBoxDriverID.Text);
                            cmd.Parameters.AddWithValue("@accidents_count", textBoxAccident.Text);
                            cmd.Parameters.AddWithValue("@id", selectedId.Value);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Запис оновлено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Якщо selectedId null, то це новий запис
                    string query = $"INSERT INTO {currentTable} (report_date, passengers_count, delay, driverID, accidents_count) VALUES (@report_date, @passengers_count, @delay, @driverID, @accidents_count);";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@report_date", dateReport.Text);
                            cmd.Parameters.AddWithValue("@passengers_count", textBoxPassenger.Text);
                            cmd.Parameters.AddWithValue("@delay", textBoxDelay.Text);
                            cmd.Parameters.AddWithValue("@driverID", textBoxDriverID.Text);
                            cmd.Parameters.AddWithValue("@accidents_count", textBoxAccident.Text);

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
