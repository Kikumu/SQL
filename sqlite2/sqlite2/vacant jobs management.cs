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
    public partial class vacant_jobs_management : Form
    {
        SQLiteConnection connection;
        DataTable DB;
        public vacant_jobs_management(string username)
        {
            InitializeComponent();
            Filldatagrid();
            label1.Text = username;
            fillcombo1();
        }

        void fillcombo1()
        {
            connection = new SQLiteConnection("Data Source= database.vacancies");
            connection.Open();
            if (!File.Exists("./database.vacancies"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            SQLiteCommand cmd = new SQLiteCommand("select * from vacant", connection);
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
        void Filldatagrid()
        {
            connection = new SQLiteConnection("Data Source= database.vacancies");
            connection.Open();
            if (!File.Exists("./database.vacancies"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "select * from vacant";

            SQLiteDataAdapter sqldat = new SQLiteDataAdapter(query, connection);
            DB = new DataTable();
            sqldat.Fill(DB);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = DB;
            DataView DV = new DataView(DB);
            //DV.RowFilter = string.Format("user LIKE '%{0}%'", label6.Text);
            dataGridView1.DataSource = DV;
        }
        //save
        private void button1_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.vacancies");
            connection.Open();
            if (!File.Exists("./database.vacancies"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "INSERT INTO vacant (name)VALUES('" + textBox1.Text + "')";
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
            Filldatagrid();
        }

        //dalete data
        private void button2_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.vacancies");
            connection.Open();
            if (!File.Exists("./database.vacancies"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "delete from vacant where id ='" + comboBox1.Text + "'";
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
            Filldatagrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.vacancies");
            connection.Open();
            if (!File.Exists("./database.vacancies"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "update vacant set name='" + textBox1.Text + "'where id ='" + comboBox1.Text + "'";
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
            Filldatagrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            job_management job_ = new job_management(label1.Text);
            this.Hide();
            job_.Show();
        }
    }
}
