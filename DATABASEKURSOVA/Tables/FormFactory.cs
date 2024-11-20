using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DATABASEKURSOVA
{
    public class FormFactory
    {
        private string connectionString;

        public FormFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Form GetForm(string tableName, Dictionary<string, object> rowData = null)
        {
            switch (tableName)
            {
                case "drivers":
                    return new DriversForm(connectionString, tableName, rowData);

                case "routes":
                    return new RoutesForm(connectionString, tableName, rowData);

                case "trolleybuses":
                    return new TrolleyForm(connectionString, tableName, rowData);

                case "stops":
                    return new StopsForm(connectionString, tableName, rowData);

                case "reports":
                    return new ReportsForm(connectionString,tableName, rowData);

                case "expenses":
                    return new ExpensesForm(connectionString, tableName, rowData);

                case "maintenance":
                    return new MaintenanceForm(connectionString, tableName, rowData);

                default:
                    throw new ArgumentException($"Редагування записів для таблиці {tableName} не підтримується.");
            }
        }
    }
}
