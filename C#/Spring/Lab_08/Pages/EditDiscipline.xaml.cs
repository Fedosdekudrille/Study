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
			Discipline Discipline = DB.DB.GetDisciplineById(val);
			InitializeComponent();
			FirsName.Text = Discipline.Name;
			LastName.Text = Discipline.Semester.ToString();
			Address.Text = Discipline.Kurs.ToString();
			Phone.Text = Discipline.LectionsNum.ToString();
			id.Text = Discipline.LabsNum.ToString();
			Exam.IsChecked = Discipline.IsExam;
			NSF.Text = Discipline.Lector.SNP;
			department.Text = Discipline.Lector.Department;
			audience.Text = Discipline.Lector.Audience;
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
					!int.TryParse(Address.Text.Trim(), out address) || !int.TryParse(Phone.Text.Trim(), out phone) ||
					nsf.Length == 0 || department.Length == 0 || aud.Length == 0)
				{
					throw new Exception("Error");
				}
				Lector lector = new Lector()
				{
					ID = new Random().Next(),
					SNP = nsf,
					Department = department,
					Audience = aud,
				};
				Discipline Discipline = new Discipline()
				{
					ID = Id,
					Name = firstname,
					Semester = lastname,
					LabsNum = phone,
					LectionsNum = id,
					Kurs = address,
					Lector = lector,
					IsExam = isExam
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