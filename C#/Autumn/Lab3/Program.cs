using System;
using System.Runtime.InteropServices;
static class myExtensions
{
    public static void Coma(this string[] strings)
    {
        for (int i = 0; i < strings.Length; i++)
        {
            strings[i] = strings[i] + ",";
        }
    }
    static public void DelRepeat(this Set set)
    {
        string[] strings = set.strings;
        int length = set.strings.Length;
        for (int i = 0; i < length; i++)
        {
            for (int j = i + 1; j < length; j++)
            {
                if (!Convert.ToBoolean(string.Compare(strings[i], strings[j])))
                {
                    length--;
                    for (int k = j + 1; k < length; k++)
                    {
                        strings[k - 1] = strings[k];
                    }
                }
            }
        }
        set.strings = new string[length];
        for (int i = 0; i < length; i++)
        {
            set.strings[i] = strings[i];
        }
    }
}
class Set
{
    public string[] strings = { "one", "two" };
    private static (int low, int high) diapason =  (1, 10);
    public static Set operator +(Set set, string str){
        Set add = new()
        {
            strings = new string[set.strings.Length + 1]
        };
        for (int i = 0; i < set.strings.Length; i++)
        {
            add.strings[i] = set.strings[i];
        }
        add.strings[add.strings.Length - 1] = str;
        return add;
    }
    public static Set operator *(Set first, Set second)
    {
        string[] strings = new string[first.strings.Length + second.strings.Length];
        int index = 0;
        for (int i = 0; i < first.strings.Length; i++)
        {
            for (int k = 0; k < second.strings.Length; k++)
            {
                if (!Convert.ToBoolean(string.Compare(first.strings[i], second.strings[k])))
                {
                    strings[index] = first.strings[i];
                    index++;
                }
            }
        }
        Set union = new()
        {
            strings = new string[index]
        };
        for(int i = 0; i < index; i++)
        {
            union.strings[i] = strings[i];
        }
        return union;
    }
    public static explicit operator int(Set set)
    {
        return set.strings.Length;
    }
    public static bool operator false(Set set)
    {
        if (set.strings.Length < diapason.low || set.strings.Length > diapason.high)
            return false;
        else
            return true;
    }
    public static bool operator true(Set set)
    {
        if (set.strings.Length > diapason.low || set.strings.Length < diapason.high)
            return true;
        else
            return false;
    }
    public void Show()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nСтроки в классе:");
        Console.ForegroundColor = ConsoleColor.White;
        foreach(string s in strings)
        {
            Console.WriteLine(s);
        }
    }

    public class Production
    {
        public int id;
        public string name;
    }
    public class Developer
    {
        public string name;
        public int id;
        public string department;
    }
    Production production;
    Developer developer;
    public static class StaticOperation
    {
        static public Set Sum(Set set, string str)
        {
            return set + str;
        }
        static public Set Union(Set first, Set second)
        {
            return first * second;
        }
        static public int Length(Set set)
        {
            return (int)set;
        }
    }
}
class Programm
{
    static void Test()
    {
        try { 
            Set first = new();
            Set second = new();
            second += "three";
            Set result = first * second;
            result.Show();
            first += "three";
            result = first * second;
            result.Show();
            Console.WriteLine("\n" + (int)result);
            Console.WriteLine();
            if (result)
            {
                Console.WriteLine("Количество элементов находится в заданном диапазоне");
            }
            else
            {
                Console.WriteLine("Количество элементов выходит за рамки заданного диапазона");
            }
            result += "three";
            result.strings.Coma();
            result.Show();
            result.DelRepeat();
            result.Show();
        }
        catch (Exception)
        {
            Console.WriteLine("Возникли ошибки");
        }
    }
    static void Main(string[] args)
    {
        Test();
    }
}