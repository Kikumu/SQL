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
    public partial class External_user : Form
    {
        //creates a new connection to database
        public SQLiteConnection connection;

        public External_user(string username)
        {
            InitializeComponent();
            label1.Text = "Hello " + username;
            label5.Text = username;
            fillcmbobox();
            FillDataGrid();
            fillcmbobx2();
        }
        //creates a new datatable
        DataTable DB;


        //loads values of registered personal trainers to a combo box
        void fillcmbobox()
        {
            connection = new SQLiteConnection("Data Source= database.logindata");
            connection.Open();
            if (!File.Exists("./database.logindata"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            SQLiteCommand cmd = new SQLiteCommand("select * from login4", connection);
            SQLiteDataReader myReader;
            try
            {
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string pName = myReader.GetString(2);
                    string sName = myReader.GetString(0);
                    if (pName == "Personal trainer")
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //populates datagrid  with necessary data
        void FillDataGrid()
        {
            connection = new SQLiteConnection("Data Source= database.appointments");
            connection.Open();
            if (!File.Exists("./database.appointments"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "select * from booker";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataAdapter sqldat = new SQLiteDataAdapter(query, connection);
            DB = new DataTable();
            sqldat.Fill(DB);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = DB;
            DataView DV = new DataView(DB);
            DV.RowFilter = string.Format("user LIKE '%{0}%'", label5.Text);
            dataGridView1.DataSource = DV;
        }

        void fillcmbobx2()
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
                    string pName = myReader.GetString(3);
                    int sName = myReader.GetInt32(5);
                    if (pName == label5.Text)
                    {
                        comboBox3.Items.Add(sName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //goes to hiring form page
        private void button4_Click(object sender, EventArgs e)
        {
            Hiring_form h = new Hiring_form(label5.Text);
            this.Hide();
            h.Show();
        }


        //logs out user back to main login page
        private void button5_Click(object sender, EventArgs e)
        {
            Login h = new Login();
            this.Hide();
            h.Show();
        }


        private void label6_Click(object sender, EventArgs e)
        {

        }


        //saves and updates datagrid
        private void button1_Click(object sender, EventArgs e)
        {
            string External = "External User";
            connection = new SQLiteConnection("Data Source= database.appointments");
            connection.Open();
            if (!File.Exists("./database.appointments"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "INSERT INTO booker (appointments,pt,excercise,user)VALUES('" + dateTimePicker1.Text + "','" + comboBox1.SelectedItem.ToString() + "','" + comboBox2.SelectedItem.ToString() + "','" + label5.Text + "')";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            string checker = "select count (*) from booker where appointments = '" + dateTimePicker1.Text + "'";
            SQLiteCommand cmda = new SQLiteCommand(checker, connection);
            SQLiteDataReader myReader;
            int count = Convert.ToInt32(cmda.ExecuteScalar());
            try
            {
                if (count > 0)
                {
                    MessageBox.Show("There is another person that has booked this session");
                }
                else
                {
                    myReader = cmd.ExecuteReader();
                    MessageBox.Show("Saved");
                    while (myReader.Read())
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FillDataGrid();
        }


        //updates data and datagrid
        private void button2_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.appointments");
            connection.Open();
            if (!File.Exists("./database.appointments"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "update booker set appointments ='" + dateTimePicker1.Text + "',pt='" + comboBox1.SelectedItem.ToString() + "',excercise='" + comboBox2.SelectedItem.ToString() + "'where id ='" +comboBox3.Text + "'";
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

        //deletes selected appointment and updates datagrid
        private void button3_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.appointments");
            connection.Open();
            if (!File.Exists("./database.appointments"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "delete from booker where id ='" + comboBox3.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader myReader;

            try
            {
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Booking cancelled");
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

        private void External_user_Load(object sender, EventArgs e)
        {

        }
    }
}
