using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DATABASEKURSOVA
{
    public partial class TrolleyForm : Form
    {
        private string connectionString;
        private string currentTable;
        private int? selectedId; // Для редагування ми будемо зберігати ID запису

        public TrolleyForm(string connectionString, string currentTable, Dictionary<string, object> rowData = null)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.currentTable = currentTable;

            LoadEnumValues();

            // Якщо rowData не null, то це редагування, і ми заповнюємо поля
            if (rowData != null)
            {
                textBoxTrolleyNum.Text = rowData.ContainsKey("trolley_num") ? rowData["trolley_num"].ToString() : "";
                textBoxMileAge.Text = rowData.ContainsKey("mileage") ? rowData["mileage"].ToString() : "";
                comboBoxStatus.Text = rowData.ContainsKey("status") ? rowData["status"].ToString() : "";
                textBoxRouteID.Text = rowData.ContainsKey("trolley_routeID") ? rowData["trolley_routeID"].ToString() : "";

                this.selectedId = (int)rowData["id"];
                btnSave.Text = "Оновити запис"; // Якщо це редагування, змінюємо текст кнопки
                this.Text = "Редагування";
            }
            else
            {
                textBoxRouteID.Clear();
                textBoxMileAge.Clear();
                textBoxTrolleyNum.Clear();
                comboBoxStatus.SelectedIndex = 0;
                btnSave.Text = "Створити запис"; // Текст кнопки для створення нового запису
                this.Text = "Створення";
            }
        }
        private void LoadEnumValues()
        {
            try
            {
                string query = $"SHOW COLUMNS FROM {currentTable} LIKE 'status'";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string enumDefinition = reader["Type"].ToString();
                                var matches = Regex.Matches(enumDefinition, "'(.*?)'");
                                foreach (Match match in matches)
                                {
                                    comboBoxStatus.Items.Add(match.Groups[1].Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedId.HasValue)
                {
                    // Якщо selectedId не null, то це оновлення існуючого запису
                    string query = $"UPDATE {currentTable} SET trolley_num = @trolley_num, mileage = @mileage, status = @status, trolley_routeID = @trolley_routeID WHERE id = @id";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@trolley_num", textBoxTrolleyNum.Text);
                            cmd.Parameters.AddWithValue("@trolley_routeID", textBoxRouteID.Text);
                            cmd.Parameters.AddWithValue("@mileage", textBoxMileAge.Text);
                            cmd.Parameters.AddWithValue("@status", comboBoxStatus.Text);
                            cmd.Parameters.AddWithValue("@id", selectedId.Value);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Запис оновлено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Якщо selectedId null, то це новий запис
                    string query = $"INSERT INTO {currentTable} (trolley_num, mileage, status, trolley_routeID) VALUES (@trolley_num, @mileage, @status, @trolley_routeID);";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@mileage", textBoxMileAge.Text);
                            cmd.Parameters.AddWithValue("@trolley_routeID", textBoxRouteID.Text);
                            cmd.Parameters.AddWithValue("@trolley_num", textBoxTrolleyNum.Text);
                            cmd.Parameters.AddWithValue("@status", comboBoxStatus.Text);

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
