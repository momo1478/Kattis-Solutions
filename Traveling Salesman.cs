using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static int[,] graph;
    static int N;

    static int[] shortestConnections;

    static void Main(string[] args)
    {
        N = int.Parse(Console.ReadLine());

        graph = new int[N, N];
        shortestConnections = new int[N];

        int lowerBound = 0;
        for (int y = 0; y < N; y++)
        {
            string[] split = Console.ReadLine().Split(' ');

            int minEdge = int.MaxValue;
            for (int x = 0; x < N; x++)
            {
                graph[y, x] = int.Parse(split[x]);
                minEdge = (y != x && graph[y, x] < minEdge ? graph[y, x] : minEdge);
                if(x == y)
                {
                    graph[y, x] = int.MaxValue;
                }
            }
            lowerBound += minEdge;
            shortestConnections[y] = minEdge;
        }

        int minGB = int.MaxValue;
        for (int i = 0; i < N; i++)
        {
            minGB = Math.Min(GreedyTSP(i), minGB);
        }
        lowerBound -= shortestConnections[0];
        HashSet<int> visited = new HashSet<int>();
        visited.Add(0);
        Console.WriteLine(TSP(0, 0, 0, lowerBound, minGB, ref visited));
    }

    static int TSP(int start, int row, int total, int lowerBound, int greedyBound, ref HashSet<int> visited)
    {
        if (total >= greedyBound)
        {
            return int.MaxValue;
        }
        if (lowerBound + total >= greedyBound)
        {
            return int.MaxValue;
        }
        if (visited.Count == N)
        {
            return total + graph[row, start];
        }
        
        int min = int.MaxValue;
        for (int i = 0; i < N; i++)
        {
            if (!visited.Contains(i) && row != i)
            {
                visited.Add(i);
                int result = TSP(start, i, total + graph[row, i], lowerBound - shortestConnections[i], greedyBound, ref visited);
                visited.Remove(i);

                min = Math.Min(min, result);
                greedyBound = Math.Min(min, greedyBound);
            }
        }
        return min;
    }

    static int GreedyTSP(int start)
    {
        bool[] visited = new bool[N];
        visited[start] = true;

        int currVertex = 0;
        int result = 0;

        for (int i = 0; i < N; i++)
        {
            int smallestVertex = start;
            for (int j = 0; j < N; j++)
            {
                if (!visited[j] && currVertex != j && graph[currVertex, j] < graph[currVertex, smallestVertex])
                {
                    smallestVertex = j;
                }
            }

            visited[smallestVertex] = true;
            result += graph[currVertex, smallestVertex];
            currVertex = smallestVertex;
        }

        return result;
    }
}
