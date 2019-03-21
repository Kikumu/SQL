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
    public partial class item_for_hire_management : Form
    {
        SQLiteConnection connection;
         DataTable DB;
        public item_for_hire_management(string username)
        {
            InitializeComponent();
            FillDataGrid();
            fillcombo1();
            label1.Text = username;
        }

        //populate grid
        void FillDataGrid()
        {
            connection = new SQLiteConnection("Data Source= database.sqlite3");
            connection.Open();
            if (!File.Exists("./database.sqlite3"))
            {

                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "select * from hire";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataAdapter sqldat = new SQLiteDataAdapter(query, connection);
            DB = new DataTable();
            sqldat.Fill(DB);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = DB;
            DataView DV = new DataView(DB);
            // DV.RowFilter = string.Format("user LIKE '%{0}%'", label1.Text);
            dataGridView1.DataSource = DV;
            connection.Close();
        }

        //save
        private void button2_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.sqlite3");
            connection.Open();
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "INSERT INTO hire (name)VALUES('" + textBox1.Text + "')";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader myReader;
            //int count = Convert.ToInt32(cmda.ExecuteScalar());
            try
            {
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Saved");
                while (myReader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FillDataGrid();

        }

        //update
        private void button1_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.sqlite3");
            connection.Open();
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }


            string query = "update hire set name='" + textBox1.Text + "'where id ='" + comboBox1.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader myReader;

            try
            {
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Updated");
                while (myReader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FillDataGrid();

        }

        //fill combobox
        void fillcombo1()
        {
            connection = new SQLiteConnection("Data Source= database.sqlite3");
            connection.Open();
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }


            SQLiteCommand cmd = new SQLiteCommand("select * from hire", connection);
            SQLiteDataReader myReader;
            try
            {
                //int i = 0;
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    int pName = myReader.GetInt32(0);

                    comboBox1.Items.Add(pName);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        //delete
        private void button3_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.sqlite3");
            connection.Open();
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }


            string query = "delete from hire where id ='" + comboBox1.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader myReader;

            try
            {
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Updated");
                while (myReader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FillDataGrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            items_for_hire items = new items_for_hire(label1.Text);
            this.Hide();
            items.Show();
        }
    }
}
