using artemiev_lab1;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Введите характеристику поля \"p\" (простое число):");

            //int p = Convert.ToInt32(Console.ReadLine());

            //Console.WriteLine("Введите размер матрицы:");

            //int size = Convert.ToInt32(Console.ReadLine());
            //Galois.p = p;

            Galois.p = 5;


            Galois[,] galMatr1 = new Galois[3, 3] { 
                { new Galois(3), new Galois(2), new Galois(2) }, 
                { new Galois(1), new Galois(1) , new Galois(3) }, 
                { new Galois(0), new Galois(4), new Galois(4) } 
            };
            Galois[,] galMatr2 = new Galois[3, 3] { 
                { new Galois(4), new Galois(3) , new Galois(1) },
                { new Galois(4), new Galois(2), new Galois(0) }, 
                { new Galois(2), new Galois(2), new Galois(2) } 
            };

            Console.WriteLine("Матрица 1:");
            Matrix matrix1 = new Matrix(galMatr1);
            matrix1.print();

            Console.WriteLine("Матрица 2:");
            Matrix matrix2 = new Matrix(galMatr2);        
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
            Matrix inverseMatrix = Matrix.inverse(matrix1);
            inverseMatrix.print();
        }
    }
}