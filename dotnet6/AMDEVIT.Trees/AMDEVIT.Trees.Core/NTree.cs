using System;
using System.Collections.Generic;

namespace AMDEVIT.Trees.Core
{
    public class NTree<T>
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

        #region .ctor

        public NTree()
        {

        }

        public NTree(T data)
        {
            NTreeNode<T> node;

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null");

            node = new NTreeNode<T>(data);
            this.root = node;
        }

        public NTree(INTreeNode<T> root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root), "Root element cannot be null");

            if (root.Parent != null)
                throw new InvalidOperationException("Provided node element already assigned to a parent.");

            this.root = root;
        }

        #endregion

        #region Methods

        #region Manipulation

        public virtual INTreeNode<T> AddNode(T data)
        {
            INTreeNode<T> newNode = null;

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null.");

            if (this.root == null)
            {
                newNode = new NTreeNode<T>(data);
                this.root = newNode;
            }
            else
            {
                SortedList<int, INTreeNode<T>> levelTraversedElements;

                levelTraversedElements = this.LevelOrderTraversal();

                if (levelTraversedElements != null && levelTraversedElements.Count != 0)
                {
                    int lastElementIndex = levelTraversedElements.Count;
                    INTreeNode<T> lastElement = levelTraversedElements[lastElementIndex];

                    if (lastElement != null)
                        newNode = lastElement.AddChild(data);
                }
            }

            return newNode;
        }

        protected virtual bool AddNode(INTreeNode<T> newNode)
        {
            bool result = false;

            if (newNode == null)
                throw new ArgumentNullException(nameof(newNode), "Node cannot be null.");

            if (this.root == null)
            {
                this.root = newNode;
                result = true;
            }
            else
            {
                SortedList<int, INTreeNode<T>> levelTraversedElements;

                levelTraversedElements = this.LevelOrderTraversal();

                if (levelTraversedElements != null && levelTraversedElements.Count != 0)
                {
                    int lastElementIndex = levelTraversedElements.Count;
                    INTreeNode<T> lastElement = levelTraversedElements[lastElementIndex];

                    if (lastElement != null)
                    {
                        result = lastElement.AttachChild(newNode);
                    }
                }
            }

            return result;
        }

        public virtual INTreeNode<T> AddNode(INTreeNode<T> parent, T data)
        {
            INTreeNode<T> newNode;

            if (parent == null)
                throw new ArgumentNullException(nameof(parent), "Parent node cannot be null if a root element exists.");

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null.");

            newNode = parent.AddChild(data);

            return newNode;
        }

        public bool RemoveNode(INTreeNode<T> parent, INTreeNode<T> child)
        {
            bool result;

            if (parent == null)
                throw new ArgumentNullException(nameof(parent), "Parent node cannot be null if a root element exists.");

            if (child == null)
                throw new ArgumentNullException(nameof(child), "Data cannot be null.");

            result = parent.RemoveChild(child);
            return result;
        }

        #endregion

        #region Traversal and search

        public INTreeNode<T>[] Search(T data, TreeSearchOptions options)
        {
            SortedList<int, INTreeNode<T>> sortedList;
            List<INTreeNode<T>> foundList = new List<INTreeNode<T>>();
            INTreeNode<T>[] foundElements;

            if (options == null)
                options = new TreeSearchOptions();

            sortedList = this.LevelOrderTraversal(true, data);

            if (sortedList != null)
            {
                switch (options.Mode)
                {
                    case TreeSearchMode.AllMatches:
                        foundList.AddRange(sortedList.Values);
                        break;

                    case TreeSearchMode.First:
                        if (sortedList.Count > 0)
                            foundList.Add(sortedList[0]);
                        break;

                    case TreeSearchMode.Last:
                        if (sortedList.Count > 0)
                        {
                            int lastIndex = sortedList.Count - 1;
                            foundList.Add(sortedList[lastIndex]);
                        }
                        break;
                }
            }

            foundElements = foundList.ToArray();
            return foundElements;
        }

        public SortedList<int, INTreeNode<T>> LevelOrderTraversal()
        {
            SortedList<int, INTreeNode<T>> sortedNodes;

            sortedNodes = this.LevelOrderTraversal(false, null);
            return sortedNodes;
        }

        protected virtual SortedList<int, INTreeNode<T>> LevelOrderTraversal(bool search, T value)
        {
            SortedList<int, INTreeNode<T>> sortedNodes = new SortedList<int, INTreeNode<T>>();
            Queue<INTreeNode<T>> traversalQueue;
            int order = 0;

            if (this.Root == null)
                return sortedNodes;

            traversalQueue = new Queue<INTreeNode<T>>();
            traversalQueue.Enqueue(this.Root);

            while (traversalQueue.Count != 0)
            {
                int queueSize = traversalQueue.Count;

                for (int i = 0; i < queueSize; i++)
                {
                    INTreeNode<T> currentNode = traversalQueue.Dequeue();
                    if (currentNode != null)
                    {
                        bool found = false;

                        if (search == false)
                            found = true;
                        else
                        {
                            if (currentNode.Value.Equals(value))
                                found = true;
                        }

                        if (found == true)
                        {
                            sortedNodes.Add(order, currentNode);
                            order++;
                        }

                        for (int k = 0; k < currentNode.Children.Length; k++)
                        {
                            NTreeNode<T> children = currentNode.Children[k] as NTreeNode<T>;
                            if (children != null)
                                traversalQueue.Enqueue(children);
                        }
                    }
                }
            }

            return sortedNodes;
        }

        #endregion

        public static NTree<T> Create(T data)
        {
            NTree<T> tree;

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null");

            tree = new NTree<T>(data);
            return tree;
        }

        #endregion
    }
}