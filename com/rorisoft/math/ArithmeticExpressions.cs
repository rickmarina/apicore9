using System;
using System.Collections.Generic;
using System.Text;

namespace com.rorisoft.math
{
    public class ArithmeticExpressions
    {

        public static string GeneratePostFix(string expression, Func<char, int> precedence)
        {
            expression = expression.Replace(" ", string.Empty);
            StringBuilder postfixNotation = new StringBuilder();
            Stack<char> postfixStack = new Stack<char>();

            foreach (var c in expression)
            {
                if (char.IsDigit(c))
                {
                    postfixNotation.Append(c);
                }
                else if (c == '(')
                {
                    postfixStack.Push(c);
                }
                else if (c == ')')
                {
                    while (postfixStack.Count > 0 && postfixStack.Peek() != '(')
                    {
                        postfixNotation.Append(postfixStack.Pop());
                    }

                    postfixStack.TryPop(out _);
                }
                else
                {
                    while (postfixStack.Count > 0 && precedence(c) <= precedence(postfixStack.Peek()))
                    {
                        postfixNotation.Append(postfixStack.Pop());
                    }

                    postfixStack.Push(c);
                }
            }

            while (postfixStack.Count > 0)
            {
                postfixNotation.Append(postfixStack.Pop());
            }

            return postfixNotation.ToString();
        }

        public static long CalculatePostfix(string postfix)
        {
            Stack<long> expressionStack = new Stack<long>();
            foreach (char c in postfix.ToString())
            {
                if (char.IsDigit(c))
                {
                    expressionStack.Push((long)char.GetNumericValue(c));
                }
                else
                {
                    long a = expressionStack.Pop();
                    long b = expressionStack.Pop();

                    if (c == '+')
                    {
                        expressionStack.Push(a + b);
                    }
                    else if (c == '*')
                    {
                        expressionStack.Push(a * b);
                    }
                }
            }

            return expressionStack.Pop();
        }

        public static int OperatorPrecedence1(char c)
        {
            if (c == '+' || c == '*')
            {
                return 1;
            }

            return 0;
        }

        public static int OperatorPrecedence2(char c)
        {
            if (c == '+')
            {
                return 2;
            }

            if (c == '*')
            {
                return 1;
            }

            return 0;
        }

    }
}
