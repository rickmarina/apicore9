using System.Collections;
using System.Text.RegularExpressions;

namespace com.rorisoft.structures.tree
{

    public class Node
    {
        public string value { get; set; }
        public Node left { get; set; }
        public Node right { get; set; }

        public Node(string item)
        {
            this.value = item;
            left = right = null;
        }
    }
    public class ExpressionTree
    {
        public bool isOperand(string c)
        {
            Regex _operandRegex = new Regex(@"-?[0-9]+");
            return _operandRegex.IsMatch(c.ToString());
        }

        public bool isOperator(string c)
        {
            Regex _operatorRegex = new Regex(@"[+\-*\/]");
            return _operatorRegex.IsMatch(c.ToString());
        }

        public void inOrder(Node t)
        {
            if (t!=null)
            {
                inOrder(t.left);
                Console.WriteLine(t.value + " ");
                inOrder(t.right);
            }
        }

        public Node constructTree(string[] postfix)
        {
            Stack st = new Stack();
            Node t, t1, t2; 

            foreach (string token in postfix)
            {
                if (isOperand(token))
                {
                    t = new Node(token);
                    st.Push(t);
                } else if (isOperator(token))
                {
                    t = new Node(token);

                    //pop 2 top nodes
                    t1 = (Node)st.Pop();
                    t2 = (Node)st.Pop();

                    t.right = t1;
                    t.left = t2;

                    st.Push(t);

                }
            }

            t = (Node)st.Peek();
            st.Pop();

            return t;
        }
    }
}
