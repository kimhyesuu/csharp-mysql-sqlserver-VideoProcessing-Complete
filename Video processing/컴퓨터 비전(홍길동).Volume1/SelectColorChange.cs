using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 컴퓨터_비전_홍길동_.Volume1
{
    public partial class SelectColorChange : Form
    {
        public int value;

        public SelectColorChange()
        {
            InitializeComponent();
        }

        //Brightness Control
        private void btnBright_Click(object sender, EventArgs e)
        {
            value = 1;
            Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            value = 2;
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            value = 3;
            Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            value = 4;
            Close();
        }
        private void btngamma_Click(object sender, EventArgs e)
        {
            value = 5;
            Close();
        }

        //EDGE

        private void btnEdV_Click(object sender, EventArgs e)
        {
            value = 6;
            Close();
        }
        private void btnEdH_Click(object sender, EventArgs e)
        {
            value = 7;
            Close();
        }
        private void btnEDR_Click(object sender, EventArgs e)
        {
            value = 8;
            Close();
        }
        private void btnEDC_Click(object sender, EventArgs e)
        {
            value = 9;
            Close();
        }

        //Histogram

        private void btnES_Click(object sender, EventArgs e)
        {
            value = 10;
            Close();
        }
        private void btnendinSearh_Click(object sender, EventArgs e)
        {
            value = 11;
            Close();
        }
        private void btnDOG_Click(object sender, EventArgs e)
        {
            value = 12;
            Close();
        }
        private void btnLOG_Click(object sender, EventArgs e)
        {
            value = 13;
            Close();
        }
    }
}
