using Lab_8.Class;
using Lab_8.WIndows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Linq;

namespace Lab_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Database.SetInitializer<Lab_8.DB.DB>(null);

                using (var context = new Lab_8.DB.UnitOfWork())
                {
                    Users user = new Users
                    {
                        UserID = 1,
                        FirstName = "Test",
                        LastName = "Test",
                        Address = "test",
                        Phone = "fds",
                        Email = "test"
                    };


                    context.UserRepository.Add(user);
                    context.Save();
                    Orders order = new Orders()
                    {
                        OrderData = "asdfasddf",
                        TotalAmount = 2,
                        OrderID = 1,
                        UsersUserID = 1,
                        User = user
                    };

                    context.OrdersRepository.Add(order);
                    context.Save();
                }
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

            List<Users> users = (List<Users>)bd.UserRepository.GetAll();

            productDataGrid.ItemsSource = users;
            try
            {
                using (var context = new Lab_8.DB.DB())
                {
                    var user = context.UserRepository.Find(myValue);

                    if (user != null) context.UserRepository.Remove(user);
                    context.SaveChanges();




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

            List<Orders> orders = (List<Orders>)bd.OrdersRepository.GetAll();
            dataGrid.ItemsSource = orders;
            try
            {
                using (var context = new Lab_8.DB.DB())
                {
                    var order = context.Orders.Find(myValue);

                    if (order != null) context.Orders.Remove(order);




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

            EditUser edit = new EditUser(myValue);

            edit.Show();
        }


        private async void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingGrid.Visibility = Visibility.Visible;
            productDataGrid.Visibility = Visibility.Collapsed;

            List<Users> data = await LoadDataAsync();

            LoadingGrid.Visibility = Visibility.Collapsed;
            productDataGrid.Visibility = Visibility.Visible;

            productDataGrid.ItemsSource = data;

            using (var context = new Lab_8.DB.DB())
            {
                var orders = context.Orders.Include(o => o.User).ToList();
                dataGrid.ItemsSource = orders;
            }
        }

        private async Task<List<Users>> LoadDataAsync()
        {
            //await Task.Delay(2000);
            List<Users> users;
            using (var context = new Lab_8.DB.DB())
            {
                users = (List<Users>)context.Users.OrderByDescending(o=>o.UserID).ToList();
            }
            return users;
        }
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
                    List<Users> users = (List<Users>)context.Users.ToList();

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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}