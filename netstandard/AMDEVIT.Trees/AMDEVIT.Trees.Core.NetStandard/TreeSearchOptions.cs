namespace AMDEVIT.Trees.Core
{
    public class TreeSearchOptions
    {
        #region Properties

        public TreeSearchMode Mode
        {
            get;
            set;
        }

        #endregion

        #region .ctor

        public TreeSearchOptions()
            : this(TreeSearchMode.AllMatches)
        {
           
        }

        public TreeSearchOptions(TreeSearchMode searchMode)
        {
            this.Mode = searchMode;
        }

        #endregion
    }
}
