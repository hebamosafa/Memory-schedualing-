using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace first
{
   
    public partial class Form1 : Form
    {
        Label cpu_time;

        public static Form1 Current;
        public Form1()
        {
            Current = this;
            InitializeComponent();
             


        }

        

      
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2(this);
            f2.ShowDialog();
            int No_of_processes = Convert.ToInt32(textBox1.Text);
            int i = No_of_processes;// bool count = true;
          //  List<int> arrival = new List<int>();
            //List<int> cpu = new List<int>();
          
        }

       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
   
}
