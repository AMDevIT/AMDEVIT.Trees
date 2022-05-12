using AMDEVIT.Trees.Core;
using AMDEVIT.Trees.Tests.Models;

namespace AMDEVIT.Trees.Tests
{
    [TestClass]
    public class NTreeNavigation
    {
        #region Fields

        private NTree<IDDescriptionDataModel>? nTree;

        #endregion

        #region Methods

        [TestInitialize]
        public void InitializeNTree()
        {
            INTreeNode<IDDescriptionDataModel> rootNode;

            INTreeNode<IDDescriptionDataModel> aNode;
            INTreeNode<IDDescriptionDataModel> bNode;

            // A subtree

            INTreeNode<IDDescriptionDataModel> cNode;
            INTreeNode<IDDescriptionDataModel> dNode; 
            INTreeNode<IDDescriptionDataModel> eNode;

            INTreeNode<IDDescriptionDataModel> fNode;
            INTreeNode<IDDescriptionDataModel> gNode;

            // B subtree

            INTreeNode<IDDescriptionDataModel> hNode;
            INTreeNode<IDDescriptionDataModel> iNode;

            INTreeNode<IDDescriptionDataModel> lNode;
            INTreeNode<IDDescriptionDataModel> mNode;


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

            rootNode = new NTreeNode<IDDescriptionDataModel>(new IDDescriptionDataModel("Root node"));
            this.nTree = new NTree<IDDescriptionDataModel>(rootNode);

            // Two leaf for root node.
            aNode = this.nTree.AddNode((INTreeNode<IDDescriptionDataModel>)this.nTree.Root, new IDDescriptionDataModel("A"));
            bNode = this.nTree.AddNode(new IDDescriptionDataModel("B"));

            // A Node 3 leaf

            cNode = this.nTree.AddNode(aNode, new IDDescriptionDataModel("C"));
            dNode = this.nTree.AddNode(aNode, new IDDescriptionDataModel("D"));
            eNode = this.nTree.AddNode(aNode, new IDDescriptionDataModel("E"));

            // E Node 2 leaf

            fNode = this.nTree.AddNode(eNode, new IDDescriptionDataModel("F"));
            gNode = this.nTree.AddNode(eNode, new IDDescriptionDataModel("G"));

            // B node 2 leaf

            hNode = this.nTree.AddNode(bNode, new IDDescriptionDataModel("H"));
            iNode = this.nTree.AddNode(bNode, new IDDescriptionDataModel("I"));

            // I node 2 leaf

            lNode = this.nTree.AddNode(iNode, new IDDescriptionDataModel("L"));
            mNode = this.nTree.AddNode(iNode, new IDDescriptionDataModel("M"));
        }

        [TestMethod]
        public void NavigateTree()
        {
            SortedList<int, INTreeNode<IDDescriptionDataModel>> navigatedItems;

            if (this.nTree != null)
            {
                try
                {
                    navigatedItems = this.nTree.LevelOrderTraversal();
                }
                catch(Exception exc)
                {
                    Console.WriteLine(exc);
                    throw;
                }

                foreach(KeyValuePair<int, INTreeNode<IDDescriptionDataModel>> currenSortedItem in navigatedItems)
                {
                    IDDescriptionDataModel descriptionModel = currenSortedItem.Value.Value;
                    int level = currenSortedItem.Key;
                    Console.WriteLine($"Level {level} - Description: {descriptionModel.Description}");

                    switch(descriptionModel.Description)
                    {
                        case "M":
                        case "L":
                            Assert.AreEqual(level, 3);
                            break;
                    }
                }
            }
        }

        #endregion
    }
}