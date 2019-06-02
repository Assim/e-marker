using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OMR.XML;

namespace OMR.Forms
{
    public partial class SheetSelector : Form
    {
        public SheetSelector(string xmlFile)
        {
            InitializeComponent();
            for (int i = 1; i < 1000; i++)
            {
                if (i.ToString() != ((OMREnums.OMRSheet)i).ToString())
                {
                    try
                    {
                        OMRSheetReader.GetSheetPropertyLocation(xmlFile, (OMREnums.OMRSheet)i, OMREnums.OMRProperty.SheetSize);
                        listBox1.Items.Add(((OMREnums.OMRSheet)i).ToString());
                    }
                    catch { }
                }
            }
        }

        private void SheetSelector_Load(object sender, EventArgs e)
        {
           
        }
        public string Selected = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                Selected = listBox1.SelectedItem.ToString();
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
