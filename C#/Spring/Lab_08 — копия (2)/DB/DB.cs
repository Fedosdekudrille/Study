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
using System.Collections.ObjectModel;

namespace Lab_8.DB
{
    public class DB : DbContext
    {
        public DB() : base("name=Test")
        {
        }

        public DbSet<Class.Doctor> Users { get; set; }
        public DbSet<Class.Talons> Orders { get; set; }

        public IRepository<Class.Doctor> UserRepository => new UserRepository(this);
        public IRepository<Class.Talons> OrdersRepository => new OrdersRepository(this);
    }



    public interface IRepository<T> where T : class
    {

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        T Find(object val);
        ObservableCollection<T> Search(object name);

    }

    public class UserRepository : IRepository<Class.Doctor>
    {
        private readonly DB _dbContext;

        public UserRepository(DB dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Class.Doctor> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public async Task<IEnumerable<Class.Doctor>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public void Add(Class.Doctor user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void Update(Class.Doctor user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remove(Class.Doctor user)
        {
            try
            {

                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ObservableCollection<Doctor> Search(object name)
        {
            return new ObservableCollection<Doctor>(_dbContext.Users.Where(a => a.Name.Contains((string)name)).ToList());
        }
        public Class.Doctor Find(object id)
        {
            return _dbContext.Users.Find(id);
        }
    }

    public class OrdersRepository : IRepository<Class.Talons>
    {
        private readonly DB _dbContext;

        public OrdersRepository(DB dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Class.Talons> GetAll()
        {
            return _dbContext.Orders.ToList();
        }

        public async Task<IEnumerable<Class.Talons>> GetAllAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public void Add(Class.Talons orders)
        {
            _dbContext.Orders.Add(orders);
            _dbContext.SaveChanges();
        }

        public void Update(Class.Talons orders)
        {
            _dbContext.Entry(orders).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remove(Class.Talons orders)
        {
            _dbContext.Orders.Remove(orders);
            _dbContext.SaveChanges();
        }

        public Class.Talons Find(object id)
        {
            return _dbContext.Orders.Find(id);
        }
        public ObservableCollection<Talons> Search(object name)
        {
            return new ObservableCollection<Talons>(_dbContext.Orders.Where(a => a.ClientName.Contains((string)name)).ToList());
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

        private IRepository<Class.Doctor> _userRepository;
        public IRepository<Class.Doctor> UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dbContext);
                return _userRepository;
            }
        }

        private IRepository<Class.Talons> _ordersRepository;
        public IRepository<Class.Talons> OrdersRepository
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