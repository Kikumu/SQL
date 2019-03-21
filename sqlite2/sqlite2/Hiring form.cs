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
    public partial class Hiring_form : Form
    {
        public SQLiteConnection connection;
        public Hiring_form(string username)
        {
            InitializeComponent();
            label1.Text = username;
            FillDataGrid();
        }

        //create data table
        DataTable DB;

        //save and update data table
        private void button1_Click(object sender, EventArgs e)
        {
            //activate connection string
            string pending = "Pending";
            connection = new SQLiteConnection("Data Source= database.hiringitems");
            connection.Open();
            if (!File.Exists("./database.hiringitems"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "INSERT INTO items (date,name,status,user)VALUES('" + dateTimePicker1.Text + "','" + comboBox1.SelectedItem.ToString() + "','" + pending + "','" + label1.Text + "')";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            string checker = "select count (*) from items where date = '" + dateTimePicker1.Text + "'";
            SQLiteCommand cmda = new SQLiteCommand(checker, connection);
            SQLiteDataReader myReader;
            int count = Convert.ToInt32(cmda.ExecuteScalar());
            try
            {
                if (count > 0)
                {
                    MessageBox.Show("There is another person that has already hired this item");
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


        //populate data grid function
        void FillDataGrid()
        {
            connection = new SQLiteConnection("Data Source= database.hiringitems");
            connection.Open();
            if (!File.Exists("./database.hiringitems"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "select * from items";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataAdapter sqldat = new SQLiteDataAdapter(query, connection);
            DB = new DataTable();
            sqldat.Fill(DB);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = DB;
            DataView DV = new DataView(DB);
            DV.RowFilter = string.Format("user LIKE '%{0}%'", label1.Text);
            dataGridView1.DataSource = DV;
        }

        //update hiring data
        private void button2_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.hiringitems");
            connection.Open();
            if (!File.Exists("./database.hiringitems"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "update items set date ='" + dateTimePicker1.Text + "',name='" + comboBox1.SelectedItem.ToString() + "'where date ='" + dateTimePicker1.Text + "'";
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

        //cancel hiring
        private void button4_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.hiringitems");
            connection.Open();
            if (!File.Exists("./database.hiringitems"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "delete from items where date ='" + dateTimePicker1.Text +"'";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader myReader;
            try
            {
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Deleted");
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

        //logout
        private void button3_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            this.Hide();
            l.Show();
        }
    }
}
