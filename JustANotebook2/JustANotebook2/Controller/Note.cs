using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JustANotebook2.Controller
{
    static class Note
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectStr"].ConnectionString;
        public static void changeConnectionString(string dbName)
        {
            string path = @".\db\" + dbName;
            connectionString = @"Data Source=" + path;
        }
        public static List<Model.Note> Get()
        {
            string id;
            string title;
            string desc;
            List<Model.Note> notes = new List<Model.Note>();

            string sqlExpression = "SELECT id, Title, Description FROM note";
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
                        title = (string)reader.GetValue(1);
                        desc = (string)reader.GetValue(2);
                        notes.Add(new Model.Note() { id = id, Title = title, Description = desc });
                    }
                }
                reader.Close();
            }
            return notes;
        }

        public static Model.Note GetLast()
        {
            string id;
            string title;
            string desc;
            Model.Note note = new Model.Note();

            string sqlExpression = "SELECT * FROM Note ORDER BY id DESC LIMIT 1;";
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
                            title = (string)reader.GetValue(1);
                            desc = (string)reader.GetValue(2);
                            note = new Model.Note() { id = id, Title = title, Description = desc };
                        }
                    }

                    transaction.Commit();
                }
                catch(Exception ex)
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

            return note;

        }
        public static Model.Note GetById(string id)
        {
            string title;
            string desc;
            Model.Note note = new Model.Note();

            string sqlExpression = "SELECT * FROM Note WHERE id = " + id + ";";
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
                            title = (string)reader.GetValue(1);
                            desc = (string)reader.GetValue(2);
                            note = new Model.Note() { id = id, Title = title, Description = desc };
                        }
                    }
                    transaction.Commit();
                }
                catch(Exception ex)
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

            return note;
        }

        public static bool Add(Model.Note note)
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
                    command.CommandText = String.Format("INSERT INTO note (title, description) VALUES(@Title, @Description)");
                    command.Parameters.AddWithValue("Title", note.Title);
                    command.Parameters.AddWithValue("Description", note.Description);
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
                    command.CommandText = String.Format("DELETE FROM note WHERE id={0}",id);
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

        public static bool Update(Model.Note note)
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
                    command.CommandText = "UPDATE Note SET title = @Title, description = @Description WHERE id = @Id";
                    command.Parameters.AddWithValue("id", note.id);
                    command.Parameters.AddWithValue("Title", note.Title);
                    command.Parameters.AddWithValue("Description", note.Description);
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


    }
}
