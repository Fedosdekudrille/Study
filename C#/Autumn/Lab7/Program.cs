using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Lab7_From4;

namespace Lab7
{
    interface IGeneral<T>
    {
        public abstract void Add(T item);
        public abstract void Delete(T item);
        public abstract void Watch();
    }
    class CollectionType<T> : IGeneral<T>
    {
        public List<T> list { get; set; }
        public void Add(T item)
        {
            if(list != null)
                list = new List<T>(list) { item };
            else
                list = new List<T>() { item };
        }
        public void Delete(T item)
        {
            list.Remove(item);
        }
        public void Delete(int index)
        {
            try
            {
                list.RemoveAt(index);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Операция завершена");
            }
        }
        public void Watch()
        {
            for(int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            CollectionType<int> ints = new();
            for(int i = 0; i < 10; i++)
            {
                ints.Add(i);
            }
            ints.Delete(3);
            ints.Watch();

            CollectionType<char> strings = new();
            for (char i = 'A'; i < 'F'; i++)
            {
                strings.Add(i);
            }
            strings.Delete(3);
            strings.Watch();

            CollectionType<Human> humans = new();
            for (int i = 0; i < 5; i++)
            {
                Human human = new();
                humans.Add(human);
            }
            humans.Delete(3);
            humans.Watch();
            using (FileStream fstream = new(@"C:\Study\C#\Lab7\File.bin", FileMode.Create))
            {
                string strWrite = "";
                for(int i = 0; i < ints.list.Count; i++)
                {
                    strWrite += ints.list[i] + ",";
                }                   
                byte[] buffer = Encoding.Default.GetBytes(strWrite);
                fstream.WriteAsync(buffer, 0, buffer.Length);
            }
            using (FileStream fstream = File.OpenRead(@"C:\Study\C#\Lab7\File.bin"))
            {
                byte[] buffer = new byte[fstream.Length];
                fstream.ReadAsync(buffer, 0, buffer.Length);
                string getFile = Encoding.Default.GetString(buffer);
                CollectionType<int> getInt = new();
                string str = "";
                for(int i = 0; i < getFile.Length; i++)
                {
                    if (getFile[i] == ',')
                    {
                        getInt.Add(int.Parse(str));
                        str = "";
                    }
                    else
                    {
                        str += getFile[i];
                    }
                }
                getInt.Watch();
            }
        }
    }
}