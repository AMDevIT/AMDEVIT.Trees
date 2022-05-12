namespace AMDEVIT.Trees.Tests.Models
{
    internal class IDDescriptionDataModel
    {
        #region Properties

        public Guid Id
        {
            get;
            protected set;
        }

        public string Description
        {
            get;
            protected set;
        }

        #endregion

        #region .ctor

        public IDDescriptionDataModel(string name)
            : this(Guid.NewGuid(), name)
        {

        }

        public IDDescriptionDataModel(Guid id, string name)
        {
            this.Id = id;
            this.Description = name;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"[{this.Id}] {this.Description}";
        }

        #endregion
    }
}
