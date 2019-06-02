namespace OMR.Forms
{
    partial class AnswerKeyMaker
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ora = new System.Windows.Forms.RadioButton();
            this.orb = new System.Windows.Forms.RadioButton();
            this.orc = new System.Windows.Forms.RadioButton();
            this.ord = new System.Windows.Forms.RadioButton();
            this.ore = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.orx = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.existingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 36);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(128, 121);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // ora
            // 
            this.ora.AutoSize = true;
            this.ora.Location = new System.Drawing.Point(12, 11);
            this.ora.Name = "ora";
            this.ora.Size = new System.Drawing.Size(32, 17);
            this.ora.TabIndex = 6;
            this.ora.TabStop = true;
            this.ora.Text = "A";
            this.ora.UseVisualStyleBackColor = true;
            this.ora.CheckedChanged += new System.EventHandler(this.ora_CheckedChanged);
            // 
            // orb
            // 
            this.orb.AutoSize = true;
            this.orb.Location = new System.Drawing.Point(12, 34);
            this.orb.Name = "orb";
            this.orb.Size = new System.Drawing.Size(32, 17);
            this.orb.TabIndex = 6;
            this.orb.TabStop = true;
            this.orb.Text = "B";
            this.orb.UseVisualStyleBackColor = true;
            this.orb.CheckedChanged += new System.EventHandler(this.ora_CheckedChanged);
            // 
            // orc
            // 
            this.orc.AutoSize = true;
            this.orc.Location = new System.Drawing.Point(12, 57);
            this.orc.Name = "orc";
            this.orc.Size = new System.Drawing.Size(32, 17);
            this.orc.TabIndex = 6;
            this.orc.TabStop = true;
            this.orc.Text = "C";
            this.orc.UseVisualStyleBackColor = true;
            this.orc.CheckedChanged += new System.EventHandler(this.ora_CheckedChanged);
            // 
            // ord
            // 
            this.ord.AutoSize = true;
            this.ord.Location = new System.Drawing.Point(12, 80);
            this.ord.Name = "ord";
            this.ord.Size = new System.Drawing.Size(33, 17);
            this.ord.TabIndex = 6;
            this.ord.TabStop = true;
            this.ord.Text = "D";
            this.ord.UseVisualStyleBackColor = true;
            this.ord.CheckedChanged += new System.EventHandler(this.ora_CheckedChanged);
            // 
            // ore
            // 
            this.ore.AutoSize = true;
            this.ore.Location = new System.Drawing.Point(12, 103);
            this.ore.Name = "ore";
            this.ore.Size = new System.Drawing.Size(32, 17);
            this.ore.TabIndex = 6;
            this.ore.TabStop = true;
            this.ore.Text = "E";
            this.ore.UseVisualStyleBackColor = true;
            this.ore.CheckedChanged += new System.EventHandler(this.ora_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(288, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 34);
            this.button1.TabIndex = 7;
            this.button1.Text = "Generate Answer Sheet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // orx
            // 
            this.orx.AutoSize = true;
            this.orx.Location = new System.Drawing.Point(12, 126);
            this.orx.Name = "orx";
            this.orx.Size = new System.Drawing.Size(89, 17);
            this.orx.TabIndex = 8;
            this.orx.TabStop = true;
            this.orx.Text = "Any of Above";
            this.orx.UseVisualStyleBackColor = true;
            this.orx.CheckedChanged += new System.EventHandler(this.ora_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 165);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(58, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "+Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(76, 165);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(64, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "+Remove";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Unaswered";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Wrong";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Correct";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(77, 77);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(36, 20);
            this.textBox7.TabIndex = 11;
            this.textBox7.Text = "0";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(77, 51);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(36, 20);
            this.textBox8.TabIndex = 11;
            this.textBox8.Text = "0";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(77, 25);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(36, 20);
            this.textBox9.TabIndex = 11;
            this.textBox9.Text = "0";
            this.textBox9.TextChanged += new System.EventHandler(this.textBox9_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(433, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.openToolStripMenuItem.Text = "&File";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.existingToolStripMenuItem,
            this.backupToolStripMenuItem});
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem1.Text = "&Open";
            // 
            // existingToolStripMenuItem
            // 
            this.existingToolStripMenuItem.Name = "existingToolStripMenuItem";
            this.existingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.existingToolStripMenuItem.Text = "&Existing";
            this.existingToolStripMenuItem.Click += new System.EventHandler(this.existingToolStripMenuItem_Click);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.backupToolStripMenuItem.Text = "&Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.orx);
            this.groupBox1.Controls.Add(this.ore);
            this.groupBox1.Controls.Add(this.ord);
            this.groupBox1.Controls.Add(this.orb);
            this.groupBox1.Controls.Add(this.orc);
            this.groupBox1.Controls.Add(this.ora);
            this.groupBox1.Location = new System.Drawing.Point(149, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(130, 152);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Answer";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox9);
            this.groupBox2.Controls.Add(this.textBox8);
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(288, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(133, 109);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Marking";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // AnswerKeyMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 198);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AnswerKeyMaker";
            this.Text = "AnswerKeyMaker";
            this.Load += new System.EventHandler(this.AnswerKeyMaker_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RadioButton ora;
        private System.Windows.Forms.RadioButton orb;
        private System.Windows.Forms.RadioButton orc;
        private System.Windows.Forms.RadioButton ord;
        private System.Windows.Forms.RadioButton ore;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton orx;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem existingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;


    }
}