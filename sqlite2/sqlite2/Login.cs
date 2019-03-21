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

    public partial class Login : Form
    {
        
        public SQLiteConnection connection;
        public Login()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.logindata");
            connection.Open();
            if (!File.Exists("./database.logindata"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                MessageBox.Show("DB created");
            }
            string query = "select* from login4 where username = '" + textBox1.Text + "'and password = '" + textBox2.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataAdapter sqlda = new SQLiteDataAdapter(cmd);
            DataTable DT = new DataTable();
            sqlda.Fill(DT);
            string combobox = comboBox1.SelectedItem.ToString();

            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if ((DT.Rows[i]["usertype"].ToString()) == combobox)
                    {
                        MessageBox.Show("You are logged in as " + DT.Rows[i][2]);

                        if (comboBox1.SelectedIndex == 2)
                        {
                            Personal_trainer q = new Personal_trainer(textBox1.Text);
                            this.Hide();
                            q.Show();
                        }
                        else if(comboBox1.SelectedIndex==1)
                        {
                            Student g = new Student(textBox1.Text);
                            g.Show();
                            this.Hide();
                        }
                        else if(comboBox1.SelectedIndex ==3)
                        {
                            volunteer h = new volunteer(textBox1.Text);
                            this.Hide();
                            h.Show();
                        }
                        else if(comboBox1.SelectedIndex ==4)
                        {
                           External_user c = new External_user(textBox1.Text);
                            c.Show();
                            this.Hide();
                           
                        }
                        else if (comboBox1.SelectedIndex ==5)
                        {
                            Facility_manager a = new Facility_manager(textBox1.Text);
                            this.Hide();
                            a.Show();
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Wrong username, password or usertype");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
