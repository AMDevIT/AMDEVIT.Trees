using AMDEVIT.Trees.Core.Traversal;

namespace AMDEVIT.Trees.Core
{
    public interface ITree<T>
    {
        #region Properties

        ITreeNode<T> Root
        {
            get;
        }

        #endregion

        #region Methods

        TraversedItem<T>[] LevelOrderTraversal();

        #endregion
    }
}
