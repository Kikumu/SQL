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
    public partial class volunteer : Form
    {
        public SQLiteConnection connection;
        public volunteer(string username)
        {
            InitializeComponent();
            label1.Text = "Hello " + username;
            label6.Text = username;
            Filldatagrid();
            Fillcombobox();
            fillcombobox2();
        }
        DataTable DB;
        private void label5_Click(object sender, EventArgs e)
        {

        }
        //log out
        private void button3_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            this.Hide();
            l.Show();
        }

        //FILL datagrid
        void Filldatagrid()
        {
           // string pending = "Pending";
            connection = new SQLiteConnection("Data Source= database.jobmanagement");
            connection.Open();
            if (!File.Exists("./database.jobmanagement"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "select * from jobs";

            SQLiteDataAdapter sqldat = new SQLiteDataAdapter(query, connection);
            DB = new DataTable();
            sqldat.Fill(DB);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = DB;
            DataView DV = new DataView(DB);
            DV.RowFilter = string.Format("user LIKE '%{0}%'", label6.Text);
            dataGridView1.DataSource = DV;

        }
        //save data
        private void button1_Click(object sender, EventArgs e)
        {
            //activate connection string and update datagrid
            string pending = "Pending";
            connection = new SQLiteConnection("Data Source= database.jobmanagement");
            connection.Open();
            if (!File.Exists("./database.jobmanagement"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "INSERT INTO jobs (jobname,daterequest,job_status,user,start_date)VALUES('" + comboBox1.SelectedItem.ToString() + "','" + dateTimePicker1.Text + "','" + pending + "','" + label6.Text + "','"+ pending + "')";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            //string checker = "select count (*) from jobs where appointments = '" + dateTimePicker1.Text + "'";
            //SQLiteCommand cmda = new SQLiteCommand(checker, connection);
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
            Filldatagrid();
        }

        //fills combobox
        void Fillcombobox()
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
                    string pName = myReader.GetString(1);

                    comboBox1.Items.Add(pName);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        

        void fillcombobox2()
        {
            connection = new SQLiteConnection("Data Source= database.jobmanagement");
            connection.Open();
            if (!File.Exists("./database.jobmanagement"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            SQLiteCommand cmd = new SQLiteCommand("select * from jobs", connection);
            SQLiteDataReader myReader;
            try
            {
                //int i = 0;
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    int pName = myReader.GetInt32(6);
                    string sName = myReader.GetString(4);
                    if (sName == label6.Text)
                    {
                       comboBox2.Items.Add(pName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //cancel job
        private void button2_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.jobmanagement");
            connection.Open();
            if (!File.Exists("./database.jobmanagement"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "delete from jobs where id ='" + comboBox2.Text + "'";
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
            Filldatagrid();
        }

        //update datagrid
        private void button4_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.jobmanagement");
            connection.Open();
            if (!File.Exists("./database.jobmanagement"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "update jobs set daterequest ='" + dateTimePicker1.Text + "',jobname='" + comboBox1.SelectedItem.ToString()+ "'where id ='" + comboBox2.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader myReader;

            try
            {
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Booking updated");
                while (myReader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Filldatagrid();
        }

       
        private void volunteer_Load(object sender, EventArgs e)
        {

        }
    }
}
