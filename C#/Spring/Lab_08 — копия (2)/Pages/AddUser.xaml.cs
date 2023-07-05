using Lab_8.Class;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Lab_8.WIndows
{
	/// <summary>
	/// Логика взаимодействия для AddUser.xaml
	/// </summary>
	public partial class AddUser : Window
	{
		public AddUser()
		{
			InitializeComponent();
			DataContext = new Doctor();
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
					throw new Exception("Кабинет введён неверно");
				}
				Class.Doctor user = (Doctor)DataContext;

				try
				{
					using (Lab_8.DB.DB context = new Lab_8.DB.DB())
					{
						context.Users.Add(user);
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
	}
}