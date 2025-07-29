namespace com.rorisoft.structures.tree
{

    
    /// <summary>
    /// Arbol básico, donde en cada insercción conocemos previamente el nodo del padre.
    /// Un nodo permite almacenar un valor y tener N hijos
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RecursionTree<T>
    {

        public BasicNode<T> root { get; set; }
        public int total { get; set; }

        public RecursionTree()
        {
            this.total = 0;
        }

        public RecursionTree(BasicNode<T> nodo) : this()
        {
            this.root = nodo;
        }


        public bool InsertNode(T parentValue, T childValue)
        {
            BasicNode<T> found = FindNodo(root, parentValue);
            if (found != null)
            {
                BasicNode<T> node = new BasicNode<T>(childValue) { level = found.level + 1 };
                found.children.Add(node);
                total++;
            }

            return true;
        }

        private BasicNode<T>? FindNodo(BasicNode<T> nodoFrom, T value)
        {

            if (nodoFrom.value.Equals(value))
            {
                return nodoFrom;
            }

            foreach (var nodo in nodoFrom.children)
            {
                BasicNode<T>? node = FindNodo(nodo, value);
                if (node != null)
                {
                    return node;
                }
            }

            return null;
        }


        /// <summary>
        /// From root , sum all levels of each node 
        /// </summary>
        /// <returns></returns>
        public int SumAllLevels(BasicNode<T> from) 
        {
            
            int total = from.level;

            Console.WriteLine(from.value + " lvl#" + from.level);
            foreach (var node in from.children)
            {
                total += SumAllLevels(node);
            }


            return total;
        }


    }

    public class BasicNode<T>
    {
        public T value { get; set; }

        public int level { get; set; }
        public List<BasicNode<T>> children { get; set; }

        public BasicNode(T val)
        {
            this.value = val;
            this.children = [];
        }
    }
}
