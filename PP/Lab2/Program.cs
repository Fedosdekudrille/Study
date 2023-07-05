using System;

namespace Lab2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            University bstu = new University("Белорусский государственный технологический университет", "БГТУ", "Свердлова 13");
            Faculty fit = new Faculty("Факультет информационных технологий", "ФИТ", "Свердлова 13, 104-4");

            bstu.AddFaculty(fit);
            bstu.AddJobTitle(new JobTitle("Учитель"));

            List<Organization> organizations = new List<Organization> { bstu, fit };

            foreach (var item in organizations)
            {
                Console.WriteLine($"{item.ShortName} {item.GetType()}");
            }
            Console.WriteLine();
            var teacher = new JobVacancy(new JobTitle("Учитель"));
            bstu.OpenJobVacancy(teacher);
            bstu.PrintJobVacancies();
            bstu.Recruit(teacher, new Person("Олеховский Гокур Устинович"));
            bstu.CloseJobVacancy(0);
            var list = bstu.GetEmployees();
            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();
            fit.AddDepartment(new Department("Математика"));
            fit.AddDepartment(new Department("Программирование"));
            fit.PrintInfo();
            Console.WriteLine();
            fit.DelDepartment(0);
            fit.PrintInfo();
        }
    }
}