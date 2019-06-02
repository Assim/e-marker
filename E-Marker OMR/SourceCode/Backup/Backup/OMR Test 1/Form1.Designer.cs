namespace OMRReader_test1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fill_tb = new System.Windows.Forms.TrackBar();
            this.filenamet = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.minblb_tb = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.con_tb = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.thresh_tb = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.sim_tb = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.loadLast_cb = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fill_tb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minblb_tb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.con_tb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresh_tb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sim_tb)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(657, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Process with trackbars";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(13, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 467);
            this.panel1.TabIndex = 1;
            // 
            // fill_tb
            // 
            this.fill_tb.Location = new System.Drawing.Point(700, 136);
            this.fill_tb.Maximum = 255;
            this.fill_tb.Name = "fill_tb";
            this.fill_tb.Size = new System.Drawing.Size(235, 45);
            this.fill_tb.TabIndex = 0;
            this.fill_tb.Scroll += new System.EventHandler(this.fill_tb_Scroll);
            // 
            // filenamet
            // 
            this.filenamet.Location = new System.Drawing.Point(700, 101);
            this.filenamet.Name = "filenamet";
            this.filenamet.Size = new System.Drawing.Size(100, 20);
            this.filenamet.TabIndex = 2;
            this.filenamet.Text = "DSC_0005.jpg";
            this.filenamet.TextChanged += new System.EventHandler(this.filenamet_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(651, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "fill";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(654, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "file";
            // 
            // minblb_tb
            // 
            this.minblb_tb.Location = new System.Drawing.Point(700, 187);
            this.minblb_tb.Maximum = 100;
            this.minblb_tb.Name = "minblb_tb";
            this.minblb_tb.Size = new System.Drawing.Size(235, 45);
            this.minblb_tb.TabIndex = 0;
            this.minblb_tb.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(654, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "min blob";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(919, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "fill";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(919, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "fill";
            // 
            // con_tb
            // 
            this.con_tb.Location = new System.Drawing.Point(700, 238);
            this.con_tb.Maximum = 255;
            this.con_tb.Minimum = -255;
            this.con_tb.Name = "con_tb";
            this.con_tb.Size = new System.Drawing.Size(235, 45);
            this.con_tb.TabIndex = 0;
            this.con_tb.TickFrequency = 5;
            this.con_tb.Scroll += new System.EventHandler(this.trackBar1_Scroll_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(919, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "fill";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(654, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Contrast";
            // 
            // thresh_tb
            // 
            this.thresh_tb.Location = new System.Drawing.Point(700, 289);
            this.thresh_tb.Maximum = 255;
            this.thresh_tb.Name = "thresh_tb";
            this.thresh_tb.Size = new System.Drawing.Size(235, 45);
            this.thresh_tb.TabIndex = 0;
            this.thresh_tb.Scroll += new System.EventHandler(this.thresh_tb_Scroll);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(919, 321);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "fill";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(654, 300);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Thresh";
            // 
            // sim_tb
            // 
            this.sim_tb.Location = new System.Drawing.Point(706, 340);
            this.sim_tb.Maximum = 100;
            this.sim_tb.Name = "sim_tb";
            this.sim_tb.Size = new System.Drawing.Size(235, 45);
            this.sim_tb.TabIndex = 0;
            this.sim_tb.Value = 1;
            this.sim_tb.Scroll += new System.EventHandler(this.trackBar1_Scroll_2);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(925, 372);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "fill";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(660, 351);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Similarity";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(671, 392);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(272, 116);
            this.textBox1.TabIndex = 4;
            // 
            // loadLast_cb
            // 
            this.loadLast_cb.AutoSize = true;
            this.loadLast_cb.Location = new System.Drawing.Point(818, 101);
            this.loadLast_cb.Name = "loadLast_cb";
            this.loadLast_cb.Size = new System.Drawing.Size(73, 17);
            this.loadLast_cb.TabIndex = 5;
            this.loadLast_cb.Text = "Load Last";
            this.loadLast_cb.UseVisualStyleBackColor = true;
            this.loadLast_cb.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(374, 485);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Create xml";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(868, 921);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "CandidateSignature";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button2_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(757, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(85, 37);
            this.button5.TabIndex = 0;
            this.button5.Text = "Process with feedback";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(853, 12);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(85, 37);
            this.button6.TabIndex = 0;
            this.button6.Text = "Process";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(657, 55);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(85, 37);
            this.button7.TabIndex = 0;
            this.button7.Text = "Read whole paper";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(757, 55);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(85, 37);
            this.button8.TabIndex = 0;
            this.button8.Text = "Read Reg";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(848, 55);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(85, 37);
            this.button9.TabIndex = 0;
            this.button9.Text = "Create sheet";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(949, 586);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.loadLast_cb);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filenamet);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sim_tb);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.thresh_tb);
            this.Controls.Add(this.con_tb);
            this.Controls.Add(this.minblb_tb);
            this.Controls.Add(this.fill_tb);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fill_tb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minblb_tb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.con_tb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresh_tb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sim_tb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar fill_tb;
        private System.Windows.Forms.TextBox filenamet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar minblb_tb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar con_tb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar thresh_tb;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar sim_tb;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox loadLast_cb;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
    }
}

