using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Projekt
{
    internal class Program
    {
        static void Task1()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Первое задание:\n\n");
            Console.ForegroundColor = ConsoleColor.White;

            bool variableBool = true;
            Console.WriteLine(variableBool);
            byte variableByte = 255;
            Console.WriteLine(variableByte);
            sbyte variableSbyte = -128;
            Console.WriteLine(variableSbyte);
            char variableChar = 'f';
            Console.WriteLine(variableChar);
            decimal variableDecimal = 0.00000000001m;
            Console.WriteLine(variableDecimal);
            double variableDouble = 121245.023195242;
            Console.WriteLine(variableDouble);
            float variableFloat = 1302.1252453245f;
            Console.WriteLine(variableFloat);
            int variableInt = 124;
            Console.WriteLine(variableInt);
            uint variableUint = 4294967295u;
            Console.WriteLine(variableUint);
            nint varibleNint = 12312;
            Console.WriteLine(varibleNint);
            long variableLong = -1242346253453242314;
            Console.WriteLine(variableLong);
            ulong variableUlong = 12412352346345753566;
            Console.WriteLine(variableUlong);
            short variableShort = 12235;
            Console.WriteLine(variableShort);
            ushort variableUshort = 12234;
            Console.WriteLine(variableUshort);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n\nНажмите для продолжения");
            Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            int converted = 0;
            bool convertion = Convert.ToBoolean(converted);
            Console.WriteLine(convertion);
            Console.WriteLine(Convert.ToString(converted));
            Console.WriteLine("Введите число");
            string str = Console.ReadLine();
            int var1, var2 = 5, result;
            int.TryParse(str, out var1);
            result = var1 + var2;
            Console.WriteLine(result);

            int first = 123;
            double second = first;
            //first = second;
            first = (int)second;
            Console.WriteLine(first);
            double third = double.MaxValue;
            first = (int)third;
            Console.WriteLine("Ошибка:" + first);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n\nНажмите для продолжения");
            Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            int v = 12;
            object o = v;
            int v1 = (int)o;
            //var sadf = null;
            var sadf = 123;
            //sadf = "asdf";
            Console.WriteLine(sadf.GetType());
            int? nullable = null;
            Console.WriteLine(nullable.HasValue);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n\nНажмите для перехода к следующему заданию");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Task2()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.Write("Второе задание:\n\n");
            Console.ForegroundColor = ConsoleColor.White;

            string first= "asdf";
            string second = "ffsdfa";
            Console.WriteLine(first==second);
            Console.WriteLine(string.Compare(second, first));
            string third = first + second;
            Console.WriteLine(third);
            Console.WriteLine(third.LastIndexOf("df"));
            Console.WriteLine("До вставки: " + third);
            third = third.Insert(2, "ВСТАВЛЕНО");
            Console.WriteLine(third);
            Console.WriteLine("Удалено: " + third.Remove(2, "ВСТАВЛЕНО".Length));
            string polyndrom = "А роза упала на лапу Азора";
            string[] mass = polyndrom.Split(' ');
            foreach(string s in mass)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine($"Первое слово: {first}, второе слово: {second}");
            string empty = "";
            string nun = null;
            Console.WriteLine($"Пустая строка: {string.IsNullOrEmpty(empty)}\nВообще пустая строка жестб: {string.IsNullOrEmpty(nun)}");
            Console.WriteLine(nun?.Length);
            Console.WriteLine(nun ?? "Строки не существует");
            StringBuilder stringBuilder = new StringBuilder("Динамично изменяющаяся строка");
            stringBuilder.Insert(stringBuilder.Length / 2, "2");
            stringBuilder.Append("3");
            stringBuilder.Insert(0, "1");
            Console.WriteLine(stringBuilder.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n\nНажмите для перехода к следующему заданию");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Task3()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.Write("Третье задание:\n\n");
            Console.ForegroundColor = ConsoleColor.White;

            int[,] matrix = new int[4, 4];
            Random random = new Random();
            
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i,j] = random.Next(0,9);
                }
            }
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j] + "   ");
                }
                Console.Write("\n");
            }
            string[] strings = { "asdf", "ghjk", "l;'z", "xcvb" };
            foreach(string s in strings)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine(strings.Length + "\nВведите номер строки, которую хотите изменить");
            int index = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите строку, которую хотите включить в массив");
            strings[index] = Console.ReadLine();
            foreach(string s in strings)
            {
                Console.WriteLine(s);
            }
            Console.Write("\n");
            Console.WriteLine("Заполните массив восмью элементами:");
            int[][] ladder = new int[3][];
            ladder[0] = new int[2];
            ladder[1] = new int[3];
            ladder[2] = new int[3];
            for(int i = 0; i < ladder.Length; i++)
            {
                for( int j = 0; j < ladder[i].Length; j++)
                {
                    int.TryParse(Console.ReadLine(), out ladder[i][j]);
                }
            }
            Console.Write("\n");
            for (int i = 0; i < ladder.Length; i++)
            {
                for(int j = 0; j < ladder[i].Length; j++)
                {
                    Console.Write(ladder[i][j] + "  ");
                }
                Console.Write("\n");
            }

            var vars = new int[4];
            var str = "asdfasdf";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n\nНажмите для перехода к следующему заданию");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Task4()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.Write("Четвёртое задание:\n\n");
            Console.ForegroundColor = ConsoleColor.White;

            (int a, string b, char _, string _, ulong e) = (32, "qewr", 'f', "asdf", 18446744073709551615uL);
            Console.WriteLine($"{a}, {b}, {e}");
            var tuple = (f:980234,g: "asdf");
            Console.WriteLine(tuple.ToString() + " " + tuple.f + " " + tuple.g);
            //tuple == t1;
            var tpl = (980234, "asdf");
            Console.WriteLine(tuple == tpl);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n\nНажмите для перехода к следующему заданию");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Main(string[] args)
        {
            static (int, int, int, char) local(int[] mass, string str)
            {
                var tuple = (mass.Max(), mass.Min(), mass.Sum(), str[0]);
                return tuple;
            }
            static int? check()
            {
                checked
                {
                    try
                    {
                        int max = int.MaxValue;
                        max++;
                        return max;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            static int? uncheck()
            {
                unchecked
                {
                    int max = int.MaxValue;
                    max++;
                    return max;
                }
            }
            Task1();
            Task2();
            Task3();
            Task4();
            int[] mass = { 54, 3, 45 };
            Console.WriteLine(local(mass, ":adfasd"));
            Console.WriteLine(check());
            Console.WriteLine(uncheck());
        }
    }
}
