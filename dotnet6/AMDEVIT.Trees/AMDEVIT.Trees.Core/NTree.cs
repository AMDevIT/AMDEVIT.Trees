namespace AMDEVIT.Trees.Core
{
    /// <summary>
    /// Unordered generic tree.
    /// </summary>
    /// <typeparam name="T">The type of the contained object.</typeparam>
    public class NTree<T>
    {
        #region Fields

        protected NTreeNode<T> root;
        protected readonly HashSet<NTreeNode<T>> treeChildren = new HashSet<NTreeNode<T>>();

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

        public NTree(T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null");

            this.root = new NTreeNode<T>(data);
        }

        protected NTree(NTreeNode<T> rootNode)
        {
            if (rootNode == null)
                throw new ArgumentNullException(nameof(rootNode), "Root node cannot be null");

            this.root = rootNode;
        }

        #endregion

        #region Methods

        public static NTree<T> Create(T data)
        {
            NTreeNode<T> rootNode;
            NTree<T> tree;

            if (data == null)   
                throw new ArgumentNullException(nameof(data), "Data cannot be null");

            rootNode = new NTreeNode<T>(data);

            tree = new NTree<T>(rootNode);
            return tree;
        }

        public NTreeNode<T> AddChildren(T data)
        {
            NTreeNode<T> childrenNode;

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null. Cannot attach new node to current node.");

            childrenNode = this.root.AddChildren(data);
            if (childrenNode != null)
                this.treeChildren.Add(childrenNode);
            return childrenNode;
        }

        public NTreeNode<T> AddChildren(NTreeNode<T> parent, T data)
        {
            NTreeNode<T> childrenNode;

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null. Cannot attach new node to current node.");            

            if (parent == null)
                throw new ArgumentNullException(nameof(parent), "Parent node cannot be null. Cannot attach new node to parent node.");

            if (this.treeChildren.Contains(parent))
            {
                childrenNode = parent.AddChildren(data);
                if (childrenNode != null)
                    this.treeChildren.Add(childrenNode);
            }
            else
                throw new InvalidOperationException("Parent node is not a children of this tree.");

            return childrenNode;
        }

        public bool RemoveChildren(NTreeNode<T> children)
        {
            throw new NotImplementedException();
        }

        public bool RemoveChildren(NTreeNode<T> parent, NTreeNode<T> children)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Traverse the generic tree level wise.
        /// </summary>
        /// <returns>The list of element in traversed order.</returns>
        public SortedList<int, NTreeNode<T>> LevelOrderTraversal()
        {
            SortedList<int, NTreeNode<T>> traversedList = new SortedList<int, NTreeNode<T>>();
            Queue<NTreeNode<T>> traverseQueue;
            int order = 0;

            if (this.root == null)
                return traversedList;

            traverseQueue = new Queue<NTreeNode<T>>();
            traverseQueue.Enqueue(this.root);

            while(traverseQueue.Count != 0)
            {
                int n = traverseQueue.Count;
                while(n > 0)
                {
                    NTreeNode<T> currentNode = traverseQueue.Dequeue();
                    traversedList.Add(order, currentNode);
                    order++;    

                    for (int i = 0; i < currentNode.Count; i++)
                    {
                        if (currentNode.Children != null)
                        {
                            NTreeNode<T> childrenNode = currentNode.Children[i];
                            traverseQueue.Enqueue(childrenNode);    
                        }
                    }
                    n--;
                }
            }

            return traversedList;
        }

        #endregion
    }
}