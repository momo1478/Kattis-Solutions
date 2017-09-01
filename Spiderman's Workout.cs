using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());

        int[] dist;
        while (N-- > 0)
        {
            int M = int.Parse(Console.ReadLine());
            dist = new int[M];

            int sum = 0;
            string[] split = Console.ReadLine().Split();
            for (int i = 0; i < M; i++)
            {
                dist[i] = int.Parse(split[i]);
                sum += dist[i];
            }

            cellInfo[,] table = new cellInfo[M, sum + 1];

            for (int y = 0; y < M; y++)
            {
                for (int x = 0; x < sum + 1; x++)
                {
                    table[y, x] = new cellInfo(int.MaxValue);
                }
            }

            Console.WriteLine(shortestWorkoutDistance(table, dist, M, sum));
        }
    }

    private static string shortestWorkoutDistance(cellInfo[,] table, int[] dist, int m, int sum)
    {
        if (sum % 2 == 1)
        {
            return "IMPOSSIBLE";
        }
        else
        {
            table[0, dist[0]].value = dist[0];
            table[0, dist[0]].up = true;
            table[0, dist[0]].valid = true;

            for (int i = 1; i < m; i++)
            {
                for (int j = 0; j <= sum; j++)
                {
                    if (table[i - 1, j].valid)
                    {
                        if (j - dist[i] >= 0)
                        {
                            if (table[i, j - dist[i]].value > table[i - 1, j].value)
                            {
                                table[i, j - dist[i]].valid = true;
                                table[i, j - dist[i]].up = false;
                                table[i, j - dist[i]].value = table[i - 1, j].value;
                            }
                        }
                        int max = Math.Max(table[i - 1, j].value, j + dist[i]);
                        if (table[i, j + dist[i]].value > max)
                        {
                            table[i, j + dist[i]].valid = table[i, j + dist[i]].up = true;
                            table[i, j + dist[i]].value = max;
                        }
                    }
                }
            }

            if (!table[m - 1, 0].valid)
            {
                return "IMPOSSIBLE";
            }
            else
            {
                string answer = "";

                int backTrackHeight = 0;
                for (int k = m - 1; k >= 0; k--)
                {
                    if (table[k, backTrackHeight].up)
                    {
                        backTrackHeight -= dist[k];
                        answer = "U" + answer;
                    }
                    else
                    {
                        backTrackHeight += dist[k];
                        answer = "D" + answer;
                    }
                }
                return answer;
            }
        }
    }

    struct cellInfo
    {
        public bool valid, up;
        public int value;

        public cellInfo(int iValue)
        {
            value = iValue;
            valid = up = false;
        }
    }
}

