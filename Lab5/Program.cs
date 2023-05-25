namespace Lab5
{
    internal class Program
    {
        private static Queue<int> queue = new Queue<int>();
        private static readonly object lockObject = new object();
        static void Main(string[] args)
        {
            Thread producerThread = new Thread(ProducerMethod);
            producerThread.Start();

            Thread consumerThread = new Thread(ConsumerMethod);
            consumerThread.Start();

            producerThread.Join();
            consumerThread.Join();
        }
        static void ProducerMethod()
        {
            Random random = new Random();

            while (true)
            {
                int number = random.Next(1, 100);

                lock (lockObject) 
                {
                    queue.Enqueue(number);
                    Console.WriteLine($"Виробник додав число: {number}");
                }

                Thread.Sleep(random.Next(500, 2000));
            }
        }

        static void ConsumerMethod()
        {
            while (true)
            {
                int number;

                lock (lockObject)
                {
                    if (queue.Count > 0)
                    {
                        number = queue.Dequeue();
                        Console.WriteLine($"Споживач спожив число: {number}");
                    }
                    else
                    {
                        number = -1;
                    }
                }

                if (number == -1)
                {
                    Thread.Sleep(100); 
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}