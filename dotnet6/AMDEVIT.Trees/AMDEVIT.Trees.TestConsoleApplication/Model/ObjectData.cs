namespace AMDEVIT.Trees.TestConsoleApplication.Model
{
    internal class ObjectData
    {
        #region Consts

        protected const string DefaultName = "Unnamed";

        #endregion

        #region Properties

        public Guid ID
        {
            get;
            protected set;
        }

        public string Name
        {
            get;
            set;
        }

        #endregion

        #region .ctor

        public ObjectData()
            : this(DefaultName)
        {            
        }

        public ObjectData(string name)
        {
            this.ID = Guid.NewGuid();
            this.Name = name;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            string objectDataStringValue = $"[{this.ID}] {this.Name}";
            return objectDataStringValue;
        }

        #endregion
    }
}
