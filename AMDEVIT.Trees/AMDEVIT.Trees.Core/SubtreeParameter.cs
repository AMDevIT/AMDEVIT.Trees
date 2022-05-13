namespace AMDEVIT.Trees.Core
{
    internal class SubtreeParameter<T>
    {
        #region Properties

        public ITreeNode<T> Parent
        {
            get;
            protected set;
        }

        public ITreeNode<T> Current
        {
            get;
            protected set;
        }

        #endregion

        #region .ctor

        public SubtreeParameter(ITreeNode<T> parent, ITreeNode<T> current)
        {
            this.Parent = parent;
            this.Current = current;
        }

        #endregion
    }
}
