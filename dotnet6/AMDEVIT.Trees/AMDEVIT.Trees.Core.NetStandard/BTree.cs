using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDEVIT.Trees.Core
{
    public class BTree<T>
        where T : class
    {
        #region Fields

        private INTreeNode<T> root;

        #endregion

        #region Properties

        public INTreeNode<T> Root
        {
            get
            {
                return this.root;
            }
        }

        #endregion
    }
}
