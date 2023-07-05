using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace MVVM
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Good selectedGood;
        public List<Good> Goods { get; set; }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Good good = new Good();
                      Goods.Insert(0, good);
                      using(ShopContext shopContext = new ShopContext())
                      {
                          shopContext.Goods.Add(good);
                          shopContext.SaveChanges();
                          Goods = shopContext.Goods.ToList();
                      }
                      SelectedGood = good;
                      OnPropertyChanged("Goods");
                  }));
            }
        }

        // команда удаления
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Good good = obj as Good;
                      if (good != null)
                      {
                          Goods.Remove(good);
                          using (ShopContext shopContext = new ShopContext())
                          {
                              shopContext.Goods.Remove(good);
                              shopContext.SaveChanges();
                              Goods = shopContext.Goods.ToList();
                          }
                          OnPropertyChanged("Goods");
                      }
                  },
                 (obj) => Goods.Count > 0));
            }
        }
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      Good good = obj as Good;
                      if (good != null)
                      {
                          Goods.Remove(good);
                          using (ShopContext shopContext = new ShopContext())
                          {
                              shopContext.Goods.Where(o => o.Id == good.Id)?.FirstOrDefault()?.SetGoodParams(good);
                              shopContext.SaveChanges();
                              Goods = shopContext.Goods.ToList();
                          };
                          OnPropertyChanged("Goods");
                      }
                  },
                 (obj) => Goods.Count > 0));
            }
        }

        public Good SelectedGood
        {
            get { return selectedGood; }
            set
            {
                selectedGood = value;
                OnPropertyChanged("SelectedGood");
            }
        }
        public void LoadData()
        {
            using (ShopContext shopContext = new())
            {
                Goods = shopContext.Goods.ToList();
            }
        }
        public ApplicationViewModel()
        {
            LoadData();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}