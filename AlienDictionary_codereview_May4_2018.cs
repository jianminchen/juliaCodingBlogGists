using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _269AlienDictionary_Review
{
    class Program
    {
        /// <summary>
        /// code review May 4, 2018
        /// Leetcode: Alien Dictionary
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] words = { "wrt", "wrf", "er", "ett", "rftt" };

            // verify result manually here: "wertf"
            // 5 minutes warmup the algorithm of topological sort with a dictionary of words:
            // "wrt" -> "wrf", t -> f, so f's dependency list {t}
            // "wrf" -> "er", w -> e, so e's dependency list {w}
            // "er"  -> "ett", r -> t, so t's dependency list {r}
            // "ett" -> "rftt", e -> r, so r's dependency list {e}
            // right now w's dependency list is empty, indegree is 0. => remove w first
            // update e's dependency list and e's indegree from 1 to 0. 
            // continue to work on rest...
            var result = AlienOrder(words);
            Debug.Assert(result.CompareTo("wertf") == 0);
        }

        /// <summary>
        /// code review on May 4, 2018
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static string AlienOrder(string[] words)
        {
            if (words == null || words.Length <= 1)
            {
                return "";
            }

            // Graph prensentation - 3 variables at least 
            // nodes, node's dependency list and inDegree array
            // nodes - function getNodes()
            var nodes = getNodes(words);

            var dependencyList = new Dictionary<char, HashSet<char>>();
            var inDegree       = new int[26];

            graphSetup(words, dependencyList, inDegree);

            return topologicalSort(words, dependencyList, inDegree, nodes);
        }

        /// <summary>
        /// code review on May 4, 2018
        /// every char is a node in the graph
        /// getNodes - get all chars in the words
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        private static HashSet<char> getNodes(string[] words)
        {
            var hashset = new HashSet<char>();

            foreach (string s in words)
            {
                hashset.UnionWith(s.ToList()); 
            }

            return hashset;
        }

        /*
         * Explain the function graphSetup design using the most simple example: 
         * set up graph for {"wrt","wrf"}
         * 
         * Here are the task to complete:
         * 1. Find the first different char - actually, it is an edge in the graph, 
         * here is t-> f
         * 2. Need to handle case - no edge 
         * 3. At most one edge for two consecutive words
         * 4. Only handle the array of string by two neighboring nodes - transmitive 
         * 
         * what to construct in the graph:
         * 1. Add 't' to dependencyList, and also add 't' as char, its neighbors {'f'}, 'f' depends on 't'. 
         * 2. Work on inDegree, increment inDegree['f'-'a']++, 'f' has one more indegree from 't'.
         * 3. need to get the first different char from two strings. 
         * 
         * precondition:
         * words's length >=2 
         * base case <=1 is handled in callee function
         */
        private static void graphSetup(
            string[] words,
            Dictionary<char, HashSet<char>> dependencyList,
            int[] inDegree
            )
        {
            var length = words.Length;

            // go over each word
            for (int i = 1; i < length; i++)
            {
                var previous = words[i - 1];
                var current  = words[i];

                int firstUnmatched = 0;
                while (previous[firstUnmatched] == current[firstUnmatched])
                {
                    firstUnmatched++;
                }

                // no edge 
                if (firstUnmatched >= Math.Min(previous.Length, current.Length))
                    continue;

                // work on style - double checking - using correct variable 
                char edgeFrom = previous[firstUnmatched];
                char edgeTo   = current[firstUnmatched];

                if (!dependencyList.ContainsKey(edgeFrom))
                {
                    dependencyList.Add(edgeFrom, new HashSet<char>());
                }

                if (!dependencyList[edgeFrom].Contains(edgeTo))
                {
                    dependencyList[edgeFrom].Add(edgeTo);
                    inDegree[edgeTo - 'a']++;
                }

                // skip if edgeTo is in the hashset. 
            }
        }

        /*
         * Two tasks:
         * 1. get nodes with indegree 0 - enqueue those nodes
         * 2. play with queue
         *    a node is dequeue, append to the output stringBuilder
         *    adjust its dependency list - inDegree value - decrement one
         *    if the neighbor node is with 0 indegree value, push it to the queue
         *   
         * Checking list about queue:
         * 1. First queue is not empty - add nodes into queue first - with indegree 0 nodes
         * 2. when node is dequeued, the indegree is updated accordingly, then, more nodes will be 
         *    added to queue when its indegree is 0
         * 3. Every node in the queue is with indegree 0  - this is a fact.
         */
        public static string topologicalSort(
           string[] words,
           Dictionary<char, HashSet<char>> dependencyList,
           int[]         inDegree,
           HashSet<char> nodes
           )
        {           
            var queue = new Queue<char>();

            // add those nodes with inDegree value 0 to the queue
            for (int i = 0; i < 26; i++)
            {
                var visit = (char)(i + 'a');

                if (!nodes.Contains(visit))
                {
                    continue;
                }

                if (inDegree[i] == 0)
                {
                    queue.Enqueue(visit);
                }
            }

            var stringBuilder = new StringBuilder();
            while (queue.Count > 0)
            {
                var visit = queue.Dequeue();
                stringBuilder.Append(visit);

                if (!dependencyList.ContainsKey(visit))  // bug001 - forget these 2 lines, runtime execution error 
                {
                    continue;
                }

                var neighbors = dependencyList[visit];
                foreach (char node in neighbors)
                {
                    int index = node - 'a';
                    inDegree[index]--;

                    // update queue
                    if (inDegree[index] == 0)
                    {
                        queue.Enqueue(node);
                    }
                }
            }

            // edge case: 
            return stringBuilder.Length < nodes.Count ? "" : stringBuilder.ToString();
        }
    }
}