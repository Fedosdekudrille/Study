using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal interface IStaff
    {
        public List<JobVacancy> GetJobVacancies();
        public List<Employee> GetEmployees();
        public List<JobTitle> GetJobTitles();
        public int AddJobTitle(JobTitle jobTitle);
        public void PrintJobVacancies();
        public bool DelJobTitle(int index);
        public int OpenJobVacancy(JobVacancy vacancy);
        public bool CloseJobVacancy(int index);
        public Employee Recruit(JobVacancy vacancy, Person person);
        public void Dismiss(int index, Reason reason);
    }
}
