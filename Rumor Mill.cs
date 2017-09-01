using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        //System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\John\Desktop\CS_4150\PS5\PS5-6\PS5-6\test2.in");

        Dictionary<string, HashSet<string>> friendships = new Dictionary<string, HashSet<string>>();

        //Read students into studentBody
        string line = Console.ReadLine();
        string[] split = line.Split(new char[] { ' ' });

        int n = int.Parse(split[0]);
        for (int i = 0; i < n; i++)
        {
            split = Console.ReadLine().Split(new char[] { ' ' });

            friendships.Add(split[0], new HashSet<string>());
        }

        //Read friendships
        line = Console.ReadLine();
        split = line.Split(new char[] { ' ' });

        int f = int.Parse(split[0]);
        for (int i = 0; i < f; i++)
        {
            split = Console.ReadLine().Split(new char[] { ' ' });

            friendships[split[0]].Add(split[1]);
            friendships[split[1]].Add(split[0]);
        }

        line = Console.ReadLine();
        split = line.Split(new char[] { ' ' });

        int r = int.Parse(split[0]);

        for (int i = 0; i < r; i++)
        {
            split = Console.ReadLine().Split(new char[] { ' ' });

            BFS(friendships, split[0]);
        }
    }

    static void BFS(Dictionary<string, HashSet<string>> friendships, string start)
    {
        Dictionary<string, int> dist = new Dictionary<string, int>();       // (StudentName, Distance)

        foreach (string student in friendships.Keys)
        {
            dist.Add(student, int.MaxValue);
        }

        dist[start] = 0;

        Queue<string> q = new Queue<string>();
        q.Enqueue(start);

        while (q.Count != 0)
        {
            string student = q.Dequeue();

            foreach (string friend in friendships[student])
            {
                if (dist[friend] == int.MaxValue)
                {
                    q.Enqueue(friend); //ENQUEUE

                    dist[friend] = dist[student] + 1;
                }
            }

        }

        dist = dist.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
        dist = dist.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

        foreach (var student in dist)
        {
            Console.Write(student.Key+" ");
        }
        Console.WriteLine();

    }
}