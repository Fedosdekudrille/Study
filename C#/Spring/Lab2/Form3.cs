using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form3 : Form
    {
        private Form1 form;
        public Form3(Form1 form1)
        {
            InitializeComponent();
            form = form1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.Book book = new Form1.Book(textBox4.Text, textBox5.Text, dateTimePicker1.Value);
            var context = new ValidationContext(book);
            var results = new List<ValidationResult>();
            if (Validator.TryValidateObject((object)book, context, results, true))
            {
                Form1.discipline.LiteratureList.Add(book);
                form.ChangeLastAction("Добавление книги");
                this.Close();
            }
            else
            {
                string str = "Не удалось добавить книжку, и вот почему:\n";
                foreach (var error in results)
                {
                    str += error.ErrorMessage + "\n";
                }
                MessageBox.Show(str);
            }
        }
    }
}
