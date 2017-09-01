using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        //toCities = City -> Connections. Cities you can travel to from this KEY city.
        Dictionary<string, HashSet<string>> toCities = new Dictionary<string, HashSet<string>>();

        Dictionary<string, int> tolls = new Dictionary<string, int>();

        //Read cities and toll amounts.
        string line = Console.ReadLine();
        string[] split = line.Split(new char[] { ' ' });

        int n = int.Parse(split[0]);
        for (int i = 0; i < n; i++)
        {
            split = Console.ReadLine().Split(new char[] { ' ' });

            toCities.Add(split[0], new HashSet<string>());

            tolls.Add(split[0], int.Parse(split[1]));
        }

        //Read connections
        line = Console.ReadLine();
        split = line.Split(new char[] { ' ' });

        int h = int.Parse(split[0]);
        for (int i = 0; i < h; i++)
        {
            split = Console.ReadLine().Split(new char[] { ' ' });

            toCities[split[0]].Add(split[1]);
        }

        LinkedList<string> topSort = TopologicalSort(toCities);

        line = Console.ReadLine();
        split = line.Split(new char[] { ' ' });

        int t = int.Parse(split[0]);

        for (int i = 0; i < t; i++)
        {
            split = Console.ReadLine().Split(new char[] { ' ' });

            int answer = ProcessTS(split[1], split[0], toCities, topSort, tolls);

            if (answer == int.MaxValue)
            {
                Console.WriteLine("NO");
            }
            else
            {
                Console.WriteLine(answer);
            }
        }
    }

    static LinkedList<string> TopologicalSort(Dictionary<string, HashSet<string>> graph)
    {
        HashSet<string> visited = new HashSet<string>();
        LinkedList<string> result = new LinkedList<string>();

        foreach (string city in graph.Keys)
        {
            if (!visited.Contains(city))
            {
                Visit(graph, city, visited, result);
            }
        }

        return result;
    }

    static void Visit(Dictionary<string, HashSet<string>> graph, string city, HashSet<string> visited, LinkedList<string> result)
    {
        visited.Add(city);

        foreach (string destination in graph[city])
        {
            if (!visited.Contains(destination))
            {
                Visit(graph, destination, visited, result);
            }
        }

        result.AddFirst(city);
    }

    static int ProcessTS(string start, string source, Dictionary<string, HashSet<string>> graph, LinkedList<string> ts, Dictionary<string, int> tolls)
    {
        Dictionary<string, int> distances = new Dictionary<string, int>();

        foreach (string city in ts)
        {
            if (city == source)
            {
                distances.Add(city, 0);
            }
            else
            {
                distances.Add(city, int.MaxValue);
            }
        }

        foreach (string u in ts)
        {
            if (distances[u] != int.MaxValue)
            {
                foreach (string v in graph[u])
                {
                    if (distances[v] > (distances[u] + tolls[v]))
                    {
                        distances[v] = (distances[u] + tolls[v]);
                    }
                }
            }
        }

        return distances[start];
    }
}