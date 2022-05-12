using System;

namespace AMDEVIT.Trees.Core
{
    public class BTreeNode<T>
        : IBTreeNode<T>
        where T : class
    {
        #region Fields

        IBTreeNode<T> parent;   
        private T data;

        #endregion

        #region Properties

        public ITreeNode<T> Parent 
        { 
            get
            {
                return this.parent;
            }
            protected set
            {
                IBTreeNode<T> newValue;

                if (value != null && value is not IBTreeNode<T>)
                    throw new InvalidOperationException("Value must be a NTree node");

                newValue = value as IBTreeNode<T>;
                this.parent = newValue;
            }
        }

        public IBTreeNode<T> Left
        {
            get;
            protected set;
        }

        public IBTreeNode<T> Right
        {
            get;
            protected set;
        }

        public T Value
        {
            get
            {
                return this.data;
            }
            protected set
            {
                this.data = value;
            }
        }
        
        #endregion

        #region .ctor

        public BTreeNode(T value)
            : this(value, null)
        {
        }

        protected BTreeNode(T value, NTreeNode<T> parent)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Value cannot be null.");

            this.data = value;         
        }

        public ITree<T> CreateSubTree()
        {
            throw new NotImplementedException();
        }

        public ITreeNode<T> Clone()
        {
            BTreeNode<T> clonedNode;

            clonedNode = new BTreeNode<T>(this.Value);
            return clonedNode;
        }

        #endregion
    }
}
