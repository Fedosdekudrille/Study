using System.Text;

namespace ExamV
{
    class EmptyStringException : Exception
    {
        public EmptyStringException() : base("login или password пусты"){ }
    }
    class ShortStringException : Exception
    {
        public ShortStringException() : base("Строка слишком коротка") { }
    }
    class LongStringException : Exception
    {
        public LongStringException() : base("Строка слишком длинна") { }
    }
    class MyString
    {
        public StringBuilder StrBuff { get; set; }
        public MyString() 
        {
            StrBuff= new StringBuilder();
        }
        public override string ToString()
        {
            return StrBuff.ToString();
        }
    }
    class MyTextBox : MyString 
    {
        public string Color { get; set; }
        public int Size { get; set; }
    }
    interface IValidable
    {
        public bool Validate();
    }
    class LoginForm : IValidable
    {
        public MyTextBox login;
        public MyTextBox password;
        public bool Validate()
        {
            if (password == null || login == null)
            {
                throw new EmptyStringException();
            }
            else if(password.ToString().Length < 6)
            {
                throw new ShortStringException();
            }
            else if(password.ToString().Length > 12)
            {
                throw new LongStringException();
            }
            if(!string.Equals(login.ToString(), password.ToString())) 
            {
                return true;
            }
            return false;
        }
    }
    class User
    {
        public event Action? Enter;
        public void LogIn()
        {
            Enter?.Invoke();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.login = new MyTextBox() { Color = "Yellow", Size = 5, StrBuff = new StringBuilder("log") };
            loginForm.password = new MyTextBox() { Color = "Red", Size = 4, StrBuff = new StringBuilder("pass") };
            Console.WriteLine(loginForm.login.ToString());
            try
            {
                Console.WriteLine(loginForm.Validate());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            loginForm.password.StrBuff.Clear();
            loginForm.password.StrBuff.Append("password123");
            Console.WriteLine(loginForm.Validate());

            LoginForm loginForm2 = new LoginForm();
            loginForm2.login = new MyTextBox() { Color = "Yellow", Size = 2, StrBuff = new StringBuilder("asdfasdfasdf") };
            loginForm2.password = new MyTextBox() { Color = "Red", Size = 2, StrBuff = new StringBuilder("asdfasdfasdf") };

            LoginForm loginForm3 = new LoginForm();
            loginForm3.login = new MyTextBox() { Color = "Yellow", Size = 2, StrBuff = new StringBuilder("log2") };
            loginForm3.password = new MyTextBox() { Color = "Red", Size = 2, StrBuff = new StringBuilder("password3") };
            User user = new User();
            user.Enter += () => { Console.WriteLine(loginForm2.Validate()); };
            user.Enter += () => { Console.WriteLine(loginForm3.Validate()); };
            user.LogIn();
        }
    }
}