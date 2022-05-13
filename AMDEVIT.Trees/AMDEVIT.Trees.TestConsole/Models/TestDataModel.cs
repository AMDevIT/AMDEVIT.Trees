namespace AMDEVIT.Trees.TestConsole.Models
{
    internal class TestDataModel
    {
        #region Properties

        public Guid Id
        {
            get;
            protected set;
        }

        public string Name
        {
            get;
            protected set;
        }

        #endregion

        #region .ctor

        public TestDataModel(string name)
            : this(Guid.NewGuid(), name)
        {

        }

        public TestDataModel(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"[{this.Id}] {this.Name}";
        }

        #endregion
    }
}
