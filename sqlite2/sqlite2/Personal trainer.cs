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
    public partial class Personal_trainer : Form
    {
        public SQLiteConnection connection;
        public Personal_trainer(string username)
        {
            InitializeComponent();
            label1.Text ="Hello "+ username;
            label3.Text = username;
            Fillcombo();
            loadTable();
            combobox2();
        }
        DataTable DB;
       
        void Fillcombo()
        {
            connection = new SQLiteConnection("Data Source= database.appointments");
            connection.Open();
            if (!File.Exists("./database.appointments"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            SQLiteCommand cmd = new SQLiteCommand("select * from booker", connection);
            SQLiteDataReader myReader;
            try
            {
                //int i = 0;
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string pName = myReader.GetString(1);
                    string sName = myReader.GetString(3);
                    //i++;
                    if (pName == label3.Text)
                    {
                            comboBox1.Items.Add(sName);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void loadTable()
        {
            connection = new SQLiteConnection("Data Source= database.appointments");
            connection.Open();
            if (!File.Exists("./database.appointments"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "select * from booker";
            SQLiteDataAdapter sqldat = new SQLiteDataAdapter(query, connection);
            DB = new DataTable();
            sqldat.Fill(DB);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = DB;
            DataView DV = new DataView(DB);
            DV.RowFilter = string.Format("pt LIKE '%{0}%'", label3.Text);
            dataGridView1.DataSource = DV;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            /*
            connection = new SQLiteConnection("Data Source= database.appointments");
            connection.Open();
            if (!File.Exists("./database.appointments"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "select * from booker";
            SQLiteDataAdapter sqldat = new SQLiteDataAdapter(query, connection);
            DataTable DB = new DataTable();
            sqldat.Fill(DB);
            dataGridView1.DataSource = DB;
            */
            connection = new SQLiteConnection("Data Source= database.appointments");
            connection.Open();
            if (!File.Exists("./database.appointments"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "select * from booker";
            SQLiteDataAdapter sqldat = new SQLiteDataAdapter(query, connection);
            DB = new DataTable();
            sqldat.Fill(DB);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = DB;
            DataView DV = new DataView(DB);
            DV.RowFilter = string.Format("pt LIKE '%{0}%'", label3.Text);
            dataGridView1.DataSource = DV;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        //update booking
        private void button2_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.appointments");
            connection.Open();
            if (!File.Exists("./database.appointments"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "update booker set appointments ='" + dateTimePicker1.Text + "',user='" + comboBox1.SelectedItem.ToString() + "',excercise='" + textBox1.Text + "'where id ='" + comboBox2.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader myReader;
            try
            {
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Updated");
                while (myReader.Read())
                {

                }
                loadTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //delete items
        private void button3_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.appointments");
            connection.Open();
            if (!File.Exists("./database.appointments"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "delete from booker where id ='" + comboBox2.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader myReader;

            try
            {
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Booking Removed");
                while (myReader.Read())
                {

                }
                loadTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //POPULATE ID COMBOBOX
        void combobox2()
        {
            connection = new SQLiteConnection("Data Source= database.appointments");
            connection.Open();
            if (!File.Exists("./database.appointments"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            SQLiteCommand cmd = new SQLiteCommand("select * from booker", connection);
            SQLiteDataReader myReader;
            try
            {
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string pName = myReader.GetString(1);
                    int sName = myReader.GetInt32(5);
                    if (pName == label3.Text)
                    {
                        comboBox2.Items.Add(sName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
