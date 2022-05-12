using System.Collections.Generic;

namespace AMDEVIT.Trees.Core
{
    public interface INTree<T> 
        : ITree<T>
        where T : class
    {
        #region Properties
       
        #endregion

        #region Methods

        INTreeNode<T> AddNode(INTreeNode<T> parent, T data);
        INTreeNode<T> AddNode(T data);
        SortedList<int, INTreeNode<T>> LevelOrderTraversal();
        bool RemoveNode(INTreeNode<T> parent, INTreeNode<T> child);
        INTreeNode<T>[] Search(T data, TreeSearchOptions options);

        #endregion
    }
}