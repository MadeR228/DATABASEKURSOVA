using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DATABASEKURSOVA
{
    public partial class DriversForm : Form
    {
        private string connectionString; // Строка подключения
        private string currentTable;     // Текущая таблица
        private int? selectedId; // Для редагування ми будемо зберігати ID запису

        public DriversForm(string connectionString, string currentTable, Dictionary<string, object> rowData = null)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.currentTable = currentTable;

            // Якщо rowData не null, то це редагування, і ми заповнюємо поля
            if (rowData != null)
            {
                // Заповнюємо текстові поля існуючими значеннями
                textBoxSurname.Text = rowData.ContainsKey("surname") ? rowData["surname"].ToString() : "";
                textBoxName.Text = rowData.ContainsKey("name") ? rowData["name"].ToString() : "";
                textBoxMiddleName.Text = rowData.ContainsKey("middle_name") ? rowData["middle_name"].ToString() : "";
                textBoxAge.Text = rowData.ContainsKey("age") ? rowData["age"].ToString() : "";
                textBoxExperience.Text = rowData.ContainsKey("years_of_experience") ? rowData["years_of_experience"].ToString() : "";
                textBoxDriversLicense.Text = rowData.ContainsKey("drivers_licence") ? rowData["drivers_licence"].ToString() : "";
                comboBoxCategory.Text = rowData.ContainsKey("category") ? rowData["category"].ToString() : "";
                textBoxWorkSchedule.Text = rowData.ContainsKey("work_schedule") ? rowData["work_schedule"].ToString() : "";

                // Зберігаємо ID запису для редагування
                this.selectedId = (int)rowData["id"];
                btnSave.Text = "Оновити запис"; // Якщо це редагування, змінюємо текст кнопки
                this.Text = "Редагування";
            }
            else
            {
                // Якщо це новий запис, залишаємо поля порожніми
                textBoxSurname.Clear();
                textBoxName.Clear();
                textBoxMiddleName.Clear();
                textBoxAge.Clear();
                textBoxExperience.Clear();
                textBoxDriversLicense.Clear();
                comboBoxCategory.SelectedIndex = 0;
                textBoxWorkSchedule.Clear();
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
                    string query = $"UPDATE {currentTable} SET surname = @surname, name = @name, middle_name = @middle_name, " +
                                   $"age = @age, years_of_experience = @years_of_experience, drivers_licence = @drivers_licence, " +
                                   $"category = @category, work_schedule = @work_schedule WHERE id = @id";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@surname", textBoxSurname.Text);
                            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                            cmd.Parameters.AddWithValue("@middle_name", textBoxMiddleName.Text);
                            cmd.Parameters.AddWithValue("@age", textBoxAge.Text);
                            cmd.Parameters.AddWithValue("@years_of_experience", textBoxExperience.Text);
                            cmd.Parameters.AddWithValue("@drivers_licence", textBoxDriversLicense.Text);
                            cmd.Parameters.AddWithValue("@category", comboBoxCategory.Text);
                            cmd.Parameters.AddWithValue("@work_schedule", textBoxWorkSchedule.Text);
                            cmd.Parameters.AddWithValue("@id", selectedId.Value);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Запис оновлено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Якщо selectedId null, то це новий запис
                    string query = $"INSERT INTO {currentTable} (surname, name, middle_name, age, years_of_experience, drivers_licence, category, work_schedule) " +
                                   $"VALUES (@surname, @name, @middle_name, @age, @years_of_experience, @drivers_licence, @category, @work_schedule)";

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@surname", textBoxSurname.Text);
                            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                            cmd.Parameters.AddWithValue("@middle_name", textBoxMiddleName.Text);
                            cmd.Parameters.AddWithValue("@age", textBoxAge.Text);
                            cmd.Parameters.AddWithValue("@years_of_experience", textBoxExperience.Text);
                            cmd.Parameters.AddWithValue("@drivers_licence", textBoxDriversLicense.Text);
                            cmd.Parameters.AddWithValue("@category", comboBoxCategory.Text);
                            cmd.Parameters.AddWithValue("@work_schedule", textBoxDriversLicense.Text);

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