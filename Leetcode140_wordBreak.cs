using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode140WordBreakII
{
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
            var results = wordBreak("catsanddog", 
                new HashSet<string>(new string[] { "cat", "cats", "and", "sand", "dog" })); 
        }
         
        public static IList<string> wordBreak(String original, HashSet<string> dict) {  
            var res = new List<string>();

            if (original == null || original.Length == 0)
            {
                return res;
            }

            depthFirstSearchHelper(original, dict, 0, "", res);  

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
            if(start >= original.Length)  
            {  
                res.Add(wordByWord);  
                return;  
            }  

            var stringBuilder = new StringBuilder();  
            for(int i = start;i < original.Length;i++)  
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
    }
}
