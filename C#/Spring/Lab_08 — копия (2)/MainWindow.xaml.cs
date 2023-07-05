using Lab_8.Class;
using Lab_8.WIndows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Migrations.Model;

namespace Lab_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ApplicationViewModel viewModel;
        public MainWindow()
        {
            try
            {
                viewModel = new ApplicationViewModel();
                DataContext = viewModel;

                InitializeComponent();
                //using (var context = new Lab_8.DB.UnitOfWork())
                //{
                //    Class.Doctor doctor = new Class.Doctor
                //    {
                //        Id = 1,
                //        Name = "Test",
                //        Sphere = "Test",
                //        Kab = 12
                //    };


                //    context.UserRepository.Add(doctor);
                //    context.Save();
                //    Class.Talons talon = new Class.Talons()
                //    {
                //        ClientName = "client",
                //        Date = "12 14:15",
                //        Id = 1,
                //        DoctorId = 1,
                //        Doctor = doctor
                //    };

                //    context.OrdersRepository.Add(talon);
                //    context.Save();
                //    DataContext = context.UserRepository;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var myValue = ((Button)sender).Tag;
            var bd = new Lab_8.DB.DB();

            List<Class.Doctor> users = (List<Class.Doctor>)bd.UserRepository.GetAll();

            //productDataGrid.ItemsSource = users;
            try
            {
                using (var context = new Lab_8.DB.DB())
                {
                    var user = context.UserRepository.Find(myValue);

                    if (user != null) context.UserRepository.Remove(user);
                    context.SaveChanges();
                    //viewModel.Searched?.Remove(user);
                    //SearchedItems.ItemsSource = viewModel.Searched;
                    LoadDataButton_Click(sender, e);



                }

                /*Lab_8.DB.DB.DeleteUser((int)myValue);
				List<User> users = (List<User>)Lab_8.DB.DB.GetAllUsers();*/

                /*productDataGrid.ItemsSource = users;*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_DelOrder(object sender, RoutedEventArgs e)
        {
            var myValue = ((Button)sender).Tag;


            var bd = new Lab_8.DB.DB();

            List<Class.Talons> orders = (List<Class.Talons>)bd.OrdersRepository.GetAll();
            //dataGrid.ItemsSource = orders;
            try
            {
                using (var context = new Lab_8.DB.DB())
                {
                    var order = context.OrdersRepository.Find(myValue);

                    if (order != null) context.OrdersRepository.Remove(order);

                    LoadData();


                }

                /*Lab_8.DB.DB.DeleteUser((int)myValue);
				List<User> users = (List<User>)Lab_8.DB.DB.GetAllUsers();*/

                /*productDataGrid.ItemsSource = users;*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            int myValue = (int)((Button)sender).Tag;
            viewModel.SelectedDoctor = viewModel.Doctor.Where(x => x.Id == myValue).FirstOrDefault();
            EditUser edit = new EditUser(myValue);

            edit.Show();
        }

        public async void LoadData()
        {
            LoadingGrid.Visibility = Visibility.Visible;
            //productDataGrid.Visibility = Visibility.Collapsed;

            List<Class.Doctor> data = await viewModel.LoadDataAsync();
            DoctorList.ItemsSource = viewModel.Doctor;
            TalonsList.ItemsSource = viewModel.Talons;

            LoadingGrid.Visibility = Visibility.Collapsed;
            //productDataGrid.Visibility = Visibility.Visible;

            //productDataGrid.ItemsSource = data;

            //using (var context = new Lab_8.DB.DB())
            //{
            //    var orders = context.Orders.Include(o => o.Doctor).OrderByDescending(o => o.Id).ToList();
            //    Talons.ItemsSource = orders;
            //    Doctor.ItemsSource = context.Users.ToList();
            //}
        }
        private async void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        //private async Task<List<Class.Doctor>> LoadDataAsync()
        //{
        //    //await Task.Delay(2000);
        //    List<Class.Doctor> users;
        //    users = viewModel.DoctorEntity.Users.ToList();
        //    return users;
        //}
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddUser userWindow = new AddUser();
            userWindow.Show();
        }

        private void Button_Image(object sender, RoutedEventArgs e)
        {
            AddUser userWindow = new AddUser();
            userWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                /*		var bd = new Lab_8.DB.DB();

						List<User> users = (List<User>)bd.Users.ToList();
						productDataGrid.ItemsSource = users;*/

                using (var context = new Lab_8.DB.DB())
                {
                    List<Class.Doctor> users = (List<Class.Doctor>)context.Users.ToList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                /*var myValue = ((Button)sender).Tag;
				var res = new DB.DB();
				List<User> result = await res.SortByAsync(myValue.ToString());
				productDataGrid.ItemsSource = result;*/

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            int myValue = (int)((Button)sender).Tag;
            AddOrder addOrder = new AddOrder(myValue);
            addOrder.Show();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}