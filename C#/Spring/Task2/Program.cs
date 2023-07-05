using System.Text.Json;

namespace Task2
{
    class InvalidKursException : Exception
    {
        public InvalidKursException(string str) : base(str)
        {

        }
    }
    class People
    {
        public readonly int id;
        public string Name { get; set; }
        public byte age;
        public People(string name, byte age)
        {
            Name = name;
            this.age = age;
        }
    }
    class Student : People
    {
        public static string university;
        static Student()
        {
            university = "BSTU";
        }
        public Student(float averageMark, int kurs, string name, byte age) : base(name, age)
        {
            this.averageMark = averageMark;
            Kurs = kurs;
        }

        public float averageMark;
        private int kurs;
        public int Kurs
        {
            get { return kurs; }
            set
            {
                if (value < 1 || value > 4)
                    throw new InvalidKursException("Курс может быть от 1 до 4, а никак не " + value);
                else
                {
                    kurs = value;
                }
            }
        }
        public static Student operator +(Student student, int kurs)
        {
            student.Kurs += kurs;
            return student;
        }
        public static Student operator -(Student student, int kurs)
        {
            student.Kurs -= kurs;
            return student;
        }
        public override string ToString()
        {
            return $"{Name} {Kurs}";
        }
    }
    static class Decan
    {
        public static event Action ToNextKourse;
        public static void ToNextYear()
        {
            ToNextKourse.Invoke();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new(8.4f, 2, "Fyodor", 18);
            Student student2 = new(5.2f, 2, "Sombody", 19);
            Student student3 = new(7f, 4, "Ded", 21);
            List<Student> students = new()
            { student1, student2, student3};
            foreach(Student student in students)
            {
                Console.Write(student + "; ");
            }
            Console.WriteLine();
            try
            {
                student1 += 1;
                student3 -= 2;
                student1 -= 3;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            foreach (Student student in students)
            {
                Console.Write(student + "; ");
            }
            Console.WriteLine();
            var newType = from student in students
                          select new
                          {
                              student.Kurs,
                              student.Name,
                              student.averageMark
                          };
            foreach (var type in newType)
            {
                Console.Write($"{type.Name} {type.Kurs} {type.averageMark}; ");
            }
            Console.WriteLine();
            using (FileStream fileStream = new(@"../../../student.json", FileMode.OpenOrCreate))
                foreach(Student student in students)
                    JsonSerializer.Serialize(fileStream, student);
            foreach(Student student in students)
            {
                Decan.ToNextKourse += () => student.Kurs += 1;
            }
            Decan.ToNextYear();
            foreach (Student student in students)
            {
                Console.Write(student + "; ");
            }
            Console.WriteLine();
        }
    }
}