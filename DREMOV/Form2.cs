using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DREMOV
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 10;
            if(panel2.Width >= 528)
            {
                timer1.Stop();
                Form1 frm1 = new Form1();
                frm1.Show();
                this.Hide();
            }
        }
    }
}
