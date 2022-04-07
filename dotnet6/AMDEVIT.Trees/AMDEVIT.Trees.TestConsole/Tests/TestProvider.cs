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

        private NTree<TestDataModel>? tree;
        private TestDataModel? hnln;
        private readonly List<TestDataModel> testData = new List<TestDataModel>();

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
            
            INTreeNode<TestDataModel> h1l1Node;
            INTreeNode<TestDataModel> h1l2Node;
            INTreeNode<TestDataModel> h1l3Node;
            INTreeNode<TestDataModel> h2l1Node;
            INTreeNode<TestDataModel> h2l2Node;            
            List<INTreeNode<TestDataModel>> nNodeList = new List<INTreeNode<TestDataModel>>();


            this.tree = NTree<TestDataModel>.Create(rootDataModel);

            h1l1 = new TestDataModel("Height 1, leaf 1");
            h1l2 = new TestDataModel("Height 1, leaf 2");
            h1l3 = new TestDataModel("Height 1, leaf 3");

            h1l1Node = this.tree.AddNode(this.tree.Root, h1l1);
            h1l2Node = this.tree.AddNode(this.tree.Root, h1l2);
            h1l3Node = this.tree.AddNode(this.tree.Root, h1l3);

            this.testData.Add(h1l1);
            this.testData.Add(h1l2);
            this.testData.Add(h1l3);

            // H2 
            h2l1 = new TestDataModel("Height 2, leaf 1");
            h2l2 = new TestDataModel("Height 2, leaf 2");

            h2l1Node = this.tree.AddNode(h1l1Node, h2l1);            
            h2l2Node = this.tree.AddNode(h1l1Node, h2l2);

            this.testData.Add(h2l1);
            this.testData.Add(h2l2);

            // HNLN
            this.hnln = new TestDataModel("Sparse height, sparse leaf");

            // H = 0

            this.tree.AddNode(this.tree.Root, hnln);

            // H = 1

            h1l1Node.AddChild(this.hnln);

            // H = 3

            h2l2Node.AddChild(this.hnln);
            h2l2Node.AddChild(this.hnln);
            h2l2Node.AddChild(this.hnln);
        }

        public void Print()
        {
            if (this.tree != null)
            {
                SortedList<int, INTreeNode<TestDataModel>> sortedTraversalList = this.tree.LevelOrderTraversal();
                TestDataModel searchDataModel;
                TreeSearchOptions searchOptions;
                INTreeNode<TestDataModel>[] foundNodes;
                int searchElementIndex;

                if (sortedTraversalList != null)
                {
                    foreach(KeyValuePair<int, INTreeNode<TestDataModel>> currentElement in sortedTraversalList)
                    {
                        Console.WriteLine($"Index: {currentElement.Key}, Data {currentElement.Value.Value?.ToString()}");
                    }
                }

                searchElementIndex = Random.Shared.Next(0, this.testData.Count - 1);
                searchDataModel = this.testData[searchElementIndex];
                searchOptions = new TreeSearchOptions(TreeSearchMode.First);
                Console.WriteLine($"Serching for {searchDataModel.Name} element in current tree.");
                foundNodes = this.tree.Search(searchDataModel, searchOptions);
                if (foundNodes != null)
                    Console.WriteLine($"Found {foundNodes.Length} number of elements.");
                else
                    Console.WriteLine($"Found no elements.");

                Console.WriteLine("Searching for data that's missing in the tree.");
                searchDataModel = new TestDataModel("Not in tree data model.");

                Console.WriteLine($"Serching for {searchDataModel.Name} element in current tree.");
                foundNodes = this.tree.Search(searchDataModel, searchOptions);
                if (foundNodes != null)
                    Console.WriteLine($"Found {foundNodes.Length} number of elements.");
                else
                    Console.WriteLine($"Found no elements.");

                Console.WriteLine($"Serching for multiple {this.hnln?.Name} elements in current tree.");
                searchOptions = new TreeSearchOptions(TreeSearchMode.AllMatches);
                if (this.hnln != null)
                {
                    foundNodes = this.tree.Search(this.hnln, searchOptions);
                    if (foundNodes != null)
                        Console.WriteLine($"Found {foundNodes.Length} number of elements.");
                    else
                        Console.WriteLine($"Found no elements.");
                }

                Console.WriteLine($"Serching for first single {this.hnln?.Name} elemente in current tree.");
                searchOptions = new TreeSearchOptions(TreeSearchMode.First);
                if (this.hnln != null)
                {
                    foundNodes = this.tree.Search(this.hnln, searchOptions);
                    if (foundNodes != null)
                        Console.WriteLine($"Found {foundNodes.Length} number of elements.");
                    else
                        Console.WriteLine($"Found no elements.");
                }
            }
        }

        #endregion
    }
}
