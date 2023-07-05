using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MVVM
{
    [Table("Good")]
    public class Good : INotifyPropertyChanged
    {
        private string name;
        private int number;
        private int price;
        public int Id { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Number
        {
            get { return number; }
            set
            {
                if (value < 0)
                    number = 0;
                else
                    number = value;
                OnPropertyChanged("Number");
            }
        }
        public int Price
        {
            get { return price; }
            set
            {
                if(value < 0 ) 
                    price = 0;
                else
                    price = value;
                OnPropertyChanged("Price");
            }
        }
        public void SetGoodParams(Good good)
        {
            Name = good.Name;
            Number = good.Number;
            Price = good.Price;
        }
        public Good()
        {
            Name = string.Empty;
            Number = 0;
            Price = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
