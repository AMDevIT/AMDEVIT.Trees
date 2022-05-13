using AMDEVIT.Trees.Core;
using AMDEVIT.Trees.Core.Traversal;
using AMDEVIT.Trees.Tests.Models;

namespace AMDEVIT.Trees.Tests
{
    [TestClass]
    public class NTreeNavigationTests
    {
        #region Fields

        private NTree<IDDescriptionDataModel>? nTree;

        private INTreeNode<IDDescriptionDataModel> rootNode;

        private INTreeNode<IDDescriptionDataModel> aNode;
        private INTreeNode<IDDescriptionDataModel> bNode;

        // A subtree

        private INTreeNode<IDDescriptionDataModel> cNode;
        private INTreeNode<IDDescriptionDataModel> dNode;
        private INTreeNode<IDDescriptionDataModel> eNode;

        private INTreeNode<IDDescriptionDataModel> fNode;
        private INTreeNode<IDDescriptionDataModel> gNode;

        // B subtree

        private INTreeNode<IDDescriptionDataModel> hNode;
        private INTreeNode<IDDescriptionDataModel> iNode;

        private INTreeNode<IDDescriptionDataModel> lNode;
        private INTreeNode<IDDescriptionDataModel> mNode;

        #endregion

        #region Methods

        [TestInitialize]        
        public void InitializeNTree()
        {
            // Tree:
            //
            //          Root Node
            //          /       \
            //         /         \
            //        A           B
            //        |          / \
            //     ------       H   I
            //     |  |  |          |\
            //     C  D  E          L M
            //           |\
            //           F G

            // Root node

            this.rootNode = new NTreeNode<IDDescriptionDataModel>(new IDDescriptionDataModel("Root node"));
            this.nTree = new NTree<IDDescriptionDataModel>(rootNode);

            // Two leaf for root node.
            this.aNode = this.nTree.AddNode((INTreeNode<IDDescriptionDataModel>)this.nTree.Root, new IDDescriptionDataModel("A"));
            this.bNode = this.nTree.AddNode(new IDDescriptionDataModel("B"), AttachMode.AttachToRoot);

            // A Node 3 leaf

            this.cNode = this.nTree.AddNode(aNode, new IDDescriptionDataModel("C"));
            this.dNode = this.nTree.AddNode(aNode, new IDDescriptionDataModel("D"));
            this.eNode = this.nTree.AddNode(aNode, new IDDescriptionDataModel("E"));

            // E Node 2 leaf

            this.fNode = this.nTree.AddNode(eNode, new IDDescriptionDataModel("F"));
            this.gNode = this.nTree.AddNode(eNode, new IDDescriptionDataModel("G"));

            // B node 2 leaf

            this.hNode = this.nTree.AddNode(bNode, new IDDescriptionDataModel("H"));
            this.iNode = this.nTree.AddNode(bNode, new IDDescriptionDataModel("I"));

            // I node 2 leaf

            this.lNode = this.nTree.AddNode(iNode, new IDDescriptionDataModel("L"));
            this.mNode = this.nTree.AddNode(iNode, new IDDescriptionDataModel("M"));
        }

        [TestMethod]
        public void ValidateNodeDataComparison()
        {
            IDDescriptionDataModel hReferenceValue = new IDDescriptionDataModel(0, "H");
            IDDescriptionDataModel lReferenceValue = new IDDescriptionDataModel(0, "L");
            IDDescriptionDataModel mReferenceValue = new IDDescriptionDataModel(0, "M");
            bool referenceCheck;

            // Check for data comparison.
            Assert.IsNotNull(this.hNode, "H Node is actually null.");
            Assert.IsNotNull(this.lNode, "L Node is actually null.");
            Assert.IsNotNull(this.mNode, "M Node is actually null.");

            // Referencecheck for Equals method.
            referenceCheck = hReferenceValue.Equals(this.hNode.Value);
            Assert.IsTrue(referenceCheck, "Reference check for equals method is false.");

            Assert.AreEqual(hReferenceValue, this.hNode.Value, "H Node value is not equal to H reference value");
            Assert.AreEqual(lReferenceValue, this.lNode.Value, "L Node value is not equal to L reference value");
            Assert.AreEqual(mReferenceValue, this.mNode.Value, "M Node value is not equal to M reference value");

        }

        public void SearchForANode()
        {

        }

