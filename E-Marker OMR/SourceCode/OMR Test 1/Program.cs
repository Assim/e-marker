using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OMR;
using System.Drawing;
using OMRReader;

namespace OMRReader_test1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            /*
            if (args.Length > 0)
            {
                string teacher = args[0];
                string student = args[1];
                string imagePath = args[2];

                OMR omr = new OMR();
                omr.action(teacher, student, imagePath);
            }*/
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}