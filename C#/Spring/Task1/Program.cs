namespace Task1
{
    class SameDishException : Exception
    {
        public SameDishException(string str) : base(str)
        {

        }
    }
    class Dish
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Num { get; set; }
        public bool Equals(Dish dish)
        {
            if(dish.Name == Name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return $"{Name} {Price} {Num}";
        }
    }
    class Menu
    {
        public List<Dish> dishes = new List<Dish>();
        public event Action Revision;
        public Menu(params Dish[] dishes)
        {
            this.dishes = new List<Dish>(dishes);
        }
        public void Add(Dish dish)
        {
            for(int i = 0; i < dishes.Count; i++)
            {
                if (dishes[i].Name == dish.Name)
                {
                    throw new SameDishException("Current dish already exists");
                }
            }
            dishes.Add(dish);
        }
        public void Show()
        {
            foreach(Dish dish in dishes)
            {
                Console.WriteLine(dish);
            }
        }
        public void RemooveNotCosher()
        {
            float lowestPrice = float.MaxValue;
            int lowestIndex = -1;
            for(int i = 0; i < dishes.Count; i++)
            {
                Dish dish = dishes[i];
                if (dish.Price < lowestPrice)
                {
                    lowestIndex = i;
                    lowestPrice = dish.Price;
                }
            }
            if(lowestIndex != -1)
            {
                dishes.RemoveAt(lowestIndex);
            }
        }
        public void RemooveLast()
        {
            if(dishes.Count > 0)
            {
                dishes.RemoveAt(dishes.Count - 1);

            }
        }
        public void Invoke()
        {
            Revision?.Invoke();
        }
    }
    static class Extensions
    {
        public static (int, float) Sum(this Menu menu)
        {
            return (menu.dishes.Count, menu.dishes.Sum(dish => dish.Price));
        }
        public static void WriteInFile(this Menu menu)
        {
            using (StreamWriter streamWriter = new(@"..\..\..\Text.txt"))
                foreach(Dish dish in menu.dishes)
                {
                    streamWriter.WriteLine(dish);
                }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //1
            Dish Cutlet = new Dish { Name = "Cutlet", Price = 2, Num = 15 };
            Dish Cutlet2 = new Dish { Name = "Cutlet", Price = 2, Num = 15 };
            Menu menu = new(new Dish { Name = "Puree", Price = 1.5f, Num = 50}, new Dish { Name = "Soap", Price = 5, Num = 20 }, new Dish { Name = "Salad", Price = 3, Num = 5 });
            try
            {
                menu.Add(Cutlet);
                menu.Add(Cutlet2);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();
            menu.Show();
            //2
            menu.RemooveNotCosher();
            Console.WriteLine("\nУбрано самое дешёвое блюдо:");
            menu.Show();
            menu.RemooveLast();
            Console.WriteLine("\nУбрано последнее блюдо:");
            menu.Show();
            //3
            var sum = menu.Sum();
            Console.WriteLine($"\n{sum.Item1} {sum.Item2}");
            menu.WriteInFile();
            //4
            Console.WriteLine($"\n{menu.dishes.Where(dish => dish.Price * dish.Num == menu.dishes.Max(dish => dish.Price * dish.Num)).ToArray()[0].Name}");
            //5
            menu.Revision += menu.RemooveNotCosher;
            Console.WriteLine();
            menu.Show();
            menu.Invoke();
            Console.WriteLine();
            menu.Show();
    }
}
}