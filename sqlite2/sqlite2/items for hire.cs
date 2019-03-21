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
    public partial class items_for_hire : Form
    {
        SQLiteConnection connection;
        DataTable DB;
        public items_for_hire(string username)
        {
            InitializeComponent();
            FillDataGrid();
            fillcombobox();
            label1.Text = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            item_for_hire_management item_For_Hire_ = new item_for_hire_management(label1.Text);
            this.Hide();
            item_For_Hire_.Show();
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
           // DV.RowFilter = string.Format("user LIKE '%{0}%'", label1.Text);
            dataGridView1.DataSource = DV;
            connection.Close();
        }

        //populate ID
        void fillcombobox()
        {
            connection = new SQLiteConnection("Data Source= database.hiringitems");
            connection.Open();
            if (!File.Exists("./database.hiringitems"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            SQLiteCommand cmd = new SQLiteCommand("select * from items", connection);
            SQLiteDataReader myReader;
            try
            {
                //int i = 0;
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    int pName = myReader.GetInt32(4);

                    comboBox1.Items.Add(pName);

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //update status
        private void button2_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.hiringitems");
            connection.Open();
            if (!File.Exists("./database.hiringitems"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }

            string query = "update items set status ='" + comboBox2.Text + "'where id ='" + comboBox1.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader myReader;

            try
            {
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Status updated");
                while (myReader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FillDataGrid();
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Facility_manager manager = new Facility_manager(label1.Text);
            this.Hide();
            manager.Show();
        }
    }
}
