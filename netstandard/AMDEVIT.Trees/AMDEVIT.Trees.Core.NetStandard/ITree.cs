using AMDEVIT.Trees.Core.Traversal;
using System;

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
        ITreeNode<T>[] Search(T data, TreeSearchOptions options);
        ITreeNode<T>[] Search(T data, TreeSearchOptions options, Func<T, bool> searchPattern);

        #endregion
    }
}
