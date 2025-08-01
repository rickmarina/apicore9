﻿using System;
using System.Collections.Generic;
using System.Text;

namespace com.rorisoft.math
{
    public class ChineseRemainderTheorem
    {

        public static long Solve(int[] n, int[] a)
        {
            long prod = 1;
            foreach (int num in n)
            {
                prod *= num;
            }
            //int prod = n.Aggregate(1, (i, j) => i * j);
            long p;
            long sm = 0;
            for (int i = 0; i < n.Length; i++)
            {
                p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }
            return sm % prod;
        }

        private static long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (long x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
    }
}
