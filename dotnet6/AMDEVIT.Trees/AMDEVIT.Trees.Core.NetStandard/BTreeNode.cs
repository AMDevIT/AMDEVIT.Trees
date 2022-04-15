namespace AMDEVIT.Trees.Core
{
    public class BTreeNode<T>
        : IBTreeNode<T>
        where T : class
    {
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
            get;
            protected set;
        }

        #endregion        
    }
}
