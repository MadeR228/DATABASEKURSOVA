using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DATABASEKURSOVA
{
    public class DatabaseAudit
    {
        private string connectionString;

        public DatabaseAudit(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Метод перевірки існування бази даних і запуску .bat-файлу, якщо він не існує
        public void CheckAndRunBatFile(string connectionString, string database, string batFilePath)
        {
            if (!DatabaseExists(connectionString, database))
            {
                Console.WriteLine($"Базу даних '{database}' не знайдено. Запускаємо .bat файл для імпорту...");
                RunBatFile(batFilePath);
            }
            else
            {
                Console.WriteLine($"База даних '{database}' вже існує.");
            }
        }

        // Метод для перевірки існування бази даних
        public bool DatabaseExists(string connectionString, string database)
        {

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = $"SHOW DATABASES LIKE '{database}';";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        return result != null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка під час перевірки бази даних: {ex.Message}");
                return false;
            }
        }

        // Метод для запуску .bat файлу
        public void RunBatFile(string filePath)
        {
            // Перевірка існування файлу .bat
            if (!File.Exists(filePath))
            {
                return;
            }

            try
            {
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при запуску файлу .bat: {ex.Message}");
            }
        }
    }
}
