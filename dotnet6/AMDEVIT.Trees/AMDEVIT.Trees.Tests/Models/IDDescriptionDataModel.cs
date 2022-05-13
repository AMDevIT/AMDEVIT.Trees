namespace AMDEVIT.Trees.Tests.Models
{
    internal class IDDescriptionDataModel
        : IEquatable<IDDescriptionDataModel>    
    {      
        #region Properties

        public int Id
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
            : this(0, name)
        {

        }

        public IDDescriptionDataModel(int id, string name)
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

        public override bool Equals(object? obj)
        {
            if (Object.ReferenceEquals(this, obj))
                return true;

            if (obj is IDDescriptionDataModel)            
                return this.Equals(obj as IDDescriptionDataModel);            
            return false;
        }

        public bool Equals(IDDescriptionDataModel? other)
        {
            if (Object.ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            if (this.Id == other.Id &&
                this.Description.Equals(other.Description))
                return true;
            
            return false;
        }

        public override int GetHashCode()
        {
            int hash;

            hash = base.GetHashCode() ^
                   this.Id.GetHashCode() ^
                   this.Description.GetHashCode();

            return hash;
        }

        public static bool operator == (IDDescriptionDataModel? leftOperator, IDDescriptionDataModel? rightOperator)
        {
            if (System.Object.ReferenceEquals(leftOperator, rightOperator))
                return true;

            if (leftOperator == null || rightOperator == null)
                return false;            
            
            return leftOperator.Equals(rightOperator);
        }

        public static bool operator != (IDDescriptionDataModel? leftOperator, IDDescriptionDataModel? rightOperator)
        {
            return !(leftOperator == rightOperator);    
        }

        #endregion
    }
}
