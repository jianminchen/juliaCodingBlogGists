using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zigZagOrder
{
    public class TreeNode
    {
        public int      val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }

    /// <summary>
    /// code review May 3, 2018
    /// I spent 30 minutes to go over C# code on this zigzag traversal algorithm. 
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var root = new TreeNode(3);

            root.left  = new TreeNode(9);
            root.right = new TreeNode(20);

            root.right.left  = new TreeNode(15);
            root.right.right = new TreeNode(7);

            var output = zigzagOutput(root);
        }

        /*
         * http://rleetcode.blogspot.ca/2014/02/binary-tree-zigzag-level-order.html
         * Time Complexity is O(n)
           Take advantage of two stacks. One is used to hold current level's node, 
           another one is used to hold next level's hold. Moreover, there is a flag 
           variable used to mark the sequence (left to rigth or right to left) put 
           the root first into current stack then pop it out, put left and right into 
           next_level stack (pay attention to sequence). Once current stack is empty, 
           swap current and next level, increment variable level, reverse sequence. 
         */
        public static IList<IList<int>> zigzagOutput(TreeNode root)
        {
            var zigzagLevelNumbers = new List<IList<int>>();

            if (root == null)
                return zigzagLevelNumbers;

            var currentStack = new Stack<TreeNode>();
            var nextStack    = new Stack<TreeNode>();
            
            currentStack.Push(root);

            var leftFirst = true;  // left and right

            int levelIndex = 0;
            var temp = new List<int>();

            while (currentStack.Count != 0)
            {
                // new level starts by checking levelIndex
                if (zigzagLevelNumbers.Count == levelIndex)
                {
                    temp = new List<int>();
                    zigzagLevelNumbers.Add(temp);
                }
                
                while (currentStack.Count != 0)
                {
                    var node = currentStack.Pop();
                    var left  = node.left;
                    var right = node.right;

                    var hasLeftChild  = left  != null;
                    var hasRightChild = right != null;

                    temp.Add(node.val);

                    if (leftFirst)
                    {
                        if (hasLeftChild)
                        {
                            nextStack.Push(left);
                        }

                        if (hasRightChild)
                        {
                            nextStack.Push(right);
                        }
                    }
                    else
                    {
                        if (hasRightChild)
                        {
                            nextStack.Push(right);
                        }

                        if (hasLeftChild)
                        {
                            nextStack.Push(left);
                        }
                    }
                }

                if (currentStack.Count == 0)
                {
                    // swap levels
                    var tmp = currentStack;
                    currentStack = nextStack;
                    nextStack   = tmp;

                    // change the order
                    leftFirst = !leftFirst;

                    levelIndex++;
                }
            }

            return zigzagLevelNumbers;
        }        
    }
}