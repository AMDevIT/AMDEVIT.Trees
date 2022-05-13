using AMDEVIT.Trees.Core.Traversal;
using System;
using System.Collections.Generic;

namespace AMDEVIT.Trees.Core
{
    public class NTree<T> 
        : INTree<T> 
        where T : class
    {
        #region Fields

        private INTreeNode<T> root;

        #endregion

        #region Properties

        public ITreeNode<T> Root
        {
            get
            {
                return this.root;
            }
            protected set
            {
                INTreeNode<T> newValue;

                if (value != null && value.GetType() != typeof(INTreeNode<T>))
                    throw new InvalidOperationException("Value must be a NTree node");

                newValue = value as INTreeNode<T>;
                this.root = newValue;
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

        public virtual INTreeNode<T> AddNode(T data, AttachMode attachMode = AttachMode.AttachToLastLevel)
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
                switch (attachMode)
                {
                    case AttachMode.AttachToRoot:
                        {
                            INTreeNode<T> currentRoot = (INTreeNode <T>) this.Root;
                            newNode = currentRoot.AddChild(data);
                        }
                        break;

                    case AttachMode.AttachToLastLevel:
                        {
                            TraversedItem<T>[] levelTraversedElements;

                            levelTraversedElements = this.LevelOrderTraversal();

                            if (levelTraversedElements != null && levelTraversedElements.Length != 0)
                            {
                                int lastElementIndex = levelTraversedElements.Length - 1;
                                INTreeNode<T> lastElement = (INTreeNode<T>)levelTraversedElements[lastElementIndex].Node;

                                if (lastElement != null)
                                    newNode = lastElement.AddChild(data);
                            }                            
                        }
                        break;
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
                TraversedItem<T>[] levelTraversedElements;

                levelTraversedElements = this.LevelOrderTraversal();

                if (levelTraversedElements != null && levelTraversedElements.Length != 0)
                {
                    int lastElementIndex = levelTraversedElements.Length - 1;
                    INTreeNode<T> lastElement = (INTreeNode<T>) levelTraversedElements[lastElementIndex];

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

        public ITreeNode<T>[] Search(T data, TreeSearchOptions options)
        {
            return this.Search(data, options, (T nodeValue) =>
            {
                return nodeValue.Equals(data);
            });
        }

        public ITreeNode<T>[] Search(T data, TreeSearchOptions options, Func<T, bool> searchPattern)
        {
            TraversedItem<T>[] traversedItems;
            List<INTreeNode<T>> foundList = new List<INTreeNode<T>>();
            INTreeNode<T>[] foundElements;

            if (options == null)
                options = new TreeSearchOptions();

            if (searchPattern == null)
                traversedItems = this.LevelOrderTraversal(null, null);
            else
            {

                traversedItems = this.LevelOrderTraversal((INTreeNode<T> currentNode) =>
                {             
                    return searchPattern.Invoke(currentNode?.Value);
                },
                options.Mode);
            }

            if (traversedItems != null)
            {
                switch (options.Mode)
                {
                    case TreeSearchMode.AllMatches:
                        for (int i = 0; i < traversedItems.Length; i++)
                        {
                            TraversedItem<T> item = traversedItems[i];
                            foundList.Add((INTreeNode<T>)item.Node);
                        }
                        break;

                    case TreeSearchMode.First:
                        if (traversedItems.Length > 0)
                            foundList.Add((INTreeNode<T>)traversedItems[0].Node);
                        break;

                    case TreeSearchMode.Last:
                        if (traversedItems.Length > 0)
                        {
                            int lastIndex = traversedItems.Length - 1;
                            foundList.Add((INTreeNode<T>)traversedItems[lastIndex].Node);
                        }
                        break;
                }
            }

            foundElements = foundList.ToArray();
            return foundElements;
        }

        public TraversedItem<T>[] LevelOrderTraversal()
        {
            TraversedItem<T>[] sortedNodes;

            sortedNodes = this.LevelOrderTraversal(null, null);
            return sortedNodes;
        }

        // protected virtual TraversedItem<T>[] LevelOrderTraversal(bool search, T value, Func<T, INTreeNode<T>> searchPatternHandler)
        protected virtual TraversedItem<T>[] LevelOrderTraversal(Func<INTreeNode<T>, bool> searchPatternHandler, TreeSearchMode? searchMode)
        {
            List<TraversedItem<T>> sortedNodes = new List<TraversedItem<T>>();
            Queue<TraversalStackItem<T>> traversalQueue;
            INTreeNode<T> currentRoot;
            int iteractions = 0;
            // int level = 0;

            if (this.Root == null)
                return sortedNodes.ToArray();

            currentRoot = (INTreeNode<T>)this.Root;
            traversalQueue = new Queue<TraversalStackItem<T>>();            
            traversalQueue.Enqueue(new TraversalStackItem<T>(0, currentRoot));

            while (traversalQueue.Count != 0)
            {
                int queueSize = traversalQueue.Count;                

                for (int i = 0; i < queueSize; i++)
                {
                    TraversalStackItem<T> currentTraversalStackItem;
                    INTreeNode<T> currentNode;

                    iteractions++;
                    currentTraversalStackItem = traversalQueue.Dequeue();
                    currentNode = (INTreeNode<T>)currentTraversalStackItem.Node;

                    if (currentNode != null)
                    {
                        bool found;

                        if (searchPatternHandler != null)
                            found = searchPatternHandler(currentNode);
                        else
                            found = true;

                        //if (search == false)
                        //    found = true;
                        //else
                        //{
                        // Search using search pattern handler

                        // if (currentNode.Value.Equals(value))
                        //    found = true;                            
                        //}

                        if (found == true)
                        {
                            sortedNodes.Add(new TraversedItem<T>(currentNode, currentTraversalStackItem.Level, iteractions));
                            if (searchPatternHandler != null && searchMode.HasValue && searchMode == TreeSearchMode.First)
                            {
                                // Break out the while and the for.
                                traversalQueue.Clear();
                                break;                                
                            }
                        }

                        for (int k = 0; k < currentNode.Children.Length; k++)
                        {
                            NTreeNode<T> children = currentNode.Children[k] as NTreeNode<T>;
                            if (children != null)
                            {
                                TraversalStackItem<T> childrenTraversalStackItem;
                                childrenTraversalStackItem = new TraversalStackItem<T>(currentTraversalStackItem.Level + 1, children);
                                traversalQueue.Enqueue(childrenTraversalStackItem);
                            }
                        }
                    }
                }
            }

            sortedNodes.Sort();             // Sort elements first.
            return sortedNodes.ToArray();
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