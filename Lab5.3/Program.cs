namespace Lab5._3
{
    internal class Program
    {
        private const int MatrixSize = 20;
        private static int[,] matrixA = new int[MatrixSize, MatrixSize];
        private static int[,] matrixB = new int[MatrixSize, MatrixSize];
        private static int[,] resultMatrix = new int[MatrixSize, MatrixSize];
        private static Semaphore semaphore = new Semaphore(2, 2);
        static void Main(string[] args)
        {
            Random random = new Random();

            Console.WriteLine("Матриця A:");
            FillMatrix(matrixA, random);
            PrintMatrix(matrixA);

            Console.WriteLine("Матриця B:");
            FillMatrix(matrixB, random);
            PrintMatrix(matrixB);

            MultiplyMatricesParallel();

            Console.WriteLine("Результуюча матриця:");
            PrintMatrix(resultMatrix);
        }
        static void FillMatrix(int[,] matrix, Random random)
        {
            for (int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    matrix[i, j] = random.Next(1, 10);
                }
            }
        }

        static void MultiplyMatricesParallel()
        {
            for (int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    Thread thread = new Thread(() => MultiplyCell(i, j));
                    thread.Start();
                    thread.Join();
                }
            }
        }

        static void MultiplyCell(int row, int col)
        {
            int cellSum = 0;

            for (int k = 0; k < MatrixSize; k++)
            {
                cellSum += matrixA[row, k] * matrixB[k, col];
            }

            lock (resultMatrix) 
            {
                semaphore.WaitOne(); 

                resultMatrix[row, col] = cellSum;

                semaphore.Release(); 
            }
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}