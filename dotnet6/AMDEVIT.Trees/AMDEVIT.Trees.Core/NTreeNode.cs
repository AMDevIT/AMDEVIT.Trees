using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDEVIT.Trees.Core
{
    public class NTreeNode<T>
    {
        #region Fields

        private T data;        
        private NTreeNode<T> parent;
        private readonly List<NTreeNode<T>> children = new List<NTreeNode<T>>();

        #endregion

        #region Properties

        public NTreeNode<T> Parent
        {
            get
            {
                return parent;  
            }
        }

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

        public NTreeNode<T>[] Children
        {
            get
            {
                NTreeNode<T>[] result;
                                
                result = this.children.ToArray();
                return result;
            }
        }

        #endregion

        #region .ctor

        public NTreeNode(T data)
            : this(data, null)
        {   
        }

        protected NTreeNode(T data, NTreeNode<T> parent)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null.");
    
            this.data = data;
            this.parent = parent;
        }

        #endregion

        #region Methods

        public void AddChild(T data)
        {
            NTreeNode<T> newNode;

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null.");

            newNode = new NTreeNode<T>(data, this);
            this.children.Add(newNode);
        }

        public bool RemoveChild(NTreeNode<T> child)
        {
            bool result = false;
            if (child == null)
                throw new ArgumentNullException(nameof(child), "Child element cannot be null.");

            try
            {
                this.children.Add(child);
                result = true;
            }
            catch(Exception exc)
            {
                _ = exc;
            }

            return result;
        }

        public bool AttachChild(NTreeNode<T> child)
        {
            throw new NotImplementedException();
        }

        public bool DetachChild(NTreeNode<T> child)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
