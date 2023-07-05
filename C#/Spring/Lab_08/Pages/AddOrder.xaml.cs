﻿using Lab_8.Class;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab_8.WIndows
{
	/// <summary>
	/// Логика взаимодействия для AddOrder.xaml
	/// </summary>
	public partial class AddOrder : Window
	{
		private int Id { get; set; }

		public AddOrder(int id)
		{
			InitializeComponent();

			Id = id;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string name = val.Text;
			if (name.Trim().Length > 0)
			{
				try
				{

					
		
					using (var context = new DB.DB())
					{
						/*User user = context.Users.Find(Id);*/
						Users user = context.UserRepository.Find(Id);
						MessageBox.Show(Id.ToString());
					
						Orders orders = new Orders()
						{
							OrderData = name,
							User = user
						};

						context.Orders.Add(orders);
						context.SaveChanges();
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
	}
}