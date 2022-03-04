using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDEVIT.Trees.Core
{
    /// <summary>
    /// Unordered generic tree node.
    /// </summary>
    /// <typeparam name="T">The type of the contained objects</typeparam>
    public class NTreeNode<T>
    {
        #region Fields      

        private bool invalidatedChildrenCache = true;
        private T data;
        private NTreeNode<T> parent;
        private NTreeNode<T>[] childrenCache = null;
        private readonly HashSet<NTreeNode<T>> children = new HashSet<NTreeNode<T>>();

        #endregion

        #region Properties

        public T Data
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

        public NTreeNode<T> Parent
        {
            get
            {
                return this.parent;
            }
        }

        public int Count
        {
            get
            {
                return this.children.Count; 
            }
        }

        public NTreeNode<T>[] Children
        {
            get 
            {
                if (this.childrenCache == null || this.invalidatedChildrenCache == true)
                    childrenCache = this.children.ToArray();   
                else
                {
                    if (this.childrenCache.Length != this.children.Count)
                        this.childrenCache.ToArray();
                }
                return this.childrenCache; 
            }    
        }

        public bool IsEmpty
        {
            get 
            { 
                return this.children.Count == 0; 
            } 
        }

        #endregion

        #region .ctor

        public NTreeNode(T data)
        {        
            this.data = data;   
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create and attach a new children node.
        /// </summary>
        /// <param name="data">The data contained in the node.</param>
        /// <returns>The new created children node</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public NTreeNode<T> AddChildren(T data)
        {
            NTreeNode<T> node;
            bool addResult;

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null. Cannot attach new node to current node.");

            node = new NTreeNode<T>(data);

            addResult = this.children.Add(node);
            if (addResult == true)
            {
                node.parent = this;
                this.invalidatedChildrenCache = true;
            }
            return node;
        }

        /// <summary>
        /// Remove the requested children from the node, and discard the children node.
        /// </summary>
        /// <param name="node">The node to remove</param>
        /// <returns>True if the node was removed, otherwise else.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public bool RemoveChildren(NTreeNode<T> node)
        {
            bool removed = false;   

            if (node == null)
                throw new ArgumentNullException(nameof(node), "Requested node to detach cannot be null.");

            if (node.parent == null)
                throw new InvalidOperationException("Requested children is not a children, parent is null.");

            if (node.parent != this)
                throw new InvalidOperationException("Requested node is a children of a different node");

            try
            {
                if (this.children.Contains(node))
                {
                    removed = this.children.Remove(node);
                    if (removed == true)
                    {
                        node.parent = null;
                        this.invalidatedChildrenCache = true;
                    }
                }
            }
            catch (Exception exc)
            {
                TreeException treeException = new TreeException(TreeOperation.Remove, exc);
                throw treeException;
            }

            return removed;
        }

        #region Attach/Detach

        public bool AttachChildren(NTreeNode<T> node)
        {
            bool addResult;

            if (node == null)
                throw new ArgumentNullException(nameof(node), "Requested node to detach cannot be null.");

            if (node.parent != null)
                throw new InvalidOperationException("Requested node is already a children of a different node.");

            if (node.parent == this)
                throw new InvalidOperationException("Requested node is already a children of this node");

            addResult = this.children.Add(node);

            if (addResult == true)
            {
                node.parent = this;
                this.invalidatedChildrenCache = true;
            }

            return addResult;
        }

        public bool DetachChildren(NTreeNode<T> children)
        {
            bool removed;

            if (children == null)
                throw new ArgumentNullException(nameof(children), "Requested children to detach cannot be null.");

            if (children.parent == null)
                throw new InvalidOperationException("Requested children is not a children, parent is null.");

            if (children.parent != this)
                throw new InvalidOperationException("Requested node is a children of a different node");

            removed = this.children.Remove(children);

            if (removed == true)
            {
                children.parent = null;
                this.invalidatedChildrenCache = true;
            }

            return removed;
        }

        #endregion

        #endregion
    }
}
