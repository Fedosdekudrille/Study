namespace Lab1
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
            this.mainString = new System.Windows.Forms.TextBox();
            this.resultString = new System.Windows.Forms.Label();
            this.mainSubstring = new System.Windows.Forms.TextBox();
            this.operationSelector = new System.Windows.Forms.ComboBox();
            this.subSubString = new System.Windows.Forms.TextBox();
            this.Welcome = new System.Windows.Forms.Label();
            this.stringEnteringText = new System.Windows.Forms.Label();
            this.substringText = new System.Windows.Forms.Label();
            this.subSubStringText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainString
            // 
            this.mainString.Location = new System.Drawing.Point(301, 120);
            this.mainString.Name = "mainString";
            this.mainString.Size = new System.Drawing.Size(231, 27);
            this.mainString.TabIndex = 1;
            this.mainString.TextChanged += new System.EventHandler(this.Calculate);
            // 
            // resultString
            // 
            this.resultString.AutoSize = true;
            this.resultString.Location = new System.Drawing.Point(301, 317);
            this.resultString.Name = "resultString";
            this.resultString.Size = new System.Drawing.Size(0, 20);
            this.resultString.TabIndex = 2;
            // 
            // mainSubstring
            // 
            this.mainSubstring.Location = new System.Drawing.Point(301, 196);
            this.mainSubstring.Name = "mainSubstring";
            this.mainSubstring.Size = new System.Drawing.Size(231, 27);
            this.mainSubstring.TabIndex = 3;
            this.mainSubstring.TextChanged += new System.EventHandler(this.Calculate);
            // 
            // operationSelector
            // 
            this.operationSelector.FormattingEnabled = true;
            this.operationSelector.Items.AddRange(new object[] {
            "Замена подстроки",
            "Удаление заданной подстроки",
            "Символ по индексу",
            "Длина строки",
            "Количество гласных",
            "Количество согласных",
            "Количество предложений",
            "Количество слов"});
            this.operationSelector.Location = new System.Drawing.Point(301, 48);
            this.operationSelector.Name = "operationSelector";
            this.operationSelector.Size = new System.Drawing.Size(231, 28);
            this.operationSelector.TabIndex = 4;
            this.operationSelector.SelectedIndexChanged += new System.EventHandler(this.Calculate);
            // 
            // subSubString
            // 
            this.subSubString.Location = new System.Drawing.Point(301, 271);
            this.subSubString.Name = "subSubString";
            this.subSubString.Size = new System.Drawing.Size(231, 27);
            this.subSubString.TabIndex = 5;
            this.subSubString.TextChanged += new System.EventHandler(this.Calculate);
            // 
            // Welcome
            // 
            this.Welcome.AutoSize = true;
            this.Welcome.Location = new System.Drawing.Point(286, 25);
            this.Welcome.Name = "Welcome";
            this.Welcome.Size = new System.Drawing.Size(266, 20);
            this.Welcome.TabIndex = 6;
            this.Welcome.Text = "Выберите интересующий вас режим\r\n";
            // 
            // stringEnteringText
            // 
            this.stringEnteringText.AutoSize = true;
            this.stringEnteringText.Location = new System.Drawing.Point(360, 97);
            this.stringEnteringText.Name = "stringEnteringText";
            this.stringEnteringText.Size = new System.Drawing.Size(114, 20);
            this.stringEnteringText.TabIndex = 7;
            this.stringEnteringText.Text = "Введите строку";
            // 
            // substringText
            // 
            this.substringText.AutoSize = true;
            this.substringText.Location = new System.Drawing.Point(254, 161);
            this.substringText.Name = "substringText";
            this.substringText.Size = new System.Drawing.Size(325, 20);
            this.substringText.TabIndex = 8;
            this.substringText.Text = "Введите подстроку, которую хотите заменить";
            // 
            // subSubStringText
            // 
            this.subSubStringText.AutoSize = true;
            this.subSubStringText.Location = new System.Drawing.Point(229, 239);
            this.subSubStringText.Name = "subSubStringText";
            this.subSubStringText.Size = new System.Drawing.Size(377, 20);
            this.subSubStringText.TabIndex = 9;
            this.subSubStringText.Text = "Введите подстроку, которой хотите заменить другую";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.subSubStringText);
            this.Controls.Add(this.substringText);
            this.Controls.Add(this.stringEnteringText);
            this.Controls.Add(this.Welcome);
            this.Controls.Add(this.subSubString);
            this.Controls.Add(this.operationSelector);
            this.Controls.Add(this.mainSubstring);
            this.Controls.Add(this.resultString);
            this.Controls.Add(this.mainString);
            this.Name = "Form1";
            this.Text = "Fedosdekudrille";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox mainString;
        private Label resultString;
        private TextBox mainSubstring;
        private ComboBox operationSelector;
        private TextBox subSubString;
        private Label Welcome;
        private Label stringEnteringText;
        private Label substringText;
        private Label subSubStringText;
    }
}