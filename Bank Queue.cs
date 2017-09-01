using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string[] split = Console.ReadLine().Split(' ');

        int N = int.Parse(split[0]);
        int T = int.Parse(split[1]);

        Dictionary<int, List<int>> customers = new Dictionary<int, List<int>>();
        List<int> temp; int maxCustomerTime = -1;
        for (int i = 0; i < N; i++)
        {
            split = Console.ReadLine().Split(' ');

            int c = int.Parse(split[0]);
            int t = int.Parse(split[1]);

            if (!customers.TryGetValue(t, out temp))
            {
                customers.Add(t, new List<int>());

                maxCustomerTime = t > maxCustomerTime ? t : maxCustomerTime;
            }
            customers[t].Add(c);
        }

        foreach (List<int> cashAmounts in customers.Values)
        {
            cashAmounts.Sort();
        }

        int moneySum = 0, timeElapsed = 0;

        for (int i = maxCustomerTime; i >= 0; i--)
        {
            int maxOfMaxes = -1, indexMax = -1;
            for (int j = maxCustomerTime; j >= maxCustomerTime - timeElapsed; j--)
            {
                if (!customers.TryGetValue(j, out temp))
                {
                    continue;
                }

                maxOfMaxes = maxOfMaxes < customers[j][customers[j].Count - 1] ? customers[j][customers[j].Count - 1] : maxOfMaxes;
                if(maxOfMaxes == customers[j][customers[j].Count - 1])
                {
                    indexMax = j;
                }
            }
            timeElapsed++;

            if (indexMax != -1)
            {
                customers[indexMax].RemoveAt(customers[indexMax].Count - 1);
                if (customers[indexMax].Count == 0)
                {
                    customers.Remove(indexMax);
                }
            }

            moneySum += maxOfMaxes == -1 ? 0 : maxOfMaxes;
        }
        Console.WriteLine(moneySum);
    }
}