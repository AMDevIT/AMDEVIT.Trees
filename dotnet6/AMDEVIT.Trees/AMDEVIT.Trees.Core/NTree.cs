namespace AMDEVIT.Trees.Core
{
    public class NTree<T>
    {
        #region Fields

        private NTreeNode<T> root;

        #endregion

        #region Properties

        public NTreeNode<T> Root
        {
            get 
            { 
                return this.root; 
            }    
        }

        #endregion

        #region .ctor

        public NTree()
        {

        }

        public NTree(T data)
        {   
            NTreeNode<T> node;

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null");

            node = new NTreeNode<T>(data);  
            this.root = node;   
        }

        public NTree(NTreeNode<T> root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root), "Root element cannot be null");

            if (root.Parent != null)
                throw new InvalidOperationException("Provided node element already assigned to a parent.");

            this.root = root;   
        }

        #endregion

        #region Methods

        public static NTree<T> Create(T data)
        {
            NTree<T> tree;

            if (data == null)   
                throw new ArgumentNullException(nameof(data), "Data cannot be null");    

            tree = new NTree<T>(data);
            return tree;
        }

        #endregion
    }
}