using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTool
{
    public class Matrix
    {
        int rows;
        int columns;
        double[,] value;
        private Matrix()//这是一个快速创建什么字段都没有初始化的矩阵对象的方法，只允许内部使用！
        {

        }
        public Matrix(double num)
        {
            rows = 1;
            columns = 1;
            value = new double[1, 1] { {num} };
        }
        public Matrix(double[] num)
        {
            rows = 1;
            columns = num.GetLength(0);
            value = new double[1,columns];
            for (int i = 0; i < columns; i++)
            {
                value[0, i] = num[i];
            }
        }
        public Matrix(double[,] num)
        {
            rows = num.GetLength(0);
            columns = num.GetLength(1);
            value = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    value[i, j] = num[i, j];
        }
        public Matrix(Matrix inMatrix)
        {
            rows = inMatrix.rows;
            columns = inMatrix.columns;
            value = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    value[i, j] = inMatrix.value[i, j];
        }
        public int Rows
        {
            get
            {
                return rows;
            }
            set
            {
                rows = value;
            }
        }
        public int Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
            }
        }
        public double[,] Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        //一些特殊矩阵的生成方法，功能都类似MATLAB中对应函数
        public static Matrix Ones(int dimension)
        {
            Matrix result = new Matrix();
            result.rows = dimension;
            result.columns = dimension;
            result.value = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++)
                for (int j = 0; j < dimension; j++)
                    result.value[i, j] = 1;
            return result;
        }
        public static Matrix Ones(int row, int column)
        {
            Matrix result = new Matrix();
            result.rows = row;
            result.columns = column;
            result.value = new double[row, column];
            for (int i = 0; i < row; i++)
                for (int j = 0; j < column; j++)
                    result.value[i, j] = 1;
            return result;
        }
        public static Matrix Eye(int dimension)
        {
            Matrix result = new Matrix();
            result.rows = dimension;
            result.columns = dimension;
            result.value = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++)
                for (int j = 0; j < dimension; j++)
                {
                    if (i == j)
                        result.value[i, j] = 1;
                    else
                        result.value[i, j] = 0;
                }
            return result;
        }
        public static Matrix ElementarySwitch(int dimension, int i, int j)//互换初等矩阵,i、j行(列)互换
        {
            if (i > dimension)
                return null;
            if (j > dimension)
                return null;
            Matrix result = Matrix.Eye(dimension);
            if (i!=j)
            {
                result.value[i-1, i-1] = 0;
                result.value[i-1, j-1] = 1;
                result.value[j-1, i-1] = 1;
                result.value[j-1, j-1] = 0; 
            }
            return result;
        }
        public static Matrix ElementaryMultiple(int dimension, int i, double k)//倍乘初等矩阵，k倍i行(列)
        {
            if (i > dimension)
                return null;
            Matrix result = Matrix.Eye(dimension);
            result.value[i-1, i-1] = k * result.value[i-1, i-1];
            return result;
        }
        public static Matrix ElementaryMulAdd(int dimension, int i, double k, int j)//倍加初等矩阵,i行加k倍的j行(j列加k倍i列)
        {
            if (i > dimension)
                return null;
            if (j > dimension)
                return null;
            Matrix result = Matrix.Eye(dimension);
            if (i!=j)
            {
                result.value[i-1, j-1] = k; 
            }
            return result;
        }
        public static Matrix Zeros(int dimension)
        {
            Matrix result = new Matrix();
            result.rows = dimension;
            result.columns = dimension;
            result.value = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++)
                for (int j = 0; j < dimension; j++)
                    result.value[i, j] = 0;
            return result;
        }
        public static Matrix Zeros(int row, int column)
        {
            Matrix result = new Matrix();
            result.rows = row;
            result.columns = column;
            result.value = new double[row, column];
            for (int i = 0; i < row; i++)
                for (int j = 0; j < column; j++)
                    result.value[i, j] = 0;
            return result;
        }
        public static Matrix Random(int dimension)
        {
            Matrix result = new Matrix();
            result.rows = dimension;
            result.columns = dimension;
            result.value = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++)
                for (int j = 0; j < dimension; j++)
                    result.value[i, j] = Matrix.GetRandomNum();
            return result;
        }
        public static Matrix Random(int row, int column)
        {
            Matrix result = new Matrix();
            result.rows = row;
            result.columns = column;
            result.value = new double[row, column];
            for (int i = 0; i < row; i++)
                for (int j = 0; j < column; j++)
                    result.value[i, j] = Matrix.GetRandomNum();
            return result;
        }
        //这个方法是自定义的随机数产生方法，辅助作用，与这个类并无太大关系
        public static double GetRandomNum()
        {
            Random ran = new Random(Guid.NewGuid().GetHashCode());
            return ran.NextDouble();
        }
        //矩阵的其他方法
        public void DisplayInConsole()
        {
            for(int i=0;i<rows;i++)
                for (int j = 0; j < columns; j++)
                {
                    if (j == 0)
                        Console.Write("[");
                    Console.Write("{0}\t", value[i, j]);
                    if (j == columns - 1)
                        Console.WriteLine("]");
                }
        }
        public static Matrix Reverse(Matrix x)
        {
            if (x.rows != x.columns)
                return null;
            if (x.rows == 1)
                if (x.value[0, 0] != 0)
                {
                    return new Matrix(1 / x.value[0, 0]);
                }
                else
                    return null;
            double detVal=Matrix.Det(x);
            if (detVal == 0)
                return null;
            detVal = 1 / detVal;
            return Matrix.Transfer(Matrix.CompanionMatrix(x)) * detVal;
        }
        public static double Cofactor(Matrix x, int i, int j)//求逆的辅助函数，求余子式，注意判断行列数相同
        {
            double[,] array = new double[x.rows - 1, x.columns - 1];
            for (int m = 0; m < x.rows - 1; m++)
            {
                for (int n = 0; n < x.columns - 1; n++)
                {
                    int s = m, t = n;
                    if (s >= i)
                        s++;
                    if (t >= j)
                        t++;
                    array[m, n] = x.value[s, t];
                }
            }
            Matrix cofactor = new Matrix(array);
            return Matrix.Det(cofactor);
        }
        public static double AlgeCofactor(Matrix x, int i, int j)//代数余子式
        {
            return Math.Pow(-1, i + j) * Matrix.Cofactor(x, i, j);
        }
        public static Matrix CompanionMatrix(Matrix x)
        {
            double[,] array = new double[x.rows, x.columns];
            for (int i = 0; i < x.rows; i++)
            {
                for (int j = 0; j < x.columns; j++)
                {
                    array[i, j] = Matrix.AlgeCofactor(x, i, j);
                }
            }
            return new Matrix(array);
        }
        public static Matrix Transfer(Matrix x)
        {
            Matrix result = new Matrix();
            result.rows = x.columns;
            result.columns = x.rows;
            result.value = new double[result.rows, result.columns];
            for (int i = 0; i < result.rows; i++)
                for (int j = 0; j < result.columns; j++)
                    result.value[i, j] = x.value[j, i];
            return result;
        }
        public static double Det(Matrix x)//使用前一定要判断行数、列数是否相等！
        {
            if (x.rows == 1)
                return x.value[0, 0];
            if (x.rows == 2)
                return x.value[0, 0] * x.value[1, 1] - x.value[0, 1] * x.value[1, 0];
            else
            {
                Matrix temp = new Matrix(x);
                temp = Matrix.ToStepMatrix(temp);
                double leftup = temp.value[0, 0];
                temp = Matrix.SubMatrix(temp, 2, 2, true);
                return leftup * Matrix.Det(temp);
            }
        }
        public static Matrix SubMatrix(Matrix x, int i, int j, bool reverse=false)//reverse参数表示是否取矩阵后i行j列子矩阵
        {
            Matrix result = new Matrix(x);
            if (i > x.rows || j > x.columns)
                return null;
            else
            {
                if (!reverse)
                {
                    result.rows = i;
                    result.columns = j;
                    result.value = new double[i, j];
                    for (int m = 0; m < i; m++)
                        for (int n = 0; n < j; n++)
                            result.value[m, n] = x.value[m, n];
                }
                else
                {
                    result.rows = x.rows - i+1;
                    result.columns = x.columns - j + 1;
                    result.value = new double[result.rows, result.columns];
                    for (int m = 0; m < result.rows; m++)
                        for (int n = 0; n < result.columns; n++)
                            result.value[m, n] = x.value[m + i - 1, n + j - 1];
                }
            }
            return result;
        }
        public static Matrix ToStepMatrix(Matrix x)
        {
            Matrix result = new Matrix(x);
            if (result.rows > 1)
            {
                for (int i = 0; i < result.rows-1; i++)
                {
                    for (int j = i+1; j < result.rows; j++)
                    {
                        double mul = -result.value[j, i] / result.value[i, i];
                        for (int k = i; k < result.columns; k++)
                        {
                            result.value[j, k] = result.value[j, k] + mul * result.value[i, k];
                        }
                    }
                }
            }
            return result;
        }
        //矩阵的运算符重载
        public static Matrix operator +(Matrix x, Matrix y)
        {
            if (x.rows != y.rows || x.columns != y.columns)
                return null;
            Matrix result = new Matrix();
            result.rows = x.rows;
            result.columns = x.columns;
            result.value=new double[result.rows,result.columns];
            for (int i = 0; i < result.rows; i++)
                for (int j = 0; j < result.columns; j++)
                    result.value[i, j] = x.value[i, j] + y.value[i, j];
            return result;
        }
        public static Matrix operator +(Matrix x, double y)
        {
            return x + (y * Ones(x.rows, x.columns));
        }
        public static Matrix operator +(double y, Matrix x)
        {
            return x + y;
        }
        public static Matrix operator -(Matrix x, Matrix y)
        {
            if (x.rows != y.rows || x.columns != y.columns)
                return null;
            Matrix result = new Matrix();
            result.rows = x.rows;
            result.columns = x.columns;
            result.value = new double[result.rows, result.columns];
            for (int i = 0; i < result.rows; i++)
                for (int j = 0; j < result.columns; j++)
                    result.value[i, j] = x.value[i, j] - y.value[i, j];
            return result;
        }
        public static Matrix operator -(Matrix x, double y)
        {
            return x + (-y);
        }
        public static Matrix operator -(double y, Matrix x)
        {
            return -1*x + (y);
        }
        public static Matrix operator *(Matrix x, Matrix y)
        {
            if (x.columns != y.rows)
                return null;
            Matrix result = new Matrix();
            result.rows = x.rows;
            result.columns = y.columns;
            result.value = new double[result.rows, result.columns];
            for (int i = 0; i < result.rows; i++)
                for (int j = 0; j < result.columns; j++)
                {
                    result.value[i, j] = 0;
                    for (int k = 0; k < x.columns; k++)
                    {
                        result.value[i, j] += x.value[i, k] * y.value[k, j];
                    }
                }
            return result;
        }
        public static Matrix operator *(Matrix x, double y)
        {
            Matrix result=new Matrix();
            result.rows = x.rows;
            result.columns = x.columns;
            result.value = new double[result.rows, result.columns];
            for (int i = 0; i < result.rows; i++)
                for (int j = 0; j < result.columns; j++)
                    result.value[i, j] = x.value[i, j] * y;
            return result;
        }
        public static Matrix operator *(double y, Matrix x)
        {
            return x * y;
        }
        public static Matrix operator ++(Matrix x)
        {
            return x + (Ones(x.rows, x.columns));
        }
        public static Matrix operator --(Matrix x)
        {
            return x - (Ones(x.rows, x.columns));
        }
        public static Matrix operator %(Matrix x, Matrix y)
        {
            if (x.rows != y.rows)
                return null;
            if (x.columns != y.columns)
                return null;
            Matrix result = new Matrix();
            result.rows = x.rows;
            result.columns = x.columns;
            result.value = new double[result.rows, result.columns];
            for (int i = 0; i < result.rows; i++)
                for (int j = 0; j < result.columns; j++)
                    result.value[i, j] = x.value[i, j] % y.value[i, j];
            return result;
        }
        public static Matrix operator %(Matrix x, double y)
        {
            return x % (y * Ones(x.rows, x.columns));
        }
        public static bool operator ==(Matrix left, Matrix right)//默认left和right均不为null，使用时注意先判断null
        {
            if (left.rows != right.rows)
                return false;
            if (left.columns != right.columns)
                return false;
            for (int i = 0; i < left.rows; i++)
                for (int j = 0; j < left.columns; j++)
                {
                    if (left.value[i, j] != right.value[i, j])
                        return false;
                }
            return true;
        }
        public static bool operator !=(Matrix left, Matrix right)
        {
            return !(left == right);
        }
        //矩阵初等变换函数,以下6个
        public static Matrix RowSwitch(Matrix x, int i, int j)
        {
            if (i <= x.rows && j <= x.rows)
            {
                Matrix result = new Matrix(x);
                Matrix ele = Matrix.ElementarySwitch(x.rows, i, j);
                if (i != j)
                    result = ele * x;
                return result;
            }
            else
            {
                return null;
            }
        }
        public static Matrix ColumnSwitch(Matrix x, int i, int j)
        {
            if (i <= x.columns && j <= x.columns)
            {
                Matrix result = new Matrix(x);
                Matrix ele = Matrix.ElementarySwitch(x.columns, i, j);
                if (i != j)
                    result = x * ele;
                return result;
            }
            else
            {
                return null;
            }
        }
        public static Matrix RowMultiple(Matrix x, int i, double k)
        {
            if (i <= x.rows)
            {
                Matrix ele = Matrix.ElementaryMultiple(x.rows, i, k);
                Matrix result = ele * x;
                return result;
            }
            else
            {
                return null;
            }
        }
        public static Matrix ColumnMultiple(Matrix x, int i, double k)
        {
            if (i <= x.columns)
            {
                Matrix ele = Matrix.ElementaryMultiple(x.columns, i, k);
                Matrix result = x * ele;
                return result;
            }
            else
            {
                return null;
            }
        }
        public static Matrix RowMulAdd(Matrix x, int i, double k, int j)//i行加k倍的j行
        {
            if (i <= x.rows && j <= x.rows)
            {
                Matrix result = new Matrix(x);
                if (i != j)
                {
                    Matrix ele = Matrix.ElementaryMulAdd(x.rows, i, k, j);
                    result = ele * x;
                }
                return result;
            }
            else
            {
                return null;
            }
        }
        public static Matrix ColumnMulAdd(Matrix x, int i, double k, int j)//j列加k倍的i列
        {
            if (i <= x.columns && j <= x.columns)
            {
                Matrix result = new Matrix(x);
                if (i != j)
                {
                    Matrix ele = Matrix.ElementaryMulAdd(x.columns, i, k, j);
                    result = x * ele;
                }
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
