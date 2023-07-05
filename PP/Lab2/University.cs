using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class University : Organization
    {
        protected List<Faculty> Faculties;

        public University()
            : base()
        {
            Faculties = new List<Faculty>();
            JobTitles = new List<JobTitle>();
            JobVacancies = new List<JobVacancy>();
            Employees = new List<Employee>();
        }

        public University(University university)
            : base(university)
        {
            Faculties = university.Faculties;
            JobTitles = university.JobTitles;
            JobVacancies = university.JobVacancies;
            Employees = university.Employees;
        }

        public University(string name, string shortName, string address)
            : base(name, shortName, address)
        {
            Faculties = new List<Faculty>();
            JobTitles = new List<JobTitle>();
            JobVacancies = new List<JobVacancy>();
            Employees = new List<Employee>();
        }

        public int AddFaculty(Faculty faculty)
        {
            if (Faculties.Contains(faculty)) return 0;
            Faculties.Add(faculty);
            return 1;
        }

        public bool DelFaculty(int index)
        {
            if (Faculties.Count < index) return false;
            Faculties.RemoveAt(index);
            return true;
        }

        public bool UpdFaculty(Faculty faculty)
        {
            for (int i = 0; i < Faculties.Count; i++)
            {
                if (Faculties[i].Name == faculty.Name)
                {
                    Faculties[i] = faculty;
                    return true;
                }
            }
            return false;
        }

        private bool VerFaculty(int id)
        {
            for (int i = 0; i < Faculties.Count; i++)
            {
                if (Faculties[i].Id == id) return true;
            }
            return false;
        }

        public List<Faculty> GetFaculties()
        {
            return Faculties;
        }

        new public void PrintInfo()
        {
            Console.WriteLine($"{Id}\t{Name}\t{ShortName}\n\t{Address}\t{TimeStamp}");
            foreach (var faculty in Faculties)
            {
                faculty.PrintInfo();
            }
        }
    }
}
