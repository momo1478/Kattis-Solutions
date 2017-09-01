using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        string firstLine = Console.ReadLine();

        string[] fLine = firstLine.Split(new char[] { ' ' });
        int n = int.Parse(fLine[0]);
        int k = int.Parse(fLine[1]);

        string line; List<Tree> trees = new List<Tree>();
        while ((line = Console.ReadLine()) != null)
        {
            string[] tokens = line.Split(new char[] { ' ' });

            Tree tree = new Tree();

            foreach (string item in tokens)
            {
                tree.Add(int.Parse(item), ref tree.root);
            }

            trees.Add(tree);
        }

        List<Tree> goodTrees = new List<Tree>();
        for (int i = 0; i < trees.Count; i++)
        {
            bool isDistinct = true;
            foreach (Tree tree in goodTrees)
            {
                if (Tree.Equivalent(trees[i].root, tree.root))
                {
                    isDistinct = false;
                }
            }

            if (isDistinct)
                goodTrees.Add(trees[i]);
        }

        Console.WriteLine(goodTrees.Count);

        //Tree myTree = new Tree();
        //Tree myTree2 = new Tree();

        //myTree.Add(5, ref myTree.root);
        //myTree.Add(3, ref myTree.root);
        //myTree.Add(4, ref myTree.root);
        //myTree.Add(2, ref myTree.root);
        //myTree.Add(6, ref myTree.root);
        //myTree.Add(5, ref myTree.root);

        //myTree2.Add(4, ref myTree2.root);
        //myTree2.Add(2, ref myTree2.root);
        //myTree2.Add(3, ref myTree2.root);
        //myTree2.Add(1, ref myTree2.root);
        //myTree2.Add(5, ref myTree2.root);
        //myTree2.Add(4, ref myTree2.root);

        //Console.WriteLine(Tree.Equivalent(myTree.root, myTree2.root));

        //Console.ReadLine();
    }
}

class Node
{
    public Node left;
    public Node right;
    public int value;

    public Node(int iV, Node iL = null, Node iR = null)
    {
        value = iV;
        left = iL;
        right = iR;
    }
}

class Tree
{
    public Node root;

    public Tree(Node iR = null)
    {
        root = iR;
    }

    public void Add(int num, ref Node runner)
    {
        if (runner == null)
        {
            runner = new Node(num);
        }
        else if (num < runner.value)
        {
            Add(num, ref runner.left);
        }
        else // (num >= runner.value)
        {
            Add(num, ref runner.right);
        }
    }

    public static bool Equivalent(Node r1, Node r2)
    {
        //Both trees are empty
        if (r1 == null && r2 == null)
        {
            return true;
        }
        //Both tree aren't empty, check theirs lefts and rights for same structure.
        if(r1 != null && r2 != null)
        {
            return Equivalent(r1.left, r2.left)
                && Equivalent(r1.right, r2.right);
        }
        //Trees aren't the same depth, not similar therefore.
            return false;
    }
}