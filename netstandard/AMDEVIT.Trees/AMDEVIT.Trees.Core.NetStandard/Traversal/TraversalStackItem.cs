namespace AMDEVIT.Trees.Core.Traversal
{
    internal class TraversalStackItem<T>
        where T: class
    {
        #region Properties

        public int Level
        {
            get;
            protected set;
        }

        public ITreeNode<T> Node
        {
            get;
            protected set;
        }

        #endregion

        #region .ctor

        public TraversalStackItem(int level, ITreeNode<T> node)
        {
            this.Level = level;
            this.Node = node;
        }

        #endregion
    }
}
