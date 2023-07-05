namespace Lab2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("3");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("4");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Курс", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.disciplineName = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.speciality = new System.Windows.Forms.ComboBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button8 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.result = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // disciplineName
            // 
            this.disciplineName.Location = new System.Drawing.Point(49, 40);
            this.disciplineName.Name = "disciplineName";
            this.disciplineName.Size = new System.Drawing.Size(150, 27);
            this.disciplineName.TabIndex = 0;
            this.disciplineName.TextChanged += new System.EventHandler(this.disciplineNameChange);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 26);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(38, 24);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.semesterChange);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(50, 26);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(38, 24);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(49, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(90, 57);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Семестр";
            // 
            // speciality
            // 
            this.speciality.FormattingEnabled = true;
            this.speciality.Items.AddRange(new object[] {
            "ПОИТ",
            "ДЭВИ",
            "ПОИБМС",
            "ИСиТ"});
            this.speciality.Location = new System.Drawing.Point(49, 269);
            this.speciality.Name = "speciality";
            this.speciality.Size = new System.Drawing.Size(151, 28);
            this.speciality.TabIndex = 4;
            this.speciality.SelectedIndexChanged += new System.EventHandler(this.speciality_SelectedIndexChanged);
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(49, 122);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "1";
            treeNode1.Text = "1";
            treeNode2.Name = "2";
            treeNode2.Text = "2";
            treeNode3.Name = "3";
            treeNode3.Text = "3";
            treeNode4.Name = "4";
            treeNode4.Text = "4";
            treeNode5.Name = "Kurs";
            treeNode5.Text = "Курс";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.treeView1.Size = new System.Drawing.Size(151, 121);
            this.treeView1.TabIndex = 5;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.changeKurs);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(49, 331);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(150, 27);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.labsNumChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(49, 382);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(150, 56);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.Scroll += new System.EventHandler(this.lectionsNumChanged);
            // 
            // domainUpDown1
            // 
            this.domainUpDown1.Items.Add("Зачёт");
            this.domainUpDown1.Items.Add("Экзамен");
            this.domainUpDown1.Location = new System.Drawing.Point(50, 423);
            this.domainUpDown1.Name = "domainUpDown1";
            this.domainUpDown1.Size = new System.Drawing.Size(150, 27);
            this.domainUpDown1.TabIndex = 8;
            this.domainUpDown1.Text = "Тип экзамена";
            this.domainUpDown1.SelectedItemChanged += new System.EventHandler(this.domainUpDown1_SelectedItemChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Название дисциплины";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Специальность";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Количество лабораторных";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 361);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Количество лекций: 0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(300, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(208, 29);
            this.button1.TabIndex = 19;
            this.button1.Text = "Заполнить лектора";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.lectorCall);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(300, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(208, 29);
            this.button2.TabIndex = 20;
            this.button2.Text = "Добавить книжку";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(591, 35);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(208, 29);
            this.button3.TabIndex = 21;
            this.button3.Text = "Записать в файл";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(591, 87);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(208, 29);
            this.button4.TabIndex = 22;
            this.button4.Text = "Прочитать из файла";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(300, 139);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(208, 29);
            this.button5.TabIndex = 24;
            this.button5.Text = "Поиск";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(591, 136);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(208, 29);
            this.button6.TabIndex = 25;
            this.button6.Text = "Сортировка";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(684, 422);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(115, 29);
            this.button7.TabIndex = 26;
            this.button7.Text = "О программе";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 410);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 20);
            this.label5.TabIndex = 27;
            this.label5.Text = "label5";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(637, 387);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(162, 29);
            this.button8.TabIndex = 28;
            this.button8.Text = "Панель управления";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(97, 606);
            this.toolStrip1.TabIndex = 29;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(94, 24);
            this.toolStripButton1.Text = "Поиск";
            this.toolStripButton1.Click += new System.EventHandler(this.button5_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(94, 24);
            this.toolStripButton2.Text = "Сортировка";
            this.toolStripButton2.Click += new System.EventHandler(this.button6_Click);
            // 
            // result
            // 
            this.result.Location = new System.Drawing.Point(224, 177);
            this.result.Name = "result";
            this.result.ReadOnly = true;
            this.result.Size = new System.Drawing.Size(575, 204);
            this.result.TabIndex = 30;
            this.result.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 477);
            this.Controls.Add(this.result);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.domainUpDown1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.speciality);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.disciplineName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox disciplineName;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private GroupBox groupBox1;
        private ComboBox speciality;
        private TreeView treeView1;
        private NumericUpDown numericUpDown1;
        private TrackBar trackBar1;
        private DomainUpDown domainUpDown1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        public Label label5;
        private System.Windows.Forms.Timer timer1;
        private Button button8;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private RichTextBox result;
    }
}