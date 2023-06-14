using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveFunctionCollapse
{
    //Code From ChatGPT :((
    internal class NumberConnections
    {
        private Dictionary<int, List<int>> goodNumbers;
        private Dictionary<int, List<int>> badNumbers;

        public NumberConnections()
        {
            goodNumbers = new Dictionary<int, List<int>>();
            badNumbers = new Dictionary<int, List<int>>();
        }

        public void AddGoodNumber(int number, List<int> connectedNumbers)
        {
            if (!goodNumbers.ContainsKey(number))
            {
                goodNumbers[number] = new List<int>();
            }

            goodNumbers[number].AddRange(connectedNumbers);
        }

        public void AddBadNumber(int number, List<int> connectedNumbers)
        {
            if (!badNumbers.ContainsKey(number))
            {
                badNumbers[number] = new List<int>();
            }

            badNumbers[number].AddRange(connectedNumbers);
        }

        public List<int> GetGoodNumbers(int number)
        {
            if (goodNumbers.ContainsKey(number))
            {
                return goodNumbers[number];
            }

            return new List<int>();
        }

        public List<int> GetBadNumbers(int number)
        {
            if (badNumbers.ContainsKey(number))
            {
                return badNumbers[number];
            }

            return new List<int>();
        }
    }
}


