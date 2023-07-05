namespace Name
{
    internal class Researched : IComparable<Researched>
    {
        public int i;
        int j;
        string[] a;
        string str => "aboba";
        string prop { get; set; }
        public Researched()
        {
            i = 25;
        }
        public static void Method1(string str, int i)
        {
            Console.WriteLine("asdf");
        }
        public static bool Method2(string str, int i)
        {
            Random r = new Random();
            return Convert.ToBoolean(r.Next(0, 2));
        }
        public int CompareTo(Researched researched)
        {
            if(i == researched.i)
            {
                return 0;
            }
            return 1;
        }
    }
}
