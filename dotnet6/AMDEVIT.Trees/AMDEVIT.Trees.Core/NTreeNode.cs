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
            parent.AttachChild(this);
        }

        #endregion

        #region Methods

        public NTreeNode<T> AddChild(T data)
        {
            NTreeNode<T> newNode;

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null.");

            newNode = new NTreeNode<T>(data, this);
            this.children.Add(newNode);
            return newNode;
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
            bool result;

            if (child == null)
                throw new ArgumentNullException(nameof(child), "Child node cannot be null");

            // Throw an exception or return false?

            if (child.parent != null)
                throw new InvalidOperationException("Child node already have a parent. Cannot attach the node.");

            try
            {
                this.children.Add(child);
                child.parent = this;
                result = true;
            }
            catch(Exception exc)
            {
                _ = exc;
                result = false;
            }

            return result;
        }

        public bool DetachChild(NTreeNode<T> child)
        {
            bool result;

            if (child == null)
                throw new ArgumentNullException(nameof(child), "Child node cannot be null");

            // Throw an exception or return false?

            if (child.parent == null)
                throw new InvalidOperationException("Child node does not have a parent. Cannot attach the node.");

            if (child.parent != this)
                result = false;
            else
            {
                try
                {
                    this.children.Remove(child);
                    child.parent = null;
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

        #endregion
    }
}
