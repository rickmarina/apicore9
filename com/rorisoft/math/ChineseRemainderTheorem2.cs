using System;
using System.Collections.Generic;
using System.Text;

namespace com.rorisoft.math
{
    public class ChineseRemainderTheorem2
    { 
        // Returns modulo inverse of 
        // 'a' with respect to 'm' 
        // using extended Euclid Algorithm. 
        // Refer below post for details: 
        // https://www.geeksforgeeks.org/
        // multiplicative-inverse-under-modulo-m/ 
        private static long inv(long a, long m)
        {
            long m0 = m, t, q;
            long x0 = 0, x1 = 1;

            if (m == 1)
                return 0;

            // Apply extended 
            // Euclid Algorithm 
            while (a > 1)
            {
                // q is quotient 
                q = a / m;

                t = m;

                // m is remainder now, 
                // process same as 
                // euclid's algo 
                m = a % m; a = t;

                t = x0;

                x0 = x1 - q * x0;

                x1 = t;
            }

            // Make x1 positive 
            if (x1 < 0)
                x1 += m0;

            return x1;
        }

        // k is size of num[] and rem[]. 
        // Returns the smallest number 
        // x such that: 
        // x % num[0] = rem[0], 
        // x % num[1] = rem[1], 
        // .................. 
        // x % num[k-2] = rem[k-1] 
        // Assumption: Numbers in num[] 
        // are pairwise coprime (gcd 
        // for every pair is 1) 
        public static long findMinX(int[] num,
                            int[] rem,
                            int k)
        {
            // Compute product 
            // of all numbers 
            long prod = 1;
            for (int i = 0; i < k; i++)
                prod *= num[i];

            // Initialize result 
            long result = 0;

            // Apply above formula 
            for (int i = 0; i < k; i++)
            {
                long pp = prod / num[i];
                result += rem[i] *
                        inv(pp, num[i]) * pp;
            }

            return result % prod;
        }
    }
}
