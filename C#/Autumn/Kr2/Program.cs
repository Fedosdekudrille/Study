namespace Kr2
{
    class SuperQueue<T>
    {
        public Queue<T> queue;
        public SuperQueue() {
            queue = new Queue<T>();
        }
        public void Enqueue(T item)
        {
            if (queue.Count == 3)
                throw new Exception("Очередь может содержать не более 3 элементов");
            queue.Enqueue(item);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SuperQueue<DateTime> queue = new();
            try
            {
                for(int i = 0; i < 5; i++) 
                {
                    queue.Enqueue(new DateTime(i * 1000, i * 3, i * 7));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}