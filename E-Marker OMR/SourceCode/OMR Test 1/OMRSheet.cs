using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using OMR;
using System.Drawing;
using OMRReader;
using System.Windows.Forms;

namespace OMRReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OMR omr = new OMR();
            omr.action("", "", textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path;
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                path = file.FileName;
                textBox1.Text = path;
            }
        }
    }
}
