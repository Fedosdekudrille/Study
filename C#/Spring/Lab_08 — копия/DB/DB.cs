using Lab_8.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;

namespace Lab_8.DB
{
	public static class DB
	{
        private static SqlConnection connection;
        private static readonly string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private static readonly string connectionStringRecover = ConfigurationManager.AppSettings["ConnectionStringRecover"];
        private static readonly string scriptFilePath = ConfigurationManager.AppSettings["scriptFilePath"];

        private static void OpenConnection()
        {
            try
            {
                if (connection == null)
                {
                    connection = new SqlConnection(connectionString);
                }
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
            }
            catch
            {
                string script = File.ReadAllText(scriptFilePath);
                string[] queries = script.Split(new[] { "go" }, StringSplitOptions.RemoveEmptyEntries);

                using (SqlConnection connection = new SqlConnection(connectionStringRecover))
                {
                    connection.Open();
                    string secQuery = "create database Flat";
                    using (SqlCommand command = new SqlCommand(secQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    foreach (string query in queries)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

					secQuery = "CREATE PROCEDURE AddFlat\r\n    @Name VARCHAR(50),\r\n    @Semester int,\r\n    @Kurs int,\r\n    @LectionsNum int,\r\n\t@LabsNum int,\r\n\t@IsExam bit, \r\n\t@Image VARBINARY(MAX)\r\nAS\r\nBEGIN\r\nINSERT INTO Flats(Name, Square, BuildYear, Floor, Price, IsBricked, Image)\r\nVALUES (@Name, @Semester, @Kurs, @LectionsNum, @LabsNum, @IsExam, @Image)\r\nEND";
                    using (SqlCommand command = new SqlCommand(secQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

					secQuery = "CREATE PROCEDURE AddAddress\r\n\t@DisciplineID INT,\r\n    @Name VARCHAR(50),\r\n\t@Department VARCHAR(20),\r\n    @Auditory VARCHAR(10)\r\nAS\r\nBEGIN\r\nINSERT INTO Addresses(FlatID, City, Street, House)\r\nVALUES (@DisciplineID, @Name, @Department, @Auditory)\r\nEND";
                    using (SqlCommand command = new SqlCommand(secQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

					secQuery = "Create PROCEDURE AddFlatWithAddress\r\n\t@Name VARCHAR(50),\r\n    @Semester int,\r\n    @Kurs int,\r\n    @LectionsNum int,\r\n\t@LabsNum int,\r\n\t@IsExam bit, \r\n\t@Image VARBINARY(MAX),\r\n\t@LectorName VARCHAR(50),\r\n\t@Department VARCHAR(20),\r\n    @Auditory VARCHAR(10)\r\nAS\r\nBEGIN\r\nEXEC AddFlat @Name, @Semester, @Kurs, @LectionsNum, @LabsNum, @IsExam, @Image;\r\nDECLARE @IdentityValue INT;\r\nSET @IdentityValue = IDENT_CURRENT('Flats');\r\nSELECT @IdentityValue;\r\nEXEC AddAddress @IdentityValue, @LectorName, @Department, @Auditory\r\nEND";
                    using (SqlCommand command = new SqlCommand(secQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                OpenConnection();

            }
        }

        private static void CloseConnection()
		{
			if (connection != null && connection.State == System.Data.ConnectionState.Open)
			{
				connection.Close();
			}
		}

        private static Flat ReadFlatWithAddress(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            int semester = reader.GetInt32(2);
            int kurs = reader.GetInt32(3);
            int lectionsNum = reader.GetInt32(4);
            int labsNum = reader.GetInt32(5);
            bool isExam = reader.GetBoolean(6);
            byte[] image = (byte[])reader["Image"];

            Address lector = new Address()
            {
                ID = reader.GetInt32(8),
                House = reader.GetString(9),
                City = reader.GetString(10),
                Street = reader.GetString(11),
            };
            Flat DisciplineIDS = new Flat
            {
                Name = name,
                Square = semester,
                BuildYear = kurs,
                Floor = lectionsNum,
                Price = labsNum,
                ID = id,
                IsBricked = isExam,
                Address = lector,
                Image = image
            };
            return DisciplineIDS;
        }

		public static void AddFlatWithAddress(Flat Discipline)
		{
			OpenConnection();

			using (SqlCommand command = new SqlCommand("AddFlatWithAddress", connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@Name", Discipline.Name);
				command.Parameters.AddWithValue("@Semester", Discipline.Square);
				command.Parameters.AddWithValue("@Kurs", Discipline.BuildYear);
				command.Parameters.AddWithValue("@LectionsNum", Discipline.Floor);
				command.Parameters.AddWithValue("@LabsNum", Discipline.Price);
				command.Parameters.AddWithValue("@IsExam", Discipline.IsBricked);
				command.Parameters.AddWithValue("@Image", Discipline.Image);
				command.Parameters.AddWithValue("@LectorName", Discipline.Address.House);
				command.Parameters.AddWithValue("@Department", Discipline.Address.City);
				command.Parameters.AddWithValue("@Auditory", Discipline.Address.Street);

				int rowsAffected = command.ExecuteNonQuery();
				MessageBox.Show(rowsAffected.ToString());
			}

			CloseConnection();
		}

		public static Flat GetDisciplineById(int disciplineId)
		{
			OpenConnection();

			string query = "SELECT * FROM FlatsWithAddresses WHERE ID = @DisciplineID";

			using (SqlCommand command = new SqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@DisciplineID", disciplineId);

				using (SqlDataReader reader = command.ExecuteReader())
				{

					while (reader.Read())
					{
						return ReadFlatWithAddress(reader);
					}
					reader.Close();
				}
			}

			CloseConnection();
			return null;
		}

		public static void DeleteDiscipline(int DisciplineId)
		{
            OpenConnection();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string secQuery = "DELETE FROM Addresses WHERE FlatID=@ID";
                using (SqlCommand command = new SqlCommand(secQuery, connection))
                {
					command.Transaction = transaction;
                    command.Parameters.AddWithValue("@ID", DisciplineId);
                    int result = command.ExecuteNonQuery();
                }
                string query = "DELETE FROM Flats WHERE ID=@ID";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Transaction = transaction;
					command.Parameters.AddWithValue("@ID", DisciplineId);
					int result = command.ExecuteNonQuery();
				}
				throw new Exception("my");
				transaction.Commit();
            }
			catch (Exception ex)
			{
				transaction.Rollback();
				MessageBox.Show(ex.Message);
			}
			finally
			{
				CloseConnection();
			}
		}

		public static void UpdateDiscipline(int DisciplineId, Flat Discipline)
		{
			try
			{
				OpenConnection();
                string secQuery = "UPDATE Addresses SET House=@FirstName, City=@LastName, Street=@Email WHERE FlatID=@DisciplineId";
                using (SqlCommand command = new SqlCommand(secQuery, connection))
                {
                    command.Parameters.AddWithValue("@DisciplineId", DisciplineId);
                    command.Parameters.AddWithValue("@FirstName", Discipline.Address.House);
                    command.Parameters.AddWithValue("@LastName", Discipline.Address.City);
                    command.Parameters.AddWithValue("@Email", Discipline.Address.Street);
                    int result = command.ExecuteNonQuery();
                }
                string query = "UPDATE Flats SET Name=@FirstName, Square=@LastName, BuildYear=@Email, Floor=@Address, Price=@Phone, IsBricked=@IsExam WHERE ID=@DisciplineId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@DisciplineId", DisciplineId);
					command.Parameters.AddWithValue("@FirstName", Discipline.Name);
					command.Parameters.AddWithValue("@LastName", Discipline.Square);
					command.Parameters.AddWithValue("@Email", Discipline.BuildYear);
					command.Parameters.AddWithValue("@Address", Discipline.Price);
					command.Parameters.AddWithValue("@Phone", Discipline.Floor);
					command.Parameters.AddWithValue("@IsOpen", Discipline.IsBricked);
					command.Parameters.AddWithValue("@IsExam", Discipline.IsBricked);
					int result = command.ExecuteNonQuery();
				}

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				CloseConnection();
			}
		}

		public static IEnumerable<Flat> Sort(int selectedId)
		{
			var Disciplines = GetAllDisciplines().ToList();

			switch (selectedId)
			{
				case 0:
					break;

				case 1:
					Disciplines.Sort((u1, u2) => u1.ID.CompareTo(u2.ID));
					break;

				case 2:
					Disciplines.Sort((u1, u2) => u1.Name.CompareTo(u2.Name));
					break;

				case 3:
					Disciplines.Sort((u1, u2) => u1.Square.CompareTo(u2.Square));
					break;

				case 4:
					Disciplines.Sort((u1, u2) => u1.BuildYear.CompareTo(u2.BuildYear));
					break;

				case 5:
					Disciplines.Sort((u1, u2) => u1.Floor.CompareTo(u2.Floor));
					break;

				case 6:
					Disciplines.Sort((u1, u2) => u1.Price.CompareTo(u2.Price));
					break;

				case 7:
                    Disciplines.Sort((u1, u2) => u1.Price.CompareTo(u2.IsBricked));
					break;

				case 8:
					Disciplines.Sort((u1, u2)=> u1.Address.House.CompareTo(u2.Address.House));
					break;
                default:
					throw new ArgumentException("Invalid sort parameter");
			}

			return Disciplines;
		}
		public static IEnumerable<Flat> GetAllDisciplines()
		{
			try
			{
				OpenConnection();
				string query = "SELECT * FROM FlatsWithAddresses";
				List<Flat> Disciplines = new List<Flat>();
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Disciplines.Add(ReadFlatWithAddress(reader));
						}
						reader.Close();
						return Disciplines;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
			finally { CloseConnection(); }
		}

		public interface ISortStrategy
		{
			IEnumerable<Flat> Sort(IEnumerable<Flat> Disciplines);
		}

		public class SortById : ISortStrategy
		{
			public IEnumerable<Flat> Sort(IEnumerable<Flat> Disciplines)
			{
				return Disciplines.OrderBy(u => u.ID);
			}
		}

		public class SortByFirstName : ISortStrategy
		{
			public IEnumerable<Flat> Sort(IEnumerable<Flat> Disciplines)
			{
				return Disciplines.OrderBy(u => u.Name);
			}
		}

		public class SortByLastName : ISortStrategy
		{
			public IEnumerable<Flat> Sort(IEnumerable<Flat> Disciplines)
			{
				return Disciplines.OrderBy(u => u.Square);
			}
		}

		public class SortByEmail : ISortStrategy
		{
			public IEnumerable<Flat> Sort(IEnumerable<Flat> Disciplines)
			{
				return Disciplines.OrderBy(u => u.BuildYear);
			}
		}

		public class SortByPhone : ISortStrategy
		{
			public IEnumerable<Flat> Sort(IEnumerable<Flat> Disciplines)
			{
				return Disciplines.OrderBy(u => u.Floor);
			}
		}

		public class SortByAddress : ISortStrategy
		{
			public IEnumerable<Flat> Sort(IEnumerable<Flat> Disciplines)
			{
				return Disciplines.OrderBy(u => u.Price);
			}
		}

		public class DisciplineSorter
		{
			private readonly ISortStrategy _strategy;

			public DisciplineSorter(ISortStrategy strategy)
			{
				_strategy = strategy;
			}

			public IEnumerable<Flat> Sort(IEnumerable<Flat> Disciplines)
			{
				return _strategy.Sort(Disciplines);
			}
		}
	}
}