namespace AMDEVIT.Trees.Core
{
    public interface INTreeNode<T>
       where T : class
    {
        #region Properties

        INTreeNode<T>[] Children
        {
            get;
        }

        INTreeNode<T> Parent
        {
            get;
        }

        T Value
        {
            get;
        }

        #endregion

        #region Methods

        INTreeNode<T> AddChild(T value);
        bool AttachChild(INTreeNode<T> child);
        bool DetachChild(INTreeNode<T> child);
        bool RemoveChild(INTreeNode<T> child);

        #endregion
    }
}
