using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace artemiev_lab1
{
    internal class Matrix
    {    
        public Galois[,] value;
        public int length
        {
            get { return value.GetLength(0); }
        }

        public Matrix() 
        {
            this.value = new Galois[1, 1];
        }

        public Matrix(int size) {
            this.value = new Galois[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    this.value[i, j] = new Galois();  
        }

        public Matrix(Galois[,] value)
        {
            this.value = value;
        }

        public Matrix(Matrix matrix)
        {
            this.value = new Galois[matrix.length, matrix.length];
            for (int i = 0; i < matrix.length; i++)
                for (int j = 0; j < matrix.length; j++)
                    this.value[i, j] = matrix[i, j];
        }

        public void print()
        {
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < this.length; i++)
            {
                for (int j = 0; j < this.length; j++)
                    result += this[i, j].ToString() + "\t";
                result += "\n";
            }

            return result;
        }

        private static void getCofactor(Matrix A, Matrix temp, int p, int q, int n)
        {
            int i = 0, j = 0;

            for (int row = 0; row < A.length; row++)
            {
                for (int col = 0; col < A.length; col++)
                {
                    if (row != p && col != q)
                    {
                        temp[i, j++] = A[row, col];

                        if (j == n - 1)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }
        }

        static void adjoint(Matrix A, Matrix adj)
        {
            int size = A.length;

            if (size == 1)
            {
                adj[0, 0] = new Galois(1);
                return;
            }

            // temp is used to store cofactors of [,]A
            bool sign = true;
            Matrix temp = new Matrix(size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // Get cofactor of A[i,j]
                    getCofactor(A, temp, i, j, size);

                    // sign of adj[j,i] positive if sum of row
                    // and column indexes is even.
                    sign = (i + j) % 2 == 0;

                    // Interchanging rows and columns to get the
                    // transpose of the cofactor matrix
                    if (sign)
                        adj[j, i] = getDeterminant(temp, size - 1);
                    else
                        adj[j, i] = (getDeterminant(temp, size - 1)).getOppositeValue();
                }
            }
        }

        public static Matrix inverse(Matrix A)
        {
            int size = A.length;

            Matrix inverseMatrix = new Matrix(size);

            // Find determinant of [,]A
            Galois det = getDeterminant(A, size);
            if (det.value == 0)
            {
                Console.Write("Обратной матрицы не существует");
                return null;
            }

            // Find adjoint
            Matrix adj = new Matrix(size);
            adjoint(A, adj);

            // Find Inverse using formula "inverse(A) = adj(A)/det(A)"
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    inverseMatrix[i, j] = adj[i, j] / det;

            return inverseMatrix;
        }

        public static Galois getDeterminant(Matrix matrix, int matrixLength = 0)
        {
            Galois[] results = CrossProduct(matrix, matrixLength);

            Galois sum = new Galois();
            foreach (Galois gal in results)
                sum += gal;

            return sum;
        }

        private static Galois[] CrossProduct(Matrix matrix, int matrixLength = 0)
        {
            int size = (matrixLength == 0 ? matrix.length : matrixLength);

            if (size == 0)
                return new Galois[] { new Galois(1) };

            if (size == 1)
                return new Galois[] { new Galois(matrix[0, 0]) };

            if (size == 2)
                return new Galois[] { (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]) };

            Galois[] results = new Galois[size];

            for (int col = 0; col < size; col++)
            {
                // var checkerboardFactor = col % 2.0 == 0 ? 1 : -1;
                bool checkerboardFactor = col % 2.0 == 0;
                Galois coeffecient = matrix[0, col];

                Matrix crossMatrix = GetCrossMatrix(matrix, 0, col);
                if (checkerboardFactor)
                    results[col] = coeffecient * getDeterminant(crossMatrix);
                else
                    results[col] = (coeffecient * getDeterminant(crossMatrix)).getOppositeValue();
            }

            return results;
        }

        private static Matrix GetCrossMatrix(Matrix matrix, int skipRow, int skipCol)
        {
            int size = matrix.length;

            Galois[,] output = new Galois[size - 1, size - 1];
            int outputRow = 0;

            for (int row = 0; row < size; row++)
            {
                if (row == skipRow)
                    continue;

                int outputCol = 0;

                for (int col = 0; col < size; col++)
                {
                    if (col == skipCol)
                        continue;

                    output[outputRow, outputCol] = matrix[row, col];

                    outputCol++;
                }
                outputRow++;
            }

            return new Matrix(output);
        }

        // Сложение матриц
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.length != matrix2.length)
                throw new Exception("Разная размерность матриц");

            int size = matrix1.length;
            Galois[,] newValue = new Galois[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    newValue[i, j] = matrix1[i, j] + matrix2[i, j];

            return new Matrix(newValue);
        }

        // Умножение матриц
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.length != matrix2.length)
                throw new Exception("Разная размерность матриц");


            int size = matrix1.length;
            Galois[,] newValue = new Galois[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    newValue[i, j] = new Galois();
                    for (int k = 0; k < size; k++)
                        newValue[i, j] += matrix1[i, k] * matrix2[k, j];
                }
            }

            return new Matrix(newValue);
        }

        public static Matrix operator *(Matrix matrix, Galois gal)
        {
  
            int size = matrix.length;
            Galois[,] newValue = new Galois[size, size];

            for (int i = 0; i < size; i++)         
                for (int j = 0; j < size; j++)
                        newValue[i, j] = matrix[i, j] * gal;

            return new Matrix(newValue);
        }

        public static Matrix operator *(Galois gal, Matrix matrix)
        {

            int size = matrix.length;
            Galois[,] newValue = new Galois[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    newValue[i, j] = matrix[i, j] * gal;

            return new Matrix(newValue);
        }

        public Galois this[int row, int column]
        {
            get
            {
                return this.value[row, column];
            }

            set
            {
                this.value[row, column] = value;
            }
        }
    }
}
