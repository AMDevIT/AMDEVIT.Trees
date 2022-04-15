using System;

namespace AMDEVIT.Trees.Core
{
    public class BTreeNode<T>
        : IBTreeNode<T>
        where T : class
    {
        #region Fields

        private T data;

        #endregion

        #region Properties

        public IBTreeNode<T> Parent 
        { 
            get; 
            protected set; 
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

        #endregion
    }
}
