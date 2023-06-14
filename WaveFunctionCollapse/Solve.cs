using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WaveFunctionCollapse
{
    internal class Solve
    {
        Random random = new Random();
        int[,] array;
        List<int> goodNumbers;
        List<int> badNumbers;

        private int currentX;
        private int currentY;

        public ArrayCreation arrayCreation;
        public NumberConnections numberConnections;

        public int CurrentX
        {
            get { return currentX; }
            set
            {
                 currentX = value;
            }
        }

        public int CurrentY{
            get { return currentY; }
            set { currentY = value; }
        }
        public Solve(int x, int y )
        {
            arrayCreation = new ArrayCreation();
            numberConnections = new NumberConnections();
            numberConnections.AddGoodNumber(1, new List<int> { 1,2, 3 ,4 });
            numberConnections.AddGoodNumber(2, new List<int> { 1, 3, 2 });
            numberConnections.AddGoodNumber(3, new List<int> { 1, 2, 3 });
            numberConnections.AddGoodNumber(4, new List<int> { 1 });

            numberConnections.AddBadNumber(1, new List<int> {0});
            numberConnections.AddBadNumber(2, new List<int> { 4,});
            numberConnections.AddBadNumber(3, new List<int> { 4 });
            numberConnections.AddBadNumber(4, new List<int> { 3,4, 2 });


            array = arrayCreation.CreateArray(x, y);
            int count = numberConnections.goodNumbers.Count;
            int centerPointValue = random.Next(1, count);
            currentX = (array.GetLength(0) - 1) / 2;
            currentY = (array.GetLength(1) - 1) / 2;
            //currentX = 0;
            //currentY = 0;
            AssignPoint(currentX, currentY, centerPointValue);
            goodNumbers = numberConnections.goodNumbers[centerPointValue];
            badNumbers = numberConnections.badNumbers[centerPointValue];
            GetNeighbors(currentX, currentY);

            // Get the solve to do all the work m8!!

            SolveArray();


            //arrayCreation.PrintArray(array);
            List<(int, int)> list = GetUnsovledPoints();

        }

        public void AssignPoint(int x, int y, int j)
        {
            array[x, y] = j;
        }

        public List<(int, int)> GetNeighbors(int x, int y)
        {
            
            List<(int, int)> neighbors = new List<(int, int)>();
            int xLength = array.GetLength(0) - 1;
            int yLength = array.GetLength(1) - 1;
            //top
            if (x > 0)
            {
                if (badNumbers.Contains(array[x - 1, y]) || array[x - 1, y] == 0) neighbors.Add((x - 1, y));

            }
            //bottom
            if (x < xLength)
            {
                if (badNumbers.Contains(array[x + 1, y]) || array[x + 1, y] == 0) neighbors.Add((x + 1, y));
            }
            //right
            if (y < yLength)
            {
                if (badNumbers.Contains(array[x, y + 1]) || array[x, y + 1] == 0)
                neighbors.Add((x, y + 1));
            }
            //left
            if (y > 0)
            {
                if (badNumbers.Contains(array[x, y - 1]) || array[x, y - 1] == 0)
                neighbors.Add((x, y - 1));
            }


            return neighbors;

        }

        public bool CheckNeighbors(int x, int y, List<int> badNumbers)
        {
            List<(int, int)> neighbors;

            bool containsBadNumbers = false;

                neighbors = GetNeighbors(x, y);

 

            foreach ((int neighborX, int neighborY) in neighbors)
            {
                int neighborValue = array[neighborX, neighborY];
                if (badNumbers.Contains(neighborValue))
                {
                    containsBadNumbers = true;
                    break; // No need to continue checking if a bad number is found
                }
            }

            return containsBadNumbers;
        }

        public List<(int,int)> GetUnsovledPoints()
        {
            List<(int, int)> points = new List<(int, int)>();
            for(int x = 0; x < array.GetLength(0); x++) {
                for(int y = 0;  y < array.GetLength(1); y++)
                {
                    if (array[x, y] == 0)
                    {
                        points.Add((x, y));

                    }

                }
            }
            return points;
        }
        public void SolveNeighbors(List<(int,int)> neighbors, List<int> goodNumbers)
        {

            foreach((int neighborX, int neighborY) in neighbors)
            {
                int nRandom = random.Next(0, goodNumbers.Count());
                int nValue = goodNumbers[nRandom];
                array[neighborX, neighborY] = nValue;
            }

        }

        public void SolveArray()
        {
            List<int> canBe;
            
            while (!arrayCreation.IsComplete(array))
            {

                List<(int, int)> neighbors;
                int currentPointValue = array[currentX, currentY];

                goodNumbers = numberConnections.GetGoodNumbers(currentPointValue);
                badNumbers = numberConnections.GetBadNumbers(currentPointValue);

                if (GetNeighbors(currentX, currentY).Count != 0)
                {
                    neighbors = GetNeighbors(currentX, currentY);
                }
                else
                {
                    List<(int, int)> point = GetUnsovledPoints();
                    neighbors = GetNeighbors(point[0].Item1, point[0].Item2);
                    Console.WriteLine("//");


                }
             

                SolveNeighbors(neighbors, goodNumbers);



                currentX = neighbors[0].Item1;
                currentY = neighbors[0].Item2;
                arrayCreation.PrintArray(array);
                Console.WriteLine();



            }
        }
    }

}
