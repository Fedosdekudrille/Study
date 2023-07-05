using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Lab_8.Class
{
    [Table("talons")]
    public class Talons : INotifyPropertyChanged
    {
        private int id;
        [Key]
        public int Id
        {
            get { return id; }
            set { 
                id = value;
                OnPropertyChanged("Id");
            }
        }
        private int doctorId;

        public int DoctorId
        {
            get { return doctorId; }
            set { doctorId = value;
                OnPropertyChanged("DoctorId");
            }
        }
        private string clientName;

        public string ClientName
        {
            get { return clientName; }
            set {
                clientName = value;
                OnPropertyChanged("ClientName");
            }
        }
        //private DateTime date;

        //public string Date
        //{
        //    get { return date.ToString(); }
        //    set
        //    {
        //        date = DateTime.Parse(value);
        //        OnPropertyChanged("Date");
        //    }
        //}
        private DateTime date = DateTime.Now;

        public string Date
        {
            get { return date.ToString(); }
            set
            {
                date = DateTime.Parse(value);
                OnPropertyChanged("Date");
            }
        }

        public virtual Doctor Doctor { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    [Table("doctor")]
    public class Doctor
    {
        private int id;
        [Key]
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set {
                foreach (char c in value)
                    if (char.IsNumber(c))
                    {
                        MessageBox.Show("Имя возвращено к изначальному значению");
                        return;

                    }
                name = value;
                OnPropertyChanged("Name");

            }
        }

        private string sphere;

        public string Sphere
        {
            get { return sphere; }
            set {
                foreach (char c in value)
                    if (char.IsNumber(c))
                    {
                        MessageBox.Show("Сфера возвращена к изначальному значению");
                        return;
                    }
                sphere = value;
                OnPropertyChanged("Sphere");
            }
        }
        private int kab;

        public int Kab
        {
            get { return kab; }
            set { 
                if(value < 0)
                {
                    kab = -value;
                }
                else
                    kab = value;
                OnPropertyChanged("Kab");
            }
        }

        public virtual ICollection<Talons> Talons { get; set; }

        public Doctor()
        {
            Id = new Random().Next(1, 10000);
            Name = string.Empty;
            Sphere = string.Empty;
            Kab = 0;
        }

        public Doctor(int customerID, string firstName, string lastName, int email)
        {
            Id = customerID;
            Name = firstName;
            Sphere = lastName;
            Kab = email;
        }
        public void GetParams(Doctor doctor)
        {
            Name = doctor.Name;
            Sphere = doctor.Sphere;
            Kab = doctor.Kab;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}