using artemiev_lab1;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {        
            int p = 0;
            while (p < 2)
            {
                Console.WriteLine("Введите характеристику поля \"p\" (простое число) >= 2:");
                p = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Введите размер матриц:");

            int size = Convert.ToInt32(Console.ReadLine());

            Galois.p = p;

            Console.WriteLine("Заполнение матрицы 1:");

            Matrix matrix1 = inputMatrix(size);

            Console.WriteLine("Заполнение матрицы 2:");
            Matrix matrix2 = inputMatrix(size);

            Console.WriteLine("Матрица 1:");         
            matrix1.print();

            Console.WriteLine("Матрица 2:");       
            matrix2.print();

            Console.WriteLine("Сложение матриц:");
            Matrix matrixSum = matrix1 + matrix2;
            matrixSum.print();

            Console.WriteLine("Умножение матриц:");
            Matrix matrixMult = matrix1 * matrix2;
            matrixMult.print();

            Galois gal = new Galois(3);
            Console.WriteLine("Умножение матрицы 1 на элемент поля:");
            Matrix matrixMultOnGal = matrix1 * gal;
            matrixMultOnGal.print();

            Console.WriteLine("Определитель матрицы 1:");
            Galois determinant1 = Matrix.getDeterminant(matrix1);
            Console.WriteLine(determinant1.ToString());
            Console.WriteLine();

            Console.WriteLine("Определитель матрицы 2:");
            Galois determinant2 = Matrix.getDeterminant(matrix2);
            Console.WriteLine(determinant2.ToString());
            Console.WriteLine();

            Console.WriteLine("Обратная матрица матрице 1:");
            Matrix inverseMatrix1 = Matrix.inverse(matrix1);
            if (inverseMatrix1 != null)
                inverseMatrix1.print();
            else
            {
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine("Обратная матрица матрице 2:");
            Matrix inverseMatrix2 = Matrix.inverse(matrix2);
            if (inverseMatrix2 != null)
                inverseMatrix2.print();
        }

        static Matrix inputMatrix(int size)
        {
            Galois[,] galMatrix = new Galois[size, size];
            for (int i = 0; i < size; i++)
            {
                bool continueRead = true;
                int[] line = new int[0];
                while (continueRead)
                {
                    Console.WriteLine($"Заполнение строки {i + 1}/{size} целыми числами через пробел в промежутке [0; {Galois.p - 1}]");

                    line = Console.ReadLine().ToString().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(i =>
                    {
                        if (int.Parse(i) >= Galois.p)
                            throw new Exception("Число не входим поле Галуа");
                        return int.Parse(i);
                    }).ToArray<int>(); ;

                    if (line.Length == size)
                        continueRead = false;
                }
                for (int j = 0; j < size; j++)
                {
                    galMatrix[i, j] = new Galois(line[j]);
                }
            }

            return new Matrix(galMatrix);
        }
    }
}