using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        string[] split = Console.ReadLine().Split(' ');
        int N = int.Parse(split[0]);
        int k = int.Parse(split[1]);

        int[,] rooms = new int[N, 2];

        for (int i = 0; i < N; i++)
        {
            split = Console.ReadLine().Split(' ');

            rooms[i, 0] = int.Parse(split[0]);
            rooms[i, 1] = int.Parse(split[1]);
        }

        Dictionary<string, int> cache = new Dictionary<string, int>();
        Console.WriteLine(maxValue(ref rooms, 0, -1, k, ref cache));
    }

    static int maxValue(ref int[,] values, int r, int noCloseRoom, int k, ref Dictionary<string, int> cache)
    {
        int temp = default(int);
        if ( cache.TryGetValue(String.Join(" ", r, noCloseRoom, k), out temp) )
        {
            return temp;
        }

        if(r > values.GetLength(0) - 1)
        {
            return 0;
        }

        if (k == values.GetLength(0) - r)
        {
            switch (noCloseRoom)
            {
                case 1:
                    {
                       return cache[String.Join(" ", r, noCloseRoom, k)] = values[r, 1] + maxValue(ref values, r + 1, 1, k - 1, ref cache);
                    }

                case 0:
                    {
                        return cache[String.Join(" ", r, noCloseRoom, k)] = values[r, 0] + maxValue(ref values, r + 1, 0, k - 1, ref cache);
                    }

                case -1:
                    {
                        int a = values[r,0] + maxValue(ref values, r + 1, 0, k - 1, ref cache), b = values[r,1] + maxValue(ref values, r + 1, 1, k - 1, ref cache);
                        if (a < b)
                        {
                            return cache[String.Join(" ", r, noCloseRoom, k)] = b;
                        }
                        else
                        {
                            return cache[String.Join(" ", r, noCloseRoom, k)] = a;
                        }
                    }
            }
        }
        else
        {
            switch (noCloseRoom)
            {
                case 1:
                    {
                        int a = values[r, 1] + maxValue(ref values, r + 1, 1, k - 1, ref cache), b = values[r,0] + values[r,1] + maxValue(ref values, r + 1, -1, k, ref cache);
                        if (a < b)
                        {
                            return cache[String.Join(" ", r, noCloseRoom, k)] = b;
                        }
                        else
                        {
                            return cache[String.Join(" ", r, noCloseRoom, k)] = a;
                        }
                    }

                case 0:
                    {
                        int a = values[r, 0] + maxValue(ref values, r + 1, 0, k - 1, ref cache), b = values[r, 0] + values[r, 1] + maxValue(ref values, r + 1, -1, k, ref cache);
                        if (a < b)
                        {
                            return cache[String.Join(" ", r, noCloseRoom, k)] = b;
                        }
                        else
                        {
                            return cache[String.Join(" ", r, noCloseRoom, k)] = a;
                        }
                    }

                case -1:
                    {
                        int a = values[r, 0] + maxValue(ref values, r + 1, 0, k - 1, ref cache), b = values[r, 1] + maxValue(ref values, r + 1, 1, k - 1, ref cache), c = values[r, 0] + values[r, 1] + maxValue(ref values, r + 1, -1, k, ref cache);
                        if (a > b && a > c)
                        {
                            return cache[String.Join(" ", r, noCloseRoom, k)] = a;
                        }
                        else
                        {
                            if (b > a && b > c)
                            {
                                return cache[String.Join(" ", r, noCloseRoom, k)] = b;
                            }
                            else
                            {
                                return cache[String.Join(" ", r, noCloseRoom, k)] = c;
                            }
                        }
                    }
            }
        }
        return int.MinValue;
    }
}