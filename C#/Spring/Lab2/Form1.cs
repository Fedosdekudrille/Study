using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;
using static Lab2.Form1;

namespace Lab2
{
    public partial class Form1 : Form
    {
        public class Book
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "�������� ����������� ��� ����������")]
            public string Name { get; set; }
            [RegularExpression(@"^\w*$", ErrorMessage = "��� ������ �������� ������������ �������")]
            public string Author { get; set; }
            [BookAge(1990, 2023, ErrorMessage = "���� ����� �����������")]
            public DateTime Date { get; set; }
            public Book(string name, string author, DateTime date)
            {
                Name = name;
                Author = author;
                Date = date;
            }
            public override string ToString()
            {
                return $"{Name} �� {Author} {Date.Year} ����";
            }
        }
        public class Lector
        {

            public string Department { get; set; }
            public string SNP { get; set; }
            [Range(1,399, ErrorMessage = "��������� �� ����� ���� ������ {1} � ������ {2}")]
            public string Audience { get; set; }
            public override string ToString()
            {
                return $"{SNP} �� {Department}, ����������� � {Audience}";
            }
        }
        public class Discipline
        {
            public string Name { get; set; }
            public byte Semester { get; set; }
            public string Kurs { get; set; }
            public byte LectionsNum { get; set; }
            [Range(1, 50, ErrorMessage = "���������� ��� �� ����� ���� ������ {1} � ������ {2}")]
            public int LabsNum { get; set; }
            public string ExamType { get; set; }
            public Lector Lector = new();
            public List<Book> LiteratureList = new();
            public Discipline(string name, byte semester, string kurs, byte lectionsNum, byte labsNum, Lector lector, List<Book> literatureList)
            {
                Name = name;
                Semester = semester;
                Kurs = kurs;
                LectionsNum = lectionsNum;
                LabsNum = labsNum;
                Lector = lector;
                LiteratureList = literatureList;
            }
            public Discipline() { }
            public override string ToString()
            {
                string str =  $"��������: {Name}, �������: {Semester}, ����: {Kurs}, ���-�� ������: {LectionsNum}, ���-�� ���: {LabsNum}, {ExamType};\n������: ";
                str += Lector.ToString() + ";\n";
                foreach(Book book in LiteratureList)
                {
                    str += book.ToString() + "; ";
                }
                str += "\n\n";
                return str;
            }
        }
        static public Discipline discipline = new Discipline();
        static public string message = "";
        public Form1()
        {
            InitializeComponent();
            ChangeLastAction("���");
            timer1.Start();
            timer1.Tick += NewTime;
        }

        private void semesterChange(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                discipline.Semester = 1;
            }
            else
            {
                discipline.Semester = 2;
            }
            ChangeLastAction("�������� ���������� � ����������");

        }
        TreeNode previosCheck = new TreeNode();
        private void changeKurs(object sender, TreeViewEventArgs e)
        {
            if(e.Action != TreeViewAction.ByMouse)
            {
                return;
            }
            if (e.Node.Text != previosCheck?.Text)
            {
                previosCheck.Checked = false;
                ChangeLastAction("�������� ���������� � ����������");

            }
            else
            {
                e.Node.Checked = true;
            }
            previosCheck = e.Node;
            discipline.Kurs = e.Node.Text;
        }

        private void labsNumChanged(object sender, EventArgs e)
        {
            discipline.LabsNum = (int)numericUpDown1.Value;
            ChangeLastAction("�������� ���������� � ����������");

        }

        private void lectorCall(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.Show();
        }

        private void disciplineNameChange(object sender, EventArgs e)
        {
            discipline.Name = disciplineName.Text;
            ChangeLastAction("�������� ���������� � ����������");
        }

        private void lectionsNumChanged(object sender, EventArgs e)
        {
            discipline.LectionsNum = (byte)trackBar1.Value;
            label4.Text = "���������� ������: " + trackBar1.Value;
            ChangeLastAction("�������� ���������� � ����������");

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            discipline.ExamType = domainUpDown1.Text;
            ChangeLastAction("�������� ���������� � ����������");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new(this);
            form3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var context = new ValidationContext(discipline);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(discipline, context, results, true))
            {
                string str = "�� ������� ���������, � ��� ������:\n";
                foreach (var error in results)
                {
                    str += error.ErrorMessage + "\n";
                }
                MessageBox.Show(str);
                return;
            }
            FileInfo fileInfo = new("../../../Base/Discipline.json");
            List<Discipline> disciplines = new(1);
            List<Lector> lectors = new(1);
            List<List<Book>> literature = new(1);
            if (fileInfo.Exists)
            {
                using (FileStream fileStream = new(@"../../../Base/Discipline.json", FileMode.Open))
                    disciplines = JsonSerializer.Deserialize<List<Discipline>>(fileStream);
                using (FileStream fileStream = new(@"../../../Base/Lector.json", FileMode.OpenOrCreate))
                    lectors = JsonSerializer.Deserialize<List<Lector>>(fileStream);
                using (FileStream fileStream = new(@"../../../Base/Books.json", FileMode.OpenOrCreate))
                    literature = JsonSerializer.Deserialize<List<List<Book>>>(fileStream);
            }
            disciplines.Add(discipline);
            lectors.Add(discipline.Lector);
            literature.Add(discipline.LiteratureList);
            using (FileStream fileStream = new(@"../../../Base/Discipline.json", FileMode.OpenOrCreate))
                JsonSerializer.Serialize(fileStream, disciplines);
            using (FileStream fileStream = new(@"../../../Base/Lector.json", FileMode.OpenOrCreate))
                JsonSerializer.Serialize(fileStream, lectors);
            using (FileStream fileStream = new(@"../../../Base/Books.json", FileMode.OpenOrCreate))
                JsonSerializer.Serialize(fileStream, literature);
            ChangeLastAction("���������� � ����");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                List<Discipline> disciplines;
                List<Lector> lectors;
                List<List<Book>> literature;
                using (FileStream fileStream = new(@"../../../Base/Discipline.json", FileMode.Open))
                    disciplines = JsonSerializer.Deserialize<List<Discipline>>(fileStream);
                using (FileStream fileStream = new(@"../../../Base/Lector.json", FileMode.OpenOrCreate))
                    lectors = JsonSerializer.Deserialize<List<Lector>>(fileStream);
                string str = " ����������:\n";
                using (FileStream fileStream = new(@"../../../Base/Books.json", FileMode.OpenOrCreate))
                    literature = JsonSerializer.Deserialize<List<List<Book>>>(fileStream);
                result.Text = "";
                for(int i = 0; i < disciplines.Count; i++)
                {
                    disciplines[i].Lector = lectors[i];
                    disciplines[i].LiteratureList = literature[i];
                    result.Text += disciplines[i].ToString();
                }
                ChangeLastAction("������ �� �����");
            }
            catch
            {
                MessageBox.Show("������ ������");
            }
        }

        public void button5_Click(object sender, EventArgs e)
        {
            Form4 form4 = new(this);
            if(form4 != null)
                form4.Show();
        }
        public void button6_Click(object sender, EventArgs e)
        {
            Form5 form5 = new(this);
            if (form5 != null)
                form5.Show();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("� ����", "��� �?", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        }
        public void ChangeLastAction(string action)
        {
            FileInfo fileInfo = new("../../../Base/Discipline.json");
            int disciplines = 0;
            if (fileInfo.Exists)
                using (FileStream fileStream = new(@"../../../Base/Discipline.json", FileMode.Open))
                    disciplines = JsonSerializer.Deserialize<List<Discipline>>(fileStream).Count;
            message = $" ���-�� ���������: {disciplines};\n��������� ��������: {action};";
            label5.Text = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString() + message;
        }
        public void NewTime(object obj, EventArgs e)
        {
            label5.Text = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString() + message;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = !toolStrip1.Visible;
        }
        public void ShowLabel5()
        {
            MessageBox.Show(label5.Text);
        }
        private void speciality_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ChangeLastAction("�������� ���������� � ����������");
        }
    }
}