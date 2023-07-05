using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class JobVacancy
    {
        public int Id { get; private set; }
        public JobTitle Title { get; set; }

        public JobVacancy(JobTitle title)
        {
            Id = new Random().Next(10000, 100000);
            Title = title;
        }
    }
    public class JobTitle
    {
        public string Name { get; set; }

        public JobTitle(string name)
        {
            Name = name;
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public Person(string name)
        {
            Name = name;
        }
    }
    public class Employee : Person
    {
        public Employee(string name)
            : base(name)
        {

        }
    }
    public class Reason
    {
        public int Id { get; private set; }
        public string Text { get; set; }

        public Reason(string text)
        {
            Id = new Random().Next() % 1000;
            Text = text;
        }
    }
}
