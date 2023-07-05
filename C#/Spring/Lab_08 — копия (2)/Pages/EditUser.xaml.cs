using Lab_8.Class;
using Lab_8.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;

namespace Lab_8.WIndows
{
	/// <summary>
	/// Логика взаимодействия для EditUser.xaml
	/// </summary>
	public partial class EditUser : Window
	{
		private int Id { get; set; }
		public ApplicationViewModel MainViewModel { get; set; }
		public EditUser(int id)
		{

			try
			{
                Id = id;
				Doctor doctor;
                using (var context = new Lab_8.DB.DB())
                    doctor = context.Users.Find(id);
				DataContext = doctor;
                InitializeComponent();
				//FirsName.Text = doctor.Name;
				//LastName.Text = doctor.Sphere;
				//Address.Text = doctor.Kab.ToString();
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
                string firstname = FirsName.Text.Trim();
                string lastname = LastName.Text.Trim();
                string address = Address.Text.Trim();
				int kab;

                if (firstname.Length == 0 || lastname.Length == 0 ||
                    address.Length == 0 || !int.TryParse(address, out kab))
                {
                    throw new Exception("Error");
                }
                Class.Doctor user = (Doctor)DataContext;

                try
                {
                    using (Lab_8.DB.DB context = new Lab_8.DB.DB())
                    {
                        context.Users.Find(Id).GetParams(user);
						//if(App.Current.MainWindow is MainWindow window)
						//{
						//	window.DoctorList.ItemsSource = context.Users.ToList();
						//}
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			AddOrder addOrder = new AddOrder(Id);

            addOrder.Show();
		}
    }
}