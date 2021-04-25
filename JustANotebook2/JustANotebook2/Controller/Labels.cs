using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace JustANotebook2.Controller
{
    static class Labels
    {
        private static string connectionString;
        public static void Create(string dbName)
        {
            string path = @".\db\" + dbName;
            connectionString = @"Data Source=" + path;

            if (!File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    SQLiteTransaction transaction = connection.BeginTransaction();

                    SQLiteCommand command = connection.CreateCommand();
                    command.Transaction = transaction;

                    try
                    {
                        // выполняем две отдельные команды
                        command.CommandText = String.Format("CREATE TABLE Note ( id  INTEGER, title TEXT NOT NULL, description TEXT, PRIMARY KEY(id));");
                        command.ExecuteNonQuery();
                        command.CommandText = String.Format("CREATE TABLE Task ( id INTEGER, description TEXT NOT NULL, indicator INTEGER NOT NULL, PRIMARY KEY(id));");
                        command.ExecuteNonQuery();

                        // подтверждаем транзакцию
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        transaction.Rollback();
                    }
                    finally
                    {
                        connection.Dispose();
                    }
                }
            }
            Controller.Task.changeConnectionString(dbName);
            Controller.Note.changeConnectionString(dbName);
        }
    }
}
