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
        public SQLiteConnection connection;                                       //initialise database connections
        public Personal_trainer(string username)                                  //store username value                          
        {
            InitializeComponent();
            label1.Text ="Hello "+ username;                                     //display user label 
            label3.Text = username;                                                  
            Fillcombo();                                                         //load fillcombo method
            loadTable();                                                         //load datatable
            combobox2();                                                         //load combobox method
        }

        DataTable DB;                                                           //create new datatable
       
        void Fillcombo()
        {
            connection = new SQLiteConnection("Data Source= database.appointments");          //create new connection to database
            connection.Open();                                                                //open database connection
            if (!File.Exists("./database.appointments"))                                      //if database file doesn not exist
            {
                SQLiteConnection.CreateFile("database.sqlite3");                               //create new database
                MessageBox.Show("DB created");                                                 //display messagebox
            }
            SQLiteCommand cmd = new SQLiteCommand("select * from booker", connection);                //select table from databse 
            SQLiteDataReader myReader;                                                                //create database stream
            try
            {
                myReader = cmd.ExecuteReader();                  //execute database stream reader                  
                while (myReader.Read())                             
                {
                    string pName = myReader.GetString(1);          //add name to database
                    string sName = myReader.GetString(3);          //add name to database

                    if (pName == label3.Text)
                    {
                            comboBox1.Items.Add(sName);               //add name to combobox
                        
                    }
                }
            }
            catch (Exception ex)                           
            {
                MessageBox.Show(ex.Message);                       //display message if error is encountered
            }
        }



        void loadTable()
        {
            connection = new SQLiteConnection("Data Source= database.appointments");         //create new connection to database     
            connection.Open();                                                                //open database connection
            if (!File.Exists("./database.appointments"))                                      //if database file doesn not exist
            {
                SQLiteConnection.CreateFile("database.sqlite3");                               //create new database
                MessageBox.Show("DB created");                                                 //display messagebox                                       
            }

            string query = "select * from booker";                                                //select table from databse 
            SQLiteDataAdapter sqldat = new SQLiteDataAdapter(query, connection);                  //create data adapter
            DB = new DataTable();                                                                 //create new table
            sqldat.Fill(DB);                                                                      //fill table
            dataGridView1.AutoGenerateColumns = false;                                            //disable auto generations of columns
            dataGridView1.DataSource = DB;                                                        //obtain datasource from the datatable
            DataView DV = new DataView(DB);                                                       
            DV.RowFilter = string.Format("pt LIKE '%{0}%'", label3.Text);                        //filter row data to only show data which has the users username
            dataGridView1.DataSource = DV;                                                       //display data
        }



        private void button1_Click(object sender, EventArgs e)                                   //reload appointments button
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
