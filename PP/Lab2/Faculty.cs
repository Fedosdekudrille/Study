using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Faculty : Organization, IStaff
    {
        protected List<Department> Departments;

        public Faculty()
        {
            Departments = new List<Department>();
            JobVacancies = new List<JobVacancy>();
            JobTitles = new List<JobTitle>();
            Employees = new List<Employee>();
        }

        public Faculty(Faculty faculty)
        {
            Departments = faculty.Departments;
            JobVacancies = faculty.JobVacancies;
            JobTitles = faculty.JobTitles;
            Employees = faculty.Employees;
        }

        public Faculty(string name, string shortName, string address)
            : base(name, shortName, address)
        {
            Departments = new List<Department>();
            JobVacancies = new List<JobVacancy>();
            JobTitles = new List<JobTitle>();
            Employees = new List<Employee>();
        }

        public int AddDepartment(Department department)
        {
            if (Departments.Contains(department)) return 0;
            Departments.Add(department);
            return 1;
        }

        public bool DelDepartment(int index)
        {
            if (index > Departments.Count) return false;
            Departments.RemoveAt(index);
            return true;
        }

        public bool UpdDepartment(Department dep)
        {
            for (int i = 0; i < Departments.Count; i++)
            {
                if (dep.Name == Departments[i].Name)
                {
                    Departments[i] = dep;
                    return true;
                }
            }
            return false;
        }

        private bool VerDepartment(int id)
        {
            for (int i = 0; i < Departments.Count; i++)
            {
                if (Departments[i].Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Department> GetDepartments()
        {
            return Departments;
        }

        new public void PrintInfo()
        {
            (this as Organization).PrintInfo();

            Console.WriteLine(Departments.Count > 0 ? $"Кафедры:" : "Кафедры не добавлены");
            foreach (var item in Departments)
            {
                Console.WriteLine($"{item.Name}");
            }
        }
    }

    public class Department
    {
        public int Id
        {
            get;
            private set;
        }
        public string Name { get; set; }

        public Department(string name)
        {
            Id = new Random().Next(10000, 100000);
            Name = name;
        }
    }
}
