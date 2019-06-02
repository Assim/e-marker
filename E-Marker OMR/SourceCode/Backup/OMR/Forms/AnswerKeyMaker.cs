using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OMR;
using OMR.XML;
using System.IO;

namespace OMR.Forms
{
    public partial class AnswerKeyMaker : Form
    {
        public AnswerKeyMaker(string XMLSheets)
        {
            InitializeComponent();
            XMLFileName = XMLSheets;
        }
        string XMLFileName = "";
        OMREnums.OMRSheet workingSheet = OMREnums.OMRSheet.A550;

        private void populate()
        {
            readFile("LastWorkBackUp.omr");
        }

        private void AnswerKeyMaker_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            SheetSelector ssf = new SheetSelector(XMLFileName);
            ssf.StartPosition = FormStartPosition.CenterParent;
            ssf.ShowDialog(this);
            if (ssf.Selected == "")
                Close();
            else
            {
                for (int i = 1; i < 1000; i++)
                {
                    if (i.ToString() != ((OMREnums.OMRSheet)i).ToString())
                    {
                        try
                        {
                            OMRSheetReader.GetSheetPropertyLocation(XMLFileName, (OMREnums.OMRSheet)i, OMREnums.OMRProperty.SheetSize);
                            if (((OMREnums.OMRSheet)i).ToString() == ssf.Selected)
                                workingSheet = (OMREnums.OMRSheet)i;
                        }
                        catch { }
                    }
                }
                populate();
            }
        }
        List<int> answerkey = new List<int>();
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count < OMRSheetReader.GetSheetPropertyInt(
                XMLFileName,
                workingSheet,
                OMREnums.OMRProperty.NumOfBlocks) * (OMRSheetReader.GetSheetPropertyBool(XMLFileName, workingSheet, OMREnums.OMRProperty.TensBlock) ? 10 : 20))
            {
                listBox1.Items.Add("Q# " + (listBox1.Items.Count + 1));
                answerkey.Add(1);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
            else MessageBox.Show(workingSheet.ToString() + " does not support more than " + listBox1.Items.Count + " answers");
        }
        private void showQuestion(int ind)
        {
            if (ind >= 0)
            {
                ora.Checked = true;
                switch (answerkey[ind])
                {
                    case 1: ora.Checked = true; break;
                    case 2: orb.Checked = true; break;
                    case 3: orc.Checked = true; break;
                    case 4: ord.Checked = true; break;
                    case 5: ore.Checked = true; break;
                    case 0: orx.Checked = true; break;
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switchingQuestion = true;
            showQuestion(listBox1.SelectedIndex);
            switchingQuestion = false;
            if (listBox1.Items.Count > 0)
            {
                CreateFile(false, "LastWorkBackUp.omr");
            }
        }
        private void readFile(string File)
        {
            FileStream fs = new FileStream(File, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            textBox9.Text = sr.ReadLine();
            textBox8.Text = sr.ReadLine();
            textBox7.Text = sr.ReadLine();
            string tread = sr.ReadLine();
            switchingQuestion = true;
            while (tread != null)
            {
                string[] qparts = tread.Split(new string[] { ":$:" }, StringSplitOptions.None);
                answerkey.Add(Convert.ToInt32(qparts[1]));
                listBox1.Items.Add("Q# " + (listBox1.Items.Count + 1));
                tread = sr.ReadLine();                
            }
            switchingQuestion = false;
            if (listBox1.Items.Count > 0) listBox1.SelectedIndex = 0;
            fs.Close();
        }
        private void CreateFile(bool randomize,string File)
        {
            try
            {
                FileStream fs = new FileStream(File, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                List<int> newQnums = randomize ? randomList(0, listBox1.Items.Count - 1) : regularList(0, listBox1.Items.Count - 1);
                sw.WriteLine(textBox9.Text);
                sw.WriteLine(textBox8.Text);
                sw.WriteLine(textBox7.Text);

                foreach (int ind in newQnums)
                {
                    sw.WriteLine(String.Format("{0}:$:{1}",
                        listBox1.Items[ind], answerkey[ind]));
                }
                sw.Flush();
                fs.Close();
            }
            catch { }
        }
        private List<int> randomList(int MinNum, int maxNum)
        {
            List<int> nums = new List<int>();
            for (int i = MinNum; i <= maxNum; i++)
            {
                Random r = new Random();
                int n = r.Next(MinNum, maxNum + 1);
                while (nums.Contains(n))
                    n = r.Next(MinNum, maxNum + 1);
                nums.Add(n);
            }
            return nums;
        }
        private List<int> regularList(int MinNum, int maxNum)
        {
            List<int> nums = new List<int>();
            for (int i = MinNum; i <= maxNum; i++)
            { nums.Add(i); }
            return nums;
        }
        private 
        
        bool switchingQuestion = false;
        private void updateQuestion(int ind)
        {
            if (!switchingQuestion)
            {
                if (ora.Checked)
                    answerkey[ind] = 1;
                else if (orb.Checked)
                    answerkey[ind] = 2;
                else if (orc.Checked)
                    answerkey[ind] = 3;
                else if (ord.Checked)
                    answerkey[ind] = 4;
                else if (ore.Checked)
                    answerkey[ind] = 5;
                else if (orx.Checked)
                    answerkey[ind] = 0;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
                updateQuestion(listBox1.SelectedIndex);
        }

        private void ora_CheckedChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            { updateQuestion(listBox1.SelectedIndex); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0 && listBox1.SelectedIndex == listBox1.Items.Count - 1)
            {
                answerkey.RemoveAt(listBox1.SelectedIndex);
                int lastsel = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                try
                {
                    listBox1.SelectedIndex = lastsel - 1;
                }
                catch { try { listBox1.SelectedIndex = 0; } catch { } }
            }
            else MessageBox.Show("Nothing to delete.");
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            readFile("LastWorkBackUp.omr");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "OMR Sheets (*.omr)|*.omr";
            sfd.ShowDialog(this);
            if (sfd.FileName != null && sfd.FileName != "")
            {
                CreateFile(false, sfd.FileName);
                MessageBox.Show("File saved to " + sfd.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void existingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "OMR Sheets (*.omr)|*.omr";
            ofd.ShowDialog(this);
            if (ofd.FileName != "" && ofd.FileName != "")
                readFile(ofd.FileName);
        }
    }
}
