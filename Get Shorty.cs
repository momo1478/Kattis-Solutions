using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<int, HashSet<Connection>> NodeConnections = new Dictionary<int, HashSet<Connection>>();

        string line;
        while ((line = Console.ReadLine()) != null)
        {
            string[] split = line.Split(new char[] { ' ' });
            int n = int.Parse(split[0]);
            int m = int.Parse(split[1]);

            if(n == 0 && m == 0)
            {
                break;
            }

            for (int i = 0; i < n; i++)
            {
                NodeConnections.Add(i, new HashSet<Connection>());
            }

            for (int i = 0; i < m; i++)
            {
                string[] mSplit = Console.ReadLine().Split(new char[] { ' ' });

                int i1 = int.Parse(mSplit[0]);
                int i2 = int.Parse(mSplit[1]);
                float f = float.Parse(mSplit[2]);

                NodeConnections[i1].Add(new Connection(i2, f));
                NodeConnections[i2].Add(new Connection(i1, f));
            }

            Console.WriteLine(dijkstra(NodeConnections).ToString("n4"));

            //foreach (int Node in NodeConnections.Keys)
            //{
            //    Console.WriteLine("#" + Node);
            //    foreach (Connection conn in NodeConnections[Node])
            //    {
            //        Console.WriteLine("To : " + conn.Destination + " Factor " + conn.Factor);
            //    }
            //    Console.WriteLine();
            //}
            NodeConnections.Clear();
        }
    }   

    static float dijkstra(Dictionary<int, HashSet<Connection>> graph)
    {
        Dictionary<int, float> distance = new Dictionary<int, float>();

        foreach (int v in graph.Keys)
        {
            distance.Add(v, -1f);
        }

        distance[0] = 1f;

        PriorityQueue PQ = new PriorityQueue();
        PQ.Enqueue(0, 1f);
        while (PQ.Count > 0)
        {
            int u = PQ.Dequeue();

            foreach (Connection conn in graph[u])
            {
                if(distance[conn.Destination] < conn.Factor * distance[u])
                {
                    distance[conn.Destination] = conn.Factor * distance[u];
                    PQ.Enqueue(conn.Destination, distance[conn.Destination]);
                }
            }
        }
 
        return distance[distance.Count - 1];
    }
}



class Connection
{
    public int Destination { get; set; }

    public float Factor { get; set; }

    public Connection(int iD, float iF)
    {
        Destination = iD;
        Factor = iF;
    }
}

class PriorityQueue
{
    public SortedList<float, Queue<int>> Heap { get; set; } = new SortedList<float, Queue<int>>();

    public int Count { get; set; } = 0;

    public void Enqueue(int item, float weight)
    {
        if (!Heap.ContainsKey(weight))
        {
            Heap.Add(weight, new Queue<int>());
        }
        Heap[weight].Enqueue(item);

        Count++;
    }

    public int Dequeue()
    {
        if(Heap.Values[Heap.Count - 1].Count > 0) //makes sure that something is in the greatest factor set
        {
            int result = Heap.Values[Heap.Count - 1].Dequeue();

            if (Heap.Values[Heap.Count - 1].Count == 0)
            {
                Heap.RemoveAt(Heap.Count - 1);
            }
            Count--;

            return result;
        }
        return -1;
    }
}

