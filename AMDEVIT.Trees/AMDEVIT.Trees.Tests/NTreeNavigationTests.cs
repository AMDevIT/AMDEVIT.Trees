using AMDEVIT.Trees.Core;
using AMDEVIT.Trees.Core.Traversal;
using AMDEVIT.Trees.Tests.Models;
using System.Diagnostics;
using System.Text;

namespace AMDEVIT.Trees.Tests
{
    [TestClass]
    public class NTreeNavigationTests
    {
        #region Fields

        private NTree<IDDescriptionDataModel>? nTree;

        private INTreeNode<IDDescriptionDataModel>? rootNode;

        private INTreeNode<IDDescriptionDataModel>? aNode;
        private INTreeNode<IDDescriptionDataModel>? bNode;

        // A subtree

        private INTreeNode<IDDescriptionDataModel>? cNode;
        private INTreeNode<IDDescriptionDataModel>? dNode;
        private INTreeNode<IDDescriptionDataModel>? eNode;

        private INTreeNode<IDDescriptionDataModel>? fNode;
        private INTreeNode<IDDescriptionDataModel>? gNode;

        // B subtree

        private INTreeNode<IDDescriptionDataModel>? hNode;
        private INTreeNode<IDDescriptionDataModel>? iNode;

        private INTreeNode<IDDescriptionDataModel>? lNode;
        private INTreeNode<IDDescriptionDataModel>? mNode;

        #endregion

        #region Methods

        [TestInitialize]        
        public void InitializeNTree()
        {
            Console.WriteLine("Initializing values.");

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

            // Check for null. Just security and for remove warning of unused variables.

            Assert.IsNotNull(this.rootNode, "Root node is null.");
            Assert.IsNotNull(this.nTree, "NTree is null.");
            Assert.IsNotNull(this.aNode, "A node is null.");
            Assert.IsNotNull(this.bNode, "B node is null.");
            Assert.IsNotNull(this.cNode, "C node is null.");
            Assert.IsNotNull(this.dNode, "D node is null.");
            Assert.IsNotNull(this.eNode, "E node is null.");
            Assert.IsNotNull(this.fNode, "F node is null.");
            Assert.IsNotNull(this.gNode, "G node is null.");
            Assert.IsNotNull(this.hNode, "H node is null.");
            Assert.IsNotNull(this.iNode, "I node is null.");
            Assert.IsNotNull(this.lNode, "L node is null.");
            Assert.IsNotNull(this.mNode, "M node is null.");
        }

        [TestMethod("Validate node data comparison using equals.")]
        public void Test1ValidateNodeDataComparison()
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
        

        [TestMethod("Navigate full tree using known traversal algorithms.")]
        public void Test2NavigateTree()
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

