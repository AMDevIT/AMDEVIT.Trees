using System;
using System.Collections.Generic;

namespace AMDEVIT.Trees.Core
{
    public class NTreeNode<T>
        : INTreeNode<T>
        where T : class
    {
        #region Fields

        private T data;
        private INTreeNode<T> parent;
        protected readonly List<INTreeNode<T>> children = new List<INTreeNode<T>>();

        #endregion

        #region Properties

        public ITreeNode<T> Parent
        {
            get
            {
                return parent;
            }
            protected set
            {
                INTreeNode<T> newValue;

                if (value != null && value is not INTreeNode<T>)
                    throw new InvalidOperationException("Value must be a NTree node");

                newValue = value as INTreeNode<T>;
                this.parent = newValue;
            }
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

        public INTreeNode<T>[] Children
        {
            get
            {
                INTreeNode<T>[] result;

                result = this.children.ToArray();
                return result;
            }
        }

        #endregion

        #region .ctor

        public NTreeNode(T value)
            : this(value, null)
        {
        }

        protected NTreeNode(T value, NTreeNode<T> parent)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Value cannot be null.");

            this.data = value;
            if (parent != null)
                parent.AttachChild(this);
        }

        #endregion

        #region Methods

        public virtual INTreeNode<T> AddChild(T value)
        {
            NTreeNode<T> newNode;

            if (value == null)
                throw new ArgumentNullException(nameof(value), "Value cannot be null.");

            newNode = new NTreeNode<T>(value, this);
            return newNode;
        }

        public virtual bool RemoveChild(INTreeNode<T> child)
        {
            bool result = false;

            if (child == null)
                throw new ArgumentNullException(nameof(child), "Child element cannot be null.");

            try
            {
                if (child is NTreeNode<T>)
                {
                    NTreeNode<T> currentNode = child as NTreeNode<T>;
                    if (currentNode != null)
                        currentNode.parent = null;
                }

                this.children.Remove(child);
                result = true;
            }
            catch (Exception exc)
            {
                _ = exc;
            }

            return result;
        }

        public virtual bool AttachChild(INTreeNode<T> child)
        {
            NTreeNode<T> currentNode = null;
            bool result;

            if (child == null)
                throw new ArgumentNullException(nameof(child), "Child node cannot be null");

            if (child is NTreeNode<T>)
                currentNode = child as NTreeNode<T>;

            // Throw an exception or return false?

            if (currentNode != null && currentNode.parent != null)
                throw new InvalidOperationException("Child node already have a parent. Cannot attach the node.");

            try
            {
                this.children.Add(child);
                currentNode.parent = this;
                result = true;
            }
            catch (Exception exc)
            {
                _ = exc;
                result = false;
            }

            return result;
        }

        public virtual bool DetachChild(INTreeNode<T> child)
        {
            NTreeNode<T> currentNode = null;
            bool result;

            if (child == null)
                throw new ArgumentNullException(nameof(child), "Child node cannot be null");

            if (child is NTreeNode<T>)
                currentNode = child as NTreeNode<T>;

            // Throw an exception or return false?

            if (currentNode.parent == null)
                throw new InvalidOperationException("Child node does not have a parent. Cannot attach the node.");

            if (currentNode.parent != this)
                result = false;
            else
            {
                try
                {
                    this.children.Remove(child);
                    currentNode.parent = null;
                    result = true;
                }
                catch (Exception exc)
                {
                    _ = exc;
                    result = false;
                }
            }

            return result;
        }

        public ITree<T> CreateSubTree()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
