using AMDEVIT.Trees.Core;
using AMDEVIT.Trees.TestConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDEVIT.Trees.TestConsole.Tests
{
    internal class TestProvider
    {
        #region Fields

        private NTree<TestDataModel> tree;

        #endregion

        #region Methods

        public void Initialize()
        {
            TestDataModel rootDataModel = new TestDataModel("Root element.");
            TestDataModel h1l1;
            TestDataModel h1l2;
            TestDataModel h1l3;
            TestDataModel h2l1;
            TestDataModel h2l2;
            NTreeNode<TestDataModel> h1l1Node;
            NTreeNode<TestDataModel> h1l2Node;
            NTreeNode<TestDataModel> h1l3Node;
            NTreeNode<TestDataModel> h2l1Node;
            NTreeNode<TestDataModel> h2l2Node;


            this.tree = NTree<TestDataModel>.Create(rootDataModel);

            h1l1 = new TestDataModel("Height 1, leaf 1");
            h1l2 = new TestDataModel("Height 1, leaf 2");
            h1l3 = new TestDataModel("Height 1, leaf 3");

            h1l1Node = this.tree.AddNode(this.tree.Root, h1l1);
            h1l2Node = this.tree.AddNode(this.tree.Root, h1l2);
            h1l3Node = this.tree.AddNode(this.tree.Root, h1l3);

            // H2 
            h2l1 = new TestDataModel("Height 2, leaf 1");
            h2l2 = new TestDataModel("Height 2, leaf 2");

            h2l1Node = this.tree.AddNode(h1l1Node, h2l1);            
            h2l2Node = this.tree.AddNode(h1l1Node, h2l2);
        }

        public void Print()
        {
            if (this.tree != null)
            {
                SortedList<int, NTreeNode<TestDataModel>> sortedTraversalList = this.tree.GetLevelOrderTraversalList();

                if (sortedTraversalList != null)
                {
                    foreach(KeyValuePair<int, NTreeNode<TestDataModel>> currentElement in sortedTraversalList)
                    {
                        Console.WriteLine($"Index: {currentElement.Key}, Data {currentElement.Value.Data?.ToString()}");
                    }
                }
            }
        }

        #endregion
    }
}
