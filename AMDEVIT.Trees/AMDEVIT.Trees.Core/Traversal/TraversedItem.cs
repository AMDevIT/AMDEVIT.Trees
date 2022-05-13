using System;

namespace AMDEVIT.Trees.Core.Traversal
{
    public class TraversedItem<T>       
        : IComparable<TraversedItem<T>> 
    {
        #region Properties

        public int Level
        {
            get;
            protected set;
        }

        public int Iteraction
        {
            get;
            protected set;
        }

        public ITreeNode<T> Node
        {
            get;
            protected set;
        }

        #endregion

        #region .ctor

        public TraversedItem (ITreeNode<T> node, int level, int iteraction)
        {
            this.Node = node;
            this.Level = level;
            this.Iteraction = iteraction;
        }

        #endregion

        #region Methods

        public int CompareTo(TraversedItem<T> other)
        {
            if (other == null)
                return 1;

            switch (this.Level)
            {
                case int currentLevel when currentLevel < other.Level:
                    return -1;

                case int currentLevel when currentLevel == other.Level:
                    return 0;

                default:
                case int currentLevel when currentLevel > other.Level:
                    return 1;
            }
        }

        public static bool operator < (TraversedItem<T> leftOperand, TraversedItem<T> rightOperand)
        {
            return leftOperand.CompareTo(rightOperand) < 0;
        }

        public static bool operator > (TraversedItem<T> leftOperand, TraversedItem<T> rightOperand)
        {
            return leftOperand.CompareTo(rightOperand) > 0;
        }

        public static bool operator <= (TraversedItem<T> leftOperand, TraversedItem<T> rightOperand)
        {
            return leftOperand.CompareTo(rightOperand) <= 0;
        }

        public static bool operator >= (TraversedItem<T> leftOperand, TraversedItem<T> rightOperand)
        {
            return leftOperand.CompareTo(rightOperand) >= 0;
        }

        public override string ToString()
        {
            return $"Level: {this.Level} Iteraction: {this.Iteraction} Node: {this.Node}";
        }

        #endregion        
    }
}
