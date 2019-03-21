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
    public partial class job_management : Form
    {
        SQLiteConnection connection;
        DataTable DB;
        public job_management(string username)
        {
            InitializeComponent();
            label1.Text = username;
            fillcombobox();
            Filldatagrid();
        }

        //populate job id combo box
        void fillcombobox()
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
                   
                        comboBox1.Items.Add(pName);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


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
            //DV.RowFilter = string.Format("user LIKE '%{0}%'", label6.Text);
            dataGridView1.DataSource = DV;
        }



        private void button4_Click(object sender, EventArgs e)
        {
            Facility_manager facility = new Facility_manager(label1.Text);
            this.Hide();
            facility.Show();
        }

        //update
        private void button1_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.jobmanagement");
            connection.Open();
            if (!File.Exists("./database.jobmanagement"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "update jobs set start_date ='" + dateTimePicker1.Text + "',job_status='" + comboBox2.SelectedItem.ToString() + "'where id ='" + comboBox1.Text + "'";
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

        //delete job
        private void button2_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.jobmanagement");
            connection.Open();
            if (!File.Exists("./database.jobmanagement"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "delete from jobs where id ='" + comboBox1.Text + "'";
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

        private void button3_Click(object sender, EventArgs e)
        {
            vacant_jobs_management vacant_Jobs_ = new vacant_jobs_management(label1.Text);
            this.Hide();
            vacant_Jobs_.Show();
        }
    }
}
