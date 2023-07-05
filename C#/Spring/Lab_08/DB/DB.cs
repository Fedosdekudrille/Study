using Lab_8.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab_8.DB
{
    //public static class DB
    //{
    //	private static SqlConnection connection;
    //	private static readonly string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
    //	private static readonly string connectionStringRecover = ConfigurationManager.AppSettings["ConnectionStringRecover"];
    //       private static readonly string scriptFilePath = ConfigurationManager.AppSettings["scriptFilePath"];

    //       private static void OpenConnection()
    //	{
    //		try
    //		{
    //               if (connection == null)
    //               {
    //                   connection = new SqlConnection(connectionString);
    //               }
    //               if (connection.State != System.Data.ConnectionState.Open)
    //               {
    //                   connection.Open();
    //               }
    //           }
    //		catch
    //		{
    //			string script = File.ReadAllText(scriptFilePath);
    //			string[] queries = script.Split(new[] { "go" }, StringSplitOptions.RemoveEmptyEntries);

    //			using (SqlConnection connection = new SqlConnection(connectionStringRecover))
    //			{
    //			    connection.Open();
    //			    string secQuery = "create database University";
    //			    using (SqlCommand command = new SqlCommand(secQuery, connection))
    //			    {
    //			        command.ExecuteNonQuery();
    //			    }

    //			    foreach (string query in queries)
    //			    {
    //			        using (SqlCommand command = new SqlCommand(query, connection))
    //			        {
    //			            command.ExecuteNonQuery();
    //			        }
    //			    }
    //				secQuery = "CREATE PROCEDURE AddDiscipline\r\n    @Name VARCHAR(50),\r\n    @Semester int,\r\n    @Kurs int,\r\n    @LectionsNum int,\r\n\t@LabsNum int,\r\n\t@IsExam bit, \r\n\t@Image VARBINARY(MAX)\r\nAS\r\nBEGIN\r\n    INSERT INTO Disciplines(Name, Semester, Kurs, LectionsNum, LabsNum, IsExam, Image)\r\n    VALUES (@Name, @Semester, @Kurs, @LectionsNum, @LabsNum, @IsExam, @Image)\r\nEND";
    //			    using (SqlCommand command = new SqlCommand(secQuery, connection))
    //			    {
    //			        command.ExecuteNonQuery();
    //			    }
    //				secQuery = "CREATE PROCEDURE AddDisciplineWithLector\r\n\t@Name VARCHAR(50),\r\n    @Semester int,\r\n    @Kurs int,\r\n    @LectionsNum int,\r\n\t@LabsNum int,\r\n\t@IsExam bit, \r\n\t@Image VARBINARY(MAX),\r\n\t@LectorName VARCHAR(50),\r\n\t@Department VARCHAR(20),\r\n    @Auditory VARCHAR(10)\r\nAS\r\nBEGIN\r\nEXEC AddDiscipline @Name, @Semester, @Kurs, @LectionsNum, @LabsNum, @IsExam, @Image;\r\nDECLARE @IdentityValue INT;\r\nSET @IdentityValue = IDENT_CURRENT('Disciplines');\r\nSELECT @IdentityValue;\r\nEXEC AddLector @IdentityValue, @LectorName, @Department, @Auditory\r\nEND";
    //			    using (SqlCommand command = new SqlCommand(secQuery, connection))
    //			    {
    //			        command.ExecuteNonQuery();
    //			    }
    //				connection.Close();
    //			}
    //			OpenConnection();

    //           }
    //	}

    //	private static void CloseConnection()
    //	{
    //		if (connection != null && connection.State == System.Data.ConnectionState.Open)
    //		{
    //			connection.Close();
    //		}
    //	}

    //       private static Discipline ReadDisciplineWithLector(SqlDataReader reader)
    //       {
    //           int id = reader.GetInt32(0);
    //           string name = reader.GetString(1);
    //           int semester = reader.GetInt32(2);
    //           int kurs = reader.GetInt32(3);
    //           int lectionsNum = reader.GetInt32(4);
    //           int labsNum = reader.GetInt32(5);
    //           bool isExam = reader.GetBoolean(6);
    //           byte[] image = (byte[])reader["Image"];

    //           Lector lector = new Lector()
    //           {
    //               ID = reader.GetInt32(8),
    //               SNP = reader.GetString(9),
    //               Department = reader.GetString(10),
    //               Audience = reader.GetString(11),
    //           };
    //           Discipline DisciplineIDS = new Discipline
    //           {
    //               Name = name,
    //               Semester = semester,
    //               Kurs = kurs,
    //               LectionsNum = lectionsNum,
    //               LabsNum = labsNum,
    //               ID = id,
    //               IsExam = isExam,
    //               Lector = lector,
    //               Image = image
    //           };

    //           return DisciplineIDS;
    //       }

    //	public static void AddDisciplineWithLector(Discipline Discipline)
    //	{
    //		OpenConnection();

    //		using (SqlCommand command = new SqlCommand("AddDisciplineWithLector", connection))
    //		{
    //			command.CommandType = CommandType.StoredProcedure;
    //			command.Parameters.AddWithValue("@Name", Discipline.Name);
    //			command.Parameters.AddWithValue("@Semester", Discipline.Semester);
    //			command.Parameters.AddWithValue("@Kurs", Discipline.Kurs);
    //			command.Parameters.AddWithValue("@LectionsNum", Discipline.LectionsNum);
    //			command.Parameters.AddWithValue("@LabsNum", Discipline.LabsNum);
    //			command.Parameters.AddWithValue("@IsExam", Discipline.IsExam);
    //			command.Parameters.AddWithValue("@Image", Discipline.Image);
    //			command.Parameters.AddWithValue("@LectorName", Discipline.Lector.SNP);
    //			command.Parameters.AddWithValue("@Department", Discipline.Lector.Department);
    //			command.Parameters.AddWithValue("@Auditory", Discipline.Lector.Audience);

    //			int rowsAffected = command.ExecuteNonQuery();
    //			MessageBox.Show(rowsAffected.ToString());
    //		}

    //		CloseConnection();
    //	}

    //	public static Discipline GetDisciplineById(int disciplineId)
    //	{
    //		OpenConnection();

    //		string query = "SELECT * FROM DisciplinesWithLectors WHERE ID = @DisciplineID";

    //		using (SqlCommand command = new SqlCommand(query, connection))
    //		{
    //			command.Parameters.AddWithValue("@DisciplineID", disciplineId);

    //			using (SqlDataReader reader = command.ExecuteReader())
    //			{
    //                   while (reader.Read())
    //                   {
    //                       return ReadDisciplineWithLector(reader);
    //                   }
    //               }
    //		}

    //		CloseConnection();
    //		return null;
    //	}

    //	public static void DeleteDiscipline(int DisciplineId)
    //	{
    //		try
    //		{
    //			OpenConnection();
    //               string secQuery = "DELETE FROM Lectors WHERE DisciplineID=@ID";
    //               using (SqlCommand command = new SqlCommand(secQuery, connection))
    //               {
    //                   command.Parameters.AddWithValue("@ID", DisciplineId);
    //                   int result = command.ExecuteNonQuery();
    //               }
    //               string query = "DELETE FROM Disciplines WHERE ID=@ID";
    //			using (SqlCommand command = new SqlCommand(query, connection))
    //			{
    //				command.Parameters.AddWithValue("@ID", DisciplineId);
    //				int result = command.ExecuteNonQuery();
    //			}

    //           }
    //		catch (Exception ex)
    //		{
    //			MessageBox.Show(ex.Message);
    //		}
    //		finally
    //		{
    //			CloseConnection();
    //		}
    //	}

    //	public static void UpdateDiscipline(int DisciplineId, Discipline Discipline)
    //	{
    //		try
    //		{
    //			OpenConnection();
    //               string secQuery = "UPDATE Lectors SET Name=@FirstName, Department=@LastName, Auditory=@Email WHERE DisciplineID=@DisciplineId";
    //               using (SqlCommand command = new SqlCommand(secQuery, connection))
    //               {
    //                   command.Parameters.AddWithValue("@DisciplineId", DisciplineId);
    //                   command.Parameters.AddWithValue("@FirstName", Discipline.Lector.SNP);
    //                   command.Parameters.AddWithValue("@LastName", Discipline.Lector.Department);
    //                   command.Parameters.AddWithValue("@Email", Discipline.Lector.Audience);
    //                   int result = command.ExecuteNonQuery();
    //               }
    //               string query = "UPDATE Disciplines SET Name=@FirstName, Semester=@LastName, Kurs=@Email, LabsNum=@Address, LectionsNum=@Phone, IsExam=@IsExam WHERE ID=@DisciplineId";
    //			using (SqlCommand command = new SqlCommand(query, connection))
    //			{
    //				command.Parameters.AddWithValue("@DisciplineId", DisciplineId);
    //				command.Parameters.AddWithValue("@FirstName", Discipline.Name);
    //				command.Parameters.AddWithValue("@LastName", Discipline.Semester);
    //				command.Parameters.AddWithValue("@Email", Discipline.Kurs);
    //				command.Parameters.AddWithValue("@Address", Discipline.LabsNum);
    //				command.Parameters.AddWithValue("@Phone", Discipline.LectionsNum);
    //				command.Parameters.AddWithValue("@IsOpen", Discipline.IsExam);
    //				command.Parameters.AddWithValue("@IsExam", Discipline.IsExam);
    //				int result = command.ExecuteNonQuery();
    //			}

    //           }
    //		catch (Exception ex)
    //		{
    //			MessageBox.Show(ex.Message);
    //		}
    //		finally
    //		{
    //			CloseConnection();
    //		}
    //	}

    //	public static IEnumerable<Discipline> Sort(int selectedId)
    //	{
    //		var Disciplines = GetAllDisciplines().ToList();

    //		switch (selectedId)
    //		{
    //			case 0:
    //				break;

    //			case 1:
    //				Disciplines.Sort((u1, u2) => u1.ID.CompareTo(u2.ID));
    //				break;

    //			case 2:
    //				Disciplines.Sort((u1, u2) => u1.Name.CompareTo(u2.Name));
    //				break;

    //			case 3:
    //				Disciplines.Sort((u1, u2) => u1.Semester.CompareTo(u2.Semester));
    //				break;

    //			case 4:
    //				Disciplines.Sort((u1, u2) => u1.Kurs.CompareTo(u2.Kurs));
    //				break;

    //			case 5:
    //				Disciplines.Sort((u1, u2) => u1.LectionsNum.CompareTo(u2.LectionsNum));
    //				break;

    //			case 6:
    //				Disciplines.Sort((u1, u2) => u1.LabsNum.CompareTo(u2.LabsNum));
    //				break;

    //			case 7:
    //                   Disciplines.Sort((u1, u2) => u1.LabsNum.CompareTo(u2.IsExam));
    //				break;

    //			case 8:
    //				Disciplines.Sort((u1, u2)=> u1.Lector.SNP.CompareTo(u2.Lector.SNP));
    //				break;
    //               default:
    //				throw new ArgumentException("Invalid sort parameter");
    //		}

    //		return Disciplines;
    //	}
    //	public static IEnumerable<Discipline> GetAllDisciplines()
    //	{
    //		try
    //		{
    //			OpenConnection();
    //			string query = "SELECT * FROM DisciplinesWithLectors";
    //			List<Discipline> Disciplines = new List<Discipline>();
    //			using (SqlCommand command = new SqlCommand(query, connection))
    //			{
    //				using (SqlDataReader reader = command.ExecuteReader())
    //				{
    //					while (reader.Read())
    //					{
    //						Disciplines.Add(ReadDisciplineWithLector(reader));
    //					}
    //					reader.Close();
    //					return Disciplines;
    //				}
    //			}
    //		}
    //		catch (Exception ex)
    //		{
    //			MessageBox.Show(ex.Message);
    //			return null;
    //		}
    //		finally { CloseConnection(); }
    //	}

    //	public interface ISortStrategy
    //	{
    //		IEnumerable<Discipline> Sort(IEnumerable<Discipline> Disciplines);
    //	}

    //	public class SortById : ISortStrategy
    //	{
    //		public IEnumerable<Discipline> Sort(IEnumerable<Discipline> Disciplines)
    //		{
    //			return Disciplines.OrderBy(u => u.ID);
    //		}
    //	}

    //	public class SortByFirstName : ISortStrategy
    //	{
    //		public IEnumerable<Discipline> Sort(IEnumerable<Discipline> Disciplines)
    //		{
    //			return Disciplines.OrderBy(u => u.Name);
    //		}
    //	}

    //	public class SortByLastName : ISortStrategy
    //	{
    //		public IEnumerable<Discipline> Sort(IEnumerable<Discipline> Disciplines)
    //		{
    //			return Disciplines.OrderBy(u => u.Semester);
    //		}
    //	}

    //	public class SortByEmail : ISortStrategy
    //	{
    //		public IEnumerable<Discipline> Sort(IEnumerable<Discipline> Disciplines)
    //		{
    //			return Disciplines.OrderBy(u => u.Kurs);
    //		}
    //	}

    //	public class SortByPhone : ISortStrategy
    //	{
    //		public IEnumerable<Discipline> Sort(IEnumerable<Discipline> Disciplines)
    //		{
    //			return Disciplines.OrderBy(u => u.LectionsNum);
    //		}
    //	}

    //	public class SortByAddress : ISortStrategy
    //	{
    //		public IEnumerable<Discipline> Sort(IEnumerable<Discipline> Disciplines)
    //		{
    //			return Disciplines.OrderBy(u => u.LabsNum);
    //		}
    //	}

    //	public class DisciplineSorter
    //	{
    //		private readonly ISortStrategy _strategy;

    //		public DisciplineSorter(ISortStrategy strategy)
    //		{
    //			_strategy = strategy;
    //		}

    //		public IEnumerable<Discipline> Sort(IEnumerable<Discipline> Disciplines)
    //		{
    //			return _strategy.Sort(Disciplines);
    //		}
    //	}
    //}
    public class DB : DbContext
    {
        public DB() : base("name=Test")
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Orders> Orders { get; set; }

        public IRepository<Users> UserRepository => new UserRepository(this);
        public IRepository<Orders> OrdersRepository => new OrdersRepository(this);
    }



    public interface IRepository<T> where T : class
    {

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        T Find(object val);

    }

    public class UserRepository : IRepository<Users>
    {
        private readonly DB _dbContext;

        public UserRepository(DB dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Users> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public void Add(Users user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void Update(Users user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remove(Users user)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public Users Find(object id)
        {
            return _dbContext.Users.Find(id);
        }
    }

    public class OrdersRepository : IRepository<Orders>
    {
        private readonly DB _dbContext;

        public OrdersRepository(DB dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Orders> GetAll()
        {
            return _dbContext.Orders.ToList();
        }

        public async Task<IEnumerable<Orders>> GetAllAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public void Add(Orders orders)
        {
            _dbContext.Orders.Add(orders);
            _dbContext.SaveChanges();
        }

        public void Update(Orders orders)
        {
            _dbContext.Entry(orders).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remove(Orders orders)
        {
            _dbContext.Orders.Remove(orders);
            _dbContext.SaveChanges();
        }

        public Orders Find(object id)
        {
            return _dbContext.Orders.Find(id);
        }
    }

    public class UnitOfWork : IDisposable
    {
        private readonly DB _dbContext;
        private bool disposed = false;

        public UnitOfWork()
        {
            _dbContext = new DB();
        }

        private IRepository<Users> _userRepository;
        public IRepository<Users> UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dbContext);
                return _userRepository;
            }
        }

        private IRepository<Orders> _ordersRepository;
        public IRepository<Orders> OrdersRepository
        {
            get
            {
                if (_ordersRepository == null)
                    _ordersRepository = new OrdersRepository(_dbContext);
                return _ordersRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }



}