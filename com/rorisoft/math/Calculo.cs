using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace com.rorisoft.math
{
    class Calculo
    {


        /// <summary>
        /// Solución Recursiva para calcular el número n en la secuencia Fibonacci
        /// Utiliza un diccionario para almacenar los resultados para futuras llamadas
        /// </summary>
        /// <param name="n"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public static BigInteger Fibonacci(long n, Dictionary<long, BigInteger> memo)
        {
            BigInteger total = 0;
            if (n == 0)
                return 0;
            else if (n == 1)
            {
                return 1;
            }
            else if (memo.ContainsKey(n))
            {
                return memo[n];
            }

            total += Fibonacci(n - 1, memo) + Fibonacci(n - 2, memo);

            memo.Add(n, total);
            return total;
        }

        /// <summary>
        /// Método iterativo para calcular el número reqDigits de la secuencia Fibonacci
        /// </summary>
        /// <param name="huge1"></param>
        /// <param name="huge2"></param>
        /// <param name="reqDigits"></param>
        /// <returns></returns>
        public static BigInteger FibHugesUntil(BigInteger huge1, BigInteger huge2, int reqDigits)
        {
            int number = 1;
            while (number < reqDigits)
            {
                var huge3 = huge1 + huge2;
                huge1 = huge2;
                huge2 = huge3;
                number++;
            }
            return huge2;
        }

        /// <summary>
        /// Método iterativo para calcular la suma dsede el número fibonacci From hasta To
        /// Se apoya en el diccionario memo a modo de caché
        /// </summary>
        /// <param name="huge1"></param>
        /// <param name="huge2"></param>
        /// <param name="current"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public static BigInteger SumFibonacciUntil(BigInteger huge1, BigInteger huge2, int current, int from, int to, Dictionary<int, BigInteger> memo)
        {
            int number = current;

            if (from > 0)
            {
                while (number < from)
                {
                    var huge3 = huge1 + huge2;
                    huge1 = huge2;
                    huge2 = huge3;
                    number++;
                }

                //Nos guardamos en fibocaché este comienzo
                if (!memo.ContainsKey(number - 1))
                    memo.Add(number - 1, huge1);
                if (!memo.ContainsKey(number))
                    memo.Add(number, huge2);
            }
            else
            {
                number++;
            }
            BigInteger suma = huge2;

            while (number < to)
            {
                var huge3 = huge1 + huge2;
                huge1 = huge2;
                huge2 = huge3;
                suma += huge3;
                number++;
            }
            //Nos guardamos en fibocaché este comienzo
            if (!memo.ContainsKey(number - 1))
                memo.Add(number - 1, huge1);
            if (!memo.ContainsKey(number))
                memo.Add(number, huge2);

            return suma;
        }

        /// <summary>
        /// Método iterativo para calcular el número n de la secuencia Fibonacci utilizando matrices
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static BigInteger FibonacciMatrix(int n)
        {

            int num = Math.Abs(n);
            if (num == 0)
            {
                return BigInteger.Zero;
            }
            else if (num <= 2)
            {
                return BigInteger.One;
            }


            BigInteger[,] number = { { BigInteger.One, BigInteger.One }, { BigInteger.One, BigInteger.Zero } };
            BigInteger[,] result = { { BigInteger.One, BigInteger.One }, { BigInteger.One, BigInteger.Zero } };

            while (num > 0)
            {
                if (num % 2 == 1) result = MultiplyMatrix(result, number);
                number = MultiplyMatrix(number, number);
                num /= 2;
            }

            //return result[1,1].multiply(BigInteger.valueOf(((n < 0) ? -1 : 1)));

            return BigInteger.Multiply(result[1, 1], (BigInteger)((n < 0) ? -1 : 1));
        }

        public static BigInteger[,] MultiplyMatrix(BigInteger[,] mat1, BigInteger[,] mat2)
        {
            return new BigInteger[,] {
            {
                BigInteger.Add(BigInteger.Multiply(mat1[0,0], mat2[0,0]),BigInteger.Multiply(mat1[0,1], mat2[1,0])),
                BigInteger.Add(BigInteger.Multiply(mat1[0,0], mat2[0,1]),BigInteger.Multiply(mat1[0,1], mat2[1,1]))
            },
            {
                BigInteger.Add(BigInteger.Multiply(mat1[1,0], mat2[0,0]),BigInteger.Multiply(mat1[1,1], mat2[1,0])),
                BigInteger.Add(BigInteger.Multiply(mat1[1,0], mat2[0,1]),BigInteger.Multiply(mat1[1,1], mat2[1,1]))
            }
        };
        }

        static ulong GCD(ulong a, ulong b)
        {

            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;

            }
            return a | b;
        }
    }
}
