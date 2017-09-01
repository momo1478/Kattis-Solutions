using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        List<int> distances = new List<int>(int.Parse(Console.ReadLine().Split()[0]));
        distances.Add(0);

        string line = Console.ReadLine();
        while ((line = Console.ReadLine()) != null)
        {
            string[] split = line.Split(' ');
            distances.Add(int.Parse(split[0]));
        }

        Console.WriteLine( LeastPenalty( distances, 0, new Dictionary<int,int>(distances.Capacity) ) );
    }

    private static int LeastPenalty(List<int> distances, int hotel, Dictionary<int,int> cache)
    {
        int temp;
        if (cache.TryGetValue(hotel, out temp))
        {
            return temp;
        }

        if (hotel == distances.Count - 1)
        {
            return 0;
        }
        else
        {
            int leastPenalty = int.MaxValue;

            for (int i = distances.Count - 1; i > hotel; i--)
            {
                leastPenalty = Math.Min(leastPenalty, (int)Math.Pow(400 - (distances[i] - distances[hotel]), 2) + LeastPenalty(distances, i, cache));
            }

            cache[hotel] = leastPenalty;
            return leastPenalty;
        }
    }
}