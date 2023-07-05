using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Lab_8.Class;
using System.Data.Entity;
using System.Windows;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Lab_8.DB;

namespace Lab_8
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Doctor selectedDoctor;
        public Talons CurrentTalon { get; set; } = new Talons();
        //public DB.UnitOfWork unitOfWork = new DB.UnitOfWork();
        private ObservableCollection<Doctor> doctor;
        public ObservableCollection<Doctor> Doctor { get { return doctor; }  set { doctor = value; OnPropertyChanged("Doctor"); } }

        private ObservableCollection<Talons> talons;

        public ObservableCollection<Talons> Talons
        {
            get { return talons; }
            set { 
                talons = value;
                OnPropertyChanged("Talons");
            }
        }
        private ObservableCollection<Doctor> searched;

        public ObservableCollection<Doctor> Searched
        {
            get { return searched; }
            set { 
                searched = value; 
                OnPropertyChanged("Searched");
            }
        }
        private RelayCommand search;
        public RelayCommand Search
        {
            get
            {
                return search ??
                  (search = new RelayCommand(obj =>
                  {
                      if (obj is string str)
                          if (str.Length <= 0)
                              return;
                      using (UnitOfWork unitOfWork = new UnitOfWork())
                      {
                          Searched = new ObservableCollection<Doctor>(unitOfWork.UserRepository.Search(obj));
                          OnPropertyChanged("Searched");
                      }
                  }));
            }
        }
        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      using(UnitOfWork unitOfWork = new UnitOfWork())
                      {
                          Doctor doctor = new Doctor();
                          Doctor.Insert(0, doctor);
                          unitOfWork.UserRepository.Add(doctor);
                          SelectedDoctor = doctor;
                          OnPropertyChanged("Doctor");
                      }

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
                        if (obj is int id)
                        {

                            using (UnitOfWork unitOfWork = new UnitOfWork())
                            {
                                Doctor.Remove(Doctor.Where(i => i.Id == id).FirstOrDefault());
                                if(Searched?.Count(i => i.Id == id) > 0)
                                    Searched.Remove(Searched.Where(i => i.Id == id)?.FirstOrDefault());

                                unitOfWork.UserRepository.Remove(unitOfWork.UserRepository.Find(id));
                                OnPropertyChanged("Doctor");
                            }
                        }
                    },
                (obj) => Doctor.Count > 0));
            }
        }
        private RelayCommand addTalonCommand;
        public RelayCommand AddTalonCommand
        {
            get
            {
                return addTalonCommand ??
                  (addTalonCommand = new RelayCommand(obj =>
                  {

                      using (UnitOfWork unitOfWork = new UnitOfWork())
                      {
                          CurrentTalon.Doctor = obj as Doctor;
                          Talons.Insert(0, CurrentTalon);
                          unitOfWork.OrdersRepository.Add(CurrentTalon);
                          CurrentTalon = new Talons();
                          OnPropertyChanged("Talon");
                      }
                  }));
            }
        }
        private RelayCommand removeTalonCommand;
        public RelayCommand RemoveTalonCommand
        {
            get
            {
                return removeTalonCommand ??
                  (removeTalonCommand = new RelayCommand(obj =>
                  {
                      if (obj is int id)
                      {

                          using (UnitOfWork unitOfWork = new UnitOfWork())
                          {
                              Talons.Remove(Talons.Where(i => i.Id == id).FirstOrDefault());
                              unitOfWork.OrdersRepository.Remove(unitOfWork.OrdersRepository.Find(id));
                              OnPropertyChanged("Talons");
                          }
                      }
                  },
                 (obj) => Talons.Count > 0));
            }
        }
        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
            }
        }

        public ApplicationViewModel()
        {
            //Doctor doctor = new Doctor { Id = 2, Name = "Doc", Sphere = "Ter", Kab = 12 };
            //context.UserRepository.Add(doctor);
            //context.OrdersRepository.Add(new Talons { ClientName = "client", Date = "22.10.2023 16:00", Doctor = doctor, DoctorId = 2, Id = 2 });
            //context.Save();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                List<Class.Doctor> users;
                users = unitOfWork.UserRepository.GetAll().ToList();
                Doctor = new ObservableCollection<Doctor>(users);
                Talons = new ObservableCollection<Talons>(unitOfWork.OrdersRepository.GetAll().ToList());
            }
        }
        public async Task<List<Class.Doctor>> LoadDataAsync()
        {
            //await Task.Delay(2000);

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                List<Class.Doctor> users;
                users = unitOfWork.UserRepository.GetAll().ToList();
                Doctor = new ObservableCollection<Doctor>(users);
                OnPropertyChanged("Doctor");
                //SelectedDoctor?.GetParams(unitOfWork.)
                Talons = new ObservableCollection<Talons>(unitOfWork.OrdersRepository.GetAll().ToList());
                OnPropertyChanged("Talons");
                return users;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
