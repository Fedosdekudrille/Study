using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab2.Form1;

namespace Lab2
{
    public partial class Form5 : Form
    {
        List<Discipline> disciplines;
        List<Lector> lectors;
        List<List<Book>> literature;
        IOrderedEnumerable<Discipline>? result = null;
        Form1 form;
        public Form5(Form1 form)
        {
            InitializeComponent();
            this.form = form;
            try
            {
                using (FileStream fileStream = new(@"../../../Base/Discipline.json", FileMode.Open))
                    disciplines = JsonSerializer.Deserialize<List<Discipline>>(fileStream);
                using (FileStream fileStream = new(@"../../../Base/Lector.json", FileMode.OpenOrCreate))
                    lectors = JsonSerializer.Deserialize<List<Lector>>(fileStream);
                string str = " Литература:\n";
                using (FileStream fileStream = new(@"../../../Base/Books.json", FileMode.OpenOrCreate))
                    literature = JsonSerializer.Deserialize<List<List<Book>>>(fileStream);
                for (int i = 0; i < disciplines.Count; i++)
                {
                    disciplines[i].Lector = lectors[i];
                    disciplines[i].LiteratureList = literature[i];
                }
            }
            catch
            {
                Close();
                MessageBox.Show("Нечего сортировать");
            }
            comboBox1.SelectedIndex = 0;
        }
        private void Sort(object sender, EventArgs e)
        {
            form.ChangeLastAction("Сортировка");
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    if(checkBox1.Checked)
                        result = disciplines.OrderByDescending(discipline => discipline.LectionsNum);
                    else
                        result = disciplines.OrderBy((discipline) => discipline.LectionsNum);
                    break;
                case 1:
                    if (checkBox1.Checked)
                        result = disciplines.OrderByDescending(discipline => discipline.ExamType);
                    else
                        result = disciplines.OrderBy(discipline => discipline.ExamType);
                    break;
            }
            label2.Text = "";
            foreach(var discipline in result)
            {
                label2.Text += discipline.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FileStream fileStream = new(@"../../../Sort/Discipline.json", FileMode.OpenOrCreate))
                JsonSerializer.Serialize(fileStream, result);
            lectors.Clear();
            literature.Clear();
            foreach(Discipline discipline in result)
            {
                lectors.Add(discipline.Lector);
                literature.Add(discipline.LiteratureList);
            }
            using (FileStream fileStream = new(@"../../../Sort/Lector.json", FileMode.OpenOrCreate))
                JsonSerializer.Serialize(fileStream, lectors);
            using (FileStream fileStream = new(@"../../../Sort/Books.json", FileMode.OpenOrCreate))
                JsonSerializer.Serialize(fileStream, literature);
        }
    }
}
