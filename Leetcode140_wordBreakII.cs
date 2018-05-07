using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode140WordBreakII
{
    /// <summary>
    /// Leetcode 140: Word break II
    /// May 7, 2018
    /// </summary>
    class Program
    {
        /*
         s = "catsanddog"
        wordDict = ["cat", "cats", "and", "sand", "dog"]
        Output:
        [
            "cats and dog",
            "cat sand dog"
        ]
         */
        static void Main(string[] args)
        {
            RunTestcaseTwo(); 
        }

        public static void RunTestcaseOne()
        {
            var results = wordBreak("catsanddog",
                new List<string>(new string[] { "cat", "cats", "and", "sand", "dog" }));
        }

        public static void RunTestcaseTwo()
        {
            var results = wordBreak("aaaaaaa",
                new List<string>(new string[] { "aaa", "aaaa" }));
        }

        public static IList<string> wordBreak(string original, IList<string> list)
        {
            var res = new List<string>();

            if (original == null || original.Length == 0)
            {
                return res;
            }

            var dict = new HashSet<string>(list);
            var orginalDict = new HashSet<string>(dict); 

            if (canWordBreakUsingDepthFirstSearch(original, dict))
            {
                depthFirstSearchHelper(original, orginalDict, 0, "", res);
            }

            return res;
        }

        /// <summary>
        /// depth first search 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="dict"></param>
        /// <param name="start"></param>
        /// <param name="wordByWord"></param>
        /// <param name="res"></param>
        private static void depthFirstSearchHelper(string original, HashSet<string> dict, int start, string wordByWord, IList<string> res)
        {
            // base case 
            if (start >= original.Length)
            {
                res.Add(wordByWord);
                return;
            }

            var stringBuilder = new StringBuilder();
            for (int i = start; i < original.Length; i++)
            {
                stringBuilder.Append(original[i]);
                var current = stringBuilder.ToString();

                if (dict.Contains(current))
                {
                    var newItem = wordByWord.Length > 0 ? (wordByWord + " " + current) : current;
                    depthFirstSearchHelper(original, dict, i + 1, newItem, res);
                }
            }
        }

        /// <summary>
        /// this function is to do checking to prevent TLE bug 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="wordDict"></param>
        /// <returns></returns>
        private static bool canWordBreakUsingDepthFirstSearch(String s, HashSet<String> wordDict)
        {     //Word Break I
            if (s == null || wordDict == null)
            {
                return false;
            }

            var length = s.Length;

            if (length == 0)
            {
                return true;
            }

            for (int i = 1; i <= length; i++)
            {
                var firstWord = s.Substring(0, i);
                var rest  = s.Substring(i, length - i);

                if (wordDict.Contains(firstWord))
                {
                    if (canWordBreakUsingDepthFirstSearch(rest, wordDict))
                    {
                        return true;
                    }

                    wordDict.Remove(firstWord);
                }
            }

            return false;
        }
    }
}