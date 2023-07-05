using Lab_8.Class;
using Lab_8.Pages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lab_8
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			try
			{
				/*Flat Flat = new Flat() { Name = "Stas", Square = "Gaykow", Price = "12.2112", ID = 1, BuildYear = "fds", Floor = "+3753032" };
				Lab_8.DB.DB.AddFlatWithAddress(Flat);*/
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
			}
		}

		private void Button_Delete(object sender, RoutedEventArgs e)
		{
			var myValue = ((Button)sender).Tag;

			Lab_8.DB.DB.DeleteDiscipline((int)myValue);
			List<Flat> Disciplines = (List<Flat>)Lab_8.DB.DB.GetAllDisciplines();

			disciplineDataGrid.ItemsSource = Disciplines;
		}

		private void Button_Edit(object sender, RoutedEventArgs e)
		{
			int myValue = (int)((Button)sender).Tag;

			EditDiscipline edit = new EditDiscipline(myValue);

			edit.Show();
		}

		private async void LoadDataButton_Click(object sender, RoutedEventArgs e)
		{
			disciplineDataGrid.Visibility = Visibility.Collapsed;

			List<Flat> data = await LoadDataAsync();

			disciplineDataGrid.Visibility = Visibility.Visible;

			disciplineDataGrid.ItemsSource = data;
		}

		private async Task<List<Flat>> LoadDataAsync()
		{
			List<Flat> Disciplines = (List<Flat>)Lab_8.DB.DB.GetAllDisciplines();

			return Disciplines;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			List<Flat> Disciplines = (List<Flat>)Lab_8.DB.DB.GetAllDisciplines();

			disciplineDataGrid.ItemsSource = Disciplines;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			AddDiscipline DisciplineWindow = new AddDiscipline();
			DisciplineWindow.Show();
		}

		private void Button_Image(object sender, RoutedEventArgs e)
		{
			AddDiscipline DisciplineWindow = new AddDiscipline();
			DisciplineWindow.Show();
		}

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var myValue = ((ComboBox)sender).SelectedIndex;
            List<Flat> result = (List<Flat>)DB.DB.Sort(myValue);
			if(disciplineDataGrid != null)
				disciplineDataGrid.ItemsSource = result;
        }
    }
}