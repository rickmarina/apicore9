namespace com.rorisoft.structures.tree
{

    /// <summary>
    /// El árbol diccionario provee una estructura de datos que simula un árbol, 
    /// apoyada por una estructura de diccionario donde ir almacenando los nodos
    /// ya que la insercción de los nodos va relacionada con un nodo padre, que no siempre conoceremos
    /// De esta manera el nodo queda registrado en el diccionario a la espera de poder linkarlo con su padre correspondiente. 
    /// Un nodo permite un valor de padre y un único hijo
    /// </summary>
    public class DictionaryTree<T> where T : notnull
    {
        private readonly Dictionary<T, DictionaryNode<T>> elements;

        public DictionaryTree()
        {
            elements = [];
        }

        /// <summary>
        /// Si el padre no existe, lo insertamos con padre a null
        /// Si el hijo no existe , lo insertamos con el nodo almacenado del padre
        /// Si el hijo si existe (esto indica que fue padre y ahora es enviado como hijo con su padre), modificamos su padre al enviado 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public void AddNode(T parent, T child) 
        {
            if (!elements.ContainsKey(parent)) elements.Add(parent, new DictionaryNode<T>(parent, null));
            if (!elements.ContainsKey(child)) elements.Add(child, new DictionaryNode<T>(child, elements[parent]));
            else elements[child].parent = elements[parent]; //Existe el elemento hijo pero no tenía padre y ahora sí

        }
    }


    public class DictionaryNode<T>
    {
        public T valor { get; }
        private DictionaryNode<T>? _parent;
        public DictionaryNode<T>? parent
        {
            get { return _parent; }
            set
            {
                if (_parent != null && _parent != value)
                    throw new ArgumentException($"Node[{valor}] already has a parent.");
                _parent = value;
            }
        }

        // Método recursivo para medir la altura del arbol desde el nodo en cuestión
        public int orbitCount => (parent == null) ? 0 : parent.orbitCount + 1;

        public DictionaryNode(T name, DictionaryNode<T>? parent)
        {
            this.valor = name;
            this.parent = parent;
        }
    }
}
