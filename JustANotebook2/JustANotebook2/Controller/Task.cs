using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace JustANotebook2.Controller
{
    static class Task
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connectStr"].ConnectionString;
        public static void changeConnectionString(string dbName)
        {
            string path = @".\db\" + dbName;
            connectionString = @"Data Source=" + path;
        }
        public static List<Model.Task> Get()
        {
            string id;
            string description;
            string indicator;
            List<Model.Task> tasks = new List<Model.Task>();

            string sqlExpression = "SELECT * FROM Task";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        id = reader.GetValue(0).ToString();
                        description = (string)reader.GetValue(1);
                        indicator = reader.GetValue(2).ToString();
                        tasks.Add(new Model.Task() { id = id, Description = description, Indicator = indicator });
                    }
                }

                reader.Close();
            }

            return tasks;

        }
        public static Model.Task GetById(string id)
        {
            string desc;
            string indicator;
            Model.Task task = new Model.Task();

            string sqlExpression = "SELECT * FROM Task WHERE id = " + id + ";";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteTransaction transaction = connection.BeginTransaction();

                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                SQLiteDataReader reader = command.ExecuteReader();

                try
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            desc = reader.GetValue(1).ToString();
                            indicator = reader.GetValue(2).ToString();
                            task = new Model.Task() { id = id, Description = desc, Indicator = indicator };
                        }
                    }
                    transaction.Commit();
                    return task;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                    return null;
                }
                finally
                {
                    reader.Close();
                    connection.Dispose();
                }
            }
        }
        public static Model.Task GetLast()
        {
            string id;
            string desc;
            string indicator;
            Model.Task task = new Model.Task();

            string sqlExpression = "SELECT * FROM Task ORDER BY id DESC LIMIT 1;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteTransaction transaction = connection.BeginTransaction();

                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                SQLiteDataReader reader = command.ExecuteReader();

                try
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            id = reader.GetValue(0).ToString();
                            desc = reader.GetValue(1).ToString();
                            indicator = reader.GetValue(2).ToString();
                            task = new Model.Task() { id = id, Description = desc, Indicator = indicator};
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }
                finally
                {
                    reader.Close();
                    connection.Dispose();
                }

            }
            return task;
        }
        public static bool Add(Model.Task task)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteTransaction transaction = connection.BeginTransaction();

                SQLiteCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    // выполняем две отдельные команды
                    command.CommandText = String.Format("INSERT INTO task (description, indicator) VALUES(@Description, @Indicator)");
                    command.Parameters.AddWithValue("Description", task.Description);
                    command.Parameters.AddWithValue("Indicator", task.Indicator);
                    command.ExecuteNonQuery();

                    // подтверждаем транзакцию
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                    return false;
                }
                finally
                {
                    connection.Dispose();
                }
            }
        }

        public static bool Remove(string id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteTransaction transaction = connection.BeginTransaction();

                SQLiteCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    // выполняем две отдельные команды
                    command.CommandText = String.Format("DELETE FROM Task WHERE id={0}",id);
                    command.ExecuteNonQuery();

                    // подтверждаем транзакцию
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public static bool Update(Model.Task task)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteTransaction transaction = connection.BeginTransaction();

                SQLiteCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    // выполняем две отдельные команды
                    command.CommandText = "UPDATE Task SET description = @Description, indicator = @Indicator WHERE id = @Id";
                    command.Parameters.AddWithValue("Description", task.Description);
                    command.Parameters.AddWithValue("Indicator", task.Indicator);
                    command.Parameters.AddWithValue("Id", task.id);
                    command.ExecuteNonQuery();

                    // подтверждаем транзакцию
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
