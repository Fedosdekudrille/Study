using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using static Lab2.Form1;
using System.Text.Json;

namespace Lab2
{
    public partial class Form2 : Form
    {
        private Form1 form;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            form= form1;
        }
        void departmentChange(object sender, EventArgs e)
        {
            Form1.Lector lector = new();
            lector.Department = textBox2.Text;
            var context = new ValidationContext(lector);
            var results = new List<ValidationResult>();
            if (Validator.TryValidateObject((object)lector, context, results, true))
            {
                Form1.discipline.Lector.Audience = textBox1.Text;
                form.ChangeLastAction("Изменение информации о лекторе");
            }
            else
            {
                textBox2.Text = string.Empty;
                string str = "Лектор некорректен, и вот почему:\n";
                foreach (var error in results)
                {
                    str += error.ErrorMessage + "\n";
                }
                MessageBox.Show(str);
            }
        }
        void SNPchange(object sender, EventArgs e)
        {
            Form1.Lector lector = new();
            lector.SNP = textBox3.Text;
            var context = new ValidationContext(lector);
            var results = new List<ValidationResult>();
            if (Validator.TryValidateObject((object)lector, context, results, true))
            {
                Form1.discipline.Lector.Audience = textBox1.Text;
                form.ChangeLastAction("Изменение информации о лекторе");
            }
            else
            {
                textBox1.Text = string.Empty;
                string str = "Лектор некорректен, и вот почему:\n";
                foreach (var error in results)
                {
                    str += error.ErrorMessage + "\n";
                }
                MessageBox.Show(str);
            }
        }
        void AudienceChange(object sender, EventArgs e)
        {
            Form1.Lector lector = new();
            lector.Audience = textBox1.Text;
            var context = new ValidationContext(lector);
            var results = new List<ValidationResult>();
            if (Validator.TryValidateObject((object)lector, context, results, true))
            {
                Form1.discipline.Lector.Audience = textBox1.Text;
                form.ChangeLastAction("Изменение информации о лекторе");
            }
            else
            {
                textBox1.Text = string.Empty;
                string str = "Лектор некорректен, и вот почему:\n";
                foreach (var error in results)
                {
                    str += error.ErrorMessage + "\n";
                }
                MessageBox.Show(str);
            }
        }
        void AuditoryValidate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                 Form1.discipline.Lector.Audience = ((TextBox)sender).Text;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
    }
}
