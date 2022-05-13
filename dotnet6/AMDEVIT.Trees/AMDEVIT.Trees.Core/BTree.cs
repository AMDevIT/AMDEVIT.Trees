using AMDEVIT.Trees.Core.Traversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDEVIT.Trees.Core
{
    public class BTree<T>
        : ITree<T>
        where T : class
    {
        #region Fields

        private IBTreeNode<T> root;

        #endregion

        #region Properties

        public ITreeNode<T> Root
        {
            get
            {
                return this.root;
            }
            protected set
            {
                IBTreeNode<T> newValue;

                if (value != null && value is not IBTreeNode<T>)
                    throw new InvalidOperationException("Value must be a BTree node");

                newValue = value as IBTreeNode<T>;
                this.root = newValue;                
            }
        }

        #endregion

        #region .ctor

        public BTree(T data)
        {
            BTreeNode<T> node;

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null");

            node = new BTreeNode<T>(data);
            this.root = node;
        }

        public BTree(IBTreeNode<T> root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root), "Root element cannot be null");

            if (root.Parent != null)
                throw new InvalidOperationException("Provided node element already assigned to a parent.");

            this.root = root;
        }

        public TraversedItem<T>[] LevelOrderTraversal()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
