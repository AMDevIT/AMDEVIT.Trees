using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDEVIT.Trees.Core
{
    public interface IBTreeNode<T>
        where T : class 
    {
        #region Properties

        IBTreeNode<T> Parent
        {
            get;
        }

        T Value
        {
            get;
        }

        #endregion
    }
}
