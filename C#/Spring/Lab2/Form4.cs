using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab2.Form1;

namespace Lab2
{
    public partial class Form4 : Form
    {
        List<Discipline> disciplines;
        List<Discipline>? result = new();
        List<Lector> lectors;
        List<List<Book>> literature;
        Form1 form;
        public Form4(Form1 form)
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
                MessageBox.Show("Нечего искать");
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            textBox1.Select();
        }
        private int CountDisciplineMatches(Discipline discipline, Regex regex)
        {
            string str = "";
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        str = discipline.Lector.SNP;
                        break;
                    }
                case 1:
                    {
                        str = Convert.ToString(discipline.Semester);
                        break;
                    }
                case 2:
                    {
                        str = Convert.ToString(discipline.Kurs);
                        break;
                    }
            }
            if(str == null)
            {
                return 0;
            }
            return regex.Count(str);
        }
        private void Search(object sender, EventArgs e)
        {
            form.ChangeLastAction("Поиск");
            result.Clear();
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    {
                        Regex regex = new(".*" + textBox1.Text + ".*");
                        foreach(Discipline discipline in disciplines)
                        {
                            if(CountDisciplineMatches(discipline, regex) > 0)
                            {
                                result.Add(discipline);
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        Regex regex = new("^" + string.Join("", Enumerable.Repeat(".", (int)numericUpDown1.Value - 1)) + textBox1.Text);
                        foreach (Discipline discipline in disciplines)
                        {
                            if(CountDisciplineMatches(discipline, regex) > 0)
                            {
                                result.Add(discipline);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        Regex regex = new(textBox1.Text);
                        foreach (Discipline discipline in disciplines)
                        {
                            if(CountDisciplineMatches(discipline, regex) == numericUpDown1.Value)
                            {
                                result.Add(discipline);
                            }
                        }
                        break;
                    }
            }
            resultText.Text = "";
            foreach(var discipline in result)
            {
                resultText.Text += discipline.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FileStream fileStream = new(@"../../../Search/Discipline.json", FileMode.OpenOrCreate))
                JsonSerializer.Serialize(fileStream, result);
            lectors.Clear();
            literature.Clear();
            foreach (Discipline discipline in result)
            {
                lectors.Add(discipline.Lector);
                literature.Add(discipline.LiteratureList);
            }
            using (FileStream fileStream = new(@"../../../Search/Lector.json", FileMode.OpenOrCreate))
                JsonSerializer.Serialize(fileStream, lectors);
            using (FileStream fileStream = new(@"../../../Search/Books.json", FileMode.OpenOrCreate))
                JsonSerializer.Serialize(fileStream, literature);
        }
    }
}
