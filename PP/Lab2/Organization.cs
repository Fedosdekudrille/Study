using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Organization : IStaff
    {

        protected List<JobTitle> JobTitles;
        protected List<JobVacancy> JobVacancies;
        protected List<Employee> Employees;
        public List<JobVacancy> GetJobVacancies()
        {
            return new List<JobVacancy>();
        }

        public int AddJobTitle(JobTitle title)
        {
            if (JobTitles.Contains(title)) return 0;
            JobTitles.Add(title);
            return 1;
        }

        public bool DelJobTitle(int index)
        {
            if (index > JobTitles.Count) return false;
            JobTitles.RemoveAt(index);
            return true;
        }

        public int OpenJobVacancy(JobVacancy vacancy)
        {
            if (JobVacancies.Contains(vacancy)) return 0;
            JobVacancies.Add(vacancy);
            return 1;
        }

        public bool CloseJobVacancy(int index)
        {
            if (index > JobVacancies.Count) return false;
            JobVacancies.RemoveAt(index);
            return true;
        }

        public Employee Recruit(JobVacancy vacancy, Person person)
        {
            var employee = new Employee(person.Name);
            Employees.Add(employee);
            return employee;
        }

        public void Dismiss(int index, Reason reason)
        {
            Employees.RemoveAt(index);
            Console.WriteLine($"{Employees[index].Name} был уволен по причине {reason.Text}");
        }

        public List<Employee> GetEmployees()
        {
            return Employees;
        }

        public List<JobTitle> GetJobTitles()
        {
            return JobTitles;
        }

        public void PrintJobVacancies()
        {
            foreach (var item in JobVacancies)
            {
                Console.WriteLine($"{item.Id} - {item.Title.Name}");
            }
        }

        public int Id { get; private set; }

        public string Name { get; protected set; }

        public string ShortName { get; protected set; }

        public string Address { get; protected set; }

        public DateTime TimeStamp { get; protected set; }

        public Organization()
        {
            Id = new Random().Next(10000, 100000);
            Name = "Не назначено";
            ShortName = "Нет";
            Address = "Не назначен";
            TimeStamp = DateTime.Now;
            JobTitles = new List<JobTitle>();
            JobVacancies = new List<JobVacancy>();
            Employees = new List<Employee>();
        }

        public Organization(string name, string shortName, string address)
        {
            Id = new Random().Next(10000, 100000);
            Name = name;
            ShortName = shortName;
            Address = address;
            TimeStamp = DateTime.Now;
            JobTitles = new List<JobTitle>();
            JobVacancies = new List<JobVacancy>();
            Employees = new List<Employee>();
        }

        public Organization(Organization organization)
        {
            Id = organization.Id;
            Name = organization.Name;
            ShortName = organization.ShortName;
            Address = organization.Address;
            TimeStamp = organization.TimeStamp;
            JobTitles = organization.JobTitles;
            JobVacancies = organization.JobVacancies;
            Employees = organization.Employees;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Id}: {Name} {Address} {TimeStamp}");
        }
    }
}
