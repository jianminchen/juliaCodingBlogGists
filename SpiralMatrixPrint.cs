using System;
using System.Collections.Generic;

class Solution
{
    public static int[] SpiralCopy(int[,] inputMatrix)
    {
      if(inputMatrix == null)
        return new int[0]; 
      
      var rows    = inputMatrix.GetLength(0); 
      var columns = inputMatrix.GetLength(1); 
      
      var totalNumber = rows * columns; 
      var directions = new List<int[]>(); 
      directions.Add(new int[]{0, 1}); // go right
      directions.Add(new int[]{1, 0}); // go down
      directions.Add(new int[]{0, -1}); // go left
      directions.Add(new int[]{-1, 0}); // go up
      
      var visited = new bool[rows, columns]; // false 
      
      var direction = 0; 
      
      // 0, -1 -> 
      int row = 0; 
      int col = -1; 
      int index = 0; 
      
      var spiral = new int[totalNumber]; 
      
      while(index < totalNumber)
      {
          // go right -> 1, 2, 3, 4, 5
          var nextRow = row + directions[direction][0];  // 0 1 + 0
          var nextCol = col + directions[direction][1];  // 0 0 + 1
        
          if(nextRow >= 0 && nextRow < rows && nextCol >= 0 && nextCol < columns && !visited[nextRow, nextCol])// 
          {
            spiral[index++] = inputMatrix[nextRow, nextCol];  
            
            visited[nextRow, nextCol] = true; 
            
            row = nextRow; 
            col = nextCol; 
          }
          else 
          {
            direction = (direction + 1) % 4; // 0 
          }
      }
      
      return spiral;       
    }

    static void Main(string[] args)
    {
 
    }
}

/*
1 2 3 4 5 -> automatically -> visited 1 2 3 4 5 -> change direction -> 
  right: [0, 1] 
  down:  [1, 0]
  left:  [0, -1]
  up: [-1, 0]
  */
