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
    public partial class Form1 : Form
    {
        public SQLiteConnection connection;
        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.logindata");
            connection.Open();
            if (!File.Exists("./database.logindata"))
            {
                SQLiteConnection.CreateFile("database.logindata");
                MessageBox.Show("DB created");
            }
            string query = "INSERT INTO login4 (username,password,usertype)VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.SelectedItem.ToString() + "')";
            string checker = "select count (*) from login4 where username = '" + textBox1.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteCommand cmda = new SQLiteCommand(checker, connection);
            SQLiteDataReader myReader;
            int count = Convert.ToInt32(cmda.ExecuteScalar());
            try
            {
                if (count > 0)
                {
                    MessageBox.Show("There is another person using this username");
                }
                else
                {

                    myReader = cmd.ExecuteReader();
                    MessageBox.Show("Account Created");
                    while (myReader.Read())
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }
    }
}

