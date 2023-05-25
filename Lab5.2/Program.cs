namespace Lab5._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 84496, 423, 888, 664, 5, 78, 0, 78636, 25565 };

            Console.WriteLine("Початковий масив:");
            PrintArray(array);

            QuickSort(array);

            Console.WriteLine("Відсортований масив:");
            PrintArray(array);
        }
        static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }
        static void QuickSort(int[] array, int left, int right)
        {
                int pivotIndex = Partition(array, left, right);

                Thread leftThread = new Thread(() => QuickSort(array, left, pivotIndex - 1));
                Thread rightThread = new Thread(() => QuickSort(array, pivotIndex + 1, right));

                leftThread.Start();
                rightThread.Start();

                leftThread.Join();
                rightThread.Join();
        }
        static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    Swap(array, i, j);
                }
            }

            Swap(array, i + 1, right);

            return i + 1;
        }
        static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
        static void PrintArray(int[] array)
        {
            foreach (int num in array)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }
    }
}