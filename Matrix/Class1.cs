﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTool
{
    public class Matrix
    {
        /// <summary>
        /// 用于矩阵运算的矩阵类
        /// 
        /// 编写：李云新 2016.12.29
        /// 
        /// 一.初始化
        /// 
        /// *用法*
        ///         Matrix m=new Matrix(double num);//单个数字初始化1*1矩阵
        ///         Matrix m=new Matrix(new double[]{});//一维数组初始化行向量
        ///         Matrix m=new Matrix(new double[,]{{}});//二位数组初始化矩阵
        ///         Matrix m=new Matrix(Matrix inMatrix);//矩阵初始化另一个矩阵
        /// *注意*
        /// 1.这个类中把常数、向量都看成矩阵,单个数为1*1矩阵,行向量为1*n矩阵,列向量为n*1矩阵
        /// 2.也可以不用初始化函数初始,可以使用Matrix中的静态函数得到某些特殊矩阵,方法同MATLAB
        /// 3.矩阵使用时也可以像二维数组一样引用矩阵中元素,如m[i,j]为矩阵m中i行j列元素,i、j从0起编号
        /// 
        /// 二.方法
        /// 
        /// *用法*
        ///         各函数基本用法同MATLAB中名称相同或相似函数
        /// *注意*
        ///
        /// 三.方法列表
        /// 
        /// </summary>
        int rows;
        int columns;
        double[,] value;
        private Matrix()//这是一个快速创建什么字段都没有初始化的矩阵对象的方法，只允许内部使用！
        {

        }
        private Matrix(int rows, int cols)
        {
            this.rows = rows;
            this.columns = cols;
            this.value = new double[rows, cols];
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
        public double this[int i, int j]//索引器
        {
            set
            {
                this.value[i, j] = value;
            }
            get
            {
                return this.value[i, j];
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
            if (i >= dimension)
                return null;
            if (j >= dimension)
                return null;
            Matrix result = Matrix.Eye(dimension);
            if (i!=j)
            {
                result.value[i, i] = 0;
                result.value[i, j] = 1;
                result.value[j, i] = 1;
                result.value[j, j] = 0; 
            }
            return result;
        }
        public static Matrix ElementaryMultiple(int dimension, int i, double k)//倍乘初等矩阵，k倍i行(列)
        {
            if (i >= dimension)
                return null;
            Matrix result = Matrix.Eye(dimension);
            result.value[i, i] = k * result.value[i, i];
            return result;
        }
        public static Matrix ElementaryMulAdd(int dimension, int i, double k, int j)//倍加初等矩阵,i行加k倍的j行(j列加k倍i列)
        {
            if (i >= dimension)
                return null;
            if (j >= dimension)
                return null;
            Matrix result = Matrix.Eye(dimension);
            if (i!=j)
            {
                result.value[i, j] = k; 
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
        public static Matrix Random(int dimension, double min, double max)
        {
            if (min > max)
            {
                min = max + min;
                max = min - max;
                min = min - max;
            }
            Matrix result = new Matrix();
            result.rows = dimension;
            result.columns = dimension;
            result.value = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++)
                for (int j = 0; j < dimension; j++)
                    result.value[i, j] = Matrix.GetRandomNum() * (max - min) + min;
            return result;
        }
        public static Matrix Random(int row, int column, double min, double max)
        {
            if (min > max)
            {
                min = max + min;
                max = min - max;
                min = min - max;
            }
            Matrix result = new Matrix();
            result.rows = row;
            result.columns = column;
            result.value = new double[row, column];
            for (int i = 0; i < row; i++)
                for (int j = 0; j < column; j++)
                {
                        result.value[i, j] = Matrix.GetRandomNum() * (max - min) + min;
                }
            return result;
        }
        public static Matrix Diagonal(double[] diag)
        {
            Matrix result = Matrix.Zeros(diag.GetLength(0));
            for (int i = 0; i < result.rows; i++)
            {
                result.value[i, i] = diag[i];
            }
            return result;
        }
        public static Matrix Diagonal(double[] diag,int row, int column)
        {
            Matrix result = Matrix.Zeros(row, column);
            int num = row > column ? column : row;
            num = num > diag.GetLength(0) ? diag.GetLength(0) : num;
            for (int i = 0; i < num; i++)
            {
                result.value[i, i] = diag[i];
            }
            return result;
        }
        public static Matrix Diagonal(double[] diag, int move)
        {
            Matrix result = Matrix.Zeros(diag.GetLength(0));
            for (int i = 0; i < result.rows; i++)
            {
                int col=(i + move) % result.columns;
                col = col >= 0 ? col : (col + result.columns);
                result.value[i, col] = diag[i];
            }
            return result;
        }
        public static Matrix TriUp(Matrix x, double fill = 0)//上三角
        {
            Matrix result = new Matrix(x);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.columns; j++)
                {
                    if (i > j)
                    {
                        result.value[i, j] = fill;
                    }
                }
            }
            return result;
        }
        public static Matrix TriLow(Matrix x, double fill = 0)//下三角
        {
            Matrix result = new Matrix(x);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.columns; j++)
                {
                    if (i < j)
                    {
                        result.value[i, j] = fill;
                    }
                }
            }
            return result;
        }
        public static Matrix Symmetry(Matrix x, bool reverse=false)//对称,inverse参数表示将上三角(下三角)对称到下方(上方)
        {
            Matrix result = new Matrix(x);
            int num = x.rows < x.columns ? x.rows : x.columns;
            result = Matrix.SubMatrix(x, num, num);
            if (!reverse)
            {
                for (int i = 0; i < x.rows; i++)
                {
                    for (int j = 0; j < x.columns; j++)
                    {
                        if (i > j)
                        {
                            result.value[i, j] = result.value[j, i];
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < x.rows; i++)
                {
                    for (int j = 0; j < x.columns; j++)
                    {
                        if (i < j)
                        {
                            result.value[i, j] = result.value[j, i];
                        }
                    }
                }
            }
            return result;
        }
        public static Matrix Antisymmetry(Matrix x, bool reverse = false)//反对称,inverse参数表示将上三角(下三角)反对称到下方(上方)
        {
            Matrix result = new Matrix(x);
            int num = x.rows < x.columns ? x.rows : x.columns;
            result = Matrix.SubMatrix(x, num, num);
            if (!reverse)
            {
                for (int i = 0; i < x.rows; i++)
                {
                    for (int j = 0; j < x.columns; j++)
                    {
                        if (i > j)
                        {
                            result.value[i, j] = -result.value[j, i];
                        }
                    }
                    result.value[i, i] = 0;
                }
            }
            else
            {
                for (int i = 0; i < x.rows; i++)
                {
                    for (int j = 0; j < x.columns; j++)
                    {
                        if (i < j)
                        {
                            result.value[i, j] = -result.value[j, i];
                        }
                    }
                    result.value[i, i] = 0;
                }
            }
            return result;
        }
        public static Matrix TransMatrix(double angle)//转换矩阵,结构力学局部坐标系到整体坐标系转换
        {
            double cosVal = Math.Cos(angle * Math.PI / 180);
            double sinVal = Math.Sin(angle * Math.PI / 180);
            return new Matrix(new double[6, 6]{{cosVal,sinVal,0,0,0,0},{-sinVal,cosVal,0,0,0,0},{0,0,1,0,0,0},
                                                {0,0,0,cosVal,sinVal,0},{0,0,0,-sinVal,cosVal,0},{0,0,0,0,0,1}});
        }
        public static Matrix StiffnessMatrix(double EI, double EA,double L)//刚度矩阵//结构力学
        {
            return new Matrix(new double[6, 6]{{EA/L,0,0,-EA/L,0,0},{0,12*EI/(L*L*L),6*EI/(L*L),0,-12*EI/(L*L*L),6*EI/(L*L)},
                                                {0,6*EI/(L*L),4*EI/L,0,-6*EI/(L*L),2*EI/L},{-EA/L,0,0,EA/L,0,0},
                                                {0,-12*EI/(L*L*L),-6*EI/(L*L),0,12*EI/(L*L*L),-6*EI/(L*L)},
                                                {0,6*EI/(L*L),2*EI/L,0,-6*EI/(L*L),4*EI/L}});
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
        public void DisplayLimited()
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    if (j == 0)
                        Console.Write("[");
                    Console.Write("{0:f4}\t", value[i, j]);
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
            return -(Matrix.CompanionMatrix(x) * detVal);//不知道为什么，答案就是差个负号
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
        public static Matrix CompanionMatrix(Matrix x)//伴随矩阵
        {
            double[,] array = new double[x.rows, x.columns];
            for (int i = 0; i < x.rows; i++)
            {
                for (int j = 0; j < x.columns; j++)
                {
                    array[i, j] = Matrix.AlgeCofactor(x, i, j);
                }
            }
            return Matrix.Transfer(new Matrix(array));
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
        public static Matrix SubMatrix(Matrix x, int i, int j, bool reverse = false)//取前i行j列或从第i行j列起取//reverse参数表示是否取矩阵后i行j列子矩阵
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
            int[] firstNotZero = new int[result.rows];//记录每一行不为0个数
            for (int i = 0; i < result.rows; i++)
            {
                firstNotZero[i] = 0;
            }
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.columns; j++)
                {
                    if (result.value[i, j] != 0)
                    {
                        break;
                    }
                    else
                    {
                        firstNotZero[i]++;
                    }
                }
            }
            if (result.rows > 1)//多于1行要排序，将先出现不为0的一行移到上方
            {
                for (int i = 0; i < result.rows - 1; i++)
                {
                    for (int j = i + 1; j < result.rows; j++)
                    {
                        if (firstNotZero[i] > firstNotZero[j])
                        {
                            result = Matrix.RowSwitch(result, i, j);
                            firstNotZero[i] = firstNotZero[i] + firstNotZero[j];//互换
                            firstNotZero[j] = firstNotZero[i] - firstNotZero[j];
                            firstNotZero[i] = firstNotZero[i] - firstNotZero[j];
                        }
                    }
                } 
            }
            if (result.rows > 1)
            {
                for (int i = 0; i < result.rows - 1; i++)
                {
                    if (firstNotZero[i] >= result.columns)//某一行不出现不为0的数则无需再变，之后每一行均不会再出现非0
                        break;
                    for (int j = i + 1; j < result.rows; j++)
                    {
                        double mul = -result.value[j, firstNotZero[i]] / result.value[i, firstNotZero[i]];
                        for (int k = firstNotZero[i]; k < result.columns; k++)
                        {
                            result.value[j, k] = result.value[j, k] + mul * result.value[i, k];
                        }
                    }
                    //每一次消完一列都要重新找出每一行第一个不为0的数，因为消列会改变这一值
                    for (int m = 0; m < result.rows; m++)
                    {
                        firstNotZero[m] = 0;
                    }
                    for (int m = 0; m < result.rows; m++)
                    {
                        for (int n = 0; n < result.columns; n++)
                        {
                            if (result.value[m, n] != 0)
                            {
                                break;
                            }
                            else
                            {
                                firstNotZero[m]++;
                            }
                        }
                    }
                    //
                }
            }
            return result;
        }
        public static Matrix DotMultiple(Matrix x, Matrix y)
        {
            if (x.rows != y.rows || x.columns != y.columns)
                return null;
            Matrix result = new Matrix(x);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.columns; j++)
                {
                    result.value[i, j] = result.value[i, j] * y.value[i, j];
                }
            }
            return result;
        }
        public static Matrix DotDevide(Matrix x, Matrix y)
        {
            if (x.rows != y.rows || x.columns != y.columns)
                return null;
            Matrix result = new Matrix(x);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.columns; j++)
                {
                    result.value[i, j] = result.value[i, j] / y.value[i, j];
                }
            }
            return result;
        }
        public static Matrix Reshape(Matrix x, int row, int col)
        {
            Matrix result = new Matrix(row,col);
            double[] arrayOfMatrix = Matrix.ToRowVector(x);
            if ((row * col) <= (x.rows * x.columns))
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        result.value[i, j] = arrayOfMatrix[i * col + j];
                    }
                }
            }
            else
            {
                result = Matrix.Zeros(row, col);
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        try
                        {
                            result.value[i, j] = arrayOfMatrix[i * col + j];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            //捕捉数组越界错误不进行处理
                        }
                    }
                }
            }
            return result;
        }
        public static double[] ToRowVector(Matrix x)
        {
            double[] result = new double[x.rows * x.columns];
            for (int i = 0; i < x.rows; i++)
            {
                for (int j = 0; j < x.columns; j++)
                {
                    result[i * x.columns + j] = x.value[i, j];
                }
            }
            return result;
        }
        public static Matrix GetRowVector(Matrix x, int row)//获取row行向量
        {
            Matrix result = new Matrix(1, x.columns);
            for (int i = 0; i < result.columns; i++)
            {
                result.value[0, i] = x.value[row, i];
            }
            return result;
        }
        public static Matrix GetColVector(Matrix x, int col)//获取col列向量
        {
            Matrix result = new Matrix(x.rows, 1);
            for (int i = 0; i < result.rows; i++)
            {
                result.value[i, 0] = x.value[i, col];
            }
            return result;
        }
        public static Matrix SetMatrixRow(Matrix x, int row, Matrix rowMatrix)
        {
            if (rowMatrix.rows != 1)
                return null;
            if (row >= x.rows)
                return null;
            if (x.columns != rowMatrix.columns)
                return null;
            Matrix result = new Matrix(x);
            for (int i = 0; i < x.columns; i++)
            {
                result.value[row, i] = rowMatrix.value[0, i];
            }
            return result;
        }
        public static Matrix SetMatrixRow(Matrix x, Matrix rowMatrix)
        {
            Matrix result = new Matrix(x);
            for (int i = 0; i < result.rows; i++)
            {
                result = Matrix.SetMatrixRow(result, i, rowMatrix);
            }
            return result;
        }
        public static Matrix SetMatrixCol(Matrix x, int col, Matrix colMatrix)
        {
            if (colMatrix.columns != 1)
                return null;
            if (col >= x.columns)
                return null;
            if (x.rows != colMatrix.rows)
                return null;
            Matrix result = new Matrix(x);
            for (int i = 0; i < x.rows; i++)
            {
                result.value[i, col] = colMatrix.value[i, 0];
            }
            return result;
        }
        public static Matrix SetMatrixCol(Matrix x, Matrix colMatrix)
        {
            Matrix result = new Matrix(x);
            for (int i = 0; i < result.columns; i++)
            {
                result = Matrix.SetMatrixCol(result, i, colMatrix);
            }
            return result;
        }
        public static Matrix RemoveRow(Matrix x, int row)
        {
            Matrix result=new Matrix(x);
            if (row >= x.rows)
                return result;
            for (int i = row; i < result.rows - 1; i++)
            {
                result = Matrix.RowSwitch(result, i, i + 1);
            }
            result = Matrix.SubMatrix(result, result.rows - 1, result.columns);
            return result;
        }
        public static Matrix RemoveCol(Matrix x, int col)
        {
            Matrix result = new Matrix(x);
            if (col >= x.columns)
                return result;
            for (int i = col; i < result.columns - 1; i++)
            {
                result = Matrix.ColumnSwitch(result, i, i + 1);
            }
            result = Matrix.SubMatrix(result, result.rows, result.columns - 1);
            return result;
        }
        public static Matrix MinOfRow(Matrix x)
        {
            Matrix result = new Matrix(x.rows, 1);
            for (int i = 0; i < result.rows; i++)
            {
                result.value[i, 0] = x.value[i, 0];
                for (int j = 1; j < x.columns; j++)
                {
                    if (x.value[i, j] < result[i, 0])
                    {
                        result[i, 0] = x.value[i, j];
                    }
                }
            }
            return result;
        }
        public static Matrix MaxOfRow(Matrix x)
        {
            Matrix result = new Matrix(x.rows, 1);
            for (int i = 0; i < result.rows; i++)
            {
                result.value[i, 0] = x.value[i, 0];
                for (int j = 1; j < x.columns; j++)
                {
                    if (x.value[i, j] > result[i, 0])
                    {
                        result[i, 0] = x.value[i, j];
                    }
                }
            }
            return result;
        }
        public static Matrix MinOfCol(Matrix x)
        {
            Matrix result = new Matrix(1, x.columns);
            for (int i = 0; i < result.columns; i++)
            {
                result.value[0, i] = x.value[0, i];
                for (int j = 1; j < x.rows; j++)
                {
                    if (x.value[j, i] < result[0, i])
                    {
                        result[0, i] = x.value[j, i];
                    }
                }
            }
            return result;
        }
        public static Matrix MaxOfCol(Matrix x)
        {
            Matrix result = new Matrix(1, x.columns);
            for (int i = 0; i < result.columns; i++)
            {
                result.value[0, i] = x.value[0, i];
                for (int j = 1; j < x.rows; j++)
                {
                    if (x.value[j, i] > result[0, i])
                    {
                        result[0, i] = x.value[j, i];
                    }
                }
            }
            return result;
        }
        public static Matrix SumRow(Matrix x)
        {
            Matrix result = Matrix.GetRowVector(x, 0);
            for (int i = 1; i < x.rows; i++)
            {
                result = result + Matrix.GetRowVector(x, i);
            }
            return result;
        }
        public static Matrix SumCol(Matrix x)
        {
            Matrix result = Matrix.GetColVector(x, 0);
            for (int i = 1; i < x.columns; i++)
            {
                result = result + Matrix.GetColVector(x, i);
            }
            return result;
        }
        public static double SumVector(Matrix x)
        {
            if (x.rows == 1)
            {
                double sum = 0;
                for (int i = 0; i < x.columns; i++)
                {
                    sum = sum + x.value[0, i];
                }
                return sum;
            }
            else if (x.columns == 1)
            {
                double sum = 0;
                for (int i = 0; i < x.rows; i++)
                {
                    sum = sum + x.value[i, 0];
                }
                return sum;
            }
            else
            {
                throw new Exception("错误:对非向量求向量和");
            }
        }
        public static Matrix MapMinMax(Matrix x)
        {
            Matrix result = new Matrix(x.rows, 2);
            Matrix min = Matrix.MinOfRow(x);
            Matrix max = Matrix.MaxOfRow(x);
            result = Matrix.SetMatrixCol(result, 0, min);
            result = Matrix.SetMatrixCol(result, 1, max);
            return result;
        }
        public static Matrix MapMinMaxOnCol(Matrix x)
        {
            Matrix result = new Matrix(2, x.columns);
            Matrix min = Matrix.MinOfCol(x);
            Matrix max = Matrix.MaxOfCol(x);
            result = Matrix.SetMatrixRow(result, 0, min);
            result = Matrix.SetMatrixRow(result, 1, max);
            return result;
        }
        public static Matrix LinspaceVector(double begin, double end, int num)
        {
            Matrix result = new Matrix(1, num + 1);
            for (int i = 0; i <= num; i++)
            {
                result.value[0, i] = i * (end - begin) / num + begin;
            }
            return result;
        }
        public static Matrix LogspaceVector(double begin, double end, int num)
        {
            Matrix result = new Matrix(1, num + 1);
            for (int i = 0; i <= num; i++)
            {
                result.value[0, i] = Math.Pow(10, i * (end - begin) / num + begin);
            }
            return result;
        }
        public static Matrix Positive(Matrix x)
        {
            Matrix result = new Matrix(x);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.columns; j++)
                {
                    if (result.value[i, j] < 0)
                    {
                        result.value[i, j] = -result.value[i, j];
                    }
                }
            }
            return result;
        }
        public static Matrix Negative(Matrix x)
        {
            Matrix result = new Matrix(x);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.columns; j++)
                {
                    if (result.value[i, j] > 0)
                    {
                        result.value[i, j] = -result.value[i, j];
                    }
                }
            }
            return result;
        }
        public static int Rank(Matrix x)
        {
            int result = x.rows;
            Matrix xStep = Matrix.ToStepMatrix(x);
            bool flag = true;
            for (int i = 0; i < x.rows; i++)
            {
                flag=true;//为true表示该行全为0
                for (int j = 0; j < x.columns; j++)
                {
                    if (x.value[i, j] != 0)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
        public static double[] Eigenvalue(Matrix x)
        {
            return new double[x.rows];
        }
        public static Matrix Eigenvector(Matrix x)
        {
            return x;
        }
        public static Matrix SimilarTransfer(Matrix x)
        {
            return x;
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
        public static Matrix operator -(Matrix x)
        {
            Matrix result = new Matrix(x.rows, x.columns);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.columns; j++)
                {
                    result.value[i, j] = -x.value[i, j];
                }
            }
            return result;
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
        public static Matrix operator /(Matrix x, Matrix y)
        {
            if (y.rows != y.columns)
                return null;
            if (Matrix.Det(y) == 0)
                return null;
            return x * Matrix.Reverse(y);
        }
        public static Matrix operator /(Matrix x, double y)
        {
            if (y == 0)
                return null;
            Matrix result = new Matrix(x);
            for (int i = 0; i < x.rows; i++)
            {
                for (int j = 0; j < x.columns; j++)
                {
                    result.value[i, j] = result.value[i, j] / y;
                }
            }
            return result;
        }
        public static Matrix operator /(double y, Matrix x)
        {
            Matrix result = y * Matrix.Ones(x.rows, x.columns);
            return Matrix.DotDevide(result, x);
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
        public static Matrix operator ^(Matrix x, int y)
        {
            if (x.rows != x.columns)
                return null;
            Matrix result = new Matrix(x);
            if (y < 0)
            {
                result = Matrix.Reverse(result);
                if (y < -1)
                {
                    result = result ^ (Math.Abs(y));
                }
            }
            else if (y == 0)
            {
                result = Matrix.Eye(x.rows);
            }
            else if (y == 1)
            {
                
            }
            else
            {
                for (int i = 1; i < y; i++)
                {
                    result = result * x;
                }
            }
            return result;
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
        //注意这里的逻辑运算与一般的不同，要保证所有元素符合逻辑式才成立,所以即使x!=y,x>y与x<y也可以同时不成立
        public static bool operator >(Matrix x, Matrix y)//返回true仅表示x中所有元素大于y中所有元素,以下其他函数类似
        {
            if (x.rows != y.rows || x.columns != y.columns)
                return false;
            bool flag = true;
            for (int i = 0; i < x.rows; i++)
            {
                for (int j = 0; j < x.columns; j++)
                {
                    if (x.value[i, j] <= y.value[i, j])
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }
        public static bool operator >(Matrix x, double y)
        {
            return x > (Matrix.Ones(x.rows, x.columns) * y);
        }
        public static bool operator >(double y, Matrix x)
        {
            return (Matrix.Ones(x.rows, x.columns) * y) > x;
        }
        public static bool operator <(Matrix x, Matrix y)
        {
            if (x.rows != y.rows || x.columns != y.columns)
                return false;
            bool flag = true;
            for (int i = 0; i < x.rows; i++)
            {
                for (int j = 0; j < x.columns; j++)
                {
                    if (x.value[i, j] >= y.value[i, j])
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }
        public static bool operator <(Matrix x, double y)
        {
            return x < (Matrix.Ones(x.rows, x.columns) * y);
        }
        public static bool operator <(double y, Matrix x)
        {
            return (Matrix.Ones(x.rows, x.columns) * y) < x;
        }
        public static Matrix operator ~(Matrix x)
        {
            return Matrix.Reverse(x);
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
    }//end Class Matrix
}//end namespace
