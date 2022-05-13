using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDEVIT.Trees.Core
{
    public interface IBTreeNode<T>
        : ITreeNode<T>        
        where T : class 
    {
        #region Properties

        IBTreeNode<T> Left
        {
            get;
        }

        IBTreeNode<T> Right
        {
            get;
        }

        #endregion
    }
}
