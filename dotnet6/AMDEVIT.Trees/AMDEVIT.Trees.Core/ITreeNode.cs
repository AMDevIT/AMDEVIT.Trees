using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDEVIT.Trees.Core
{
    public interface ITreeNode<T>
    {
        #region Properties

        T Value
        {
            get;
        }

        ITreeNode<T> Parent
        {
            get;
        }

        #endregion

        #region Methods

        ITree<T> CreateSubTree();

        #endregion
    }
}
