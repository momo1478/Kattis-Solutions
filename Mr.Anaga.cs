using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] firstLine = Console.ReadLine().Split(' ');

            int n = Int32.Parse(firstLine[0]);
            int k = Int32.Parse(firstLine[1]);

            HashSet<string> solutions = new HashSet<string>();
            HashSet<string> rejected = new HashSet<string>();

            string line;
            string sortedWord;
            for (int i = 0; i < n; i++)
            {
                line = Console.ReadLine();
                sortedWord = String.Concat(line.OrderBy(c => c));

                if(solutions.Contains(sortedWord))
                {
                    solutions.Remove(sortedWord);
                    rejected.Add(sortedWord);
                }
                else if (!rejected.Contains(sortedWord))
                {
                    solutions.Add(sortedWord);
                }
            }

            Console.WriteLine(solutions.Count);
        }
    }
}