using System.Diagnostics;

namespace Lab6
{
    class MyException : Exception
    {
        public int Value;
        public MyException(string str, int val) : base(str)
        {
            Value = val;
        }
    }
    class ZeroException : DivideByZeroException
    {
        public int Value { get; }
        public ZeroException(int value) : base()
        {
            Value = value;
        }
    }
    class InvalidCast : InvalidCastException
    {
        public InvalidCast()
        {
            Console.WriteLine("Создана ошибка... Но зачем?");
        }
    }
    internal class Program
    {
        static void Level1()
        {
            try
            {
                Level2();
                int x = 10;
                int y = 0;
                if (y != 0)
                {
                    Console.WriteLine(x/y);
                }
                else
                {
                    throw new ZeroException(x);
                }
            }
            catch(ZeroException e)
            {
                Console.WriteLine($"Я вам запрещаю делить {e.Value} на 0");
            }
        }
        static void Level2()
        {
            try
            {
                object x = 1;
                if(x is string)
                {
                    Console.WriteLine("Успех");
                }
                else
                {
                    throw new InvalidCast();
                }
            }
            finally
            {
                Console.WriteLine("Сейчас начнётся...\n");
            }
        }
        static void Main(string[] args)
        {
            try
            {
                Level1();
                throw new MyException("Накладочка вышла...", 12);
            }
            catch(MyException ex) when (true)
            {
                Console.WriteLine($"Оппа {ex.Message} {ex.Value} ");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Сшибая всех на ходу, ошибка поднялась на несколько уровней вверх и попала в этот catch. Информация о ней:");
                Console.WriteLine($"Место: {ex.StackTrace};\nДиагностика:{ex.HResult}; Причина: {ex.Message}\n");
                int[] ints = null;
                try
                {
                    Console.WriteLine(ints[2]);
                }
                catch(NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    try
                    {
                        ints = new int[2];
                        Console.WriteLine(ints[2]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибка была проброшена куда подальше, то есть сюда");
                }
            }
            finally
            {
                Console.WriteLine("Все молодцы");
            }
            Debug.Assert(false, "Расходимся");
        }
    }
}