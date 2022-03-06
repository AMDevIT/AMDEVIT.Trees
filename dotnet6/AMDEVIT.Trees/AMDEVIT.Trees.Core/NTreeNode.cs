using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDEVIT.Trees.Core
{
    public class NTreeNode<T>
    {
        #region Fields

        private T data;
        private readonly SortedList<int, NTreeNode<T>> children = new SortedList<int, NTreeNode<T>>();

        #endregion

        #region Properties

        public T Data
        {
            get
            {
                return this.data;
            }
            protected set
            {
                this.data = value;  
            }
        }

        public NTreeNode<T>[] Children
        {
            get
            {
                NTreeNode<T>[] result;
                                
                result = this.children.Values.ToArray();
                return result;
            }
        }

        #endregion
    }
}
