using System;

class Solution
{
    public static int ShiftedArrSearch(int[] shiftArr, int num)
    {
       if(shiftArr == null) 
         return -1; 
      
       var length = shiftArr.Length; 
      
       var start = 0; 
       var end = length - 1; 
      
       while(start <= end)
       {
         var middle = start + (end - start)/ 2; 
         var middleValue = shiftArr[middle]; 
         
         if(middleValue == num)
         {
           return middle;           
         }
         
         var startValue = shiftArr[start];
         var endValue   = shiftArr[end];
         
         var firstHalfAscending  = startValue  <= middleValue; 
         //var secondHalfAscending = middleValue <= endValue; 
         
         if(firstHalfAscending)
         {
           // get rid of half range - left/ right
           var isInFirstHalf = num >= startValue && num <= middleValue; 
           if(isInFirstHalf)
           {
             end = middle - 1; 
           }
           else 
           {
             start = middle + 1; 
           }
         }
         else 
         {
           // assert that second half ascending
           var isInSeondHalf = num >= middleValue && num <= endValue; 
           if(isInSeondHalf)
           {
             start = middle + 1; 
           }
           else 
           {
             end = middle - 1; 
           }
         }         
       }
      
       return -1; 
    }

    static void Main(string[] args)
    {

    }
}
              
/*              ?
           M-
           
           
                      -
 - 
keywords:
sorted array, distinc integer, 
shift to the left unknown offset k 

given shifted array, integer num, 
ask: find the index of num 
constraint: offset k > 0, k < length - 1
  
  
Time complexity: O(logn), n is the size of the array
Space complexity: O(1)
  
  [1, 2, 3, 4, 5]
  
  [3, 4, 5, 1, 2] -> ascending order
  
  1. One approach -> find pivot value 
  
            |
  go back to search [3, 4, 5], [1, 2]
  
  2. [3, 4, 5, 1, 2]
            middle
      ascending order -> 3 < 5, 5 > 2, second half -> not ascending 
  tell if the given value is in ascending half or not
*/