        [TestMethod("Split the main tree into two subtree.")]
        public void Test3SplitIntoSubTree()
        {
            ITree<IDDescriptionDataModel>? nodeESubTree = null;
            ITree<IDDescriptionDataModel>? nodeISubTree = null;            

            Console.WriteLine($"NTree subtree split on elements \"E\" and \"I\". {Environment.NewLine}");
            Assert.IsNotNull(this.nTree, "NTree is null");
            if (this.nTree != null)
            {
                TreeSearchOptions treeSearchOptions = new TreeSearchOptions(TreeSearchMode.AllMatches);
                ITreeNode<IDDescriptionDataModel>[] allMatchesE;
                ITreeNode<IDDescriptionDataModel>[] allMatchesI;

                allMatchesE = this.nTree.Search(new IDDescriptionDataModel(0, "E"), treeSearchOptions);
                Assert.IsNotNull(allMatchesE);

                if (allMatchesE != null)
                {
                    Console.WriteLine($"Found {allMatchesE.Length} elements that match \"E\". Must be 1.");
                    Assert.AreEqual(allMatchesE.Length, 1, "Elements that matches \"E\" are not only one.");
                    for (int i = 0; i < allMatchesE.Length; i++)
                    {
                        INTreeNode<IDDescriptionDataModel> currentNode = (INTreeNode<IDDescriptionDataModel>) allMatchesE[i];
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

        [TestMethod("Search for node F e M")]
        public void Test4SearchForANode()
        {
            Stopwatch searchTimer = new Stopwatch();
            TreeSearchOptions allMatchesTreeSearchOptions;
            ITreeNode<IDDescriptionDataModel>[] fNodeEqualsPatternSearchResult;
            ITreeNode<IDDescriptionDataModel>[] mNodeEqualsPatternSearchResult;

            Console.WriteLine("Search for nodes F e M using equals and special search pattern.");
            Assert.IsNotNull(this.nTree, "NTree is null.");
            Assert.IsNotNull(this.rootNode, "Root node is null.");

            allMatchesTreeSearchOptions = new TreeSearchOptions(TreeSearchMode.AllMatches);
            searchTimer.Start();
            fNodeEqualsPatternSearchResult = this.nTree.Search(new IDDescriptionDataModel(0, "F"), allMatchesTreeSearchOptions);
            searchTimer.Stop();
            Console.WriteLine($"Search time F node, equas default pattern: {searchTimer.ElapsedMilliseconds} ms.");
            Assert.IsNotNull(fNodeEqualsPatternSearchResult);
            Assert.IsTrue(fNodeEqualsPatternSearchResult.Length > 0, "No nodes F found using default equals pattern.");
            searchTimer.Reset();

            searchTimer.Start();
            mNodeEqualsPatternSearchResult = this.nTree.Search(new IDDescriptionDataModel(0, "M"), allMatchesTreeSearchOptions);
            searchTimer.Stop();
            Console.WriteLine($"Search time M node, equas default pattern: {searchTimer.ElapsedMilliseconds} ms.");
            Assert.IsNotNull(mNodeEqualsPatternSearchResult);
            Assert.IsTrue(mNodeEqualsPatternSearchResult.Length > 0, "No nodes M found using default equals pattern.");
            searchTimer.Reset();
        }

        [TestMethod("High volume random search test")]        
        public void Test5HighVolumeRandomSearchTest()
        {
            Stopwatch stopwatch = new Stopwatch();
            Random randomForID = new Random();
            Random randomForSearch = new Random();
            Random randomForAttach = new Random();
            List<INTreeNode<IDDescriptionDataModel>> nodesList;
            INTreeNode<IDDescriptionDataModel> searchNode;
            NTree <IDDescriptionDataModel> highVolumTestTree;
            int numberOfNodes;
            int searchIndex;
            int maxNodes = 20000;

            Console.WriteLine("Initializing high volume tree.");
            highVolumTestTree = new NTree<IDDescriptionDataModel>(new IDDescriptionDataModel(0, "Root node"));
            // Root node is excluded.
            nodesList = new List<INTreeNode<IDDescriptionDataModel>>();            
            numberOfNodes = randomForSearch.Next(1, maxNodes);

            Console.WriteLine($"Will be generated {numberOfNodes} nodes randomly.");

            stopwatch.Start();
            for (int i = 0; i < numberOfNodes; i++)
            {
                IDDescriptionDataModel currentDataModel;
                INTreeNode<IDDescriptionDataModel> newNode;
                int id;
                string currentName;
                int attachMode;

                id = randomForID.Next();
                currentName = this.GenerateRandomName();
                currentDataModel = new IDDescriptionDataModel(id, currentName);
                attachMode = randomForAttach.Next(0, 2);

                switch (attachMode)
                {
                    case 0:
                        // Attach to root
                        newNode = highVolumTestTree.AddNode(currentDataModel, AttachMode.AttachToRoot);
                        break;

                    default:
                    case 1:
                        // Attach to latest
                        newNode = highVolumTestTree.AddNode(currentDataModel, AttachMode.AttachToLastLevel);
                        break;

                    case 2:
                        {
                            // Attach to random:
                            if (nodesList.Count > 1)
                            {
                                int randomNodeIndex = randomForAttach.Next(0, nodesList.Count - 1);
                                INTreeNode<IDDescriptionDataModel> selectedAttachNode;

                                selectedAttachNode = nodesList[randomNodeIndex];
                                newNode = highVolumTestTree.AddNode(selectedAttachNode, currentDataModel);
                            }
                            else
                                newNode = highVolumTestTree.AddNode(currentDataModel);
                        }
                        break;
                }                
                nodesList.Add(newNode);
            }
            stopwatch.Stop();

            Console.WriteLine($"Added {nodesList.Count} nodes in {stopwatch.ElapsedMilliseconds} ms.");

            searchIndex = randomForSearch.Next(0, nodesList.Count - 1);
            Console.WriteLine($"Selected node number {searchIndex} for search request.");
            searchNode = nodesList[searchIndex];

            if (searchNode != null)
            {
                IDDescriptionDataModel valueData = new IDDescriptionDataModel(searchNode.Value.Id,
                                                                              searchNode.Value.Description);
                TreeSearchOptions treeSearchOptions = new TreeSearchOptions(TreeSearchMode.AllMatches);
                ITreeNode<IDDescriptionDataModel>[] refSearchResult;
                ITreeNode<IDDescriptionDataModel>[] valueSearchResult;

                // Search for reference
                stopwatch.Start();
                refSearchResult = highVolumTestTree.Search(searchNode.Value, treeSearchOptions);
                stopwatch.Stop();

                Assert.IsNotNull(refSearchResult, "Reference result data is null.");
                Assert.IsTrue(refSearchResult.Length > 0, "Reference result data have no matches.");

                Console.WriteLine($"Found {refSearchResult.Length} nodes using reference value in {stopwatch.ElapsedMilliseconds} ms.");
                for (int i = 0; i < refSearchResult.Length; i++)
                {
                    ITreeNode<IDDescriptionDataModel> currentTreeNode = refSearchResult[i];
                    ITreeNode<IDDescriptionDataModel> levelNode = currentTreeNode;
                    int level = 0;

                    while (levelNode.Parent != null)
                    {
                        if (levelNode.Parent != null)
                        {
                            levelNode = levelNode.Parent;
                            level++;
                        }
                    }

                    Console.WriteLine($"Item found, level {level} - ID: {currentTreeNode.Value.Id}, description: {currentTreeNode.Value.Description}.");
                }

                // Search for value
                stopwatch.Start();
                valueSearchResult = highVolumTestTree.Search(valueData, treeSearchOptions);
                stopwatch.Stop();

                Assert.IsNotNull(valueSearchResult, "Value result data is null.");
                Assert.IsTrue(valueSearchResult.Length > 0, "Value result data have no matches.");

                Console.WriteLine($"Found {valueSearchResult.Length} nodes using new instance value in {stopwatch.ElapsedMilliseconds} ms.");
                for (int i = 0; i < valueSearchResult.Length; i++)
                {
                    ITreeNode<IDDescriptionDataModel> currentTreeNode = valueSearchResult[i];
                    ITreeNode<IDDescriptionDataModel> levelNode = currentTreeNode;
                    int level = 0;
                    
                    while(levelNode.Parent != null)
                    {
                        if (levelNode.Parent != null)
                        {
                            levelNode = levelNode.Parent;
                            level++;
                        }
                    }

                    Console.WriteLine($"Item found, level {level}- ID: {currentTreeNode.Value.Id}, description: {currentTreeNode.Value.Description}.");
                }
            }

            Console.WriteLine("Add random duplicated nodes of current selected.");
            if (searchNode != null)
            {
                // Create another context.
                int numberOfDuplicates = 50;                
                for (int i = 0; i < numberOfDuplicates; i++)
                {
                    IDDescriptionDataModel newReferenceData = new IDDescriptionDataModel(searchNode.Value.Id,
                                                                                         searchNode.Value.Description);
                    int randomAttachNode = randomForAttach.Next(0, nodesList.Count - 1);
                    INTreeNode<IDDescriptionDataModel> currentAttachTreeNode = nodesList[randomAttachNode];
                    INTreeNode<IDDescriptionDataModel> attachedNode;

                    attachedNode = highVolumTestTree.AddNode(currentAttachTreeNode, newReferenceData);
                    nodesList.Add(attachedNode);
                    Console.WriteLine($"Attached node {newReferenceData.Id}, {newReferenceData.Description} to node {currentAttachTreeNode.Value.Id}" +
                                      $", {currentAttachTreeNode.Value.Description}.");
                }

                // Search for duplicates.

                IDDescriptionDataModel valueData = new IDDescriptionDataModel(searchNode.Value.Id,
                                                                              searchNode.Value.Description);
                TreeSearchOptions treeSearchOptions = new TreeSearchOptions(TreeSearchMode.AllMatches);
                TreeSearchOptions treeFirstMatchSearchOptions = new TreeSearchOptions(TreeSearchMode.First);
                TreeSearchOptions treeLastMatchSearchOptions = new TreeSearchOptions(TreeSearchMode.Last);
                ITreeNode<IDDescriptionDataModel>[] refSearchResult;
                ITreeNode<IDDescriptionDataModel>[] valueSearchResult;
                ITreeNode<IDDescriptionDataModel>[] valueSearchFirstResult;
                ITreeNode<IDDescriptionDataModel>[] valueSearchLastResult;

                // Search for reference using matchall
                stopwatch.Start();
                refSearchResult = highVolumTestTree.Search(searchNode.Value, treeSearchOptions);
                stopwatch.Stop();

                Assert.IsNotNull(refSearchResult, "Reference result data is null.");
                Assert.IsTrue(refSearchResult.Length > 0, "Reference result data have no matches.");

                Console.WriteLine($"Found {refSearchResult.Length} nodes using reference value in {stopwatch.ElapsedMilliseconds} ms.");
                for (int i = 0; i < refSearchResult.Length; i++)
                {
                    ITreeNode<IDDescriptionDataModel> currentTreeNode = refSearchResult[i];
                    ITreeNode<IDDescriptionDataModel> levelNode = currentTreeNode;
                    int level = 0;

                    while (levelNode.Parent != null)
                    {
                        if (levelNode.Parent != null)
                        {
                            levelNode = levelNode.Parent;
                            level++;
                        }
                    }

                    Console.WriteLine($"Item found, level {level} - ID: {currentTreeNode.Value.Id}, description: {currentTreeNode.Value.Description}.");
                }

                // Search for value using matchall
                stopwatch.Start();
                valueSearchResult = highVolumTestTree.Search(valueData, treeSearchOptions);
                stopwatch.Stop();

                Assert.IsNotNull(valueSearchResult, "Value result data is null.");
                Assert.IsTrue(valueSearchResult.Length > 0, "Value result data have no matches.");

                Console.WriteLine($"Found {valueSearchResult.Length} nodes using new instance value in {stopwatch.ElapsedMilliseconds} ms.");
                for (int i = 0; i < valueSearchResult.Length; i++)
                {
                    ITreeNode<IDDescriptionDataModel> currentTreeNode = valueSearchResult[i];
                    ITreeNode<IDDescriptionDataModel> levelNode = currentTreeNode;
                    int level = 0;

                    while (levelNode.Parent != null)
                    {
                        if (levelNode.Parent != null)
                        {
                            levelNode = levelNode.Parent;
                            level++;
                        }
                    }

                    Console.WriteLine($"Item found, level {level}- ID: {currentTreeNode.Value.Id}, description: {currentTreeNode.Value.Description}.");
                }

                // Search for value using first
                stopwatch.Start();
                valueSearchFirstResult = highVolumTestTree.Search(valueData, treeFirstMatchSearchOptions);
                stopwatch.Stop();

                Assert.IsNotNull(valueSearchFirstResult, "Value result data first is null.");
                Assert.IsTrue(valueSearchFirstResult.Length > 0, "Value result data first have no matches.");

                Console.WriteLine($"Found {valueSearchFirstResult.Length} nodes using match first new instance value in {stopwatch.ElapsedMilliseconds} ms.");
                for (int i = 0; i < valueSearchFirstResult.Length; i++)
                {
                    ITreeNode<IDDescriptionDataModel> currentTreeNode = valueSearchFirstResult[i];
                    ITreeNode<IDDescriptionDataModel> levelNode = currentTreeNode;
                    int level = 0;

                    while (levelNode.Parent != null)
                    {
                        if (levelNode.Parent != null)
                        {
                            levelNode = levelNode.Parent;
                            level++;
                        }
                    }

                    Console.WriteLine($"Item found, level {level}- ID: {currentTreeNode.Value.Id}, description: {currentTreeNode.Value.Description}.");
                }

                // Search for value using last
                stopwatch.Start();
                valueSearchLastResult = highVolumTestTree.Search(valueData, treeLastMatchSearchOptions);
                stopwatch.Stop();

                Assert.IsNotNull(valueSearchLastResult, "Value result data last is null.");
                Assert.IsTrue(valueSearchLastResult.Length > 0, "Value result data last have no matches.");

                Console.WriteLine($"Found {valueSearchLastResult.Length} nodes using match first new instance value in {stopwatch.ElapsedMilliseconds} ms.");
                for (int i = 0; i < valueSearchLastResult.Length; i++)
                {
                    ITreeNode<IDDescriptionDataModel> currentTreeNode = valueSearchLastResult[i];
                    ITreeNode<IDDescriptionDataModel> levelNode = currentTreeNode;
                    int level = 0;

                    while (levelNode.Parent != null)
                    {
                        if (levelNode.Parent != null)
                        {
                            levelNode = levelNode.Parent;
                            level++;
                        }
                    }

                    Console.WriteLine($"Item found, level {level}- ID: {currentTreeNode.Value.Id}, description: {currentTreeNode.Value.Description}.");
                }
            }
        }

        private string GenerateRandomName()
        {
            char[] letters =
            {
                'A',
                'B',
                'C',
                'D',
                'E',
                'F',
                'G',
                'H',
                'I',
                'L',
                'M',
                'N',
                'O',
                'P',
                'Q',
                'R',
                'S',
                'T',
                'U',
                'V',
                'W',
                'X',
                'Y',
                'Z',
            };
            string? name = null;
            Random currentRandom = new Random();
            int stringSize;

            stringSize = currentRandom.Next(1, 5);

            for (int i = 0; i < stringSize; i++)
            {
                int randomLetterIndex;
                char currentChar;

                randomLetterIndex = currentRandom.Next(0, letters.Length - 1);
                currentChar = letters[randomLetterIndex];
                if (string.IsNullOrEmpty(name))
                    name = $"{currentChar}";
                else
                    name += $"{currentChar}";
            }

            if (string.IsNullOrEmpty(name))
                name = $"{letters[stringSize]}";
            return name;
        }

        #endregion
    }
}