        [TestMethod]
        public void NavigateTree()
        {
            TraversedItem<IDDescriptionDataModel>[] traversedItems;

            Console.WriteLine($"NTree level-order traversal test. {Environment.NewLine}");
            Assert.IsNotNull(this.nTree, "NTree is null");
            if (this.nTree != null)
            {
                try
                {
                    traversedItems = this.nTree.LevelOrderTraversal();
                }
                catch(Exception exc)
                {
                    Console.WriteLine(exc);
                    throw;
                }

                Assert.IsNotNull(traversedItems);

                if (traversedItems != null)
                {
                    Console.WriteLine("Check levels correspondency.");
                    for (int i = 0; i < traversedItems.Length; i++)
                    {
                        TraversedItem<IDDescriptionDataModel> currenSortedItem = traversedItems[i];
                        IDDescriptionDataModel descriptionModel = currenSortedItem.Node.Value;
                        int level = currenSortedItem.Level;
                        Console.WriteLine($"Level {level} - Description: {descriptionModel.Description}({descriptionModel.Id})");

                        switch (descriptionModel.Description)
                        {
                            case "A":
                            case "B":
                                Assert.AreEqual(level, 1, "A and B element are not level 1");
                                break;

                            case "C":   
                            case "D":   
                            case "E":
                            case "H":
                            case "I":
                                Assert.AreEqual(level, 2, "C,D,E,H,I are not level 2");
                                break;

                            case "F":
                            case "G":
                            case "M":
                            case "L":
                                Assert.AreEqual(level, 3, "F,G,M,L are not level 3");
                                break;
                        }
                    }
                }
                else
                    Console.WriteLine("Traversed items are null.");
            }            
        }

        [TestMethod]
        public void SplitIntoSubTree()
        {
            ITree<IDDescriptionDataModel>? nodeESubTree = null;
            ITree<IDDescriptionDataModel>? nodeISubTree = null;            

            Console.WriteLine($"NTree subtree split on elements \"E\" and \"I\". {Environment.NewLine}");
            Assert.IsNotNull(this.nTree, "NTree is null");
            if (this.nTree != null)
            {
                TreeSearchOptions treeSearchOptions = new TreeSearchOptions(TreeSearchMode.AllMatches);
                INTreeNode<IDDescriptionDataModel>[] allMatchesE;
                ITreeNode<IDDescriptionDataModel>[] allMatchesI;

                allMatchesE = this.nTree.Search(new IDDescriptionDataModel(0, "E"), treeSearchOptions);
                Assert.IsNotNull(allMatchesE);

                if (allMatchesE != null)
                {
                    Console.WriteLine($"Found {allMatchesE.Length} elements that match \"E\". Must be 1.");
                    Assert.AreEqual(allMatchesE.Length, 1, "Elements that matches \"E\" are not only one.");
                    for (int i = 0; i < allMatchesE.Length; i++)
                    {
                        INTreeNode<IDDescriptionDataModel> currentNode = allMatchesE[i];
                        Console.WriteLine("Creating subtree from E.");
                        nodeESubTree = currentNode.CreateSubTree();
                    }
                }

                allMatchesI = this.nTree.Search(new IDDescriptionDataModel(0, "I"), treeSearchOptions);
                Assert.IsNotNull(allMatchesI);

                if (allMatchesI != null)
                {
                    Console.WriteLine($"Found {allMatchesI.Length} elements that match \"I\". Must be 1.");
                    Assert.AreEqual(allMatchesI.Length, 1, "Elements that matches \"I\" are not only one.");
                    for (int i = 0; i < allMatchesI.Length; i++)
                    {
                        INTreeNode<IDDescriptionDataModel> currentNode = (INTreeNode<IDDescriptionDataModel>)allMatchesI[i];
                        Console.WriteLine("Creating subtree from E.");
                        nodeISubTree = currentNode.CreateSubTree();
                    }
                }
            }

            Assert.IsNotNull(nodeESubTree, "Subtree from E node is null.");
            Assert.IsNotNull(nodeISubTree, "Subtree from I node is null.");

            if (nodeESubTree != null)
            {
                TraversedItem<IDDescriptionDataModel>[] traversedItems;

                traversedItems = nodeESubTree.LevelOrderTraversal();
                Assert.IsNotNull(traversedItems, "Traversed items for Subtree E is null.");
                Console.WriteLine($"Printing subtree E.{Environment.NewLine}");
                for (int i = 0; i < traversedItems.Length; i++)
                {
                    TraversedItem<IDDescriptionDataModel> item = traversedItems[i];
                    Console.WriteLine($"Level {item.Level} - Description: {item.Node.Value?.Description}({item.Node.Value?.Id})");
                }
            }
            Console.WriteLine();
            if (nodeISubTree != null)
            {
                TraversedItem<IDDescriptionDataModel>[] traversedItems;

                traversedItems = nodeISubTree.LevelOrderTraversal();
                Assert.IsNotNull(traversedItems, "Traversed items for Subtree I is null.");
                Console.WriteLine($"Printing subtree I.{Environment.NewLine}");
                for (int i = 0; i < traversedItems.Length; i++)
                {
                    TraversedItem<IDDescriptionDataModel> item = traversedItems[i];
                    Console.WriteLine($"Level {item.Level} - Description: {item.Node.Value?.Description}({item.Node.Value?.Id})");
                }
            }
            Console.WriteLine();
        }

        #endregion
    }
}