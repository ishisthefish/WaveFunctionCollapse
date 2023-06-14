using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WaveFunctionCollapse
{
        internal class ArrayCreation
        {
            public int[,] array;
            public int[,] CreateArray(int sizeX, int sizeY)
            {
                array = new int[sizeX, sizeY];

                for (int x = 0; x < sizeX; x++)
                {
                    for (int y = 0; y < sizeY; y++)
                    {
                        array[x, y] = 0;
                    }
                }

                return array;
            }

        public bool IsComplete(int[,] array)
        {
            bool isComplete = true;
            for(int x = 0; x <  array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
            
                    if (array[x, y] == 0)
                    {
                        isComplete = false;
                        break;
                    }
                }

            }
            return isComplete;
        }


            public void PrintArray(int[,] array)
            {
                int sizeX = array.GetLength(0);
                int sizeY = array.GetLength(1);

                for (int x = 0; x < sizeX; x++)
                {
                    for (int y = 0; y < sizeY; y++)
                    {
                        Console.Write(array[x, y] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }



