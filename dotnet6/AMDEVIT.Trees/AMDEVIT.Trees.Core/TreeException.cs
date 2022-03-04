namespace AMDEVIT.Trees.Core
{
    public class TreeException
        : Exception
    {
        #region Properties

        public TreeOperation Operation
        {
            get;
            protected set;
        } = TreeOperation.Unknown;

        #endregion

        #region .ctor

        internal TreeException()
            : base()
        {
        }

        internal TreeException(string message) 
            : base(message)
        {
        }
        
        internal TreeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal TreeException(Exception innerException)
          : base(innerException.Message, innerException)
        {
        }

        internal TreeException(TreeOperation operation, string message)
            : base(message)
        {
            Operation = operation;
        }

        internal TreeException(TreeOperation operation, Exception innerException)
          : base(innerException.Message, innerException)
        {
            Operation = operation;
        }

        internal TreeException(TreeOperation operation, string message, Exception innerException)
            : base(message, innerException)
        {
            Operation = operation;
        }

        #endregion
    }
}
