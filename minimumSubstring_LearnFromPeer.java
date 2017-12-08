import java.io.*;
import java.util.*;

class Solution {
/*
APPROACH:
1. put all elemnets from arr in a hashmap -> x, y,z // a->1, b->1, c->1
2. start =0, end=0, count = hashset.size()
3. traverse thro str, if contied in hashet, count--
start -> a
if(map,get(char)==0)
count--;
end->c
len -> 4-0 +1 = 5
['a','b','c']   "aaefbcgaxy" -> bcga
"aaefbc" - length is 6 
len ->4
-> move left -> 
"aefbc" -> length is 5 
arr-> [x,y,z]
str-> xyyzyzyx
*/
  static String getShortestUniqueSubstring(char[] arr, String str) {
    // your code goes here
    HashMap<Character,Integer> map = new HashMap<>();
    for(char c: arr)
       map.put(c, map.getOrDefault(c,0)+1);
    
    //hashmap -> [a,1][b,1][c,1]
    // hashmap->[x,1][y,1][z,1]
    
    int start=0;
    int end=0;
    int count= map.size(); //3
    int len=Integer.MAX_VALUE;
    int index=0;
    
   // end -> 8-> null
    while(end<str.length())
    {
     char c_start = str.charAt(end); //a a e f b c //x y y z y z y x
      if(map.containsKey(c_start))
      {    
       map.put(c_start,map.get(c_start)-1); // hashmap ->[a,-1][b,0][c,0]
      // hashmap-> [x,0][y,-3][z,-1]
       if(map.get(c_start)==0)
         count--; // count -> 0
      }
      end++; //5 start->0
      
      while(count==0)
      {
        char c1 = str.charAt(start); //a // y y z y z
        if(map.containsKey(c1))
        {
         map.put(c1,map.get(c1)+1) ; // c1 is not in map get(c1) return null instead of 0
            //hashmap-> [x,1][y,0][z,1]
           if(map.get(c1)>0)
              count++; //count->1
        
        }
        if(len> end-start)
        {
          len = end-start; //3
          index=start; // 5
          // xyyz len =4
        }
        
        start++; //1 start-> y
        //start -> 2 -> y
        // satrt -> 3 -> z
        // start -> 4-> y
        // start -> 5-> z
        // satrt -> 6-> y
      }
      
    }
    if(len==Integer.MAX_VALUE)
      return "";
    else
    {
    String s = str.substring(index, index+len);
    return s;
    }
  }

  public static void main(String[] args) {

  }

}