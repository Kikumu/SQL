using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace sqlite2
{
    public partial class External_user_management : Form
    {
        SQLiteConnection connection;
        DataTable DB;
        string external = "External User";
        public External_user_management(string username)
        {
            InitializeComponent();
            label1.Text = username;
            Filldatagrid();
            combobox();
        }

        //populate datagrid
        void Filldatagrid()
        {
            connection = new SQLiteConnection("Data Source= database.logindata");
            connection.Open();

            if (!File.Exists("./database.logindata"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "select * from login4";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataAdapter sqldat = new SQLiteDataAdapter(query, connection);
            DB = new DataTable();
            sqldat.Fill(DB);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = DB;
            DataView DV = new DataView(DB);
            DV.RowFilter = string.Format("usertype LIKE '%{0}%'", external);
            dataGridView1.DataSource = DV;
            connection.Close();
        }

        //populate combo box
        void combobox()
        {
            connection = new SQLiteConnection("Data Source= database.logindata");
            connection.Open();

            if (!File.Exists("./database.logindata"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "select * from login4";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader myReader;
            try
            {
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string pName = myReader.GetString(2);
                    int sName = myReader.GetInt32(3);
                    if (pName == external)
                    {
                        comboBox1.Items.Add(sName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.logindata");
            connection.Open();
            if (!File.Exists("./database.logindata"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "delete from login4 where id ='" + comboBox1.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader myReader;

            try
            {
                myReader = cmd.ExecuteReader();
                MessageBox.Show("User Removed");
                while (myReader.Read())
                {

                }
                Filldatagrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Facility_manager facility_ = new Facility_manager(label1.Text);
            this.Hide();
            facility_.Show();
        }
    }
}
