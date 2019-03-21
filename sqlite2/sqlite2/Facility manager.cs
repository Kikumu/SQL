using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sqlite2
{
    public partial class Facility_manager : Form
    {
        public Facility_manager(string username)
        {
            InitializeComponent();
            label1.Text = "Welcome " + username;
            label2.Text = username;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            job_management job = new job_management(label2.Text);
            this.Hide();
            job.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Personal_trainer_management personal = new Personal_trainer_management(label2.Text);
            this.Hide();
            personal.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            External_user_management external = new External_user_management(label2.Text);
            this.Hide();
            external.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Student_management student = new Student_management(label2.Text);
            this.Hide();
            student.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            items_for_hire items = new items_for_hire(label2.Text);
            this.Hide();
            items.Show();
        }
    }
}
