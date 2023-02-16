using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe; //DLL SQLCE
using System.Data.SQLite; //DLL QLite
using MySql.Data.MySqlClient; //DLL MySQL
using System.IO; //DLL FILES
using System.Data.SqlClient;

namespace Database
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();

            //Add Interop DLL
            var path = Path(Directory.GetCurrentDirectory());
            if (!(File.Exists(Directory.GetCurrentDirectory() + @"\SQLite.Interop.dll")))
            {
                File.Copy(path + @"\dll\SQLite.Interop.dll", Directory.GetCurrentDirectory() + @"\SQLite.Interop.dll");
            }
        }

        public string Path(string path)
        {
            for (int i = 0; i < 2; i++)
            {
                path = path.Remove(path.LastIndexOf('\\'));
            }
            return path;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            #region SQL Server CE

            string dataBase = Path(Directory.GetCurrentDirectory()) + @"\db\DBSQLServer.sdf";
            string strConnection = @"DataSource = " + dataBase + "; Password = 'admin'";

            SqlCeEngine db = new SqlCeEngine(strConnection);

            if (!File.Exists(dataBase))
            {
                db.CreateDatabase();
            }
            db.Dispose();

            SqlCeConnection connection = new SqlCeConnection(strConnection);
            //connection.ConnectionString = strConnection;

            try
            {
                connection.Open();
                labelResult.Text = "Database connected! [SQL Server CE]";

            }
            catch (Exception error)
            {
                labelResult.Text = "ERROR connect failed! [SQL Server CE]\n" + error;

            }
            finally { connection.Close(); }
            #endregion

            #region SQLite

            //string dataBase = Path(Directory.GetCurrentDirectory()) + @"\db\DBSQLite.db";
            //string strConnection = @"Data Source = " + dataBase + "; Version = 3";
            


            //if (!File.Exists(dataBase))
            //{
            //    SQLiteConnection.CreateFile(dataBase);
            //}

            //SQLiteConnection connection = new SQLiteConnection(strConnection);

            //try
            //{
            //    connection.Open();
            //    labelResult.Text = "Database connected! [SQLite]";

            //}
            //catch (Exception error)
            //{
            //    labelResult.Text = "ERROR connect failed! [SQLite]\n" + error;

            //}
            //finally { connection.Close(); }


            #endregion

            #region MySQL

            //string strConnection1 = "server=127.0.0.1;User Id=root;password=";
            ////string strConnection2 = "server=127.0.0.1;User Id=root;database=course_db;password=admin";

            //MySqlConnection connection = new MySqlConnection(strConnection1);
            ////connection.ConnectionString= sqlConnection1;

            //try
            //{
            //    connection.Open();
            //    //labelResult.Text = "Database connected! [MySQL]";


            //    MySqlCommand command = new MySqlCommand();
            //    command.Connection = connection;
            //    command.CommandText = "CREATE DATABASE IF NOT EXISTS people_db";
            //    command.ExecuteNonQuery();
            //    labelResult.Text = "Database connected! [MySQL]";
            //    command.Dispose();

            //}
            //catch (Exception error)
            //{
            //    labelResult.Text = "ERROR connect failed! [MySQL]\n" + error;

            //}
            //finally { connection.Close(); }



            #endregion
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            #region SQLServer
            string dataBase = Path(Directory.GetCurrentDirectory()) + @"\db\DBSQLServer.sdf";
            string strConnection = @"DataSource = " + dataBase + "; Password = 'admin'";

            SqlCeConnection connection = new SqlCeConnection(strConnection);
            try
            {
                connection.Open();
                SqlCeCommand command = new SqlCeCommand();
                command.Connection = connection;
                command.CommandText = "CREATE TABLE people (id INT NOT NULL PRIMARY KEY, name NVARCHAR(50), email NVARCHAR(50))";
                command.ExecuteNonQuery();

                labelResult.Text = "Table CREATED successfully! [SQL Server CE]";
                command.Dispose();

            }
            catch (Exception ex)
            {
                labelResult.Text = "Error!\n" + ex;

            }
            finally { connection.Close(); }
            #endregion

            #region SQLite

            //string dataBase = Path(Directory.GetCurrentDirectory()) + @"\db\DBSQLite.db";
            //string strConnection = @"Data Source = " + dataBase + "; Version = 3";

            //SQLiteConnection connection = new SQLiteConnection(strConnection);

            //try
            //{
            //    connection.Open();
            //    SQLiteCommand command = new SQLiteCommand();
            //    command.Connection = connection;
            //    command.CommandText = "CREATE TABLE people (id INT NOT NULL PRIMARY KEY, name NVARCHAR(50), email NVARCHAR(50))";
            //    command.ExecuteNonQuery();

            //    labelResult.Text = "Table CREATED successfully! [SQLite]";
            //    command.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    labelResult.Text = "Error! " + ex;

            //}
            //finally { connection.Close(); }

            #endregion

            #region MySQL

            //string strConnection2 = "server=127.0.0.1;User Id=root;database=people_db;password=";

            //MySqlConnection connection = new MySqlConnection(strConnection2);
            //try
            //{
            //    connection.Open();
            //    MySqlCommand command = new MySqlCommand();
            //    command.Connection = connection;
            //    command.CommandText = "CREATE TABLE people (id INT NOT NULL, name VARCHAR(50), email VARCHAR(50), PRIMARY KEY(id))";
            //    command.ExecuteNonQuery();

            //    labelResult.Text = "Table CREATED successfully! [MySQL]";
            //    command.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    labelResult.Text = "Error! " + ex;

            //}
            //finally { connection.Close(); }

            #endregion
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            #region SQLServer

            string dataBase = Path(Directory.GetCurrentDirectory()) + @"\db\DBSQLServer.sdf";
            string strConnection = @"DataSource = " + dataBase + "; Password = 'admin'";

            SqlCeConnection connection = new SqlCeConnection(strConnection);
            try
            {
                connection.Open();
                SqlCeCommand command = new SqlCeCommand();
                command.Connection = connection;

                int id = new Random(DateTime.Now.Millisecond).Next(0, 1000);
                string name = txtName.Text;
                string email = txtEmail.Text;

                command.CommandText = $"INSERT INTO people VALUES ({id}, '{name}', '{email}')";
                command.ExecuteNonQuery();

                labelResult.Text = "Data INSERTED successfully! [SQL Server CE]";
                command.Dispose();

            }
            catch (Exception ex)
            {
                labelResult.Text = ex.ToString();

            }
            finally { connection.Close(); txtName.Text = ""; txtEmail.Text = ""; }


            #endregion

            #region SQLite

            //string dataBase = Path(Directory.GetCurrentDirectory()) + @"\db\DBSQLite.db";
            //string strConnection = @"Data Source = " + dataBase + "; Version = 3";

            //SQLiteConnection connection = new SQLiteConnection(strConnection);
            //try
            //{
            //    connection.Open();
            //    SQLiteCommand command = new SQLiteCommand();
            //    command.Connection = connection;

            //    int id = new Random(DateTime.Now.Millisecond).Next(0, 1000);
            //    string name = txtName.Text;
            //    string email = txtEmail.Text;

            //    command.CommandText = $"INSERT INTO people VALUES ({id}, '{name}', '{email}')";
            //    command.ExecuteNonQuery();

            //    labelResult.Text = "Data INSERTED successfully! [SQLite]";
            //    command.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    labelResult.Text = ex.ToString();

            //}
            //finally { connection.Close(); txtName.Text = ""; txtEmail.Text = ""; }


            #endregion

            #region MySQL
            //string strConnection2 = "server=127.0.0.1;User Id=root;database=people_db;password=";

            //MySqlConnection connection = new MySqlConnection(strConnection2);
            //try
            //{
            //    connection.Open();
            //    MySqlCommand command = new MySqlCommand();
            //    command.Connection = connection;

            //    int id = new Random(DateTime.Now.Millisecond).Next(0, 1000);
            //    string name = txtName.Text;
            //    string email = txtEmail.Text;

            //    command.CommandText = $"INSERT INTO people VALUES ({id}, '{name}', '{email}')";
            //    command.ExecuteNonQuery();

            //    labelResult.Text = "Data INSERTED successfully! [MySQL]";
            //    command.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    labelResult.Text = ex.ToString();

            //}
            //finally { connection.Close(); txtName.Text = ""; txtEmail.Text = ""; }


            #endregion
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            #region SQLServer

            list.Rows.Clear();
            labelResult.Text = "";

            string dataBase = Path(Directory.GetCurrentDirectory()) + @"\db\DBSQLServer.sdf";
            string strConnection = @"DataSource = " + dataBase + "; Password = 'admin'";

            SqlCeConnection connection = new SqlCeConnection(strConnection);
            try
            {
                string query = "SELECT * FROM people";

                if (txtName.Text != "")
                {
                    query = "SELECT * FROM people WHERE name LIKE '" + txtName.Text + "'";
                }

                DataTable data = new DataTable();
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(query, strConnection);

                connection.Open();

                adapter.Fill(data);
                foreach (DataRow line in data.Rows)
                {
                    list.Rows.Add(line.ItemArray);
                }
            }
            catch (Exception ex)
            {
                list.Rows.Clear();
                labelResult.Text = ex.ToString();

            }
            finally { connection.Close(); txtName.Text = ""; txtEmail.Text = ""; }


            #endregion

            #region SQLite

            //list.Rows.Clear();
            //labelResult.Text = "";

            //string dataBase = Path(Directory.GetCurrentDirectory()) + @"\db\DBSQLite.db";
            //string strConnection = @"Data Source = " + dataBase + "; Version = 3";

            //SQLiteConnection connection = new SQLiteConnection(strConnection);
            //try
            //{
            //    string query = "SELECT * FROM people";

            //    if (txtName.Text != "")
            //    {
            //        query = "SELECT * FROM people WHERE name LIKE '" + txtName.Text + "'";
            //    }

            //    DataTable data = new DataTable();
            //    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, strConnection);

            //    connection.Open();

            //    adapter.Fill(data);
            //    foreach (DataRow line in data.Rows)
            //    {
            //        list.Rows.Add(line.ItemArray);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    list.Rows.Clear();
            //    labelResult.Text = ex.ToString();

            //}
            //finally { connection.Close(); txtName.Text = ""; txtEmail.Text = ""; }

            #endregion

            #region MySQL

            //list.Rows.Clear();
            //labelResult.Text = "";

            //string strConnection2 = "server=127.0.0.1;User Id=root;database=people_db;password=";

            //MySqlConnection connection = new MySqlConnection(strConnection2);
            //try
            //{
            //    string query = "SELECT * FROM people";

            //    if (txtName.Text != "")
            //    {
            //        query = "SELECT * FROM people WHERE name LIKE '" + txtName.Text + "'";
            //    }

            //    DataTable data = new DataTable();
            //    MySqlDataAdapter adapter = new MySqlDataAdapter(query, strConnection2);

            //    connection.Open();

            //    adapter.Fill(data);
            //    foreach (DataRow line in data.Rows)
            //    {
            //        list.Rows.Add(line.ItemArray);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    list.Rows.Clear();
            //    labelResult.Text = ex.ToString();

            //}
            //finally { connection.Close(); txtName.Text = ""; txtEmail.Text = ""; }

            #endregion

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            #region SQLServer

            string dataBase = Path(Directory.GetCurrentDirectory()) + @"\db\DBSQLServer.sdf";
            string strConnection = @"DataSource = " + dataBase + "; Password = 'admin'"; //Parametros

            SqlCeConnection connection = new SqlCeConnection(strConnection);
            try
            {
                connection.Open();
                SqlCeCommand command = new SqlCeCommand();
                command.Connection = connection;

                int id = (int)list.SelectedRows[0].Cells[0].Value;

                command.CommandText = $"DELETE FROM people WHERE id='{id}'";
                command.ExecuteNonQuery();

                labelResult.Text = "Data DELETED successfully! [SQL Server CE]";
                command.Dispose();

            }
            catch (Exception ex)
            {
                labelResult.Text = ex.ToString();

            }
            finally { connection.Close(); txtName.Text = ""; txtEmail.Text = ""; }


            #endregion

            #region SQLite

            //string dataBase = Path(Directory.GetCurrentDirectory()) + @"\db\DBSQLite.db";
            //string strConnection = @"Data Source = " + dataBase + "; Version = 3";

            //SQLiteConnection connection = new SQLiteConnection(strConnection);
            //try
            //{
            //    connection.Open();
            //    SQLiteCommand command = new SQLiteCommand();
            //    command.Connection = connection;

            //    int id = (int)list.SelectedRows[0].Cells[0].Value;

            //    command.CommandText = $"DELETE FROM people WHERE id='{id}'";
            //    command.ExecuteNonQuery();

            //    labelResult.Text = "Data DELETED successfully! [SQLite]";
            //    command.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    labelResult.Text = ex.ToString();

            //}
            //finally { connection.Close(); txtName.Text = ""; txtEmail.Text = ""; }


            #endregion

            #region MySQL
            //string strConnection2 = "server=127.0.0.1;User Id=root;database=people_db;password=";

            //MySqlConnection connection = new MySqlConnection(strConnection2);
            //try
            //{
            //    connection.Open();
            //    MySqlCommand command = new MySqlCommand();
            //    command.Connection = connection;

            //    int id = (int)list.SelectedRows[0].Cells[0].Value;

            //    command.CommandText = $"DELETE FROM people WHERE id='{id}'";
            //    command.ExecuteNonQuery();

            //    labelResult.Text = "Data DELETED successfully! [MySQL]";
            //    command.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    labelResult.Text = ex.ToString();

            //}
            //finally { connection.Close(); txtName.Text = ""; txtEmail.Text = ""; }


            #endregion
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            #region SQLServer

            string dataBase = Path(Directory.GetCurrentDirectory()) + @"\db\DBSQLServer.sdf";
            string strConnection = @"DataSource = " + dataBase + "; Password = 'admin'";

            SqlCeConnection connection = new SqlCeConnection(strConnection);
            try
            {
                connection.Open();
                SqlCeCommand command = new SqlCeCommand();
                command.Connection = connection;

                int id = (int)list.SelectedRows[0].Cells[0].Value;
                string query = $"UPDATE people SET name='{txtName.Text}', email='{txtEmail.Text}' WHERE id LIKE '{id}'";

                command.CommandText = query;
                command.ExecuteNonQuery();

                labelResult.Text = "Data UPDATED successfully! [SQL Server CE]";
                command.Dispose();

            }
            catch (Exception ex)
            {
                labelResult.Text = ex.ToString();

            }
            finally { connection.Close(); txtName.Text = ""; txtEmail.Text = ""; }


            #endregion

            #region SQLite

            //string dataBase = Path(Directory.GetCurrentDirectory()) + @"\db\DBSQLite.db";
            //string strConnection = @"Data Source = " + dataBase + "; Version = 3";

            //SQLiteConnection connection = new SQLiteConnection(strConnection);
            //try
            //{
            //    connection.Open();
            //    SQLiteCommand command = new SQLiteCommand();
            //    command.Connection = connection;

            //    int id = (int)list.SelectedRows[0].Cells[0].Value;
            //    string query = $"UPDATE people SET name='{txtName.Text}', email='{txtEmail.Text}' WHERE id LIKE '{id}'";

            //    command.CommandText = query;
            //    command.ExecuteNonQuery();

            //    labelResult.Text = "Data UPDATED successfully! [SQLite]";
            //    command.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    labelResult.Text = ex.ToString();

            //}
            //finally { connection.Close(); txtName.Text = ""; txtEmail.Text = ""; }


            #endregion

            #region MySQL
            //string strConnection2 = "server=127.0.0.1;User Id=root;database=people_db;password=";

            //MySqlConnection connection = new MySqlConnection(strConnection2);
            //try
            //{
            //    connection.Open();
            //    MySqlCommand command = new MySqlCommand();
            //    command.Connection = connection;

            //    int id = (int)list.SelectedRows[0].Cells[0].Value;
            //    string query = $"UPDATE people SET name='{txtName.Text}', email='{txtEmail.Text}' WHERE id LIKE '{id}'";

            //    command.CommandText = query;
            //    command.ExecuteNonQuery();

            //    labelResult.Text = "Data UPDATED successfully! [MySQL]";
            //    command.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    labelResult.Text = ex.ToString();

            //}
            //finally { connection.Close(); txtName.Text = ""; txtEmail.Text = ""; }


            #endregion
        }
    }
}
