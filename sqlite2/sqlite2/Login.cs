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
            textBox2.PasswordChar = '*';                               //replace character values in password field with stars
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();                                   //Creates a new form
            this.Hide();                                                //Removes current base form
            form.Show();                                                //shows new selected blank form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source= database.logindata");                  // creates new connection to database
            connection.Open();                                                                     // opens new connection to database
            if (!File.Exists("./database.logindata"))                                              //if database file does not exist
            {
                SQLiteConnection.CreateFile("database.sqlite3");                                   //create new database file
                MessageBox.Show("DB created");                                                     //Display message box
            }
            string query = "select* from login4 where username = '" + textBox1.Text + "'and password = '" + textBox2.Text + "'";      //pass values in textbox1 to username value in database and textbox2 to password value in database
            SQLiteCommand cmd = new SQLiteCommand(query, connection);                                                                 //send querry command to database connection
            SQLiteDataAdapter sqlda = new SQLiteDataAdapter(cmd);                                                                     //create data adapter
            DataTable DT = new DataTable();                                                                                           //create new datatable
            sqlda.Fill(DT);                                                                                                           //fill data table
            string combobox = comboBox1.SelectedItem.ToString();                                                                      //convert items in combobox to strings and store them in combobox variable

            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if ((DT.Rows[i]["usertype"].ToString()) == combobox)
                    {
                        MessageBox.Show("You are logged in as " + DT.Rows[i][2]);                               //depending on user type appropriate message will be displayed "you are logged in as 'usertype'"

                        if (comboBox1.SelectedIndex == 2)                                                       //if second item in combobox is selected
                        {
                            Personal_trainer q = new Personal_trainer(textBox1.Text);                          //open personal trainer form
                            this.Hide();                                                                       //Removes current base form  
                            q.Show();                                                                          //shows new desired blank form 
                        }
                        else if(comboBox1.SelectedIndex==1)                                                     //if first item in combobox is selected
                        {
                            Student g = new Student(textBox1.Text);                                             //open student form           
                            g.Show();                                                                          //shows new desired blank form 
                            this.Hide();                                                                       //Removes current base form                      
                        }
                        else if(comboBox1.SelectedIndex ==3)                                                   //if third item in combobox is selected
                        {
                            volunteer h = new volunteer(textBox1.Text);                                        //open volunteer form
                            this.Hide();                                                                       //Removes current base form
                            h.Show();                                                                          //shows new desired blank form    

                        }
                        else if(comboBox1.SelectedIndex ==4)                                                   //if fourth item in combobox is selected
                        {
                           External_user c = new External_user(textBox1.Text);                                //open External User form
                            c.Show();                                                                          //shows new desired blank form
                            this.Hide();                                                                      //Removes current base form

                        }
                        else if (comboBox1.SelectedIndex ==5)                                                 //if fifth item in combobox is selected
                        {
                            Facility_manager a = new Facility_manager(textBox1.Text);                         //open facility manager form
                            this.Hide();                                                                      //Removes current base form
                            a.Show();                                                                          //shows new desired blank form
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Wrong username, password or usertype");                               //display error message if wrong values entered
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();                                                                         //exit application if button is pressed
        }
    }
}
