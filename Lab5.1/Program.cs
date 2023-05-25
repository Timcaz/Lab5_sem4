namespace Lab5._1
{
    internal class Program
    {
        private static object trafficLightsLock = new object();
        private static Semaphore semaphore = new Semaphore(2, 2);
        static void Main(string[] args)
        {
            Thread northThread = new Thread(TrafficLightMethod);
            Thread southThread = new Thread(TrafficLightMethod);
            Thread eastThread = new Thread(TrafficLightMethod);
            Thread westThread = new Thread(TrafficLightMethod);

            northThread.Start("Північний світлофор");
            southThread.Start("Південний світлофор");
            eastThread.Start("Східний світлофор");
            westThread.Start("Західний світлофор");

            northThread.Join();
            southThread.Join();
            eastThread.Join();
            westThread.Join();
        }
        static void TrafficLightMethod(object state)
        {
            string trafficLightName = (string)state;

            while (true)
            {
                Thread.Sleep(2000);

                Console.WriteLine($"{trafficLightName} жовтий.");

                lock (trafficLightsLock) 
                {
                    Console.WriteLine($"{trafficLightName} зелений.");

                    semaphore.WaitOne();

                    Console.WriteLine($"{trafficLightName} дозволяє проїхати автомобілям.");

                    Thread.Sleep(3000);

                    Console.WriteLine($"{trafficLightName} червоний.");

                    semaphore.Release();
                }
            }
        }
    }
}