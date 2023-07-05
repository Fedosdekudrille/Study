using Lab_8.Class;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Lab_8.Pages
{
	/// <summary>
	/// Логика взаимодействия для EditDiscipline.xaml
	/// </summary>
	public partial class EditDiscipline : Window
	{
		private int Id { get; set; }

		public EditDiscipline(int val)
		{
			Flat Discipline = DB.DB.GetDisciplineById(val);
			InitializeComponent();
			FirsName.Text = Discipline.Name;
			LastName.Text = Discipline.Square.ToString();
			Address.Text = Discipline.Price.ToString();
			Phone.Text = Discipline.Floor.ToString();
			id.Text = Discipline.BuildYear.ToString();
			Exam.IsChecked = Discipline.IsBricked;
			NSF.Text = Discipline.Address.House;
			department.Text = Discipline.Address.City;
			audience.Text = Discipline.Address.Street;
			Id = val;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string firstname = FirsName.Text.Trim();
				int lastname;
				int address;
				int phone;
				int id;
				bool isExam = (bool)Exam.IsChecked;
				string nsf = NSF.Text.Trim();
				string department = this.department.Text.Trim();
				string aud = this.audience.Text.Trim();

				if (firstname.Length == 0 || !int.TryParse(LastName.Text.Trim(), out lastname) || !int.TryParse(this.id.Text.Trim(), out id) ||
                    !int.TryParse(Address.Text.Trim(), out address) || !int.TryParse(Phone.Text.Trim(), out phone) || phone <= 0 ||
                    nsf.Length == 0 || department.Length == 0 || aud.Length == 0)
				{
					throw new Exception("Error");
				}
				Address lector = new Address()
				{
					ID = new Random().Next(),
					House = nsf,
					City = department,
					Street = aud,
				};
				Flat Discipline = new Flat()
				{
					ID = Id,
					Name = firstname,
					Square = lastname,
					Price = phone,
					Floor = address,
					BuildYear = id,
					Address = lector,
					IsBricked = isExam
				};

				DB.DB.UpdateDiscipline(Id, Discipline);

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

        private void Button_Image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image|*.jpg;*.jpeg;*.png;";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    preview.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                    preview.Width = 100;
                    preview.Height = 100;
                }
                catch
                {
                    MessageBox.Show("Выберите файл подходящего формата.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}