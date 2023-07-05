using Lab_8.Class;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Lab_8.WIndows
{
	/// <summary>
	/// Логика взаимодействия для AddOrder.xaml
	/// </summary>
	public partial class AddOrder : Window
	{
		private int Id { get; set; }
		private ApplicationViewModel viewModel;

		public AddOrder(int id)
		{
			if(App.Current.MainWindow is MainWindow window)
				if (window.DataContext is ApplicationViewModel model)
					viewModel = model;
            viewModel.CurrentTalon.DoctorId = id;
            DataContext = viewModel;
			InitializeComponent();
            Id = id;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string name = val.Text;
			if (name.Trim().Length > 0)
			{
                foreach (char c in name)
                    if (char.IsNumber(c))
                    {
                        MessageBox.Show("Имя не может содержать числа");
                        return;

                    }
                try
				{

					
		
					using (var context = new DB.DB())
					{
                        /*User user = context.Users.Find(Id);*/
                        Class.Doctor user = context.UserRepository.Find(Id);
                        MessageBox.Show(Id.ToString());

						Talons orders = new Talons()
						{
							ClientName = name,
							Date = Date.Text,
							Doctor = user,
							DoctorId = Id
						};
                        viewModel.AddTalonCommand.Execute(orders);

                        //                  context.Orders.Add(orders);
                        //context.SaveChanges();
                    }
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				finally
				{
					this.Close();
				}
			}
		}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Close();
        }
    }
}