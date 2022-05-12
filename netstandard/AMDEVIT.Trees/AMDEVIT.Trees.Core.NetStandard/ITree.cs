using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
