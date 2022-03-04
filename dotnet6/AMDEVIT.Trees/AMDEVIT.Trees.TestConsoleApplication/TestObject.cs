using AMDEVIT.Trees.Core;
using AMDEVIT.Trees.TestConsoleApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDEVIT.Trees.TestConsoleApplication
{
    internal class TestObject
    {
        #region Consts

        private int DefaultMaxLevelElements = 10;
        private int DefaultMaxDepth = 20;

        #endregion

        #region Fields

        private NTree<ObjectData> tree;

        #endregion

        #region Methods

        public void Initialize()
        {
            ObjectData rootItem = new ObjectData("Root");
            Queue<NTreeNode<ObjectData>> currentGeneratedElementsQueue = new Queue<NTreeNode<ObjectData>>();
            
            int currentDepth = 0;
            int depth = Random.Shared.Next(1, DefaultMaxDepth);
            
            this.tree = NTree<ObjectData>.Create(rootItem);
            

            Console.WriteLine($"Current tree depth: {depth}");
            currentGeneratedElementsQueue.Enqueue(tree.Root);

            while(currentDepth < depth)
            {
                NTreeNode<ObjectData> currentNode;
                List <NTreeNode <ObjectData>> generatedNodes = new List<NTreeNode<ObjectData>> ();

                while (currentGeneratedElementsQueue.Count > 0)
                {
                    currentNode = currentGeneratedElementsQueue.Dequeue();
                    int currentLevelElements = Random.Shared.Next(1, DefaultMaxLevelElements);

                    Console.WriteLine($"Current element: {currentNode?.Data}");
                    Console.WriteLine($"Current element elements: {currentLevelElements}");

                    for (int i = 0; i < currentLevelElements;i++)
                    {
                        ObjectData currentData = new ObjectData($"Current data {i} depth {currentDepth}");
                        NTreeNode<ObjectData> children;
                        children = currentNode.AddChildren(currentData);
                        generatedNodes.Add(children);
                    }
                }

                if (generatedNodes.Count > 0)
                    for (int i1 = 0; i1 < generatedNodes.Count; i1++)
                    {
                        NTreeNode<ObjectData> node = generatedNodes[i1];
                        currentGeneratedElementsQueue.Enqueue(node);
                    }
                currentDepth++;
            }                                    
        }

        public void Print()
        {
            SortedList<int, NTreeNode<ObjectData>> sortedElements;

            sortedElements = this.tree.LevelOrderTraversal();
            for (int i = 0; i < sortedElements.Count;i++)
            {
                NTreeNode<ObjectData> node = sortedElements[i];
                Console.WriteLine($"{node?.Data}");
            }
        }

        #endregion
    }
}
