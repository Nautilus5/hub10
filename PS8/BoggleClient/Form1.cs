using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoggleClient
{
    public partial class Form1 : Form
    {
        internal double time = 0;
        internal bool ready = false;
        internal string nickname = "";
        internal string Token = "barac501___3nc012ck1c23c123cwGFBVYE";
        internal string url = "";

        public Form1()
        {
            InitializeComponent();
            //groupBox1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void playerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            GroupBox setup = new GroupBox();
            setup.Show();
            Form1 frm = new Form1();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (groupBox2.Visible)
            {
                groupBox2.Hide();
            }
            else
            {
                groupBox2.Show();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if(groupBox1.Visible)
            {
                groupBox1.Hide();
            }
            else
            {
                groupBox1.Show();
            }
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            nickname = textBox3.Text;
            url = textBox2.Text;
            if(textBox4 != null)
            {
                if (Double.TryParse(textBox4.Text, out time))//if time input is a number
                {
                    if (nickname != null && nickname != "")//If there is a nickname input
                    {
                        if (url == "http://bogglecs3500s16.azurewebsites.net/")//valid url
                        {
                            ready = true;
                        }
                    }
                }
            }   
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ready = false;
            groupBox2.Hide();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
